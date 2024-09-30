using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Category GetByNameAndType(string Name,CategoryType categoryType);
        Category GetByIdWithNoTracking(int id);
    }
}
