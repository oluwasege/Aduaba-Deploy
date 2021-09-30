using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.ShoppingCartSeg
{
    public class AddToCartRequestSeg
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
