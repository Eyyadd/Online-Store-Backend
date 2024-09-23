using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineStore.Application.DTOs.Products
{
    public class CreateProductDTO
    {
        
        public string Name { get; set; } = null!;
        public string seller { get; set; } = null!;
        public decimal price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile ImageCover { get; set; } = null!;
    }
}
