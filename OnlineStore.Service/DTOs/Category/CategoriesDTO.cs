using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Category
{
    public class CategoriesDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}
