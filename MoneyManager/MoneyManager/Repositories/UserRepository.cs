using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MoneyManager.DataAccessLayer.Entities;

namespace MoneyManager.DataAccessLayer.Repositories
{
    public class UserRepository: Repository<User>
    {
        public UserRepository(ApplicationContext context) : base(context) { }

        public List<User> GetSortedUsers()
        {
            return GetAll()
                .OrderBy(u => u.Name)
                .ToList();
        }

        public User GetUserByEmail(string email)
        {
            return GetAll()
                .First(u => u.Email == email);
        }

    }
}
