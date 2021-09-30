using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.ShippingAddress
{
    public class ShippingAddressResponse
    {
        public string ShippingAddressId { get; set; }
        public string ContactPersonsName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Landmark { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
