using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Cart
{
    public class UpdateCartItemDTO
    {
        public int cartItemID { get; set; }
        public int Quanity { get; set; }
    }
}
