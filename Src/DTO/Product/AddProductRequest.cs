using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Product
{
    public class AddProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string VendorId { get; set; }

    }
}
