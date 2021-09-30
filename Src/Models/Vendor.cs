using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class Vendor
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string VendorName { get; set; }

        [Required]
        public DateTime DateJoined { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
