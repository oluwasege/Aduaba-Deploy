using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class CartItemSeg
    {
        [Key]
        public string CartItemId { get; set; }


        [Required]
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Product Product { get; set; }
        public string ProductId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
