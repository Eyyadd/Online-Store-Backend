using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Application.DTOs;

namespace OnlineStore.Domain.ServicesInterfaces
{
    public interface IWishlistService
    {
        bool AddToWishlist(int productVariantId, string userId);
        Task RemoveFromWishlist(int productVariantId, string userId);
        List<ProductVariantWishlistDTO> GetWishlistProducts(string userId);
        void Create(string userId);
    }
}
