using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MoneyManager.DataAccessLayer.Helpers
{
    public class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(workingDirectory).Parent.Parent.FullName)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
