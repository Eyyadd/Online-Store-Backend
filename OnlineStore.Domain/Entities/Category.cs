using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Category:BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CategoryType CategoryType { get; set; }

        public ICollection<ProductVariant>? Products { get; set; }

    }
}
