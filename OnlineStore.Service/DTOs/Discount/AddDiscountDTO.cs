using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Discount
{
    public class AddDiscountDTO
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public DiscountType DiscountType { get; set; }

        
        [Range(0, 100)]
        public decimal Percentage { get; set; }


        public DateTime StartDiscount { get; set; }


        public DateTime EndDiscount { get; set; }

        public bool IsActive { get; set; }
    }
}
