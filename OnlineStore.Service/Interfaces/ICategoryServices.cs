using OnlineStore.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface ICategoryServices
    {
        IEnumerable<CategoriesDTO> GetCategories();
        CategoriesDTO GetCategory(int id);
        int AddCategory(CategoriesDTO category);
        int RemoveCategory(int id);
        int UpdateCategory(UpdatedCategoryDTO category);
    }
}
