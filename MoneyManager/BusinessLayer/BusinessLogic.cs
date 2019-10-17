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

        public UnitOfWork UnitOfWork
        {
            get { return unitOfWork ?? (unitOfWork = new UnitOfWork(ConnectionHelper.GetConnectionString())); }
        }

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
                unitOfWork.Transactions.Delete(transaction.Id);
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

        public IEnumerable<object> BalancePerPeriod(int userId, DateTime startDate, DateTime endDate)
        {
            //decimal income = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId)
            //    .Join(unitOfWork.Transactions.GetAll().Where(t => t.Date > startDate)
            //            .Where(t => t.Date < endDate), a => a.Id, t => t.AssetId,
            //        (asset, transaction) => transaction)
            //    .Join(unitOfWork.Categories.GetAll().Where(c => c.Type == 1), t => t.CategoryId,
            //        c => c.Id,
            //        (transaction, category) => transaction.Amount).Sum();
            ////decimal expenses = unitOfWork.Assets.GetAll()
            //    .Where(a => a.UserId == userId).Join(unitOfWork.Transactions.GetAll().Where(t => t.Date > startDate)
            //            .Where(t => t.Date < endDate), a => a.Id,
            //        t => t.AssetId, (asset, transaction) => transaction)
            //    .Join(unitOfWork.Categories.GetAll().Where(c => c.Type == 0), t => t.CategoryId,
            //        c => c.Id,
            //        (transaction, category) => transaction.Amount).Sum();
            //var user = unitOfWork.Users.Get(userId);

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
                    (i, e) => new 
                    {
                        income = i.AsEnumerable()
                        .Select(i => i.Amount)
                        .Sum(),
                        expenses = e.AsEnumerable()
                            .Select(e => e.Amount)
                            .Sum()
                        
                    });


            //var transactionGroups = GetUsersTransactionId(userId).Where(t => t.Date > startDate)
            //    .Where(t => t.Date < endDate).OrderBy(t => t.Date).GroupBy(t => t.Date.Month);
            //var incomeCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 1);
            //var expensesCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 0);
            //foreach (var g in transactionGroups)
            //{
            //    decimal balance = 0;
            //    var incomeEnumerable = g
            //        .Join(
            //            incomeCategories,
            //            t => t.CategoryId,
            //            c => c.Id,
            //            (transaction, category) => transaction.Amount);
            //    var expensesEnumerable = g
            //        .Join(
            //            expensesCategories,
            //            t => t.CategoryId,
            //            c => c.Id,
            //            (transaction, category) => transaction.Amount);
            //    yield return new
            //    {
            //        income = incomeEnumerable.Sum(),
            //        expenses = expensesEnumerable.Aggregate(balance, (current, expenses) => current - expenses),
            //        g.First().Date.Month,
            //    };

            //}
        }

        public IEnumerable<object> GetParentCategoriesAmount(int userId, bool income)
        {
            var parentCategoriesId = new List<int?>();
            foreach (var c in unitOfWork.Categories.GetAll()) parentCategoriesId.Add(c.ParentId);

            var categories = new List<Category>();
            foreach (int? id in parentCategoriesId.Distinct())
            {
                if (id != null)
                {
                    categories.Add(unitOfWork.Categories.Get((int)id));
                }
                else
                {
                    continue;
                }

                if (income)
                {
                    if (categories.Last().Type == 0) categories.Remove(categories.Last());
                }
                else
                {
                    if (categories.Last().Type == 1) categories.Remove(categories.Last());
                }
            }

            var monthCategory = unitOfWork.Transactions.GetAll().Where(t => t.Date.Month == DateTime.Today.Month)
                .Select(t => unitOfWork.Categories.Get(t.CategoryId));

            return categories.Join(
                monthCategory.Distinct(),
                c => c.Id,
                t => t.Id,
                (c, t) => new
                {
                    c.Name,
                    Amount = categories.Count
                }
            );
        }

        private IEnumerable<Transaction> GetUsersTransactionId(int userId)
        {
            var userAssets = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId);
            var userTransactionsId = unitOfWork.Transactions.GetAll()
                .Join(
                    userAssets,
                    t => t.AssetId,
                    a => a.Id,
                    (t, a) => new
                    {
                        b = t.Id
                    }
                );

            foreach (var id in userTransactionsId)
            {
                yield return unitOfWork.Transactions.Get(id.b);
            }
        }
    }
}
