using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class OrderSimi
    {
        [Key]
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public int TotalNoOfCartItem { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public string PaystackRefNo { get; set; }
        public string Address { get; set; }
        public string StatusOfDelivery { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

    }
}
