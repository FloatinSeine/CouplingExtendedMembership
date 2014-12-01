
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Coupling.Domain.Persistence
{
    public interface IRepository : IDisposable
    {

    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        void Add(TEntity entity);
        int Count();
        void Delete(TEntity entity);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(string id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void CommitChanges();
    }
}
