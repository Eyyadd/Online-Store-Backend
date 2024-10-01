using OnlineStore.Application.DTOs.Order;
using OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = _unitOfWork.OrderRepository();
        }

        public Order CreateOrder(int CartId)
        {
            var cart=_unitOfWork.CartRepository().GetCartByIdWithInclude(CartId);

            var orderItems=new List<OrderItems>();
            if (cart.Items.Any())
            {
                foreach (var item in cart.Items)
                {
                    var orderItem = new OrderItems()
                    {
                        Quantity = item.Qunatity,
                        ProductId = item.ProductID,
                    };
                    orderItems.Add(orderItem);
                }
            }
            var totalPrice = orderItems.Sum(x => x.Product.Product.Price);
            var order = new Order()
            {
                Items = orderItems,
                Address = "Maddi",
                IsPaid = false,
                UserId = cart.UserId
            };
            return order;
        }

        public IEnumerable<GetOrderItems> GetOrdersByUserID(string UserId)
        {
            var orders = _orderRepository.GetOrdersbyUserId(UserId);
            var ListOfmappingorders = new List<GetOrderItems>();
            var mappedorders = new GetOrderItems();
            foreach (var item in orders)
            {
                foreach (var i in item.Items)
                {
                    mappedorders = new GetOrderItems
                    {
                        Address = item.Address,
                        Discounted = i.Product.Product.Discounted,
                        Price = i.Product.Product.Price,
                        ProductName = i.Product.Product.Name,
                        Quantity = i.Quantity,
                    };
                }
                ListOfmappingorders.Add(mappedorders);
            }
            return ListOfmappingorders;
        }
    }
}
