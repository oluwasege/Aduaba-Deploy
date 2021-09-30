using Aduaba.DTO.ShippingAddress;
using Aduaba.DTOPresentation.ShippingAddress;
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
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingAddress;
        public ShippingAddressController(IShippingAddressService shippingAddress)
        {
            _shippingAddress = shippingAddress;
        }
        [HttpPost("Add-Shipping-Address")]
        public async Task<IActionResult> AddShippingAddressAsync(AddShippingAddressRequest form)
        {
            if (ModelState.IsValid)
            {
                //var userId = _httpCurrentUser.GetUserId();
               var result= await _shippingAddress.AddShippingAddress(form);
                return Ok(result);

            }
            return BadRequest("Some fields are incorrect");

        }
        [HttpGet("Get-All-Shipping-Address ")]
        public async Task<IActionResult> GetAllShippingAddresses ()

        {
            if(ModelState.IsValid)
            {
               // var userId = _httpCurrentUser.GetUserId();
                var result = await _shippingAddress.GetAllShippingAddresses();
                if (result !=null)
                {
                    return Ok(result);

                }
                return NoContent();

            }
            return BadRequest();
       
        }
        //[HttpGet("Get-Address-By-Id")]
        //public IActionResult GetShippingAddressById()
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var userId = _httpCurrentUser.GetUserId();
        //        var result = _shippingAddress.GetShippingAddressById();
        //        return Ok(result);

        //    }
        //    return BadRequest();
          
        //}

        [HttpDelete("Delete-Address")]
        public async Task<IActionResult> DeleteAddress([FromBody]string shipAdddressId)
        {
            if (ModelState.IsValid)
            {
                
               var result= await _shippingAddress.DeleteShippingAddress(shipAdddressId);
                if(result==null)
                {
                    return NotFound();
                }
                return Ok(result);
            }

            return BadRequest();

        }








    }

}
