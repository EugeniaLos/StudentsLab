using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyManager.DataAccessLayer;

namespace MoneyManager.BusinessLayer.Services
{
    public class TransactionService
    {
        private readonly UnitOfWork unitOfWork;

        public TransactionService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void DeleteCurrentMonthUsersTransaction(int userId)
        {
            var removableTransaction = unitOfWork.Transactions.CurrentMonth(userId);

            foreach (var transaction in removableTransaction)
            {
                unitOfWork.Transactions.Delete(transaction);
            }
            unitOfWork.Save();
        }

        public IEnumerable<OrderedTransactions> GetOrderedTransactions(int userId)
        {
            return unitOfWork.Transactions.GetOrderedWithAssetCategory(userId)
                .Select(t => new OrderedTransactions
                {
                    AssetName = t.Asset.Name,
                    CategoryName = t.Category.Name,
                    Amount = t.Amount,
                    Date = t.Date,
                    Comment = t.Comment
                });
        }
    }
}
