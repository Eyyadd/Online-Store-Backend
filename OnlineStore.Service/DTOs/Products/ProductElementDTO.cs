using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Products
{
    public class ProductElementDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string seller { get; set; } = null!;
        public decimal price { get; set; }
        public string CategoryName { get; set; } = null!;

        public string ImageCover { get; set; } = null!;
        public bool InStocke { get; set; }
    }
}
