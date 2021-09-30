using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.ShoppingCartSeg
{
    public class RemoveItemSeg
    {
        [Required]
        public string ProductId { get; set; }
    }
}
