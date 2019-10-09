using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public bool FirstCreated { get; private set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            FirstCreated = false;
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FirstCreated = true;
            //Выполняется при вызове Database.EnsureCreated(), должны быть указаны id
            //modelBuilder.Entity<User>().HasData(
            //            new User[]
            //            {
            //    new User { Name="Tom", Email = "IAmTom@groot.com"},
            //    new User { Name="Alice", Email = "Superstar1955@yahoo.uk"},
            //    new User { Name="Sam", Email = "TheGreenOne@spies.com"}
            //            });
            //InitializerDB.Initialize(modelBuilder, this);
            base.OnModelCreating(modelBuilder);
        }
    }
}
