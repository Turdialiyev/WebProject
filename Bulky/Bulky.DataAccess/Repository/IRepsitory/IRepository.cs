﻿using System.Linq.Expressions;


namespace Bulky.DataAccess.Repository.IRepsitory
{
    public interface IRepository<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
