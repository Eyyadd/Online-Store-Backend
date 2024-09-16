namespace OnlineStore.Domain.Entities
{
    public class ProductWishlist:BaseEntity
    {
        public ProductVariants? product { get; set; }

        [ForeignKey(nameof(ProductVariants))]
        public int ProductId {  get; set; }

        [ForeignKey(nameof(Wishlist))]
        public int wishlistId { get; set; }
        public Wishlist? Wishlist { get; set; }
    }
}
