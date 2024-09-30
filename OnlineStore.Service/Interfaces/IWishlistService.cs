using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Application.DTOs;
using OnlineStore.Application.DTOs.Wishlist;

namespace OnlineStore.Domain.ServicesInterfaces
{
    public interface IWishlistService
    {
        ProductWishlist AddToWishlist(int productVariantId, string userId);
        int RemoveFromWishlist(int productVariantId, string userId);
        List<ProductVariantWishlistDTO> GetWishlistProducts(string userId);
        CreatedWishlistDTO Create(string userId);
    }
}
