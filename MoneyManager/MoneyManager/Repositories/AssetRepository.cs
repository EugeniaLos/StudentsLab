using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccessLayer.Entities;

namespace MoneyManager.DataAccessLayer.Repositories
{
    public class AssetRepository : Repository<Asset>
    {
        public AssetRepository(ApplicationContext context) : base(context) { }

        public IList<Asset> GetAssetsWithTransactionsAndCategory(int userId)
        {
            return GetAll()
                .Where(x => x.UserId == userId)
                .Include(x => x.Transactions).ThenInclude(t => t.Category)
                .ToList();
        }
    }
}
