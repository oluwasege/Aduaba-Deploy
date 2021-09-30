/*using Aduaba.Data;
using Aduaba.DTO.ShoppingCart;
using Aduaba.DTO.WishList;
using Aduaba.DTOPresentation.ShoppingCart;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class WishListActions : IWishList
    {
        public string WishesId { get; set; }
        public List<WishListItem> WishListItems = null;
        private readonly ApplicationDbContext _context;
        private readonly HttpCurrentUser _currentUser;

        public WishListActions(ApplicationDbContext context, HttpCurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;

        }



        public void AddToWishList(AddToWishListRequest model)
        {
            WishesId = _currentUser.GetUserName();

            var wishListItem = _context.WishListItems.SingleOrDefault
          (c => c.WishListId == WishesId && c.ProductId == model.ProductId);


            if (wishListItem == null)
            {
                wishListItem = new WishListItem
                {
                    WishListItemId = Guid.NewGuid().ToString(),
                    WishListName = model.WishListName,
                    ProductId = model.ProductId,
                    WishListId = WishesId,
                    Product = _context.Products.SingleOrDefault(
                   p => p.Id == model.ProductId),
                    Quantity = model.Quantity,
                    DateCreated = DateTime.Now

                };


                _context.WishListItems.Add(wishListItem);


            }
            else
            {

                wishListItem.Quantity++;
            }
            _context.SaveChanges();


        }





        public int RemoveItem(string productId, string userId)
        {
            var wishlistItem = _context.WishListItems.FirstOrDefault
                (c => c.WishListId == WishesId && c.ProductId == productId);

            var localQuantity = 0;

            if (wishlistItem != null)
            {
                if (wishlistItem.Quantity > 1)
                {
                    wishlistItem.Quantity--;
                    localQuantity = wishlistItem.Quantity;
                }
                else
                {
                    _context.WishListItems.Remove(wishlistItem);
                }
            }

            _context.SaveChanges();

            return localQuantity;
        }


        public void UpdateWishItem(UpdateWishListitem model)
        {
            WishesId = _currentUser.();
            try
            {
                var wishItem = _context.WishListItems.SingleOrDefault
                    (c => c.WishListId == WishesId && c.ProductId == model.ProductId);

                if (wishItem != null && model.PurchaseQuantity != 0)
                {
                    wishItem.Quantity = model.PurchaseQuantity;
                    _context.SaveChanges();
                }
            }
            catch (Exception exp)
            {
                throw new Exception("ERROR: Unable to Update WishList - " + exp.Message.ToString(), exp);


            }


        }
        public List<WishListItem> GetWishListItems()

        {
            WishesId = _currentUser.GetUserName();

            var AllWishItems = _context.WishListItems.Where(c => c.WishListId == WishesId).ToList();


            return AllWishItems;

        }




        public string DeleteWishList()
        {
            WishesId = _currentUser.GetUserName();
            if (WishesId != null)
            {
                var wishItems = _context.WishListItems.Where(
               c => c.WishListId == WishesId).ToList();

                _context.WishListItems.RemoveRange(wishItems);


                _context.SaveChanges();
                return ("Wish List deleted successfully");
            }
            return ("Wish List does not exits");
        }

        public void ConvertToOrder()
        {

        }

        public int GetCount()
        {
            WishesId = _currentUser.GetUserName();

            int? count = (from wishes in _context.WishListItems
                          where wishes.WishListId == WishesId
                          select (int?)wishes.Quantity).Sum();

            return count ?? 0;
        }
    }
}
/
*/


