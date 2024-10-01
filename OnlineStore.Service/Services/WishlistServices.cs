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
        IRepository<ProductVariant> productRepository;
        private IMapper _mapper;

        public WishlistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            wishlistRepository = _unitOfWork.WishlistRepository();
            productVariantRepository = _unitOfWork.Repository<ProductVariants>();
            productWishlistRepository = _unitOfWork.Repository<ProductWishlist>();
            productRepository = _unitOfWork.Repository<ProductVariant>();
            _mapper = mapper;
        }

        public bool AddToWishlist(int productVariantId, string userId)
        {
            bool Added=false;
            var productVariant = productVariantRepository.GetById(productVariantId);
            if (productVariant is null)
            {
                return Added;
            }

            var wishlist = wishlistRepository.GetWishlistByUserID(userId);
            if (wishlist.ProductVariants is null && wishlist.ProductVariants.Any(pv => pv.Id == productVariantId))
            {
                return Added;
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
            Added=true;
            return Added;
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

        public IEnumerable<ProductElementDTO> GetWishlistProducts(string userId)
        {
            var wishlist = wishlistRepository.GetWishlistByUserID(userId);


            if (wishlist is not null)
            {

                var wishlistProducts = wishlistRepository.GetAllProductsfromWishlist(wishlist.Id);
                if (wishlistProducts is not null)
                {
                    
                    var ListOfWishlistItems = new List<ProductElementDTO>();
                    var WishlistItems = new ProductElementDTO();
                    foreach (var product in wishlistProducts)
                    {
                        foreach (var p in product.ProductVariants)
                        {
                            WishlistItems.price = p.Product.Price;
                            WishlistItems.seller = p.Product.Seller;
                            WishlistItems.Name = p.Product.Name;
                            WishlistItems.ImageCover = p.Product.ImageCover;
                            WishlistItems.CategoryName = p.Product.SubCategory.Name;
                        }
                        ListOfWishlistItems.Add(WishlistItems);
                    }
                    return ListOfWishlistItems;
                }
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
