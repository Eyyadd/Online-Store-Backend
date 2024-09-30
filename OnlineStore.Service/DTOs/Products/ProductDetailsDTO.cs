using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Products
{
    public class ProductDetailsDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = null!;
        public string seller { get; set; } = null!;
        public decimal price { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CoverImage { get; set; } = null!;

        public IEnumerable<ProductVariantDTO> Variants { get; set; }
    }
}
