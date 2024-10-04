using AutoMapper;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Application.Helper;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Mappign;
using OnlineStore.Domain.Specifications;
using OnlineStore.Domain.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PaginationDTO<ProductElementDTO>> AllProducts(int PageSize , int PageIndex)
        {
            var Products = _unitOfWork.ProductRepository().All();
            var PaginationResult = await Products.Paginate(PageSize, PageIndex);
            var MapperPages = _mapper.Map<IEnumerable<ProductElementDTO>>(PaginationResult.Items);

            return new PaginationDTO<ProductElementDTO>
            {
                Index = PaginationResult.Index,
                Size = PaginationResult.Size,
                Recordes = PaginationResult.Recordes,
                NoOfPages = PaginationResult.NoOfPages,
                Items = MapperPages
            };
        }

        public IEnumerable<ProductVariantDTO> AllProductsVariants()
        {
            var ProudctVariants = _unitOfWork.Repository<ProductVariants>().GetAll();
            var Result = _mapper.Map<IEnumerable<ProductVariantDTO>>(ProudctVariants);
            return Result;
        }

        public IEnumerable<ProductElementDTO> BestSellerProducts(int size)
        {
            var Products = _unitOfWork.ProductRepository().BestSeller(size);
            return _mapper.Map<IEnumerable<ProductElementDTO>>(Products);
        }

        public ProductElementDTO CreateProduct(CreateProductDTO createProductDTO )
        {
            var Proudct = _mapper.Map<ProductVariant>(createProductDTO);
            _unitOfWork.Repository<ProductVariant>().Add(Proudct);
            _unitOfWork.Commit();
            return _mapper.Map<ProductElementDTO>(Proudct);
        }

        public IEnumerable<ProductElementDTO> NewArraivelProducts(int size)
        {
           var Products = _unitOfWork.ProductRepository().NewArrival(size).ToList();
            return _mapper.Map<IEnumerable<ProductElementDTO>>(Products);
            
        }

        public ProductDetailsDTO ProductDetails(int id)
        {
            var product = _unitOfWork.ProductRepository().ProductDetails(id);
            var ProductInfo = new
            {
                product.Id,
                product.Name,
                product.Price, 
                product.Seller,
                CategoryName = product.SubCategory.Name,
                product.ImageCover
            };

            var variants = product.ProductVariants.Select(v => new ProductVariantDTO
            {
                Id = v.Id,
                Color = v.Color,
                Image = v.Image,
                Quantity = v.Quantity,
                Size = v.Size
            })
            .OrderBy(v => v.Color);

            ProductDetailsDTO productDetailsDTO = new ProductDetailsDTO
            {
                ProductID = ProductInfo.Id,
                price = ProductInfo.Price,
                CategoryName = ProductInfo.CategoryName,
                Name = ProductInfo.Name,
                seller = ProductInfo.Seller,
                CoverImage =ProductInfo.ImageCover,
                Variants = variants
            };
            return productDetailsDTO;
        }

        public IEnumerable<ProductElementDTO> ProductsByCategoryId(int CategoryID)
        {
            var Prodcuts = _unitOfWork.ProductRepository().GetByCategoryID(CategoryID);
            return _mapper.Map<IEnumerable<ProductElementDTO>>(Prodcuts);
        }

        public IEnumerable<string> ProuctsSaller()
        {
            return _unitOfWork.Repository<ProductVariant>().GetAll().Select(p => p.Seller);
        }

        public IEnumerable<ProductElementDTO> SaleProducts()
        {
            var Products = _unitOfWork.ProductRepository().ProductHaveSale();
            return _mapper.Map<IEnumerable<ProductElementDTO>> (Products);

        }


        public IEnumerable<ProductElementDTO> GetByCategoryType(string categoryType)
        {
            var Result = _unitOfWork.ProductRepository().ProductByCategoryType(categoryType);
            if (Result is not null)
            {
                return _mapper.Map<IEnumerable<ProductElementDTO>>(Result);
            }
            return new List<ProductElementDTO>();
        }

        public CreateProductVariantDTO CreateProductVariant(CreateProductVariantDTO productVariantDTO)
        {
            var ProductVariant = _mapper.Map<ProductVariants>(productVariantDTO);
            if(ProductVariant is not null)
            {
                return _mapper.Map<CreateProductVariantDTO>(ProductVariant);
            }
            return new CreateProductVariantDTO();
        }
    }
}
