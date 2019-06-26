using ProjectManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace ProjectManager.DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IRepository<User>
    {
        private readonly DbContext _dbContext;
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
        }
        public new void Update(Expression<Func<User, bool>> predicate, string propertyName, object propertyValue)
        {
            var user = Entity.Where(predicate).FirstOrDefault();
            if(user != null)
            {
                _dbContext.Entry(user).Property(propertyName).CurrentValue = propertyValue;
                _dbContext.Entry(user).Property(propertyName).IsModified = true;
                _dbContext.SaveChanges();
            }
        }

    }
}
