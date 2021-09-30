using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Order
{
    public class CreateOrderSimi
    {
       
        [Required]
        public int TotalNoOfCartItem { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaystackRefNo { get; set; }
        public string Address { get; set; }
        public string StatusOfDelivery { get; set; }
    }
}
