namespace OnlineStore.Domain.Entities
{
    public class Wishlist:BaseEntity
    {
        public ICollection<ProductVariants>? ProductVariants { get; set; }
        public User? User { get; set; }


        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
    }
}
