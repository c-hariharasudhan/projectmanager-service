using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DataAccess.Entity;

namespace ProjectManager.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly string _connectionString;
        private ProjectManagerEntities _dbContext;
        //public UnitOfWork(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        public ProjectManagerEntities DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new ProjectManagerEntities();
                }
                return _dbContext;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool doDispose)
        {
            if (doDispose)
            {
                if(_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
    }
}
