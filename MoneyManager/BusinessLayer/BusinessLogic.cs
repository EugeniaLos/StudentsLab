using MoneyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer.DataModels;

namespace BusinessLayer
{
    public class BusinessLogic
    {
        private UnitOfWork unitOfWork;

        public BusinessLogic()
        {
            unitOfWork = new UnitOfWork(ConnectionHelper.GetConnectionString());
        }

        public UnitOfWork UnitOfWork => unitOfWork ??= new UnitOfWork(ConnectionHelper.GetConnectionString());

        public void DeleteCurrentMonthUsersTransaction(int userId)
        {
            var removableTransaction = unitOfWork.Transactions.CurrentMonth()
                .Join(unitOfWork.Assets.GetAll()
                        .Where(a => a.UserId == userId),
                    t => t.AssetId,
                    a => a.Id,
                    (transaction,
                        asset) => transaction);

            foreach (var transaction in removableTransaction)
            {
                unitOfWork.Transactions.Delete(transaction);
            }
            unitOfWork.Save();
        }

        public UserBalance GetBalance(int userId)
        {
            decimal balance = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId)
                                  .Join(unitOfWork.Transactions.GetAll(), a => a.Id, t => t.AssetId,
                                      (asset, transaction) => transaction)
                                  .Join(unitOfWork.Categories.GetAll().Where(c => c.Type == 1), t => t.CategoryId,
                                      c => c.Id,
                                      (transaction, category) => transaction.Amount).Sum() - unitOfWork.Assets.GetAll()
                                  .Where(a => a.UserId == userId).Join(unitOfWork.Transactions.GetAll(), a => a.Id,
                                      t => t.AssetId, (asset, transaction) => transaction)
                                  .Join(unitOfWork.Categories.GetAll().Where(c => c.Type == 0), t => t.CategoryId,
                                      c => c.Id,
                                      (transaction, category) => transaction.Amount).Sum();
            var user = unitOfWork.Users.Get(userId);

            return new UserBalance()
            {
                Balance = balance,
                UserId = user.Id,
                UserName = user.Name,
                UserEmail = user.Email
            };
        }

        public IEnumerable<AssetBalance> GetAssetsWithBalance(int userId)
        {
            var deb = unitOfWork.Assets.GetAll()
                .Where(a => a.UserId == userId)
                .Join(unitOfWork.Transactions.GetAll(),
                    a => a.Id,
                    t => t.AssetId,
                    (asset, transaction) => transaction)
                .Join(unitOfWork.Categories.GetAll()
                        .Where(c => c.Type == 1),
                    t => t.CategoryId,
                    c => c.Id,
                    (transaction, category) => transaction)
                .GroupBy(t => t.AssetId);

            var cred = unitOfWork.Assets.GetAll()
                .Where(a => a.UserId == userId)
                .Join(unitOfWork.Transactions.GetAll(),
                    a => a.Id,
                    t => t.AssetId,
                    (asset, transaction) => transaction)
                .Join(unitOfWork.Categories.GetAll()
                        .Where(c => c.Type == 0),
                    t => t.CategoryId,
                    c => c.Id,
                    (transaction, category) => transaction)
                .GroupBy(t => t.AssetId);

            return deb.AsEnumerable()
                .Join(cred.AsEnumerable(),
                    d => d.Key,
                    c => c.Key,
                    (d, c) => new AssetBalance
                    {
                        AssetId = d.Key,
                        AssetName = unitOfWork.Assets.Get(d.Key).Name,
                        Balance =
                            d.AsEnumerable()
                                .Select(d => d.Amount)
                                .Sum() -
                            c.AsEnumerable()
                                .Select(c => c.Amount)
                                .Sum()
                    });
        }

        public IEnumerable<OrderedTransactions> GetOrderedTransactions(int userid)
        {
            return GetUsersTransactionId(userid).OrderByDescending(t => t.Date)
                .ThenBy(t => unitOfWork.Assets.Get(t.AssetId).Name)
                .ThenBy(t => unitOfWork.Categories.Get(t.CategoryId).Name).Select(t => new OrderedTransactions
                {
                    AssetName = unitOfWork.Assets.Get(t.AssetId).Name,
                    CategoryName = unitOfWork.Categories.Get(t.CategoryId).Name,
                    Amount = t.Amount,
                    Date = t.Date,
                    Comment = t.Comment
                });
        }

        public IEnumerable<BalancePeriod> BalanceByMonth(int userId, DateTime startDate, DateTime endDate)
        {
            var income = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId).Join(unitOfWork.Transactions.GetAll()
                .Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate), a => a.Id, t => t.AssetId, (asset, transaction) => transaction).Join(
                unitOfWork.Categories.GetAll()
                    .Where(c => c.Type == 1),
                t => t.CategoryId,
                c => c.Id,
                (transaction, category) => transaction).OrderBy(t => t.Date).GroupBy(t => new { t.Date.Month, t.Date.Year });

            var expenses = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId).Join(unitOfWork.Transactions.GetAll()
                .Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate), a => a.Id, t => t.AssetId, (asset, transaction) => transaction).Join(
                unitOfWork.Categories.GetAll()
                    .Where(c => c.Type == 0),
                t => t.CategoryId,
                c => c.Id,
                (transaction, category) => transaction).OrderBy(t => t.Date).GroupBy(t => new { t.Date.Month, t.Date.Year });

            return income.AsEnumerable()
                .Join(expenses.AsEnumerable(),
                    i => i.Key,
                    e => e.Key,
                    (i, e) => new BalancePeriod() 
                    {
                        Income = i.AsEnumerable()
                        .Select(i => i.Amount)
                        .Sum(),
                        Expenses = e.AsEnumerable()
                            .Select(e => e.Amount)
                            .Sum(),
                        Month = i.Key.Month,
                        Year = i.Key.Year
                    });
        }

        public IEnumerable<CategoryAmount> GetParentCategoriesAmount(int userId, bool income)
        {
            int type = 0;
            if (income)
            {
                type = 1;
            }

            return unitOfWork.Assets.GetAll().Where(a => a.UserId == userId).Join(
                unitOfWork.Transactions.GetAll().Where(t => t.Date.Year == DateTime.Today.Year)
                    .Where(t => t.Date.Month == DateTime.Today.Month), a => a.Id, t => t.AssetId,
                (asset, transaction) => transaction).Join(unitOfWork.Categories.GetAll().Where(c => c.Type == type).Where(c => c.ParentId == null),
                t => t.CategoryId, c => c.Id,
                (transaction, category) => new CategoryAmount()
                    {CategoryName = category.Name, Amount = transaction.Amount}).OrderByDescending(t => t.Amount).ThenBy(t => t.CategoryName);
        }

        private IEnumerable<Transaction> GetUsersTransactionId(int userId)
        {
            var userAssets = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId);
            return unitOfWork.Transactions.GetAll()
                .Join(
                    userAssets,
                    t => t.AssetId,
                    a => a.Id,
                    (transaction, asset) => transaction);
        }
    }
}
