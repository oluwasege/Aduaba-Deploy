using Aduaba.DTO.WishListSeg;
using Aduaba.DTOPresentation.DTOWishlistSeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IWishListServiceSeg
    {
        public Task<string> AddToWishListAsync(AddToWishlistSeg model);
        public Task<List<WishListView>> GetCustomerWishListItems();
        public Task<string> RemoveFromWishList(string wishListItemId);
    }
}
