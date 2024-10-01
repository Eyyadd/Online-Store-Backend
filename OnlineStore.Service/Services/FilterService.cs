using AutoMapper;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFilterationRepository _productRepository;
        private readonly IMapper _Mapper;


        public FilterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.FilterationRepository();
            _Mapper = mapper;
        }

        public IEnumerable<ProductDTO> FilterByMaxPrice(decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDTO> FilterByMinPrice(decimal minPrice)
        {
            if (minPrice < 0)
            {
                return Enumerable.Empty<ProductDTO>();
            }

            var filteredProducts = _productRepository.FilterByMinPrice(minPrice);

            if (filteredProducts.Any())
            {
                var filteredProductsMapping = _Mapper.Map<IEnumerable<ProductVariant>, IEnumerable<ProductDTO>>(filteredProducts);
                return filteredProductsMapping;
            }

            return Enumerable.Empty<ProductDTO>();
        }

        public IEnumerable<ProductDTO> FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
            {
                return Enumerable.Empty<ProductDTO>();
            }
            var FilteredProducts = _productRepository.FilterByPrice(minPrice, maxPrice);
            if (FilteredProducts.Any())
            {
                var FilteredProductsMapping = _Mapper.Map<IEnumerable<ProductVariant>, IEnumerable<ProductDTO>>(FilteredProducts);
                return FilteredProductsMapping;
            }
            return Enumerable.Empty<ProductDTO>();


        }

        public IEnumerable<ProductDTO> FilterBySale()
        {
            var FilteredProducts = _productRepository.FilterBySale();
            if (FilteredProducts.Any())
            {
                var FilteredProductsMapping = _Mapper.Map<IEnumerable<ProductVariant>, IEnumerable<ProductDTO>>(FilteredProducts);
                return FilteredProductsMapping;
            }
            return Enumerable.Empty<ProductDTO>();

        }

       
       
    }
}