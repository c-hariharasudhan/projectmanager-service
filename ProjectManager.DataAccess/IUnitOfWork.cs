using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DataAccess.Entity;

namespace ProjectManager.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        ProjectManagerEntities DbContext { get; }
        int Save();
    }

}
