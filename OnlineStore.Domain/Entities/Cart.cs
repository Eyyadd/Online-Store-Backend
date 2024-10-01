namespace OnlineStore.Domain.Entities
{
    public class Cart:BaseEntity
    {
        public ICollection<CartItems>? Items { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User? User { get; set; }
        public string? PaymentIntentId {  get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
