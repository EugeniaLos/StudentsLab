using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyManager.Repositories
{
    public class UserRepository: Repository<User>
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public User Get(int id)
        {
            return context.Users.First(u => u.Id == id);
        }

        public IEnumerable<object> GetSortedUsers()
        {
            return context.Users.OrderBy(u => u.Name).Select(u => new { id = u.Id, Name = u.Name, Email = u.Email });
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.First(u => u.Email == email);
        }

    }
}
