using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccessLayer.Entities;

namespace MoneyManager.DataAccessLayer.Repositories
{
    public class CategoryRepository: Repository<Category>
    {
        public CategoryRepository(ApplicationContext context) : base(context) { }
    }
}
