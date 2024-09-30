using OnlineStore.Application;
using OnlineStore.Application.Repository;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastrucutre.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OnlineStoreDbContext onlineStoreDbContext) : base(onlineStoreDbContext) { }

        public IEnumerable<Order> GetOrdersbyUserId(string UserId)
        {
            var orders = _context.Order.Include(o => o.Items)
                .ThenInclude(Oi => Oi.Product)
                .ThenInclude(p => p.Product)
                .Where(u => u.UserId == UserId);
            return orders;
        }


    }
}
