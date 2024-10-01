using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface ICartRepository:IRepository<Cart>
    {
        IQueryable<CartItems> CartItems(string UserId);
        Cart Cart(string UserId);

        public Task<int> DeleteAllCartItems(int CartId);

        public CartItems GetCartItem(int ProductId, string UserId);
        public Cart GetCartByIdWithInclude(int cartid);
    }
}
