using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MoneyManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
             .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("Connection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

        }
    }
}
