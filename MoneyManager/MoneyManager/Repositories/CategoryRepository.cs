using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repositories
{
    public class CategoryRepository: IRepository<Category>
    {
        private ApplicationContext context;

        public CategoryRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories;
        }

        public void Create(Category category)
        {
            context.Categories.Add(category);
        }

        public void Delete(int id)
        {
            Category Category = context.Categories.Find(id);
            if (Category != null)
                context.Categories.Remove(Category);
        }
    }
}
