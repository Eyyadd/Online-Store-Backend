using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Specifications;
using OnlineStore.Infrastrucutre;
using System.Linq.Expressions;

namespace OnlineStore.Application.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private OnlineStoreDbContext _context;

        public Repository(OnlineStoreDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            var OldEntity = GetById(id);
            _context.Remove(OldEntity);
        }

        public async Task<int> Delete(string SqlQuery, int id)
        {
            var RowEffected = await _context.Database.ExecuteSqlRawAsync(SqlQuery, id);
            return RowEffected;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }


        public IEnumerable<T> GetAllIncluded(string IncludedMember)
        {
            return _context.Set<T>().Include(IncludedMember).AsNoTracking().ToList();
        }

        public IEnumerable<T> GetAllWithSpec(ISpecifications<T> specifications)
        {
            return ApplySpecifications(specifications).AsNoTracking();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id)!;
        }


        public T GetByIdWithSpec(int id, ISpecifications<T> specifications)
        {
            return ApplySpecifications(specifications).FirstOrDefault();
        }
        public T GetByIdWithSpec(string id, ISpecifications<T> specifications)
        {
            return ApplySpecifications(specifications).FirstOrDefault();
        }

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationEvaluater<T>.GetQuery(_context.Set<T>(), specifications);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public IEnumerable<TResult> SelectItems<TResult>(Func<T, TResult> func, string? includes, Func<T, bool> condition)
        {
            if (includes is not null)
                return _context.Set<T>().Include(includes).Where(condition).Select(func);
            else
                return _context.Set<T>().Where(condition).Select(func);
        }

        public IEnumerable<T> ThenInclude<TFirst, TSecond>(Expression<Func<TFirst, TSecond>> ThenInclude, Expression<Func<T, TFirst>> Inclue)
        {
            return _context.Set<T>().Include(Inclue).ThenInclude(ThenInclude);
        }

        public IEnumerable<T> GetAll(Func<T, bool> Predicate)
        {
            return _context.Set<T>().AsNoTracking().Where(Predicate);
        }

        public IQueryable<T> GetAll(string query, int size)
        {
            return _context.Set<T>().FromSqlRaw(query);
        }

        //public IQueryable<T> DynamicQuery(Expression<Func<T, bool>>? WherwCondition, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? Order, Expression<Func<IQueryable<T>, IQueryable<object>>>[]? Includes, Expression<Func<IQueryable<T>, IQueryable<object>>>[]? ThenIncludes)
        //{
        //    throw new NotImplementedException();
        //}
    }
}