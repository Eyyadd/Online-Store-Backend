using Microsoft.EntityFrameworkCore;
using OnlineStore.Application;
using OnlineStore.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class CartRepository:Repository<Cart> , ICartRepository
    {
        private readonly DbSet<Cart> Carts;
        private readonly OnlineStoreDbContext _onlineStoreDbContext;

        public CartRepository(OnlineStoreDbContext onlineStoreDbContext):base(onlineStoreDbContext)
        {
            Carts = onlineStoreDbContext.Carts;
            _onlineStoreDbContext = onlineStoreDbContext;
        }

        public Cart Cart(string UserId)
        {
            return Carts.Where(c => c.UserId == UserId).FirstOrDefault();
        }

        public IQueryable<CartItems> CartItems(string UserId)
        {
            return Carts.Where(c => c.UserId == UserId)
                       .SelectMany(c => c.Items)
                       .Include(i => i.ProductVariants)
                       .ThenInclude(pv => pv.Product);
        }

        public async Task<int> DeleteAllCartItems( int CartId)
        {
            var SqlQuery = $"DELETE FROM [dbo].[CartItems] \r\n  WHERE CartId ={CartId}";
            var RowEffected = await _onlineStoreDbContext.Database.ExecuteSqlRawAsync(SqlQuery, CartId);
            return RowEffected;
        }


       public CartItems GetCartItem(int ProductId , string UserId)
        {
            return Carts.Where(c => c.UserId == UserId)
                        .SelectMany(c => c.Items)
                        .Where(c => c.ProductID == ProductId)
                        .FirstOrDefault();
        }


    }
}
