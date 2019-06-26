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
    public class ProjectRepository : BaseRepository<Project>, IRepository<Project>
    {
        private readonly DbContext _dbContext;
        public ProjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
        }

        public new IEnumerable<Project> Get()
        {
            var result = Entity.Include(obj => obj.Users).Include(obj => obj.Tasks).ToList();
            return result;
        }
        
    }
}
