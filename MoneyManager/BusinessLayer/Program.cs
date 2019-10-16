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
            foreach (var V in businessLogic.GetOrderedTransactions(8))
            {
                Console.WriteLine(V);
                Console.WriteLine();
            }
        }
    }
}
