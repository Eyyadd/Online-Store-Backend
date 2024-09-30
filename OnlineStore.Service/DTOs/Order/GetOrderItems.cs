using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Order
{
    public class GetOrderItems
    {
        public string ProductName {  get; set; }
        public decimal Price { get; set; }
        public bool Discounted { get; set; }
        public string Address { get; set; }
        public int Quantity {  get; set; }
    }
}
