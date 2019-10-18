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
    }
}
