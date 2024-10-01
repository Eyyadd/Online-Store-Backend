namespace OnlineStore.Domain.Entities
{
    public class ProductVariant:BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        [MaxLength(30)]
        public string Seller { get; set; } = null!;
        public bool Discounted { get; set; } = false;
        public decimal Price { get; set; }

        public Category? SubCategory { get; set; }

        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }

        public Discount? Discount { get; set; }

        [ForeignKey(nameof(Discount))]
        public int? DiscountId { get; set; }

        public ICollection<ProductVariants>? ProductVariants { get; set; }

        public string? ImageCover { get; set; }


    }
}
