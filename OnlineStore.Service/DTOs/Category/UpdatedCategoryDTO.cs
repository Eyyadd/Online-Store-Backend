using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.DTOs.Category
{
    public class UpdatedCategoryDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }= DateTime.Now;
        public CategoryType CategoryType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
