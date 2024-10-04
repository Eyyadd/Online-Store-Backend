namespace OnlineStore.Domain.Entities
{
    public class Order:BaseEntity
    {
        [MaxLength(100)]
        public string Address { get; set; } = null!;
        public bool IsPaid {  get; set; }
        public OrderStatus Status { get; set; }
        public User? User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public ICollection<OrderItems>? Items { get; set; }
    }
}
