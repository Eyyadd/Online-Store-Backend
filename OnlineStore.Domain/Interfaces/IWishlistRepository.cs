using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IWishlistRepository:IRepository<Wishlist>
    {
        Wishlist Create(string userId);
        Task RemoveFromWishlist(int productVariantId, string userId);
        bool AddToWishlist(int productVariantId, string userId);
        Wishlist GetWishlistByUserID(string userid);
        ProductWishlist GetProductWishlist(int productVariantId, int WishlistID);
        public IEnumerable<Wishlist> GetAllProductsfromWishlist(int wishlitID);
    }
}
