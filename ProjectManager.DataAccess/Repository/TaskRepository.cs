using ProjectManager.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ProjectManager.DataAccess.Repository
{
    public class TaskRepository : BaseRepository<Task>
    {
        private readonly DbContext _dbContext;
        public TaskRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
        }

        public new IEnumerable<Task> Get()
        {
            return Entity.Include(obj => obj.Users).Include(obj => obj.Project).Include(obj => obj.ParentTask).ToList();
        }

        public new int Delete(int id)
        {
            var objectToDelete = Entity.Find(id);
            objectToDelete.Status = false;
            _dbContext.Entry(objectToDelete).State = EntityState.Modified;
            return _dbContext.SaveChanges();
        }
    }
}
