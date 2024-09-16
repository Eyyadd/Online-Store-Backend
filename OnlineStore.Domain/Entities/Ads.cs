namespace OnlineStore.Domain.Entities
{
    public class Ads:BaseEntity
    {
        [MaxLength(30)]
        public string BrandName { get; set; } = null!;
        public string Phone {  get; set; } = null!;
        public string Poster {  get; set; } = null!;
        public User? User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }=null!;

    }
}
