
using Aduaba.Data;
using Aduaba.DTO.Order;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class OrderSimiServices : IOrderSimi
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderSimiServices(ApplicationDbContext context, IAuthenticatedUserService authenticatedUser,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<string> CancelOrder(string orderId)
        {
            var toDelete = _context.OrderSimis.FirstOrDefault(m => m.OrderId == orderId);
            if(toDelete!=null)
            {
                _context.OrderSimis.RemoveRange(toDelete);
                await _context.SaveChangesAsync();
                return "Order cancelled";
            }
            return "Order not Created";
        }

        public async Task<OrderSimi> CreateOrder(CreateOrderSimi model)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);


            var neworder = new OrderSimi
            {
                OrderId = Guid.NewGuid().ToString(),
                TotalNoOfCartItem = model.TotalNoOfCartItem,
                TotalAmount = model.TotalAmount,
                Address = model.Address,
                ApplicationUserId = currentUser.Id,
                OrderDate = DateTime.UtcNow,
                PaystackRefNo = model.PaystackRefNo,
                StatusOfDelivery = model.StatusOfDelivery,
                DeliveryDate = DateTime.Today.AddDays(3)
            };
            var usercart =  _context.CartItemSegs.Where(m => m.ApplicationUserId == currentUser.Id).ToList();
            var userShopcart = _context.ShoppingCartSegs.FirstOrDefault(m => m.ApplicationUserId == currentUser.Id);
            _context.CartItemSegs.RemoveRange(usercart);
            _context.Remove(userShopcart);
            _context.OrderSimis.Add(neworder);

           await _context.SaveChangesAsync();
            return neworder;
        }

        public async Task<List<OrderSimi>> ViewOrder()
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var result =  _context.OrderSimis.Where(m => m.ApplicationUserId == currentUser.Id).ToList();
            if(result==null)
            {
                return null;
            }
            return result;
        }
    }
}
