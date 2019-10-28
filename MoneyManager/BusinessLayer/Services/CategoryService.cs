using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyManager.BusinessLayer.DataModels;
using MoneyManager.DataAccessLayer;

namespace MoneyManager.BusinessLayer.Services
{
    public class CategoryService
    {
        private readonly UnitOfWork unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryAmount> GetParentCategoriesAmount(int userId, bool income)
        {
            int type = income ? 1 : 0;
            return unitOfWork.Transactions.GetByUserIdAndTypeWithParentCategory(userId, type)
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategoryAmount
                    { CategoryName = g.Key, Amount = g.Sum(t => t.Amount) });
        }
    }
}
