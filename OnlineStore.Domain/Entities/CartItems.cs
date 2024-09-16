namespace OnlineStore.Domain.Entities
{
    public class CartItems:BaseEntity
    {
        public int Qunatity { get; set; }
        public ProductVariants? ProductVariants { get; set; }


        [ForeignKey(nameof(ProductVariants))]
        public int ProductID { get; set; }

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
