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
