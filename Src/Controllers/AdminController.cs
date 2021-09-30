using Aduaba.DTO.Order;
using Aduaba.DTO.Product;
using Aduaba.DTO.Vendor;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Controllers
{

    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IProductServices _productServices;
        private readonly IVendorServices _vendorServices;
        private readonly IOrderServices _orderServices;

        public AdminController(IOrderServices orderServices,ICategoryServices categoryServices, IProductServices productServices, IVendorServices vendorServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _vendorServices = vendorServices;
            _orderServices = orderServices;
        }


        [HttpPost("add-categories")]
        public IActionResult AddCategories(AddCategoryRequest model)
        {
            var result = _categoryServices.AddCategory(model);

            return Ok(result);
        }

        [HttpDelete("delete-categories")]
        public IActionResult DeleteCategory(List<string> categoryId)
        {
            var result = _categoryServices.DeleteCategory(categoryId);
            return Ok(result);
        }

        [HttpPut("update-categories")]
        public IActionResult UpdateCategories(EditCategoryRequest model)
        {
            var result = _categoryServices.EditCategory(model);
            return Ok(result);
        }

        [HttpPost("add-product")]
        public IActionResult AddProducts(AddProductRequest model)
        {
            var result = _productServices.AddProduct(model);
            return Ok(result);
        }

        [HttpPut("update-product")]
        public IActionResult UpdateProducts(EditProductRequest model)
        {
            var result = _productServices.UpdateProduct(model);
            return Ok(result);
        }

        [HttpDelete("delete-product")]
        public IActionResult DeleteProducts(List<string> productId)
        {
            var result = _productServices.DeleteProduct(productId);
            return Ok(result);
        }

        [HttpPost("add-vendor")]
        public IActionResult AddVendor(AddVendorRequest model)
        {
            var result = _vendorServices.AddVendor(model);
            return Ok(result);
        }

        [HttpPut("update-vendor")]
        public IActionResult UpdateVendor(EditVendorRequest model)
        {
            var result = _vendorServices.UpdateVendor(model);
            return Ok(result);
        }

        [HttpDelete("delete-vendor")]
        public IActionResult DeleteVendors(List<string> VendorIds)
        {
            var result = _vendorServices.DeleteVendors(VendorIds);
            return Ok(result);
        }

        [HttpPost]

        [Route("ChangeOrderStatus")]
        public async Task<ActionResult> OrderStatus([FromBody] OrderStatusRequest orderStatus)
        {
            if (ModelState.IsValid)
            {

                await _orderServices.ChangeOrderStatus(orderStatus);
                return Ok("OrderStatus Changed Successfully");

            }
            return BadRequest("Some fields are incorrect");

        }

    }
}
