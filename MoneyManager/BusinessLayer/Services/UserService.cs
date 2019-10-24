using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyManager.BusinessLayer.DataModels;
using MoneyManager.DataAccessLayer;

namespace MoneyManager.BusinessLayer.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UserBalance GetBalance(int userId)
        {
            decimal balance = unitOfWork.Transactions.GetByUserIdAndType(userId, 1)
                                  .Select(t => t.Amount)
                                  .Sum() - unitOfWork.Transactions
                                  .GetByUserIdAndType(userId, 0)
                                  .Select(t => t.Amount)
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

        public IEnumerable<BalancePeriod> BalanceByMonth(int userId, DateTime startDate, DateTime endDate)
        {
            var income = unitOfWork.Transactions.GetByDateRangeAndTypeOrdered(userId, 1, startDate, endDate)
                .GroupBy(t => new { t.Date.Month, t.Date.Year });

            var expenses = unitOfWork.Transactions.GetByDateRangeAndTypeOrdered(userId, 0, startDate, endDate)
                .GroupBy(t => new { t.Date.Month, t.Date.Year });

            return income
                .Join(expenses,
                    i => i.Key,
                    e => e.Key,
                    (i, e) => new BalancePeriod()
                    {
                        Income = i
                            .Select(i => i.Amount)
                            .Sum(),
                        Expenses = e
                            .Select(e => e.Amount)
                            .Sum(),
                        Month = i.Key.Month,
                        Year = i.Key.Year
                    });
        }
    }
}
