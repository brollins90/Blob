
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Blob.Core.Data
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
