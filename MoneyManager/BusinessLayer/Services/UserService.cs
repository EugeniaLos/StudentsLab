using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyManager.BusinessLayer.DataModels;
using MoneyManager.DataAccessLayer;
using MoneyManager.DataAccessLayer.Repositories;

namespace MoneyManager.BusinessLayer.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<UserInfo> GetSortedUsersInfo()
        {
            return unitOfWork.Users.GetSortedUsers()
                .Select(u => new UserInfo()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToList();
        }

        public UserBalance GetBalance(int userId)
        {
            decimal income = unitOfWork.Transactions.GetByUserIdAndType(userId, 1)
                .Select(t => t.Amount)
                .Sum();
            decimal expenses = unitOfWork.Transactions
                .GetByUserIdAndType(userId, 0)
                .Sum(t => t.Amount);
            decimal balance = income - expenses;
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
                            .Sum(i => i.Amount),
                        Expenses = e
                            .Sum(e => e.Amount),
                        Month = i.Key.Month,
                        Year = i.Key.Year
                    });
        }
    }
}
