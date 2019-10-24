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

            var s = businessLogic.GetParentCategoriesAmount(10, true);
            foreach (var ss in s)
            {
                Console.Write(ss.Amount);
                Console.Write(ss.CategoryName);
                Console.WriteLine();
            }
        }
    }
}
