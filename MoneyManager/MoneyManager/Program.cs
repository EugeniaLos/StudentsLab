﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.WebSockets;

namespace MoneyManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var unitOfWork = new UnitOfWork(connectionString);



            //DateTime d = new DateTime(2019, 8, 1);
            //foreach (var obj in unitOfWork.GetParentCategoriesAmount(6, false))
            //{

            //    Console.WriteLine(obj);
            //    Console.WriteLine();
            //    Console.WriteLine();
            //    Console.WriteLine();
            //}
        }
    }
}
