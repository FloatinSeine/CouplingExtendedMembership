using System;
using System.Collections.Generic;
using Raven.Client;

namespace Coupling.Domain.Persistence.Raven
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IRavenSessionFactory _factory;

        protected IDocumentSession Session { get; private set; }

        //protected IDocumentSession Session
        //{
        //    get { return _factory.CreateSession(); }
        //}

        protected BaseRepository(IRavenSessionFactory factory)
        {
            _factory = factory;
            //Session = factory.CreateSession();
        }

        public TEntity Get(string id)
        {
            TEntity ent = null;
            using (var session = _factory.CreateSession())
            {
                ent = session.Load<TEntity>(id);
            }
            return ent;
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
            _factory = null;
            if (Session!=null) Session.Dispose();
            Session = null;
        }
    }
}
