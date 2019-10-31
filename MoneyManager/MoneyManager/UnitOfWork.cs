using System;
using Microsoft.EntityFrameworkCore;
using MoneyManager.DataAccessLayer.Repositories;

namespace MoneyManager.DataAccessLayer
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

        public UserRepository Users => userRepository ?? (userRepository = new UserRepository(applicationContext));

        public AssetRepository Assets => assetRepository ?? (assetRepository = new AssetRepository(applicationContext));

        public CategoryRepository Categories => categoryRepository ?? (categoryRepository = new CategoryRepository(applicationContext));

        public TransactionRepository Transactions => transactionRepository ?? (transactionRepository = new TransactionRepository(applicationContext));

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
