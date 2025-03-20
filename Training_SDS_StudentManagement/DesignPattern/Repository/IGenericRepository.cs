using System.Linq.Expressions;

namespace Training_SDS_StudentManagement.DesignPattern.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
