using OnlineStore.Domain.Specifications;
using System.Linq.Expressions;

namespace OnlineStore.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T , bool> Predicate);
        IEnumerable<T> GetAllWithSpec(ISpecifications<T> specifications);
        T GetById(int id);
        T GetByIdWithSpec(int id , ISpecifications<T> specifications);
        T GetByIdWithSpec(string id, ISpecifications<T> specifications);
        IEnumerable<TResult> SelectItems<TResult>(Func<T, TResult> func , string? includes , Func<T, bool> Condition) ;
        IEnumerable<T> ThenInclude<TFirst , TSecond>(Expression<Func<TFirst, TSecond>> ThenInclude, Expression<Func<T, TFirst>> Inclue);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        public Task<int> Delete(string SqlQuery, int CartId);
        public IEnumerable<T> GetTop<TKey>(Func<T, TKey> Selector, int Size);

    }
}
