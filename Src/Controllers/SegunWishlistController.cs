using Aduaba.DTO.WishListSeg;
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
    public class SegunWishlistController : ControllerBase
    {
        private readonly IWishListServiceSeg _wishListService;
        public SegunWishlistController(IWishListServiceSeg wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpPost("add-wishlist")]
        public async Task<IActionResult> AddToWishlist(AddToWishlistSeg model)
        {
            var result = await _wishListService.AddToWishListAsync(model);
            return Ok(result);
        }

        [HttpGet("get-wishlist")]
        public async Task<IActionResult> GetCustomerWishListItems()
        {
            var result = await _wishListService.GetCustomerWishListItems();
            return Ok(result);
        }

        [HttpDelete("delete-from-wishlist")]
        public async Task<IActionResult> RemoveFromWishList([FromBody] string wishListItemId)
        {
            var result = await _wishListService.RemoveFromWishList(wishListItemId);
            return Ok(result);
        }
    }
}
