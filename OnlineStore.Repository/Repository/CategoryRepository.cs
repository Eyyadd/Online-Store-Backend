using OnlineStore.Application;
using OnlineStore.Application.Repository;
using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineStoreDbContext onlineStoreDbContext) : base(onlineStoreDbContext) { }

        public Category GetByIdWithNoTracking(int id)
        {
           var Category= _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            return Category;
        }

        public Category GetByNameAndType(string Name, CategoryType categoryType)
        {
         var category=_context.Categories
                              .Where(cat=>cat.Name==Name&&cat.CategoryType==categoryType)
                              .FirstOrDefault();

            return category;
        }
    }
}
