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
            int type = 0;
            if (income)
            {
                type = 1;
            }

            return unitOfWork.Transactions.GetByUserIdAndTypeWithParentCategory(userId, type)
                .GroupBy(t => t.Category.Name)
                .Select(g => new CategoryAmount
                    { CategoryName = g.Key, Amount = g.Select(t => t.Amount).Sum() });
        }
    }
}
