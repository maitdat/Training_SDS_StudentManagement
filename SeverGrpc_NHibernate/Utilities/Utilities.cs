using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SeverGrpc_NHibernate.Utilities;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SeverGrpc_NHibernate.Utilities
{
    public static class Utilities
    {
        

        public static IQueryable<TSource> ApplyPaging<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }
        public static IQueryable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize).AsQueryable().AsAsyncQueryable() : source.AsQueryable().AsAsyncQueryable();
        }

        public static IQueryable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize, out int totalItem)
        {
            totalItem = source.Count();
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize).AsQueryable().AsAsyncQueryable() : ((IQueryable<TSource>)source).AsAsyncQueryable();
        }
        public static IQueryable<T> ApplySortingDate<T>(this IQueryable<T> query, Sort sortType, Expression<Func<T, DateTime>> orderByExpression)
        {
            return sortType == 0 ? query : sortType == Sort.Asc ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
        }
        public static async Task<T> GetEntityByIdAsync<T>(this DbContext context, long entityId) where T : class
        {
            var entity = await context.Set<T>().FindAsync(entityId);

            if (entity == null)
            {
                throw new Exception(string.Format(Constants.ExceptionMessage.NOT_FOUND, typeof(T).Name));
            }

            return entity;
        }
        public static string ReplaceInvalidCharsInPath(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }

        public static IEnumerable<TSource> UnionSet<TSource>(this IEnumerable<TSource> source, params IEnumerable<TSource>[] enumerables)
        {
            foreach (var item in enumerables)
            {
                source = source.Union(item);
            }
            return source;
        }
        public static bool IsSubSetOf<TSource>(this IEnumerable<TSource> smail, IEnumerable<TSource> large)
        {

            return !smail.Except(large).Any();
        }

        public static string RemoveVietNameseChars(string source)
        {
            string[] VietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    source = source.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return source;
        }
        public static T GetById<T>(this DbSet<T> entity, long id) where T : BaseEntity
        {
            var record = entity.Find(id);
            if (record == null)
            {
                throw new Exception(Constants.ExceptionMessage.ITEM_NOT_FOUND);
            }
            return record;
        }

        public static IQueryable<T> GetByIdQueryable<T>(this DbSet<T> entity, long id) where T : BaseEntity
        {
            var result = entity.Where(record => record.Id == id);
            if (!result.Any())
            {
                throw new Exception(Constants.ExceptionMessage.ITEM_NOT_FOUND);
            }
            return result;
        }

        public static T GetAvailableById<T>(this DbSet<T> entity, long id) where T : BaseEntityCommon
        {
            var record = entity.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
            if (record == null)
            {
                throw new Exception(Constants.ExceptionMessage.ITEM_NOT_FOUND);
            }
            return record;
        }

        public static IQueryable<T> GetAvailableByIdQueryable<T>(this DbSet<T> entity, long id) where T : BaseEntityCommon
        {
            var record = entity.Where(c => c.Id == id && !c.IsDeleted);
            if (!record.Any())
            {
                throw new Exception(Constants.ExceptionMessage.ITEM_NOT_FOUND);
            }
            return record;
        }

        public static void Delete<T>(this DbSet<T> entity, long id, bool isPermanent = false) where T : BaseEntityCommon
        {
            var record = entity.GetAvailableById(id);
            if (isPermanent)
            {
                entity.Remove(record);
            }
            else
            {
                record.IsDeleted = true;
                entity.Update(record);
            }
        }

        public static void Delete<T>(this DbSet<T> entity, T record, bool isPermanent = false) where T : BaseEntityCommon
        {
            if (isPermanent)
            {
                entity.Remove(record);
            }
            else
            {
                record.IsDeleted = true;
                entity.Update(record);
            }
        }

        public static void DeleteRange<T>(this DbSet<T> entity, IEnumerable<long> ids, bool isPermanent = false) where T : BaseEntityCommon
        {
            var records = entity.Where(record => ids.Contains(record.Id)).ToList();
            if (isPermanent)
            {
                entity.RemoveRange(records);
            }
            else
            {
                records.ForEach(item => item.IsDeleted = true);
                entity.UpdateRange(records);
            }
        }

        public static void DeleteRange<T>(this DbSet<T> entity, IEnumerable<T> records, bool isPermanent = false) where T : BaseEntityCommon
        {
            if (isPermanent)
            {
                entity.RemoveRange(records);
            }
            else
            {
                records.ToList().ForEach(item => item.IsDeleted = true);
                entity.UpdateRange(records);
            }
        }

        public static void PasswordValid(this string password)
        {
            var pattern = @"^\S{6,}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(password)) throw new InvalidException(nameof(password));
        }
        public static void PhoneNumberValid(this string phoneNumber)
        {
            var pattern = @"^0[0-9]{9}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(phoneNumber)) throw new InvalidException(nameof(phoneNumber));
        }
        public static void EmailValid(this string email)
        {
            var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(email)) throw new InvalidException(nameof(email));
        }

        public static void IsDefineEnum(this Type type, Enum name)
        {
            string enumName = type.GetEnumName(name);
            if (string.IsNullOrEmpty(enumName)) throw new NotFoundException(type.ToString().Substring(22));
            return;
        }
        public static string GetUrlAnhDaiDien(string urlPath, string userName)
        {
            return urlPath + "api/PhatTuUser/AnhDaiDien/" + userName;
        }
        public static bool CheckHasSpecialChar(string input)
        {
            var charMathInPath = Path.GetInvalidFileNameChars();
            foreach (var item in charMathInPath)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        private static IQueryable<TEntity> AsAsyncQueryable<TEntity>(this IEnumerable<TEntity> source)
        => new AsyncQueryable<TEntity>(source ?? throw new ArgumentNullException(nameof(source)));
        private class AsyncQueryable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
        {
            public AsyncQueryable(IEnumerable<TEntity> enumerable) : base(enumerable) { }
            public AsyncQueryable(Expression expression) : base(expression) { }
            public IAsyncEnumerator<TEntity> GetEnumerator() => new AsyncEnumerator(this.AsEnumerable().GetEnumerator());
            public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(this.AsEnumerable().GetEnumerator());
            IQueryProvider IQueryable.Provider => new AsyncQueryProvider(this);

            private class AsyncEnumerator : IAsyncEnumerator<TEntity>
            {
                private readonly IEnumerator<TEntity> inner;
                public AsyncEnumerator(IEnumerator<TEntity> inner) => this.inner = inner;
                public void Dispose() => inner.Dispose();
                public TEntity Current => inner.Current;
                public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(inner.MoveNext());
#pragma warning disable CS1998 // Nothing to await
                public async ValueTask DisposeAsync() => inner.Dispose();
#pragma warning restore CS1998
            }

            private class AsyncQueryProvider : IAsyncQueryProvider
            {
                private readonly IQueryProvider inner;
                internal AsyncQueryProvider(IQueryProvider inner) => this.inner = inner;
                public IQueryable CreateQuery(Expression expression) => new AsyncQueryable<TEntity>(expression);
                public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new AsyncQueryable<TElement>(expression);
#pragma warning disable CS8603 // Possible null reference return.
                public object Execute(Expression expression) => inner.Execute(expression);
#pragma warning restore CS8603 // Possible null reference return.
                public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);
                public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) => new AsyncQueryable<TResult>(expression);
                TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) => Execute<TResult>(expression);
            }
        }
        public static string NullIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }
        public static string NullIfWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }
        public static string GenerateRandomPassword(int numberChar)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, numberChar)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static DateTime? ConvertDateTimeFromString(string dateValue)
        {
            DateTime parsedDate;
            string[] formats = { "dd/M/yyyy", "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "d/M/yyyy hh:mm:ss tt" };

            if (DateTime.TryParseExact(dateValue, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }

            return null;
        }
    }
}
