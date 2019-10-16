using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.Repositories
{
    public class AssetRepository : Repository<Asset>
    {
        public AssetRepository(ApplicationContext context) : base(context) { }

        public Asset Get(int id)
        {
            return context.Assets.First(a => a.Id == id);
        }
    }
}
