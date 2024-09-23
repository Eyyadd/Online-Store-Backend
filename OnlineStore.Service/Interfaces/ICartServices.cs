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

        public List<RetriveCartItemsDTO> Cart(string userId);

        public List<RetriveCartItemsDTO> AddToCart( CreateCartItemDTO cartItemsDTO , string userId);

        public List<RetriveCartItemsDTO> UpdateCartProudctQuantity(UpdateCartItemDTO UpdatecartItemsDTO, string userId);

        public List<RetriveCartItemsDTO> RemoveItemFromCart(int CartItemId , string userId);

        public Task<int> RemoveCartItems(string  userId);

    }
}
