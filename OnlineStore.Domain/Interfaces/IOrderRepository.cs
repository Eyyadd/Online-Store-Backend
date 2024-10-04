﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Interfaces
{
    public interface IOrderRepository:IRepository<Order>
    {
       
        IEnumerable<Order> GetOrdersbyUserId(string UserId);
        bool GetOrderWithPaymetntIntent(string PaymetntIntent);
    }
}
