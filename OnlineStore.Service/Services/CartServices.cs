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

        public List<RetriveCartItemsDTO> AddToCart( CreateCartItemDTO cartItemsDTO , string userId)
        {
            var Item = _mapper.Map<CartItems>(cartItemsDTO);
            var IsExisit = _unitOfWork.Repository<CartItems>().GetByIdWithSpec(cartItemsDTO.ProductVariantId ,new BaseSpecification<CartItems>(c => c.ProductID == cartItemsDTO.ProductVariantId))
                is not null ? true : false;
            var ProuductVariant = _unitOfWork.Repository<ProductVariants>().GetById(cartItemsDTO.ProductVariantId);
            if(!IsExisit && ProuductVariant is not null)
            {
                _unitOfWork.Repository<CartItems>().Add(Item);
                _unitOfWork.Commit();
            }
            return Cart(userId);
        }

        public List<RetriveCartItemsDTO> Cart(string userId)
        {
            List<RetriveCartItemsDTO> CartItems = new List<RetriveCartItemsDTO>();
            var Cart = _unitOfWork.Repository<Cart>().GetByIdWithSpec(userId, new CartSpecifications(c =>c.UserId ==  userId));
            var Result = _unitOfWork.Repository<CartItems>().ThenInclude<ProductVariants , Product>(p=>p.Product , c=>c.ProductVariants);
            CartItems = _mapper.Map(Result, CartItems);
            return CartItems;
        }

        public Cart CreateCart(User user)
        {
            Cart NewCart = new Cart
            {
                UserId = user.Id
            };
            _unitOfWork.Repository<Cart>().Add(NewCart);
            _unitOfWork.Commit();
            return _unitOfWork.Repository<Cart>().GetByIdWithSpec(user.Id, new CartSpecifications(c => c.UserId == user.Id));
        }

        public async Task<int> RemoveCartItems(string userId)
        {
            var Cart = _unitOfWork.Repository<Cart>().GetByIdWithSpec(userId, new BaseSpecification<Cart>(c => c.UserId == userId));
            var sql = $"DELETE FROM [dbo].[CartItems] \r\n  WHERE CartId ={Cart.Id}";
            var NoOfEffectedRow =await  _unitOfWork.Repository<Cart>().Delete(sql , Cart.Id);
            return NoOfEffectedRow;
        }

        public List<RetriveCartItemsDTO> RemoveItemFromCart(int CartItemId , string userId)
        {
            var cartItemRepo = _unitOfWork.Repository<CartItems>();
            var CartItem = cartItemRepo.GetById(CartItemId);
            if(CartItem is not null)
            {
                cartItemRepo.Delete(CartItemId);
                _unitOfWork.Commit();
            }
            return Cart(userId);
        }

        public List<RetriveCartItemsDTO> UpdateCartProudctQuantity(UpdateCartItemDTO UpdatecartItemsDTO, string userId)
        {
            var cartItem = _unitOfWork.Repository<CartItems>().GetById(UpdatecartItemsDTO.cartItemID);
            if(cartItem is not null)
            {
                cartItem = _mapper.Map(UpdatecartItemsDTO, cartItem);
                _unitOfWork.Repository<CartItems>().Update(cartItem);
                _unitOfWork.Commit();
            }
            return Cart(userId);
        }
    }
}
