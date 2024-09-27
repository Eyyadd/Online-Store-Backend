using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Entities
{
        public class BestSeller:BaseEntity
        {
         
            public string ImageCover { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public decimal PrecentageOfSales { get; set; }
            public int Quantity { get; set; }

        }
    
}
