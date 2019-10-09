using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager.Repositories
{
    public class AssetRepository : IRepository<Asset>
    {
        private ApplicationContext context;

        public AssetRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Asset> GetAll()
        {
            return context.Assets;
        }

        public void Create(Asset asset)
        {
            context.Assets.Add(asset);
        }

        public void Delete(int id)
        {
            Asset Asset = context.Assets.Find(id);
            if (Asset != null)
                context.Assets.Remove(Asset);
        }
    }
}
