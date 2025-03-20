using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Training_SDS_StudentManagement.Data;
using Training_SDS_StudentManagement.Model.Base;

namespace Training_SDS_StudentManagement.DesignPattern.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            if (_dbSet == null)
                _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return GetAll().Where(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsNoTracking().Where(x => x.Id == id);
            if (includes.Length > 0)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return await query.FirstOrDefaultAsync();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }



    }
}
