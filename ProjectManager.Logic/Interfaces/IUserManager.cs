using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.BusinessObjects;

namespace ProjectManager.Logic.Interfaces
{
    public interface IUserManager
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        User SaveUser(User user);
        int DeleteUser(int userId);
    }
}
