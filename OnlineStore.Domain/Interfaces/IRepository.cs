using OnlineStore.Domain.Specifications;

namespace OnlineStore.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithSpec(ISpecifications<T> specifications);
        T GetById(int id);
        T GetByIdWithSpec(int id , ISpecifications<T> specifications);
        IEnumerable<TResult> SelectItems<TResult>(Func<T, TResult> func , string? includes , Func<T, bool> Condition) ;
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);


    }
}
