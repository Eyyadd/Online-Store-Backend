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
        List<ProductDTO> FilterByPrice(decimal minPrice, decimal maxPrice);
        List<ProductDTO> FilterBySale();

    }
}