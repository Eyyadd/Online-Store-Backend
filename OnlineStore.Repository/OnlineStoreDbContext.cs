using System.Reflection;

namespace OnlineStore.Application
{
    public class OnlineStoreDbContext:IdentityDbContext
    {
        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new DiscountConfiguration().Configure(builder.Entity<Discount>());
            new ProductConfiguration().Configure(builder.Entity<Product>());
            new CategoryConfiguration().Configure(builder.Entity<Category>());

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Ads> Ads { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<ProductVariants> ProductVariants { get; set; }
        public DbSet<ProductWishlist> ProductWishlist { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> SubCategories { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<BestSeller> BestSellers { get; set; }
    }
}
