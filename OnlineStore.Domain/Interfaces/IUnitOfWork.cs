namespace OnlineStore.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<T> Repository<T>() where T :BaseEntity;
        IProductRepository ProductRepository();
        ICartRepository CartRepository();

        void Commit();
    }
}
