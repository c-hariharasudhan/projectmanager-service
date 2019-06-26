using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectManager.DataAccess.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T Create(T objectToCreate);
        T Update(T objectToUpdate);
        int Delete(int id);
        IEnumerable<T> Get();

        void Update(Expression<Func<T, bool>> predicate, string propertyName, object propertyValue);


        void UpdateAll(Expression<Func<T, bool>> predicate, string propertyName, object propertyValue);
        void Delete(Expression<Func<T, bool>> predicate);
    }
}
