using Aduaba.DTO.ShoppingCartSeg;
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
    [ApiController]
    public class SegunCartController : ControllerBase
    {
        private readonly ICartServicesSeg _cartServicesSeg;

        public SegunCartController(ICartServicesSeg cartServicesSeg)
        {
            _cartServicesSeg = cartServicesSeg;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCartAsync([FromBody] AddToCartRequestSeg model)
        {
            var result = await _cartServicesSeg.AddToCartAsync(model);
            return Ok(result);
        }

        [HttpGet("view-cart")]
        public async Task<IActionResult> GetCartAsync()
        {
            var result = await _cartServicesSeg.GetCart();
            var re = result.Sum(c => c.Price);
            return Ok(result);
        }
        [HttpGet("Total-cart-amount")]
        public async Task<IActionResult> GetCartAmountAsync()
        {
            var result = await _cartServicesSeg.GetCart();
            var re = result.Sum(c => c.Price);
            return Ok(re);
        }
        [HttpGet("test")]
        public async Task<IActionResult>Get()
        {
            var result = await _cartServicesSeg.Get();
            return Ok(result);
        }

        [HttpDelete("Remove-from-cart")]
        public async Task<IActionResult> RemoveItemFromCartAsync([FromBody] RemoveItemSeg model)
        {
            var result = await _cartServicesSeg.RemoveFromCartAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateQuantity")]
        public async Task<ActionResult> UpdateQuantityOfACartItem(UpdateSeg model)
        {
            var result = await _cartServicesSeg.UpdateQuantityAsync(model);
            if (result != null)
            {
                return Ok(result);
            }
            else
                return BadRequest();
        }

    }
}
