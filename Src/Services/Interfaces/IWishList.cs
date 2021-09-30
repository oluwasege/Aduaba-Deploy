using Aduaba.DTO.ShoppingCart;
using Aduaba.DTO.WishList;
using Aduaba.DTOPresentation.ShoppingCart;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IWishList
    {
        public void AddToWishList(AddToWishListRequest model);
        public int RemoveItem(string productId, string userId);
        public void UpdateWishItem(UpdateWishListitem model);
        public string DeleteWishList();
        public void ConvertToOrder();
        public int GetCount();

    }
}




