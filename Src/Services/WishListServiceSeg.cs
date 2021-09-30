using Aduaba.Data;
using Aduaba.DTO.WishListSeg;
using Aduaba.DTOPresentation.DTOWishlistSeg;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class WishListServiceSeg : IWishListServiceSeg
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        public WishListServiceSeg(ApplicationDbContext context, IAuthenticatedUserService authenticatedUser, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }
        public async Task<string> AddToWishListAsync(AddToWishlistSeg model)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var existingWishList = await _context.WishListSegs.Include(c => c.WishListItems).Where(x => x.ApplicationUserId == currentUser.Id).ToListAsync();
            List<WishListItemSeg> wishListItems = new List<WishListItemSeg>();
            //WishListSeg newWishList = default;
            WishListItemSeg inWishlist = default;
            WishListItemSeg product = default;
            if (existingWishList == null)
            {
                product = new WishListItemSeg
                {
                    WishListItemId = Guid.NewGuid().ToString(),
                    ProductId = model.ProductId,
                    //WishListId = Guid.NewGuid().ToString()
                };
                wishListItems.Add(product);
                var createWishList = new WishListSeg
                {
                    Id = Guid.NewGuid().ToString(),
                    WishListItems = wishListItems,
                    DateCreated = DateTime.UtcNow,
                    ApplicationUserId = currentUser.Id
                };
                await _context.WishListSegs.AddAsync(createWishList);
            }

            else
            {
                foreach (var item in existingWishList)
                {
                    inWishlist = item.WishListItems.FirstOrDefault(c => c.ProductId == model.ProductId);
                    if (inWishlist != null)
                    {
                        break;
                    }
                }
                if (inWishlist == null)
                {
                    product = new WishListItemSeg
                    {
                        WishListItemId = Guid.NewGuid().ToString(),
                        ProductId = model.ProductId,
                        //WishListId = Guid.NewGuid().ToString()
                    };
                    wishListItems.Add(product);
                    var createWishList = new WishListSeg
                    {
                        Id = Guid.NewGuid().ToString(),
                        WishListItems = wishListItems,
                        DateCreated = DateTime.UtcNow,
                        ApplicationUserId = currentUser.Id
                    };
                    await _context.WishListSegs.AddAsync(createWishList);
                }
                else
                {
                    return "Item Already Exists";
                }
            }
            await _context.SaveChangesAsync();
             return "Added to wishlist";

        }


        public async Task<List<WishListView>> GetCustomerWishListItems()
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            WishListView wishListItem = default;
            var productsInWishList = new List<WishListView>();
            var customerWishList = _context.WishListSegs.Include(c => c.WishListItems).Where(c => c.ApplicationUserId == currentUser.Id).ToList();
            if (customerWishList == null)
            {
                return null;
            }
            else
            {
                foreach (var item in customerWishList)
                {
                    foreach (var wishList in item.WishListItems)
                    {
                        wishList.Product = _context.Products.First(c => c.Id == wishList.ProductId);
                        wishListItem = new WishListView
                        {
                            WishListItemId = wishList.WishListItemId,
                            ProductName = wishList.Product.Name,
                            ProductId=wishList.ProductId
                            
                        };
                        productsInWishList.Add(wishListItem);
                    }
                }
                return productsInWishList;
            }
        }

        public async Task<string> RemoveFromWishList(string wishListItemId)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            if (string.IsNullOrEmpty(wishListItemId))
            {
                return "Product not found";
            }
            else
            {
                var customerWishList = await _context.WishListSegs.Include(c => c.WishListItems).Where(x => x.ApplicationUserId == currentUser.Id).ToListAsync();
                foreach (var items in customerWishList)
                {
                    var itemTodelete = items.WishListItems.FirstOrDefault(c => c.WishListItemId == wishListItemId);
                    items.WishListItems.Remove(itemTodelete);
                }
                await _context.SaveChangesAsync();
                return "Product removed from wishlist";

            }
        }
    }
}
