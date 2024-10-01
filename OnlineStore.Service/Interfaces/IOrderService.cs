using OnlineStore.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<GetOrderItems> GetOrdersByUserID(string UserId);
        Order CreateOrder(int CartId);
    }
}
