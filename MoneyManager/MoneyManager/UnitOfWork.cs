using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Repositories;

namespace MoneyManager
{
    public class UnitOfWork: IDisposable
    {
        private ApplicationContext applicationContext;
        private UserRepository userRepository;
        private AssetRepository assetRepository;
        private CategoryRepository categoryRepository;
        private TransactionRepository transactionRepository;

        public UnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            applicationContext = new ApplicationContext(options);
            if (applicationContext.IsEmpty)
            {
                var initializer = new InitializerDB();
                initializer.Initialize(applicationContext);
            }
        }

        public UserRepository Users
        {
            get { return userRepository ?? (userRepository = new UserRepository(applicationContext)); }
        }

        public AssetRepository Assets
        {
            get { return assetRepository ?? (assetRepository = new AssetRepository(applicationContext)); }
        }

        public CategoryRepository Categories
        {
            get { return categoryRepository ?? (categoryRepository = new CategoryRepository(applicationContext)); }
        }

        public TransactionRepository Transactions
        {
            get
            {
                return transactionRepository ?? (transactionRepository = new TransactionRepository(applicationContext));
            }
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return Users.GetAll().First(u => u.Email == email);
        }

        public IEnumerable<object> GetSortedUsers()
        {
            return Users.GetAll().OrderBy(u => u.Name).Select(u => new { id = u.Id, Name = u.Name, Email = u.Email });
        }

        public void Dispose()
        {
            applicationContext.Dispose();
        }

        public void DeleteCurrentMonthUsersTransaction(int userId)
        {
            var monthTransaction = Transactions.GetAll().Where(u => u.Date.Month == DateTime.Today.Month)
                .Where(u => u.Date.Year == DateTime.Today.Year);
            var suitedAssetsId = Assets.GetAll().Where(a => a.UserId == userId);
            var removableTransactionId = monthTransaction
                .Join(
                    suitedAssetsId,
                    t => t.AssetId,
                    a => a.Id,
                    (transaction, asset) => transaction.Id);
            foreach (int id in removableTransactionId)
            {
                Transactions.Delete(id);
            }
        }

        public object GetBalance(int userId)
        {
            var incomeCategories = Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = Categories.GetAll().Where(c => c.Type == 0);
            var transactions = GetUsersTransactionId(userId);
            var incomeEnumerable = GetAmountByTransactionsAndCategories(transactions, incomeCategories);
            var expensesEnumerable = GetAmountByTransactionsAndCategories(transactions, expensesCategories);
            decimal balance = incomeEnumerable.Sum();
            balance = expensesEnumerable.Aggregate(balance, (current, expenses) => current - expenses);
            var user = Users.Get(userId);
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
            var incomeCategories = Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = Categories.GetAll().Where(c => c.Type == 0);
            var assets = Assets.GetAll().Where(a => a.UserId == userId);
            Dictionary<Asset, IEnumerable<Transaction>> transactions = assets.ToDictionary(asset => asset,
                asset => Transactions.GetAll()
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
                Asset = Assets.Get(t.AssetId).Name,
                Category = Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            });
            yield return GetUsersTransactionId(userid).OrderBy(t => Assets.Get(t.AssetId).Name).Select(t => new
            {
                Asset = Assets.Get(t.AssetId).Name,
                Category = Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            }); ;
            yield return GetUsersTransactionId(userid).OrderBy(t => Categories.Get(t.CategoryId).Name).Select(t => new
            {
                Asset = Assets.Get(t.AssetId).Name,
                Category = Categories.Get(t.CategoryId).Name,
                t.Amount,
                t.Date,
                t.Comment
            }); ;
        }

        public IEnumerable<object> BalancePerPeriod(int userId, DateTime startDate, DateTime endDate)
        {
            var transactionsPerPeriod = GetUsersTransactionId(userId).Where(t => t.Date > startDate)
                .Where(t => t.Date < endDate);
            var incomeCategories = Categories.GetAll().Where(c => c.Type == 1);
            var expensesCategories = Categories.GetAll().Where(c => c.Type == 0);
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
            foreach (var c in Categories.GetAll()) parentCategoriesId.Add(c.ParentId);

            var categories = new List<Category>();
            foreach (int? id in parentCategoriesId.Distinct())
            {
                if (id != null)
                {
                    categories.Add(Categories.Get((int)id));
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

            var monthCategory = Transactions.GetAll().Where(t => t.Date.Month == DateTime.Today.Month)
                .Select(t => Categories.Get(t.CategoryId));

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

        //private decimal GetIncomeBalance(IEnumerable<Transaction> transactions, IEnumerable<Category> categories)
        //{
        //    var incomeEnumerable = GetAmountByTransactionsAndCategories(transactions, categories);
        //    return incomeEnumerable.Sum();
        //}

        //private decimal GetExpensesBalance(IEnumerable<Transaction> transactions, IEnumerable<Category> categories)
        //{
        //    var expensesEnumerable = GetAmountByTransactionsAndCategories(transactions, categories);
        //    decimal balance = 0;
        //    return expensesEnumerable.Aggregate(balance, (current, expenses) => current - expenses);
        //}

        //private decimal GetBalanceByCategories

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
            var userAssets = Assets.GetAll().Where(a => a.UserId == userId);
            var userTransactionsId = Transactions.GetAll()
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
                yield return Transactions.Get(id.b);
            }
        }
    }
}
