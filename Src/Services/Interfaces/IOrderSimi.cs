using Aduaba.DTO.Order;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IOrderSimi
    {
        public Task<OrderSimi> CreateOrder(CreateOrderSimi model);
        public Task<string> CancelOrder(string orderId);
        public Task<List<OrderSimi>> ViewOrder();

    }
}
