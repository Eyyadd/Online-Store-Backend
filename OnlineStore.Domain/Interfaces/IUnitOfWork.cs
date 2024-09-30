namespace OnlineStore.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        IProductRepository ProductRepository();
        ICartRepository CartRepository();
        ICategoryRepository CategoryRepository();
        IFilterationRepository FilterationRepository();
        IOwnerRepository OwnerRepository();
        IWishlistRepository WishlistRepository();
        IReviewRepository ReviewRepository();
        IOrderRepository OrderRepository();

        int Commit();
    }
}
