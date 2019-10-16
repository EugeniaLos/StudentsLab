﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoneyManager
{
    public class ApplicationContext: DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //FirstCreated = false;
            Database.EnsureCreated();
            IsEmpty = !(Users.Any() || Assets.Any() || Transactions.Any() || Categories.Any());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        //public bool FirstCreated { get; private set; }
        public bool IsEmpty { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.Relational().TableName = entityType.DisplayName();
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
