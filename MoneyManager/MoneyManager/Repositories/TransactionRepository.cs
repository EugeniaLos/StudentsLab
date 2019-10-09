using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repositories
{
    public class TransactionRepository: IRepository<Transaction>
    {
        private ApplicationContext context;

        public TransactionRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return context.Transactions;
        }

        public void Create(Transaction Transaction)
        {
            context.Transactions.Add(Transaction);
        }

        public void Delete(int id)
        {
            Transaction Transaction = context.Transactions.Find(id);
            if (Transaction != null)
                context.Transactions.Remove(Transaction);
        }
    }
}
