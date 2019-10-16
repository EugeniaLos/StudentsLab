using MoneyManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var monthTransaction = unitOfWork.Transactions.CurrentMonth();
            var suitedAssetsId = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId);
            var removableTransactionId = monthTransaction
                .Join(
                    suitedAssetsId,
                    t => t.AssetId,
                    a => a.Id,
                    (transaction, asset) => transaction.Id);
            foreach (int id in removableTransactionId)
            {
                unitOfWork.Transactions.Delete(id);
            }
            unitOfWork.Save();
        }

        public object GetBalance(int userId)
        {
            var incomeCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 0);
            var transactions = GetUsersTransactionId(userId);
            var incomeEnumerable = GetAmountByTransactionsAndCategories(transactions, incomeCategories);
            var expensesEnumerable = GetAmountByTransactionsAndCategories(transactions, expensesCategories);
            decimal balance = incomeEnumerable.Sum();
            balance = expensesEnumerable.Aggregate(balance, (current, expenses) => current - expenses);
            var user = unitOfWork.Users.Get(userId);
            return new
            {
                balance,
                user.Id,
                user.Name,
                user.Email
            };
        }

        public IEnumerable<object> GetAssetsWithBalance(int userId)
        {
            var incomeCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 0);
            var assets = unitOfWork.Assets.GetAll().Where(a => a.UserId == userId);
            Dictionary<Asset, IEnumerable<Transaction>> transactions = assets.ToDictionary(asset => asset,
                asset => unitOfWork.Transactions.GetAll()
                    .Where(t => t.AssetId == asset.Id));
            Dictionary<Asset, IEnumerable<decimal>> plusBalance = transactions.Keys.ToDictionary(asset => asset,
                asset => transactions[asset]
                    .Join(incomeCategories,
                        t => t.CategoryId,
                        c => c.Id,
                        (transaction,
                            category) => transaction.Amount));
            Dictionary<Asset, IEnumerable<decimal>> minusBalance = transactions.Keys.ToDictionary(asset => asset,
                asset => transactions[asset]
                    .Join(expensesCategories,
                        t => t.CategoryId,
                        c => c.Id,
                        (transaction,
                            category) => transaction.Amount));
            Dictionary<Asset, decimal> assetBalance = plusBalance.Keys.ToDictionary(key => key,
                key => plusBalance[key]
                    .Sum());
            foreach (var key in minusBalance.Keys)
            {
                foreach (var expenses in minusBalance[key])
                {
                    assetBalance[key] -= expenses;
                }
            }

            foreach (var key in assetBalance.Keys)
            {
                yield return new
                {
                    key.Id,
                    key.Name,
                    sum = assetBalance[key]
                };
            }
        }

        public IEnumerable<IEnumerable<object>> GetOrderedTransactions(int userid)
        {
            yield return GetUsersTransactionId(userid).OrderByDescending(t => t.Date).Select(t => new
            {
                Asset = unitOfWork.Assets.Get(t.AssetId).Name,
                Category = unitOfWork.Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            });
            yield return GetUsersTransactionId(userid).OrderBy(t => unitOfWork.Assets.Get(t.AssetId).Name).Select(t => new
            {
                Asset = unitOfWork.Assets.Get(t.AssetId).Name,
                Category = unitOfWork.Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            }); ;
            yield return GetUsersTransactionId(userid).OrderBy(t => unitOfWork.Categories.Get(t.CategoryId).Name).Select(t => new
            {
                Asset = unitOfWork.Assets.Get(t.AssetId).Name,
                Category = unitOfWork.Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            }); ;
        }

        public IEnumerable<object> BalancePerPeriod(int userId, DateTime startDate, DateTime endDate)
        {
            var transactionsPerPeriod = GetUsersTransactionId(userId).Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate);
            var incomeCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = unitOfWork.Categories.GetAll().Where(c => c.Type == 0);
            var transactionGroups = transactionsPerPeriod.OrderBy(t => t.Date).GroupBy(t => t.Date.Month);
            foreach (var g in transactionGroups)
            {
                decimal balance = 0;
                var incomeEnumerable = g
                    .Join(
                        incomeCategories,
                        t => t.CategoryId,
                        c => c.Id,
                        (transaction, category) => transaction.Amount);
                var expensesEnumerable = g
                    .Join(
                        expensesCategories,
                        t => t.CategoryId,
                        c => c.Id,
                        (transaction, category) => transaction.Amount);
                yield return new
                {
                    income = incomeEnumerable.Sum(),
                    expenses = expensesEnumerable.Aggregate(balance, (current, expenses) => current - expenses),
                    g.First().Date.Month,
                };

            }
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

        private IEnumerable<decimal> GetAmountByTransactionsAndCategories(IEnumerable<Transaction> transactions, IEnumerable<Category> categories)
        {
            return transactions
                .Join(
                    categories,
                    t => t.CategoryId,
                    c => c.Id,
                    (transaction, category) => transaction.Amount);
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
