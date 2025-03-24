using FluentNHibernate.Data;
using System.Linq.Expressions;

namespace SeverGrpc_NHibernate.RepositoryNHibernate
{
    public interface INHibernateRepository<T> where T : class
    {
        bool Add(T entity);

        bool Add(IEnumerable<T> items);

        bool Update(T entity);

        bool Update(IEnumerable<T> items);

        bool Delete(T entity);

        bool Delete(IEnumerable<T> entities);

        IQueryable<T> All();

        T FindBy(Expression<Func<T, bool>> expression);

        T FindBy(int id);

        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
        Task<(List<T> Items, int TotalCount)> GetPagedListAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 10);
    }
}
