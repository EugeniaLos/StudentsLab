using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyManager.BusinessLayer.DataModels;
using MoneyManager.DataAccessLayer;

namespace MoneyManager.BusinessLayer.Services
{
    public class AssetService
    {
        private readonly UnitOfWork unitOfWork;

        public AssetService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<AssetBalance> GetAssetsWithBalance(int userId)
        {
            return unitOfWork.Assets.GetAssetsWithTransactionsAndCategory(userId)
                .Select(a => new AssetBalance
                {
                    AssetId = a.Id, AssetName = a.Name,
                    Balance = a.Transactions
                                  .Where(t => t.Category.Type == 1)
                                  .Select(t => t.Amount)
                                  .Sum() -
                              a.Transactions
                                  .Where(t => t.Category.Type == 0)
                                  .Select(t => t.Amount)
                                  .Sum()
                });
        }
    }
}
