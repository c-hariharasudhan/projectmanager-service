using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DataAccess.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private DbSet<T> _entity;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _dbContext = unitOfWork.DbContext;
            _entity = _dbContext.Set<T>();
        }
       
        public DbSet<T> Entity { get { return _entity; } }

        public T Create(T objectToCreate)
        {
            objectToCreate = Entity.Add(objectToCreate);
            _dbContext.SaveChanges();
            return objectToCreate;
        }

        public int Delete(int id)
        {
            var objectToDelete = Entity.Find(id);
            _dbContext.Entry(objectToDelete).State = EntityState.Deleted;
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> Get()
        {
            return Entity.ToList();
        }

        public T GetById(int id)
        {
            return Entity.Find(id);
        }

        public T Update(T objectToUpdate)
        {
            objectToUpdate = Entity.Attach(objectToUpdate);
            _dbContext.Entry(objectToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return objectToUpdate;
        }
    }
}
