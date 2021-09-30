using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.ShippingAddress
{
    public class AddShippingAddressRequest
    {
        [Required]
        public string ContactPersonsName { get; set; }

        [Required]
        public string Address { get; set; }

        public string City { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string Landmark { get; set; }

    }
}
