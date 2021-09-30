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
   // [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorServices _vendorServices;

        public VendorsController(IVendorServices vendorServices)
        {
            _vendorServices = vendorServices;
        }


        [HttpGet("vendors")]
        public IActionResult GetAllVendors()
        {
            var result = _vendorServices.GetAllVendors();
            if (result == null)
            {
                return NoContent();

            }
            return Ok(result);
        }

        [HttpGet("find-vendorby-name")]
        public IActionResult GetVendorByName(string name)
        {
            var result = _vendorServices.GetVendorByName(name);
            if (result == null)
            {
                return NotFound("Product not found");
            }
            return Ok(result);
        }
        [HttpGet("get-vendorby-id")]
        public IActionResult GetVendorById(string id)
        {
            var result = _vendorServices.GetVendorId(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("No Vendor found");

        }

    }
}
