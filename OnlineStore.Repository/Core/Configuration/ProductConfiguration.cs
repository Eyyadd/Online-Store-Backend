namespace OnlineStore.Application.Core.Configuration
{
    public class ProductConfiguration:IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.Property(p => p.Price).HasPrecision(18, 2);
        }
    }
}
