using ProjectManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ProjectManager.DataAccess.Repository
{
    public class ParentTaskRepository : BaseRepository<ParentTask>, IRepository<ParentTask>
    {
        public ParentTaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
