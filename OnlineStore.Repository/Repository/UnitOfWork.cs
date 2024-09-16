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
    }
}
