using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs
{
    public class CategoriesDTO
    {
        [MaxLength(40)]
        [MinLength(2)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        [Range(0, 2)]
        public CategoryType CategoryType { get; set; }
    }
}
