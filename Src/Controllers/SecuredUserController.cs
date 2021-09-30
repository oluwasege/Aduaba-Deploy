using Aduaba.DTO;
using Aduaba.Models;
using Aduaba.Services;
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
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredUserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ISearchServices _search;
        public SecuredUserController(IUserServices userServices, ISearchServices search)
        {
            _userServices = userServices;
            _search = search;
        }
        [HttpGet]
        public IActionResult GetSecuredData()
        {
            
            return Ok("This Secured Data is available only for Authenticated Users.");
        }
        
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateRequest model)
        {

            var result = await _userServices.UpdateAsync(model);
            return Ok(result);
        }
        //[HttpPost("Logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    var result = await _userServices.LogoutAsync();
        //    // accept token from request body or cookie
        //    var token = Request.Cookies["refreshToken"];
        //    var response = _userServices.RevokeRefreshToken(token);
        //    if (result == "Signed out successfully")
        //    {
        //        if (string.IsNullOrEmpty(token))
        //            return BadRequest(new { message = "Token is required" });
        //        if (!response)
        //            return NotFound(new { message = "Token not found" });
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }


        //}
        [HttpDelete("Delete")]
        public async Task<IActionResult>Delete()
        {
            var result = await _userServices.DeleteUserAsync();
            return Ok(result);
        }

        //try if it works
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>>Search(string name)
        {
            try
            {
                var result = await _search.Search(name);

                if (result.Any())
                {
                    
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
