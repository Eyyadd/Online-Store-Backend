using AutoMapper;
using OnlineStore.Application.DTOs.Cart;
using OnlineStore.Application.Interfaces;
using OnlineStore.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class CartServices : ICartServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<RetriveCartItemsDTO> AddToCart( CreateCartItemDTO cartItemsDTO , string userId)
        {
            var Item = _mapper.Map<CartItems>(cartItemsDTO);
            var IsExisit = _unitOfWork.CartRepository().GetCartItem(cartItemsDTO.ProductVariantId, userId)
                is not null ? true : false;
            
            if(!IsExisit)
            {
                var ProuductVariant = _unitOfWork.Repository<ProductVariants>().GetById(cartItemsDTO.ProductVariantId);
                if (ProuductVariant is not null)
                {
                    _unitOfWork.Repository<CartItems>().Add(Item);
                    _unitOfWork.Commit();
                }
            }
            return Cart(userId);
        }

        public IEnumerable<RetriveCartItemsDTO> Cart(string userId)
        {
            var CartItems = _unitOfWork.CartRepository().CartItems(userId);
            return _mapper.Map<IEnumerable<RetriveCartItemsDTO>>(CartItems);
        }

        public Cart CreateCart(User user)
        {
            Cart NewCart = new Cart
            {
                UserId = user.Id
            };
            _unitOfWork.Repository<Cart>().Add(NewCart);
            _unitOfWork.Commit();
            return _unitOfWork.CartRepository().Cart(user.Id);
        }

        public async Task<int> RemoveCartItems(string userId)
        {
            var Cart = _unitOfWork.CartRepository().Cart(userId);
            var NoOfEffectedRow =await _unitOfWork.CartRepository().DeleteAllCartItems(Cart.Id);
            return NoOfEffectedRow;
        }

        public IEnumerable<RetriveCartItemsDTO> RemoveItemFromCart(int CartItemId , string userId)
        {
            var CartItem = _unitOfWork.Repository<CartItems>().GetById(CartItemId);
            if(CartItem is not null)
            {
                _unitOfWork.Repository<CartItems>().Delete(CartItemId);
                _unitOfWork.Commit();
            }
            return Cart(userId);
        }

        public IEnumerable<RetriveCartItemsDTO> UpdateCartProudctQuantity(UpdateCartItemDTO UpdatecartItemsDTO, string userId)
        {
            var cartItem = _unitOfWork.Repository<CartItems>().GetById(UpdatecartItemsDTO.cartItemID);
            if(cartItem is not null)
            {
                cartItem.Qunatity = UpdatecartItemsDTO.Quanity;
                _unitOfWork.Repository<CartItems>().Update(cartItem);
                _unitOfWork.Commit();
            }
            return Cart(userId);
        }
    }
}
