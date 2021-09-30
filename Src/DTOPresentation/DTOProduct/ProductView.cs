using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.DTOProduct
{
    public class ProductView
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string VendorName { get; set; }
        public bool IsAvailable { get; set; }
        public int Quantity { get; set; }
        public string ProductAvailablity { get; set; }
        public bool ItemIsinWishlist { get; set; } 
    }
}
