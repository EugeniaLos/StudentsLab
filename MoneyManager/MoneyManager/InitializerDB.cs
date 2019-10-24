using System;
using MoneyManager.DataAccessLayer.Entities;

namespace MoneyManager.DataAccessLayer
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
                    User[] users = new User[]
                {
                    new User {Name = "Alice", Email = "Superstar1955@yahoo.uk"},
                    new User {Name = "Sam", Email = "TheGreenOne@spies.com"},
                    new User {Name = "Nemo", Email = "Dory4ever@pixar.com"},
                    new User {Name = "Alejandro", Email = "Alejandro@rambler.ru"},
                    new User {Name = "Eugenia", Email = "EugeniaLos@mail.com"},
                    new User {Name = "Anastasia", Email = "Anastasia111@gmail.com"},
                    new User {Name = "Jerry", Email = "littleprettymouse@pixar.com"},
                    new User {Name = "Micky", Email = "littleprettymouse@disney.com"},
                    new User {Name = "Alisa", Email = "supergirl@yandex.ru"},
                    new User {Name = "Miranda", Email = "parker@gmail.com"},
                    new User {Name = "Tom", Email = "IAmTom@groot.com"},
                };
                    applicationContext.AddRange(users);
                    applicationContext.SaveChanges();

                    applicationContext.AddRange(
                        new Asset[]
                        {
                            new Asset { Name = "AlfaBank debit card", User = users[0]},
                            new Asset { Name = "Cash", User = users[0]},
                            new Asset { Name = "AlfaBank credit card", User = users[1]},
                            new Asset { Name = "MTBBank debit card", User = users[2]},
                            new Asset { Name = "Cash", User = users[3]},
                            new Asset {Name = "AlfaBank debit card", User = users[4]},
                            new Asset {Name = "Cash", User = users[5]},
                            new Asset { Name = "Belarusbank account",User = users[5]},
                            new Asset { Name = "Cash", User = users[6]},
                            new Asset { Name = "Cash", User = users[7]},
                            new Asset { Name = "Cash", User = users[8]},
                            new Asset { Name = "Belarusbank credit card", User = users[9]},
                            new Asset { Name = "Cash", User = users[9]},
                            new Asset { Name = "Cash",User = users[0]},
                            new Asset { Name = "Cash", User = users[2]},
                            new Asset { Name = "Belarusbank debit card", User = users[8]},
                            new Asset { Name = "Cash", User = users[4]},
                            new Asset { Name = "Cash", User = users[1]},
                            new Asset { Name = "AlfaBank credit card", User = users[2]},
                            new Asset { Name = "Belarusbank account", User = users[1]},
                            new Asset { Name = "MTBBank debit card", User = users[0]},
                            new Asset { Name = "AlfaBank credit card", User = users[2]},
                            new Asset { Name = "MTBBank debit card", User = users[0]},
                        });
                    applicationContext.SaveChanges();

                Category[] parent = new Category[] //1 - income, 0 - expense
                {
                    new Category {Name = "Transportation", Type = 0, ParentId = null},
                    new Category {Name = "Pets", Type = 0, ParentId = null},
                    new Category {Name = "Movies and TV", Type = 0, ParentId = null},
                    new Category {Name = "Job", Type = 1, ParentId = null},
                    new Category {Name = "Freelance", Type = 1, ParentId = null},
                };
                applicationContext.AddRange(parent);
                applicationContext.SaveChanges();
                applicationContext.AddRange(
                    new Category[]
                    {
                        new Category { Name = "Taxi", Type = 0, Parent = parent[0]},
                        new Category { Name = "Subway", Type = 0, Parent = parent[0]},
                        new Category {  Name = "Train", Type = 0, Parent = parent[0]},
                        new Category {  Name = "Salary", Type = 1, Parent = parent[3]},
                        new Category { Name = "Bonus", Type = 1, Parent = parent[3]},
                        new Category {  Name = "Overtime money", Type = 1, Parent = parent[3]},
                        new Category {  Name = "Netflix subscription", Type = 0, Parent = parent[2]},
                        new Category {  Name = "Cinema tickets", Type = 0, Parent = parent[2]},
                        new Category {  Name = "Pet's food", Type = 0, Parent = parent[1]},
                        new Category {  Name = "Pet's toys", Type = 0, Parent = parent[1]},
                    });
                applicationContext.SaveChanges();

                var transactions = new Transaction[5000];
                    for(int i = 0; i < 5000; i++)
                    {
                        transactions[i] = new Transaction { Date = RandomDay(), Amount = (decimal)gen.Next(1, 110000), AssetId = gen.Next(1, 23), CategoryId = gen.Next(1, 15) };
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
            DateTime start = new DateTime(2019, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
