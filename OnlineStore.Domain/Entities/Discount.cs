namespace OnlineStore.Domain.Entities
{
    public class Discount:BaseEntity
    {
        [MaxLength(30)]
        public string DiscountCode { get; set; } = null!;   //BlackFirday Summer Sale
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDiscount {  get; set; }
        public DateTime EndDiscount { get; set; }

    }
}
