using System;
using System.ComponentModel.DataAnnotations;

namespace Aduaba.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime DateModified { get; set; }

        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }
      
        public string CategoryId { get; set; }
       
        public virtual Vendor Vendor { get; set; }
        
        public bool IsInWishlist { get; set; } = false;
        public string VendorId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
