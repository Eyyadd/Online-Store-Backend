using Microsoft.AspNetCore.Identity;
using OnlineStore.Application;
using OnlineStore.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        private readonly UserManager<User> _userManager;
        public WishlistRepository(OnlineStoreDbContext onlineStoreDbContext,UserManager<User> userManager) : base(onlineStoreDbContext) 
        {
            _userManager = userManager;
        }
        public bool AddToWishlist(int productVariantId, string userId)
        {
            throw new NotImplementedException();
        }

        public Wishlist Create(string userId)
        {
            var isUserExist=_userManager.FindByIdAsync(userId).Result;
            bool Created = false;
            var Wishlist = new Wishlist();
            if (isUserExist is not null)
            {
                
                Add(new Wishlist
                {
                    UserId = userId
                });
                Wishlist = GetWishlistByUserID(userId);
                Created= true;
            }
            return Created ? Wishlist:null ;
        }

        public Task RemoveFromWishlist(int productVariantId, string userId)
        {
            throw new NotImplementedException();
        }



        public Wishlist GetWishlistByUserID(string userId) {
            var isUserExist = _userManager.FindByIdAsync(userId).Result;
            Wishlist resultWishlist = new Wishlist();
            if (isUserExist is not null)
            {
                var wishlist=_context.Wishlist.FirstOrDefault(wishlist=>wishlist.UserId == userId);
                if (wishlist is not null)
                {
                    resultWishlist = wishlist;
                }
            }
            return resultWishlist;
        }

        public ProductWishlist GetProductWishlist(int productVariantId, int WishlistID)
        {
            return _context.ProductWishlist.FirstOrDefault(pw => pw.ProductId == productVariantId && pw.wishlistId == WishlistID);
        }
    }
}
