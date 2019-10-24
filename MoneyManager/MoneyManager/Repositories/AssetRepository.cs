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

        public IList<Asset> GetAssetsWithTransactions(int userId)
        {
            return GetAll()
                .Where(x => x.UserId == userId)
                .Include(x => x.Transactions)
                .ToList();
        }
    }
}
