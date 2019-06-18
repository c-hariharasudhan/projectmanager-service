using System.Collections.Generic;

namespace ProjectManager.DataAccess.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T Create(T objectToCreate);
        T Update(T objectToUpdate);
        void Delete(int id);
        IEnumerable<T> Get();

    }
}
