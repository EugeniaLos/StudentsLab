using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public static class InitializerDB
    {
        public static void Initialize(ModelBuilder modelBuilder, ApplicationContext applicationContext)
        {
            using (var transaction = applicationContext.Database.BeginTransaction())
            {
                try
                {
                    modelBuilder.Entity<User>().HasData(
                        new User[]
                        {
                            new User {Id = 1, Name= "Alice", Email = "Superstar1955@yahoo.uk"},
                            new User {Id = 2, Name= "Sam", Email = "TheGreenOne@spies.com"},
                            new User {Id = 3, Name = "Nemo", Email = "Dory4ever@pixar.com"},
                            new User {Id = 4, Name = "Alejandro", Email = "Alejandro@rambler.ru"},
                            new User {Id = 5, Name = "Eugenia", Email = "EugeniaLos@mail.com"},
                            new User {Id = 6, Name = "Anastasia", Email = "Anastasia111@gmail.com"},
                            new User {Id = 7, Name = "Jerry", Email = "littleprettymouse@pixar.com"},
                            new User {Id = 8, Name = "Micky", Email = "littleprettymouse@disney.com"},
                            new User {Id = 9, Name = "Alisa", Email = "supergirl@yandex.ru"},
                            new User {Id = 10, Name = "Miranda", Email = "parker@gmail.com"},
                            new User {Id = 11, Name= "Tom", Email = "IAmTom@groot.com"},
                        });

                    modelBuilder.Entity<Asset>().HasData(
                        new Asset[]
                        {
                    new Asset {Id = 1, Name = "AlfaBank debit card", UserId = 1},
                    new Asset {Id = 2, Name = "Cash", UserId = 1},
                    new Asset {Id = 3, Name = "AlfaBank credit card", UserId = 2},
                    new Asset {Id = 4, Name = "MTBBank debit card", UserId = 3},
                    new Asset {Id = 5, Name = "Cash", UserId = 4},
                    new Asset {Id = 6, Name = "AlfaBank debit card", UserId = 5},
                    new Asset {Id = 7, Name = "Cash", UserId = 6},
                    new Asset {Id = 8, Name = "Belarusbank account", UserId = 6},
                    new Asset {Id = 9, Name = "Cash", UserId = 7},
                    new Asset {Id = 10, Name = "Cash", UserId = 8},
                    new Asset {Id = 11, Name = "Cash", UserId = 9},
                    new Asset {Id = 12, Name = "Belarusbank credit card", UserId = 10},
                    new Asset {Id = 13, Name = "Cash", UserId = 10},
                    new Asset {Id = 14, Name = "Cash", UserId = 1},
                    new Asset {Id = 15, Name = "Cash", UserId = 3},
                    new Asset {Id = 16, Name = "Belarusbank debit card", UserId = 9},
                    new Asset {Id = 17, Name = "Cash", UserId = 5},
                    new Asset {Id = 18, Name = "Cash", UserId = 11},
                    new Asset {Id = 19, Name = "AlfaBank credit card", UserId = 1},
                    new Asset {Id = 20, Name = "Belarusbank account", UserId = 2},
                    new Asset {Id = 21, Name = "MTBBank debit card", UserId = 1},
                    new Asset {Id = 22, Name = "AlfaBank credit card", UserId = 3},
                    new Asset {Id = 23, Name = "MTBBank debit card", UserId = 1},
                        }
                        );

                    modelBuilder.Entity<Category>().HasData(
                        new Category[]
                        {
                    new Category { Id = 1, Name = "Transportation", Type = 0}, //1 - income, 0 - expense
                    new Category { Id = 2, Name = "Food", Type = 0},
                    new Category { Id = 3, Name = "Travelling", Type = 0},
                    new Category { Id = 4, Name = "Clothes", Type = 0},
                    new Category { Id = 5, Name = "Pets", Type = 0},
                    new Category { Id = 6, Name = "Movies", Type = 0},
                    new Category { Id = 7, Name = "Salary", Type = 1},
                    new Category { Id = 8, Name = "Bonus", Type = 1},
                    new Category { Id = 9, Name = "Overtime money", Type = 1},
                    new Category { Id = 10, Name = "Bills", Type = 0},
                    new Category { Id = 11, Name = "Netflix subscription", Type = 0},
                        }
                        );

                    //modelBuilder.Entity<Transaction>().HasData(                       Add Generator!!!!!!!!!!
                    //    new Transaction[]
                    //    {
                            
                    //    }
                    //    );
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
