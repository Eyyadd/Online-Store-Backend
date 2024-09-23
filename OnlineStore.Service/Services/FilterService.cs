using OnlineStore.Application.DTOs;
using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class FilterService : IFilterService
    {
        private readonly IRepository<Product> _productRepository;

        public FilterService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public  List<ProductDTO> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            var products = _productRepository
                .GetAll()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

            return products;
        }

        public List<ProductDTO> FilterBySale()
        {
            var products =  _productRepository
                .GetAll()
                .Where(p => p.Discounted)
                .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

            return products;

        }
    }
}