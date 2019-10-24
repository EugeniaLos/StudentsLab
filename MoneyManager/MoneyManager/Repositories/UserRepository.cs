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

        public IEnumerable<object> GetSortedUsers()
        {
            return GetAll()
                .OrderBy(u => u.Name)
                .Select(u => new { id = u.Id, Name = u.Name, Email = u.Email });
        }

        public User GetUserByEmail(string email)
        {
            return GetAll()
                .First(u => u.Email == email);
        }

    }
}
