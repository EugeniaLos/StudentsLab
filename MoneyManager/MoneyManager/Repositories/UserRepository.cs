using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.Repositories
{
    public class UserRepository: IRepository<User>
    {

        private ApplicationContext context;

        public UserRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public void Create(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(int id)
        {
            User User = context.Users.Find(id);
            if (User != null)
                context.Users.Remove(User);
        }

        public User Get(int id)
        {
            return context.Users.First(u => u.Id == id);
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}
