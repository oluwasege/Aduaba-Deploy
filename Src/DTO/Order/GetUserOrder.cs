using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Order
{
    public class GetUserOrder
    {[Required]
        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string IsAvailable { get; set; }
   
    }
}
