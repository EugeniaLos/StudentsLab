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
            var removableTransaction = unitOfWork.Transactions.GetTransactions(userId)
                .Where(u => u.Date.Month == DateTime.Today.Month)
                .Where(u => u.Date.Year == DateTime.Today.Year);

            foreach (var transaction in removableTransaction)
            {
                unitOfWork.Transactions.Delete(transaction);
            }
            unitOfWork.Save();
        }

        public UserBalance GetBalance(int userId)
        {
            decimal balance = unitOfWork.Transactions.GetTransactions(userId)
                                  .Intersect(unitOfWork.Transactions.GetTransactionByType(1)).Select(t => t.Amount)
                                  .Sum() - unitOfWork.Transactions.GetTransactions(userId)
                                  .Intersect(unitOfWork.Transactions.GetTransactionByType(0)).Select(t => t.Amount)
                                  .Sum();
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
            var deb = unitOfWork.Transactions.GetTransactionsWithAsset(userId)
                .Intersect(unitOfWork.Transactions.GetTransactionByType(1))
                .GroupBy(t => t.Asset);

            var cred = unitOfWork.Transactions.GetTransactionsWithAsset(userId)
                .Intersect(unitOfWork.Transactions.GetTransactionByType(0))
                .GroupBy(t => t.Asset);

            return deb.AsEnumerable()
                .Join(cred.AsEnumerable(),
                    d => d.Key,
                    c => c.Key,
                    (d, c) => new AssetBalance
                    {
                        AssetId = d.Key.Id,
                        AssetName = d.Key.Name,
                        Balance =
                            d.AsEnumerable()
                                .Select(d => d.Amount)
                                .Sum() -
                            c.AsEnumerable()
                                .Select(c => c.Amount)
                                .Sum()
                    });
        }

        public IEnumerable<OrderedTransactions> GetOrderedTransactions(int userId)
        {
            return unitOfWork.Transactions.GetTransactionsWithAssetCategory(userId).OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name).Select(t => new OrderedTransactions
                {
                    AssetName = t.Asset.Name,
                    CategoryName = t.Category.Name,
                    Amount = t.Amount,
                    Date = t.Date,
                    Comment = t.Comment
                });
        }

        public IEnumerable<BalancePeriod> BalanceByMonth(int userId, DateTime startDate, DateTime endDate)
        {
            var income = unitOfWork.Transactions.GetTransactions(userId).Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate).Intersect(unitOfWork.Transactions.GetTransactionByType(1)).OrderBy(t => t.Date)
                .GroupBy(t => new {t.Date.Month, t.Date.Year});

            var expenses = unitOfWork.Transactions.GetTransactions(userId).Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate).Intersect(unitOfWork.Transactions.GetTransactionByType(0)).OrderBy(t => t.Date)
                .GroupBy(t => new {t.Date.Month, t.Date.Year});

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

            return unitOfWork.Transactions.GetTransactions(userId).Where(t => t.Date.Year == DateTime.Today.Year)
                .Where(t => t.Date.Month == DateTime.Today.Month)
                .Intersect(unitOfWork.Transactions.GetTransactionByTypeWithCategory(type)
                    .Where(c => c.Category.ParentId == null))
                .GroupBy(t => t.Category.Name).Select(g => new CategoryAmount
                    {CategoryName = g.Key, Amount = g.Select(t => t.Amount).Sum()});
        }
    }
}
