using ProjectManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectManager.DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IRepository<User>
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {     
        }
    }
}
