using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class ShoppingCartSeg
    {
        [Key]
        public string ShoppingCartId { get; set; }
        public virtual List<CartItemSeg> ShoppingCartItems { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
