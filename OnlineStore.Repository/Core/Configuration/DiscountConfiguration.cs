namespace OnlineStore.Application.Core.Configuration
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(p => p.DiscountAmount).HasPrecision(18, 2);
            builder.Property(p => p.StartDiscount).HasDefaultValueSql("GETDATE()");
        }
    }
}
