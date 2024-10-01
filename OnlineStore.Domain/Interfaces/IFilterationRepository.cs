using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IFilterationRepository
    {
        IEnumerable<ProductVariant> FilterByPrice(decimal minPrice, decimal maxPrice);
        IEnumerable<ProductVariant> FilterBySale();
        IEnumerable<ProductVariant> FilterByMinPrice(decimal minPrice);
        IEnumerable<ProductVariant> FilterByMaxPrice(decimal maxPrice);
    }
}
