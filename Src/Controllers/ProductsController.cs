using Aduaba.DTOPresentation;
using Aduaba.Data;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Aduaba.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productServices.GetAllProducts();
            if (result == null)
            {
                return NoContent();
            }
            
            return Ok(result);
        }

        [HttpGet("find-productby-name")]
        public IActionResult GetProductByName(string name)
        {
            var result = _productServices.GetProductByName(name);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            else if (result.IsAvailable == false)
            {
                return NotFound(result.ProductAvailablity);
            }
            return Ok(result);
        }

        [HttpGet("get-productby-id")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productServices.GetProductById(id);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            else if (result.IsAvailable == false)
            {
                return NotFound(result.ProductAvailablity);
            }
            return Ok(result);

        }

    }
}
