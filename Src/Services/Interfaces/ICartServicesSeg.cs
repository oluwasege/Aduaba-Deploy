using Aduaba.DTO.ShoppingCartSeg;
using Aduaba.DTOPresentation.DTOShoppingCartSeg;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface ICartServicesSeg
    {
        public Task<ShoppingCartSeg> Get();
        Task<List<CartsViewUser>> GetCart();
        Task<string> AddToCartAsync(AddToCartRequestSeg model);
        public Task<string> RemoveFromCartAsync(RemoveItemSeg model);
        public Task<CartItemSeg> UpdateQuantityAsync(UpdateSeg model);
    }
}
