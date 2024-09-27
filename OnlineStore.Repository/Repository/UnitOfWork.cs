using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Domain.Specifications;
using OnlineStore.Infrastrucutre.Repository;

namespace OnlineStore.Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineStoreDbContext _dbContext;
        private Hashtable _Repos=new Hashtable();
        public UnitOfWork(OnlineStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var Key = typeof(T).Name;
            if (!_Repos.ContainsKey(Key))
            {
                
                Repository<T> Repositry = new Repository<T>(_dbContext);
                _Repos[Key] = Repositry;

            }
            return _Repos[Key] as IRepository<T>;
        }

        public IProductRepository ProductRepository()
        {
            var Key = "ProductRepository"; 
            if (!_Repos.ContainsKey(Key))
            {
                ProductRepository Repositry = new ProductRepository(_dbContext);
                _Repos[Key] = Repositry;
            }
            return _Repos[Key] as IProductRepository;
        }

         public ICartRepository CartRepository()
        {
            var Key = "CartItemsRepository";
            if (!_Repos.ContainsKey(Key))
            {
                CartRepository Repositry = new CartRepository(_dbContext);
                _Repos[Key] = Repositry;
            }
            return _Repos[Key] as ICartRepository;
        }
    }
}
