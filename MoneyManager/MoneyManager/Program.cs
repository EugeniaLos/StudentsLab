using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MoneyManager.DataAccessLayer
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
        }
    }
}
