using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.WishList
{
    public class UpdateWishListitem
    {

        [Required]
        public string ProductId { get; set; }
        [Required]
        public int PurchaseQuantity { get; set; }
        public bool RemoveItem { get; set; }
    }
}
