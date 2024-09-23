using Microsoft.Extensions.Logging;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Service.DTOs;
using System;
using System.Linq;
using System.Security.Claims;
using OnlineStore.Application.DTOs;

namespace OnlineStore.Infrastructure.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork;
        IRepository<Wishlist> wishlistRepository;
        IRepository<ProductVariants> productVariantRepository;
        IRepository<ProductWishlist> productWishlistRepository;
        IRepository<Product> productRepository;

        public WishlistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            wishlistRepository = _unitOfWork.Repository<Wishlist>();
            productVariantRepository = _unitOfWork.Repository<ProductVariants>();
            productWishlistRepository = _unitOfWork.Repository<ProductWishlist>();
            productRepository = _unitOfWork.Repository<Product>();
        }

        public bool AddToWishlist(int productVariantId, string userId)
        {
            var productVariant = productVariantRepository.GetById(productVariantId);
            if (productVariant == null)
            {
                throw new Exception("Product variant not found.");
            }

            var wishlist = wishlistRepository.GetAll().FirstOrDefault(w => w.UserId == userId);
            if (wishlist == null)
            {
                wishlist = new Wishlist
                {
                    UserId = userId,
                    ProductVariants = new List<ProductVariants>()
                };
                wishlistRepository.Add(wishlist);
            }

            if (wishlist.ProductVariants != null && wishlist.ProductVariants.Any(pv => pv.Id == productVariantId))
            {
                return false;
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

            return true;
        }
        public async Task RemoveFromWishlist(int productVariantId, string userId)
        {
            var wishlist = wishlistRepository.GetAll().FirstOrDefault(w => w.UserId == userId);
            var wishlistItem = productWishlistRepository.GetAll()
            .FirstOrDefault(pw => pw.Id == productVariantId && pw.wishlistId == wishlist.Id);


            if (wishlistItem == null)
            {
                throw new Exception("Item not found in wishlist.");
            }

            productWishlistRepository.Delete(wishlistItem.Id);
            _unitOfWork.Commit();
        }


        public List<ProductVariantWishlistDTO> GetWishlistProducts(string userId)
        {
            var wishlist = wishlistRepository
                     .GetAll()
                     .FirstOrDefault(w => w.UserId == userId);

            if (wishlist == null)
            {
                return new List<ProductVariantWishlistDTO>();
            }

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
        )
                .ToList();

            return wishlistProducts;
        }

        public void Create(string userId)
        {
            var newWishlist = new Wishlist
            {
                UserId = userId
            };

            wishlistRepository.Add(newWishlist);
            _unitOfWork.Commit();
        }
    }
}
