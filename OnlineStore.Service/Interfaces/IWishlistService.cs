using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Products;
using OnlineStore.Application.DTOs.Wishlist;

namespace OnlineStore.Domain.ServicesInterfaces
{
    public interface IWishlistService
    {
        bool AddToWishlist(int productVariantId, string userId);
        int RemoveFromWishlist(int productVariantId, string userId);
        public IEnumerable<ProductElementDTO> GetWishlistProducts(string userId);
        CreatedWishlistDTO Create(string userId);
    }
}
