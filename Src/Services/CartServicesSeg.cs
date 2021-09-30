using Aduaba.Data;
using Aduaba.DTO.ShoppingCartSeg;
using Aduaba.DTOPresentation.DTOShoppingCartSeg;
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
    public class CartServicesSeg : ICartServicesSeg
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductServices _productServices;
        //private readonly ProductServices _productServices;
        public CartServicesSeg(IAuthenticatedUserService authenticatedUser, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IProductServices productServices)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
            _productServices = productServices;

        }
        public async Task<string> AddToCartAsync(AddToCartRequestSeg model)
        {
            List<CartItemSeg> cartItems = new List<CartItemSeg>();
            CartItemSeg product = default;
            CartItemSeg existingProduct = default;
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
           // ProductServices productServices = new ProductServices(_context,_userManager,_authenticatedUser);
            var productAvailabe = await _context.Products.FirstOrDefaultAsync(c => c.Id == model.ProductId);



            if (productAvailabe.IsAvailable == false || productAvailabe == null)
            {
                return "Product not available";
            }
            else if (model.Quantity > productAvailabe.Quantity)
            {
                return $"There are only {productAvailabe.Quantity} items left";
            }
            else
            {
                var existingProductinCart = _context.ShoppingCartSegs.Include(c => c.ShoppingCartItems).Where(x => x.ApplicationUserId == currentUser.Id).ToList();
                if (existingProductinCart == null)
                {
                    product = new CartItemSeg
                    {
                        CartItemId = Guid.NewGuid().ToString(),
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        DateCreated = DateTime.UtcNow,
                        UnitPrice = productAvailabe.UnitPrice,
                        TotalPrice = (productAvailabe.UnitPrice * model.Quantity),
                        ApplicationUserId = currentUser.Id

                    };
                    cartItems.Add(product);
                    var shoppingCart = new ShoppingCartSeg
                    {
                        ApplicationUserId = currentUser.Id,
                        ShoppingCartItems = cartItems,
                        ShoppingCartId = Guid.NewGuid().ToString()

                    };
                    await _context.ShoppingCartSegs.AddAsync(shoppingCart);
                }
                else
                {
                    foreach (var item in existingProductinCart)
                    {
                        existingProduct = item.ShoppingCartItems.FirstOrDefault(c => c.ProductId == model.ProductId);
                        if (existingProduct != null)
                        {
                            break;
                        }
                    }
                    if (existingProduct == null)
                    {
                        product = new CartItemSeg
                        {
                            CartItemId = Guid.NewGuid().ToString(),
                            ProductId = model.ProductId,
                            Quantity = model.Quantity,
                            DateCreated = DateTime.UtcNow,
                            UnitPrice = productAvailabe.UnitPrice,
                            TotalPrice = (productAvailabe.UnitPrice * model.Quantity),
                            ApplicationUserId = currentUser.Id

                        };
                        cartItems.Add(product);
                        var shoppingCart = new ShoppingCartSeg
                        {
                            ApplicationUserId = currentUser.Id,
                            ShoppingCartItems = cartItems,
                            ShoppingCartId = Guid.NewGuid().ToString()

                        };
                        await _context.ShoppingCartSegs.AddAsync(shoppingCart);
                    }
                    else
                    {


                        existingProduct.Quantity += model.Quantity;
                        existingProduct.TotalPrice = existingProduct.UnitPrice * existingProduct.Quantity;


                    }
                }
                productAvailabe.Quantity = productAvailabe.Quantity - model.Quantity;
                await _context.SaveChangesAsync();

                return "product added to cart";
            }


        }

        public async Task<List<CartsViewUser>> GetCart()
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            CartsViewUser cartItem = default;
            List<CartsViewUser> shoppingCart = new List<CartsViewUser>();

            var customercart = await _context.ShoppingCartSegs.Include(c => c.ShoppingCartItems).Where(x => x.ApplicationUserId == currentUser.Id).ToListAsync();
            if (customercart == null)
            {
                return null;
            }
            else
            {
                List<Product> productsToSearch = await _context.Products.Include(c=>c.Vendor).ToListAsync();

                foreach (var item in customercart)
                {
                    foreach (var cart in item.ShoppingCartItems)
                    {
                        cart.Product = productsToSearch.FirstOrDefault(c => c.Id == cart.ProductId);
                        
                        cartItem = new CartsViewUser
                        {
                            CartItemId = cart.CartItemId,
                            
                            ProductName = cart.Product.Name,
                            Quantity = cart.Quantity,
                            UnitPrice = cart.UnitPrice,
                            Price = cart.TotalPrice,
                            VendorName=cart.Product.Vendor.VendorName,
                            IsAvailable=cart.Product.IsAvailable,
                            ProductId=cart.Product.Id,                           
                            ProductImage = cart.Product.ImageUrl

                        };
                        if (cart.Product.IsAvailable)
                        {
                            cartItem.ProductAvailablity = "In Stock";
                        }
                        else
                        {
                            cartItem.ProductAvailablity = "Out of Stock";
                        }
                        shoppingCart.Add(cartItem);
                    }


                }



                return shoppingCart;
            }
        }

        public async Task<ShoppingCartSeg> Get()
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var customercart = await _context.ShoppingCartSegs.Include(c => c.ShoppingCartItems).FirstOrDefaultAsync(x => x.ApplicationUserId == currentUser.Id);
            if (customercart == null)
            {
                return null;
            }
            else
            {
                return customercart;
            }
        }

        public async Task<string> RemoveFromCartAsync(RemoveItemSeg model)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var foundProductInCart = _context.CartItemSegs.Where(c => c.ApplicationUserId == currentUser.Id);
            var productToRemove = foundProductInCart.FirstOrDefault(x => x.ProductId == model.ProductId);
            var product = _context.Products.FirstOrDefault(c => c.Id == productToRemove.ProductId);
            //int restoreQuantity;
            if (productToRemove == null)
            {
                return "Product is not in your cart";
            }
            else
            {

                product.Quantity += productToRemove.Quantity;
                _context.CartItemSegs.Remove(productToRemove);
                await _context.SaveChangesAsync();
                return "Product removed from your cart";
            }

        }

        public async Task<CartItemSeg> UpdateQuantityAsync(UpdateSeg model)
        {
            if (model.Quantity != 0)
            {
                var foundItem = _context.CartItemSegs.Include(x=>x.Product).Include(m=>m.ApplicationUser).FirstOrDefault(c => c.ProductId == model.ProductId);
                foundItem.Quantity = model.Quantity;
                await _context.SaveChangesAsync();
                return foundItem;
            }
            else
            {
                return null;
            }
        }
    }
}
