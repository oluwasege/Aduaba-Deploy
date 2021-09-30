using Aduaba.Data;
using Aduaba.DTO.Order;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IShippingAddressService _shippingAddress;
        private readonly ApplicationDbContext _context;
        private readonly IHttpCurrentUser _httpCurrentUser;
        //private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        // private readonly IMapper _mapper;
        public OrderServices(IShippingAddressService shippingAddress, ApplicationDbContext context, IHttpCurrentUser httpCurrentUser)
        {
            this._shippingAddress = shippingAddress;
            this._context = context;
            _httpCurrentUser = httpCurrentUser;


        }


        public async Task<Order> GetOrderItems(List<string> orderItemId)
        {
            var userId = _httpCurrentUser.GetUserId();
            Order order = default;
            List<CartItemSeg> cartItems = new List<CartItemSeg>();
            decimal total = default;
            ApplicationUser user = default;
            foreach (var item in orderItemId)
            {
                var foundItems = await _context.CartItemSegs.FirstOrDefaultAsync(c => c.CartItemId == item);
                var productInFoundItem = await _context.Products.Include(m=>m.Vendor).FirstOrDefaultAsync(c => c.Id == foundItems.ProductId);
                user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
                foundItems.Product = productInFoundItem;

                cartItems.Add(foundItems);

                total += foundItems.TotalPrice;
            }
            order = new Order
            {
                OrderItems = cartItems,
                OrderDate = DateTime.Now,
                OrderId = Guid.NewGuid().ToString(),
                ApplicationUser = user,
                OrderTotal = total,
                OrderReferenceNumber = GetOrderReference,
                OrderStatus = new OrderStatus
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = "Awaiting Payment",
                    PaymentStatus = false
                }
            };
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }


        public Task<List<Order>> OrderItems()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> OrderItems(CreateOrderRequest order)
        {
            var userId = _httpCurrentUser.GetUserId();
            var orderItems = await _context.Orders.Include(c => c.OrderStatus).Include(c => c.ShippingAddress).Include(c => c.ApplicationUser)
                .Include(c => c.OrderItems).FirstOrDefaultAsync(o => o.OrderId == order.OrderItemId);
            var shippingAddress = await _context.ShippingAddresses.FirstOrDefaultAsync(a => a.ApplicationUserId == userId);
            orderItems.OrderType = order.OrderType;
            orderItems.ShippingAddress = shippingAddress;
            orderItems.OrderReferenceNumber = GetOrderReference;
            var id = orderItems.OrderStatus.Id;
            var orderstaus = await _context.OrderStatuses.FirstOrDefaultAsync(c => c.Id == id);
            orderstaus.Status = "Shipping in Progress";
            orderItems.OrderStatus = orderstaus;
            await _context.SaveChangesAsync();
            return orderItems;
        }

        public async Task<OrderStatus> TrackOrder(string trackingId)
        {
            OrderStatus orderStatus = default;
            var order = await _context.Orders.Include(c => c.OrderStatus).FirstOrDefaultAsync(c => c.OrderReferenceNumber == trackingId);
            orderStatus = order.OrderStatus;
            return orderStatus;
        }
        public async Task ChangeOrderStatus(OrderStatusRequest model)
        {
            var order = await _context.Orders.Include(c => c.OrderStatus).FirstOrDefaultAsync(a => a.OrderId == model.OrderItemId);
            var id = order.OrderStatus.Id;
            var orderstatus = await _context.OrderStatuses.FirstOrDefaultAsync(c => c.Id == id);
            orderstatus.Status = model.OrderStatus;
            orderstatus.PaymentStatus = model.PaymentStatus;
            orderstatus.HasBeenShipped = model.HasBeenShipped;
            order.OrderStatus = orderstatus;
            
            await _context.SaveChangesAsync();
        }

        private async Task<decimal> CalculateTotalBilling(List<string> cartItems)
        {
            decimal SubTotal = default;
            foreach (var items in cartItems)
            {
                var foundItems = await _context.CartItemSegs.FirstOrDefaultAsync(c => c.CartItemId == items);
                var productInFoundItem = await _context.Products.FirstOrDefaultAsync(c => c.Id == foundItems.ProductId);
                foundItems.Product = productInFoundItem;
                decimal total = foundItems.Product.UnitPrice * foundItems.Quantity;
                SubTotal += total;
            }
            return SubTotal;
        }

        public async Task ChangePaymentStatus(string orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(a => a.OrderId == orderId);
        }



        public void PayOnDelivery()
        {

        }

        public string GetOrderReference
        {
            get
            {
                return RandomString(6);
            }

        }
        private static Random random = new Random();
        private static string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public Task ProcessPayment()
        {
            throw new NotImplementedException();
        }
    }
}