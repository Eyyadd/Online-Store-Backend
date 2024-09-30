using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Cart
{
    public class CreateCartItemDTO
    {
        public int ProductVariantId { get; set; }
        public int CartQuantity { get; set; }
    }
}
