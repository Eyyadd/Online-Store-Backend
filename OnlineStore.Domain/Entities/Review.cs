namespace OnlineStore.Domain.Entities
{
    public class Review:BaseEntity
    {
        public User? User { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ProductVariants? ProductVariants { get; set; }

        [ForeignKey(nameof(ProductVariants))]
        public int ProductId {  get; set; }

        public string? Comment {  get; set; }


        [Range(minimum:0, maximum: 5)]
        public int Rate { get; set; }
    }
}
