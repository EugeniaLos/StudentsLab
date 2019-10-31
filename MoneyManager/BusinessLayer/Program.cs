using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using MoneyManager.BusinessLayer.Services;
using MoneyManager.DataAccessLayer;
using MoneyManager.DataAccessLayer.Helpers;

namespace MoneyManager.BusinessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var unitOfWork = new UnitOfWork(ConnectionHelper.GetConnectionString());
            var assetService = new AssetService(unitOfWork);
            var s = assetService.GetAssetsWithBalance(10);
            foreach (var ss in s)
            {
                Console.Write(ss.Balance);
                Console.WriteLine();
            }
        }
    }
}
