using OnlineStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IFilterService
    {
        IEnumerable<ProductDTO> FilterByPrice(decimal minPrice, decimal maxPrice);
        IEnumerable<ProductDTO> FilterBySale();
        IEnumerable<ProductDTO> FilterByMinPrice(decimal minPrice);
        IEnumerable<ProductDTO> FilterByMaxPrice(decimal maxPrice);

    }
}