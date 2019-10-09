using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyManager
{
    public class InitializerDB
    {
        private Random gen = new Random();
        public void Initialize(ApplicationContext applicationContext)
        {
            using (var transaction = applicationContext.Database.BeginTransaction())
            {
                try
                {
                    applicationContext.AddRange(
                        new User[]
                        {
                            new User {Name= "Alice", Email = "Superstar1955@yahoo.uk"},
                            new User { Name= "Sam", Email = "TheGreenOne@spies.com"},
                            new User { Name = "Nemo", Email = "Dory4ever@pixar.com"},
                            new User { Name = "Alejandro", Email = "Alejandro@rambler.ru"},
                            new User {Name = "Eugenia", Email = "EugeniaLos@mail.com"},
                            new User { Name = "Anastasia", Email = "Anastasia111@gmail.com"},
                            new User { Name = "Jerry", Email = "littleprettymouse@pixar.com"},
                            new User { Name = "Micky", Email = "littleprettymouse@disney.com"},
                            new User { Name = "Alisa", Email = "supergirl@yandex.ru"},
                            new User { Name = "Miranda", Email = "parker@gmail.com"},
                            new User { Name= "Tom", Email = "IAmTom@groot.com"},
                        });
                    applicationContext.SaveChanges();

                    applicationContext.AddRange(
                        new Asset[]
                        {
                            new Asset { Name = "AlfaBank debit card", UserId = 1},
                            new Asset { Name = "Cash", UserId = 1},
                            new Asset { Name = "AlfaBank credit card", UserId = 2},
                            new Asset { Name = "MTBBank debit card", UserId = 3},
                            new Asset { Name = "Cash", UserId = 4},
                            new Asset {Name = "AlfaBank debit card", UserId = 5},
                            new Asset {Name = "Cash", UserId = 6},
                            new Asset { Name = "Belarusbank account", UserId = 6},
                            new Asset { Name = "Cash", UserId = 7},
                            new Asset { Name = "Cash", UserId = 8},
                            new Asset { Name = "Cash", UserId = 9},
                            new Asset { Name = "Belarusbank credit card", UserId = 10},
                            new Asset { Name = "Cash", UserId = 10},
                            new Asset { Name = "Cash", UserId = 1},
                            new Asset { Name = "Cash", UserId = 3},
                            new Asset { Name = "Belarusbank debit card", UserId = 9},
                            new Asset { Name = "Cash", UserId = 5},
                            new Asset { Name = "Cash", UserId = 11},
                            new Asset { Name = "AlfaBank credit card", UserId = 1},
                            new Asset { Name = "Belarusbank account", UserId = 2},
                            new Asset { Name = "MTBBank debit card", UserId = 1},
                            new Asset { Name = "AlfaBank credit card", UserId = 3},
                            new Asset { Name = "MTBBank debit card", UserId = 1},
                        });
                    applicationContext.SaveChanges();

                    applicationContext.AddRange(
                        new Category[]
                        {
                            new Category { Name = "Transportation", Type = 0}, //1 - income, 0 - expense
                            new Category { Name = "Food", Type = 0},
                            new Category { Name = "Travelling", Type = 0},
                            new Category {  Name = "Clothes", Type = 0},
                            new Category {  Name = "Pets", Type = 0},
                            new Category {  Name = "Movies", Type = 0},
                            new Category {  Name = "Salary", Type = 1},
                            new Category { Name = "Bonus", Type = 1},
                            new Category {  Name = "Overtime money", Type = 1},
                            new Category {  Name = "Bills", Type = 0},
                            new Category {  Name = "Netflix subscription", Type = 0},
                        });
                    applicationContext.SaveChanges();

                    Transaction[] transactions = new Transaction[210];
                    for(int i = 0; i <210; i++)
                    {
                        transactions[i] = new Transaction { Date = RandomDay(), Amount = (decimal)gen.Next(1, 110000), AssetId = gen.Next(1, 23), CategoryId = gen.Next(1, 11) };
                    }
                    applicationContext.Transactions.AddRange(transactions);
                    applicationContext.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

        }

        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
