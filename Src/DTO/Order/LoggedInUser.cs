using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Order
{
    public class LoggedInUser
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CartId { get; set; }
        public string ShippingId { get; set; }
    }
}
