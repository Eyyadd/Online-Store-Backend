using OnlineStore.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class FilterationRepository : IFilterationRepository
    {
        private readonly OnlineStoreDbContext _dbContext;

        public FilterationRepository(OnlineStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductVariant> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
           return _dbContext.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }

        public IEnumerable<ProductVariant> FilterBySale()
        {
            return _dbContext.Products.Where(p => p.Discounted).ToList();
        }
    }
}
