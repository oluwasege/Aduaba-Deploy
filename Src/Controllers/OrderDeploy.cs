using Aduaba.DTO.Order;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Controllers
{

    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    public class OrderDeploy : ControllerBase
    {
        private readonly IOrderSimi _orderSimi;
        public OrderDeploy(IOrderSimi orderSimi)
        {
            _orderSimi = orderSimi;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult>CreateOrder(CreateOrderSimi model)
        {
            var result = await _orderSimi.CreateOrder(model);
            return Ok(result);
        }
        [HttpDelete("cancel-order")]
        public async Task<IActionResult>CancelOrder(string orderId)
        {
            var result = await _orderSimi.CancelOrder(orderId);
            return Ok(result);
        }
        [HttpGet("view-order")]
        public async Task<IActionResult>ViewOrder()
        {
            var result = await _orderSimi.ViewOrder();
            if(result==null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
