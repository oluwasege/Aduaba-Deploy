using Aduaba.DTO.Order;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<Order> GetOrderItems(List<string> orderItemId);

        Task<List<Order>> OrderItems();

        Task<Order> OrderItems(CreateOrderRequest order);

        Task<OrderStatus> TrackOrder(string trackingId);

        Task ChangeOrderStatus(OrderStatusRequest model);

        //Task<decimal> CalculateTotalBilling(List<string> cartItems);

        Task ChangePaymentStatus(string orderId);

        void PayOnDelivery();

        public Task ProcessPayment();



    }
}
