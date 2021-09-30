using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.WishList
{
    public class AddToWishListRequest
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string WishListName { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
