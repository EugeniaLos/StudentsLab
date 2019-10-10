using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Repositories;

namespace MoneyManager
{
    public class UnitOfWork
    {
        private ApplicationContext applicationContext;
        private UserRepository userRepository;
        private AssetRepository assetRepository;
        private CategoryRepository categoryRepository;
        private TransactionRepository transactionRepository;

        public UnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            applicationContext = new ApplicationContext(options);
            if (applicationContext.IsEmpty)
            {
                var initializer = new InitializerDB();
                initializer.Initialize(applicationContext);
            }
        }
    }
}
