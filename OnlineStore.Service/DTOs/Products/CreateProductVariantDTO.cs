using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Products
{
    public class CreateProductVariantDTO
    {
        public int ProductId { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } = null!;
    }
}
