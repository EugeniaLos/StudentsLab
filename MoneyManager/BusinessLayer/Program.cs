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
            businessLogic.DeleteCurrentMonthUsersTransaction(10);
            foreach (var V in businessLogic.GetAssetsWithBalance(2))
            {
                Console.WriteLine(V);
                Console.WriteLine();
            }
        }
    }
}
