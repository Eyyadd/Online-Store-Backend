using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string Address {  get; set; }
        public List<GetOrderItems>? Items { get; set; }=new List<GetOrderItems>();

        public string UserId { get; set; } = null!;
        public string? PaymentIntentId { get; set; }
        public int CartId {  get; set; }
    }
}
