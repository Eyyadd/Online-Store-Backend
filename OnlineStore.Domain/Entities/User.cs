namespace OnlineStore.Domain.Entities
{
    public class User:IdentityUser
    {
        [DisplayName("First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
        public Wishlist? Wishlist { get; set; }

        [ForeignKey(nameof(Wishlist))]
        public int? WishlistId {  get; set; }

        public Cart? Cart { get; set; }
        [ForeignKey(nameof(Cart))]
        public int? CartId { get; set; }

        public ICollection<Ads>? Ads { get; set; }    // Admin Can Add many Ads

        public ICollection<Review>? Reviews { get; set; }
    }
}
