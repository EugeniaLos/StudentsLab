using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BusinessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var businessLogic = new BusinessLogic();
            var a = businessLogic.GetAssetsWithBalance(1);
            foreach (var aa in a)
            {
                Console.WriteLine(aa.Balance);
                Console.WriteLine(aa.AssetId);
                Console.WriteLine();
            }
        }
    }
}
