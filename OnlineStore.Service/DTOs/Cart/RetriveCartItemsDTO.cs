using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Cart
{
    public class RetriveCartItemsDTO
    {
        public int CartItemId { get; set; }
        public int ProductVaiantID { get; set; }
        public string ProductName { get; set; } = null!;
        public Size Size { get; set; }
        public Color Color { get; set; }
        public int CartProductQuantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; } = null!;
    }
}
