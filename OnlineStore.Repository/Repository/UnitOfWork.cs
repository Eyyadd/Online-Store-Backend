using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Domain.Specifications;
using OnlineStore.Infrastrucutre.Repository;

namespace OnlineStore.Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineStoreDbContext _dbContext;
        private Hashtable _Repos = new Hashtable();
        private readonly UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UnitOfWork(OnlineStoreDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<User> usermanager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = usermanager;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
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
        public ICategoryRepository CategoryRepository()
        {
            var key = "CategoryRepository";
            if (!_Repos.ContainsKey(key))
            {
                var Repository = new CategoryRepository(_dbContext);
                _Repos[key] = Repository;
            }
            return _Repos[key] as ICategoryRepository;
        }

        public IFilterationRepository FilterationRepository()
        {
            var key = "FilterationRepository";
            if (!_Repos.ContainsKey(key))
            {
                var repo = new FilterationRepository(_dbContext);
                _Repos[key] = repo;
            }
            return _Repos[key] as IFilterationRepository;
        }

        public IOwnerRepository OwnerRepository()
        {
            var key = "OwnerRepository";
            if (_Repos.ContainsKey(key))
            {
                var Repo = new OwnerRepository(_userManager, _roleManager);
                _Repos[key] = Repo;
            }
            return _Repos[key] as IOwnerRepository;
        }
        public IWishlistRepository WishlistRepository()
        {
            var key = "WishlistRepository";
            if (!_Repos.ContainsKey(key))
            {
                var repo = new WishlistRepository(_dbContext, _userManager);
                _Repos[key] = repo;
            }
            return _Repos[key] as IWishlistRepository;
        }
        public IReviewRepository ReviewRepository()
        {
            var key = "ReviewRepository";
            if (!_Repos.ContainsKey(key))
            {
                var repo=new ReviewRepository(_dbContext);
                _Repos[key] = repo;
            }
            return _Repos[key] as IReviewRepository;
        }

        public IOrderRepository OrderRepository()
        {
            var key = "OrderRepository";
            if (!_Repos.ContainsKey(key))
            {
                var repo=new OrderRepository(_dbContext);
                _Repos[key] = repo;
            }
            return _Repos[key] as IOrderRepository;
        }
    }
}
