using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Order
{
    public class OrderStatusRequest
    {
        [Required(ErrorMessage = "OrderItemId is required")]
        public string OrderItemId { get; set; }
        [Required(ErrorMessage = "OrderStatus is required")]
        public string OrderStatus { get; set; }
        [Required(ErrorMessage = "PaymentStatus is required")]
        public bool PaymentStatus { get; set; }
        [Required(ErrorMessage = "HasBeenShipped is required")]
        public bool HasBeenShipped { get; set; }
    }
}
