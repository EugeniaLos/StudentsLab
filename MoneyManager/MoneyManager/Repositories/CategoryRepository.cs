using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.Repositories
{
    public class CategoryRepository: Repository<Category>
    {
        public CategoryRepository(ApplicationContext context) : base(context) { }
    }
}
