using Aduaba.Data;
using Aduaba.DTO.Order;
using Aduaba.DTOPresentation.Order;
using Aduaba.Models;
using Aduaba.Services;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Controllers
{
    [Authorize]
    [ApiController]
    public class CheckOutController : ControllerBase
    {

        private readonly IShippingAddressService _shippingAddressService;
        private readonly ApplicationDbContext _context;
        private readonly IOrderServices _orderServices;
        private readonly IHttpCurrentUser _httpCurrentUser;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;


        public CheckOutController(IAuthenticatedUserService authenticatedUser,UserManager<ApplicationUser> userManager,ApplicationDbContext context, IHttpCurrentUser httpCurrentUser, IShippingAddressService shippingAddressService, IOrderServices orderServices)
        {
            _context = context;
            _orderServices = orderServices;
            _httpCurrentUser = httpCurrentUser;
            _shippingAddressService = shippingAddressService;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }


        [HttpGet("Checkout")]
        public async Task<ActionResult> Checkout([FromBody] List<string> orderItemsId)
        {

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
                string IsAvailable = default;
                List<GetUserOrder> order = new List<GetUserOrder>();
                var Items = await _orderServices.GetOrderItems(orderItemsId);
                foreach (var item in Items.OrderItems)
                {
                    if (item.Product.IsAvailable == true) IsAvailable = "Is Available";
                    else IsAvailable = "Sold Out";
                    var orderItem = new GetUserOrder
                    {
                        OrderId = Items.OrderId,
                        IsAvailable = IsAvailable,
                        ProductImage = item.Product.ImageUrl,
                        VendorName = item.Product.Vendor.VendorName,
                        ProductName = item.Product.Name,
                        Quantity = item.Quantity,
                        Total = item.TotalPrice,
                        //FirstName = currentUser.FirstName.ToString(),
                        //LastName=currentUser.LastName.ToString(),
                        //PhoneNumber=currentUser.PhoneNumber

                        
                    };
                    order.Add(orderItem);
                }
                return Ok(order);
            }
            return BadRequest("Some fields are incorrect");

        }

        [HttpGet]
        [Route("ShippingAddress")]
        public async Task<ActionResult> GetShippingAddress()
        {
           
            var shippingAddress = await _shippingAddressService.GetAllShippingAddresses();
            if (shippingAddress == null)
                return Ok("No Shipping Address found for this User");
            else
            {
                return Ok(shippingAddress);
            }
        }

        [HttpPost]
        [Route("PayOnDelivery")]
        public async Task<ActionResult> OrderItem([FromBody] CreateOrderRequest order)
        {
            if (ModelState.IsValid)
            {
                var userId = _httpCurrentUser.GetUserId();

                if (order.OrderType == "PayOnDelivery".ToLower()||order.OrderType=="PayWithCard".ToLower())
                {
                    var customerOrder = await _orderServices.OrderItems(order);
                    OrderRefNumberView sucessful = new OrderRefNumberView
                    {
                        OrderId = customerOrder.OrderReferenceNumber
                    };
                    return Ok($"Your order is successful. you can track your order with this reference number {sucessful.OrderId}");
                }
                return Ok("Order not successful");

            }
            return BadRequest("Some fields are incorrect");

        }

        

        [HttpGet]
        [Route("track-order")]
        public async Task<ActionResult> OrderStatus([FromQuery] string orderReferenceNumber)
        {
            if (ModelState.IsValid)
            {
                var orderStatus = await _orderServices.TrackOrder(orderReferenceNumber);
                if (orderStatus == null)
                    return BadRequest("No order found with this reference Number");
                else
                    return Ok(orderStatus);
            }
            return BadRequest("Some fields are incorrect");
        }
    }
}