using Microsoft.Extensions.Logging;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Service.DTOs;
using System;
using System.Linq;
using System.Security.Claims;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Wishlist;
using AutoMapper;

namespace OnlineStore.Infrastructure.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork;
        IWishlistRepository wishlistRepository;
        IRepository<ProductVariants> productVariantRepository;
        IRepository<ProductWishlist> productWishlistRepository;
        IRepository<Product> productRepository;
        private IMapper _mapper;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            wishlistRepository = _unitOfWork.WishlistRepository();
            productVariantRepository = _unitOfWork.Repository<ProductVariants>();
            productWishlistRepository = _unitOfWork.Repository<ProductWishlist>();
            productRepository = _unitOfWork.Repository<Product>();
            _mapper = mapper;
        }

        public ProductWishlist AddToWishlist(int productVariantId, string userId)
        {
            bool AddedDone = false;
            var productVariant = productVariantRepository.GetById(productVariantId);
            if (productVariant is null)
            {
                return null;
            }

            var wishlist = wishlistRepository.GetWishlistByUserID(userId);
            if (wishlist.ProductVariants is not null && wishlist.ProductVariants.Any(pv => pv.Id == productVariantId))
            {
                return null;
            }

            wishlist.ProductVariants?.Add(productVariant);
            wishlistRepository.Update(wishlist);

            var productWishlist = new ProductWishlist
            {
                ProductId = productVariantId,
                Wishlist = wishlist
            };

            productWishlistRepository.Add(productWishlist);

            _unitOfWork.Commit();

            return productWishlist;
        }
        public int RemoveFromWishlist(int productVariantId, string userId)
        {
            var wishlist = wishlistRepository.GetWishlistByUserID(userId);
            
            var wishlistItem = wishlistRepository.GetProductWishlist(productVariantId, wishlist.Id);
            int DeleteResult = -1;
            if (wishlistItem is not null)
            {
                productWishlistRepository.Delete(wishlistItem.Id);
                DeleteResult = _unitOfWork.Commit();
                return DeleteResult;
            }
            return DeleteResult;

        }

        // لسة عاوزة تتهندل 
        public List<ProductVariantWishlistDTO> GetWishlistProducts(string userId)
        {
            var wishlist = wishlistRepository.GetWishlistByUserID(userId);


            if (wishlist is not null)
            {

                var wishlistProducts = productWishlistRepository
                    .GetAll()
                    .Where(pw => pw.wishlistId == wishlist.Id)
                    .Join(
                        productVariantRepository.GetAll(),
                        pw => pw.ProductId,
                        pv => pv.ProductId,
                        (pw, pv) => new { pw, pv }
                       )
                    .Join(
                        productRepository.GetAll(),
                        joined => joined.pv.ProductId,
                        p => p.Id,
                        (joined, p) => new ProductVariantWishlistDTO
                        {
                            ProductVariantId = joined.pv.ProductId,
                            Name = p.Name,
                            Price = p.Price,
                            Image = joined.pv.Image
                        }
                    ).ToList();

                return wishlistProducts;
            }
            return [];

        }

        public CreatedWishlistDTO Create(string userId)
        {
            var CreatedResult = -1;
            var IsCreated = wishlistRepository.Create(userId);
            if (IsCreated is not null)
            {
                CreatedResult = _unitOfWork.Commit();
                if (CreatedResult > 0)
                {
                    var Wishlistmapping = _mapper.Map<Wishlist, CreatedWishlistDTO>(IsCreated);
                    return Wishlistmapping;
                }
            }
            return null;
        }
    }
}
