using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Text;

namespace MoneyManager.Repositories
{
    public class TransactionRepository: Repository<Transaction>
    {
        public TransactionRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<Transaction> CurrentMonth()
        {
            return context.Transactions.Where(u => u.Date.Month == DateTime.Today.Month)
                .Where(u => u.Date.Year == DateTime.Today.Year);
        }

        public List<Transaction> GetTransactions(int userId)
        {
            return GetAll().Where(x => x.Asset.UserId == userId).ToList();
        }

        public List<Transaction> GetTransactionsWithAsset(int userId)
        {
            return GetAll().Include(t => t.Asset).Where(x => x.Asset.UserId == userId).ToList();
        }

        public List<Transaction> GetTransactionsWithAssetCategory(int userId)
        {
            return GetAll().Include(t => t.Asset).Include(t => t.Category)
                .Where(x => x.Asset.UserId == userId)
                .ToList();
        }

        public List<Transaction> GetTransactionByType(int type)
        {
            return GetAll().Include(x => x.Category).Where(x => x.Category.Type == type).ToList();
        }

        public List<Transaction> GetTransactionByTypeWithCategory(int type)
        {
            return GetAll().Include(x => x.Category).Where(x => x.Category.Type == type).ToList();
        }
    }
}
