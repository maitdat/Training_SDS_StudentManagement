using FluentNHibernate.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ISession = NHibernate.ISession;

namespace SeverGrpc_NHibernate.RepositoryNHibernate
{
    public class NHibernateRepository<T> : INHibernateRepository<T> where T : class
    {
        private readonly ISession _session;

        public NHibernateRepository(ISession session)
        {
            _session = session;
        }

        #region Implementation of IRepository<T>

        public bool Add(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(entity);
                transaction.Commit();
            }
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (T item in items)
                {
                    _session.Save(item);
                }
                transaction.Commit();
            }
            return true;
        }

        public bool Update(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Merge(entity);
                transaction.Commit();
            }
            return true;
        }

        public bool Update(IEnumerable<T> items)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (T item in items)
                {
                    _session.Merge(item);
                }
                transaction.Commit();
            }
            return true;
        }

        public bool Delete(T entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (T entity in entities)
                {
                    _session.Delete(entity);
                }
                transaction.Commit();
            }
            return true;
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        public T FindBy(int id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        public int GetTotalCount()
        {
            return _session.Query<T>().Count();
        }

        public async Task<(List<T> Items, int TotalCount)> GetPagedListAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 10)
        {
            IQueryable<T> query = _session.Query<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            int totalCount = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();
            return (items, totalCount);
        }
        #endregion
    }

}
