using System;
using System.Collections.Generic;
using Raven.Client;

namespace Coupling.Domain.Persistence.Raven
{
    public abstract class BaseRepository<TEntity> : IDisposable where TEntity : class, new()
    {
        protected IDocumentSession Session { get; private set; }

        protected BaseRepository(IRavenSessionFactory factory)
        {
            Session = factory.CreateSession();
        }

        public TEntity Get(string id)
        {
            return Session.Load<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            Session.Store(entity);
        }

        public void Update(TEntity entity)
        {
            Session.Store(entity);
        }

        public void CommitChanges()
        {
            Session.SaveChanges();
        }

        public void Dispose()
        {
            Session.Dispose();
            Session = null;
        }
    }
}
