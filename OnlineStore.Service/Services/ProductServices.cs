using AutoMapper;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Mappign;
using OnlineStore.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProductElementDTO> AllProducts()
        {
            var Products = _unitOfWork.Repository<Product>().GetAllWithSpec(new ProductSpecification());
            var ProductsDto = new List<ProductElementDTO>();
            return _mapper.Map(Products , ProductsDto);
        }


        public IEnumerable<ProductVariants> AllProductsVariants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> BestSellerProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> NewArraivelProducts()
        {
            throw new NotImplementedException();
        }

        public ProductDetailsDTO ProductDetails(int id)
        {
            var product = _unitOfWork.Repository<Product>().GetByIdWithSpec(id,new ProductSpecification(p=>p.Id == id));
            var ProductInfo = new
            {
                product.Id, product.Name, product.Price, product.Seller, CategoryName = product.SubCategory.Name
            };
            var variants = _unitOfWork.Repository<ProductVariants>().SelectItems(v => new ProductVariantDTO
            {
                Id = v.Id,
                Color = v.Color,
                Image = v.Image,
                Quantity = v.Quantity,
                Size = v.Size
            }, null, v => v.ProductId == id);

            ProductDetailsDTO productDetailsDTO = new ProductDetailsDTO
            {
                ProductID = ProductInfo.Id,
                price = ProductInfo.Price,
                CategoryName = ProductInfo.CategoryName,
                Name = ProductInfo.Name,
                seller = ProductInfo.Seller,
                Variants = variants
            };
            return productDetailsDTO;
        }

        public IEnumerable<Product> ProductsByCategoryId(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> SaleProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> TopRatedProducts()
        {
            throw new NotImplementedException();
        }
    }
}
