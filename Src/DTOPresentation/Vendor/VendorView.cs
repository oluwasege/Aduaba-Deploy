using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.Vendor
{
    public class VendorView
    {

        public string Id { get; set; }


        public string VendorName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
