using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccessLayer.Entities;

namespace MoneyManager.DataAccessLayer.Repositories
{
    public class TransactionRepository: Repository<Transaction>
    {
        public TransactionRepository(ApplicationContext context) : base(context) { }

        public IEnumerable<Transaction> CurrentMonth(int userId)
        {
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime endDate = DateTime.Today;
            return GetAll()
                .Where(t =>
                t.Date >= startDate
                && t.Date <= endDate
                && t.Asset.UserId == userId);
        }


        public List<Transaction> GetOrderedWithAssetCategory(int userId)
        {
            return GetAll()
                .Include(t => t.Asset)
                .Include(t => t.Category)
                .Where(x => x.Asset.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name)
                .ToList();
        }

        public List<Transaction> GetByUserIdAndTypeWithParentCategory(int userId, int type)
        {
            return GetAll()
                .Include(t => t.Category)
                .Where(t =>
                t.Date.Year == DateTime.Today.Year
                && t.Date.Month == DateTime.Today.Month
                && t.Category.Type == type
                && t.Asset.UserId == userId)
                .ToList();
        }

        public List<Transaction> GetByUserIdAndType(int userId, int type)
        {
            return GetAll()
                .Where(t => t.Category.Type == type &&
                t.Asset.UserId == userId)
                .ToList();
        }

        public List<Transaction> GetByDateRangeAndTypeOrdered(int userId, int type, DateTime startDate, DateTime endDate)
        {
            return GetAll()
                .Where(t =>
                t.Asset.UserId == userId && t.Category.Type == type && t.Date >= startDate && t.Date <= endDate)
                .OrderBy(t => t.Date)
                .ToList();
        }
    }
}
