namespace OnlineStore.Domain.Entities
{
    public class ProductVariants:BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public int ProductId {  get; set; }
        public Product? Product { get; set; }    
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }

        public CartItems? CartItems { get; set; }

        [ForeignKey(nameof(CartItems))]
        public int? CartItemsId { get; set; }

        public ICollection<Wishlist>? Wishlist { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public string Image { get; set; } = null!;

        public int TotalQuantity { get; set; }

        public decimal PrecentageOfSales { get; set; }

    }
}
