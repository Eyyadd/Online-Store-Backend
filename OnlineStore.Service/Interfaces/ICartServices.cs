using OnlineStore.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface ICartServices
    {
        public Cart CreateCart(User user);

        public IEnumerable<RetriveCartItemsDTO> Cart(string userId);

        public IEnumerable<RetriveCartItemsDTO> AddToCart( CreateCartItemDTO cartItemsDTO , string userId);

        public IEnumerable<RetriveCartItemsDTO> UpdateCartProudctQuantity(UpdateCartItemDTO UpdatecartItemsDTO, string userId);

        public IEnumerable<RetriveCartItemsDTO> RemoveItemFromCart(int CartItemId , string userId);

        public Task<int> RemoveCartItems(string  userId);

    }
}
