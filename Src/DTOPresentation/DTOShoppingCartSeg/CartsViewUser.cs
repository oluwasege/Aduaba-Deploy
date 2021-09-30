using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.DTOShoppingCartSeg
{
    public class CartsViewUser
    {
        public string CartItemId { get; set; }
        public string ProductImage { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string VendorName { get; set; }
        public string ProductAvailablity { get; set; }
    }
}
