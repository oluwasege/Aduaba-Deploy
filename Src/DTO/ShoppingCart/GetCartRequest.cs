using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.ShoppingCart
{
    public class GetCartRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string UserRefreshToken { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public string ShoppingCartId { get; set; }

    }
}
