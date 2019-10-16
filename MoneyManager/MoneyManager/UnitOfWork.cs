using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoneyManager.Repositories;

namespace MoneyManager
{
    public class UnitOfWork: IDisposable
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

        public UserRepository Users
        {
            get { return userRepository ?? (userRepository = new UserRepository(applicationContext)); }
        }

        public AssetRepository Assets
        {
            get { return assetRepository ?? (assetRepository = new AssetRepository(applicationContext)); }
        }

        public CategoryRepository Categories
        {
            get { return categoryRepository ?? (categoryRepository = new CategoryRepository(applicationContext)); }
        }

        public TransactionRepository Transactions
        {
            get
            {
                return transactionRepository ?? (transactionRepository = new TransactionRepository(applicationContext));
            }
        }

        public void Save()
        {
            applicationContext.SaveChanges();
        }

        public void Dispose()
        {
            applicationContext.Dispose();
        }
    }
}
