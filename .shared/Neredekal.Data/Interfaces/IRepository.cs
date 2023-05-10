using System;
using System.Linq;
using Neredekal.Data.Entities;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Neredekal.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public void Add(T entity);
        public void Delete(T entity);
        public void Update(T entity);

        public IQueryable<T> GetQueryable();
        public T Get(Expression<Func<T, bool>> filter);
        public List<T> GetAll(Expression<Func<T, bool>>? filter = null);
    }
}