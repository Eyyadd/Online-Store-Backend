namespace OnlineStore.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<T> Repository<T>() where T :BaseEntity;
        void Commit();
    }
}
