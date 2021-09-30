using Aduaba.Data;
using Aduaba.DTO.Product;
using Aduaba.DTOPresentation;
using Aduaba.DTOPresentation.DTOProduct;
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
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWishListServiceSeg _wishlist;
        public ProductServices(IWishListServiceSeg wishlist,ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAuthenticatedUserService authenticatedUser)
        {
            _context = context;
            _userManager = userManager;
            _authenticatedUser = authenticatedUser;
            _wishlist = wishlist;
        }
        public string AddProduct(AddProductRequest model)
        {
            var productExist = _context.Products.FirstOrDefault(c => c.Name == model.Name);
            if (productExist != null)
            {
                return "Product already exist, Please check the name of the Product";
            }
                Product product=new Product()
                {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                Description = model.Description,
                UnitPrice = model.Price,
                ImageUrl = model.ImageUrl,
                Quantity = model.Quantity,
                DateAdded = DateTime.UtcNow,
                CategoryId = model.CategoryId,
                VendorId = model.VendorId,                
                };
            if (product.Quantity != 0)
            {
                product.IsAvailable = true;
            }
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return "Error occured while adding Product";
            }
            
            return "Product Added";
        }

        public async Task<List<ProductView>> GetAllProducts()
        {
            List<ProductView> listOfProducts =  new List<ProductView>();
            List<Product> availableProducts = await _context.Products.Include(c => c.Category).Include(c=>c.Vendor).ToListAsync();
            // WishListServiceSeg wishlist = new WishListServiceSeg(_context,_authenticatedUser);
           // var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var wishlistForLoggedinCustomer = await _wishlist.GetCustomerWishListItems();
            foreach (var product in availableProducts)
            {
                if (product.Quantity != 0)
                {
                    
                    product.IsAvailable = true;
                    if(wishlistForLoggedinCustomer.Count!=0)
                    {
                        foreach (var item in wishlistForLoggedinCustomer)
                        {
                            var test = wishlistForLoggedinCustomer.FirstOrDefault(c => c.ProductId == product.Id);
                            if (test != null)
                            {
                                product.IsInWishlist = true;
                            }
                        }
                                              
                    }
                    
                    
                    listOfProducts.Add(new ProductView()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        UnitPrice = product.UnitPrice,
                        ImageUrl = product.ImageUrl,
                        CategoryName = product.Category.CategoryName.ToString(),
                        VendorName = product.Vendor.VendorName,
                        Quantity = product.Quantity,
                        IsAvailable = product.IsAvailable,
                        ProductAvailablity="Product is available",
                        ItemIsinWishlist=product.IsInWishlist
                        
                    });
                    
                }

                else
                {
                    listOfProducts.Add(new ProductView()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        UnitPrice = product.UnitPrice,
                        ImageUrl = product.ImageUrl,
                        CategoryName = product.Category.CategoryName.ToString(),
                        VendorName = product.Vendor.VendorName,
                        Quantity = product.Quantity,
                        IsAvailable = product.IsAvailable,
                        ProductAvailablity="Out of Stock",
                        ItemIsinWishlist=product.IsInWishlist
                        
                    });
                }
               
                

            }
           return listOfProducts;


        }
        public async Task<ProductView> GetProductById(string id)
        {
            
            if (!string.IsNullOrEmpty(id))
            {
                var foundProduct = _context.Products.Include(c=>c.Category).Include(c => c.Vendor).FirstOrDefault(p => p.Id == id);
                var wishlistForLoggedinCustomer = await _wishlist.GetCustomerWishListItems();
                if (foundProduct == null)
                {
                    return null;
                }
                
                if (wishlistForLoggedinCustomer.Count!=0)
                {
                    foreach (var item in wishlistForLoggedinCustomer)
                    {
                        var test = wishlistForLoggedinCustomer.FirstOrDefault(c => c.ProductId == foundProduct.Id);
                        if (test != null)
                        {
                            foundProduct.IsInWishlist = true;
                        }
                    }

                }
                ProductView product = (new ProductView()
                {
                    Id = foundProduct.Id,
                    Name = foundProduct.Name,
                    Description = foundProduct.Description,
                    ImageUrl = foundProduct.ImageUrl,
                    CategoryName = foundProduct.Category.CategoryName.ToString(),
                    VendorName = foundProduct.Vendor.VendorName,
                    Quantity=foundProduct.Quantity     
                });
                if(product.Quantity==0)
                {
                    product.IsAvailable = false;
                    product.ProductAvailablity = "Product not available";
                }
                else
                {
                    product.IsAvailable = true;
                    product.ProductAvailablity = "Product avalaible";
                    product.ItemIsinWishlist = foundProduct.IsInWishlist;
                }
                return product;
            }
            return null;

        }

        public ProductView GetProductByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var foundProduct = _context.Products.Include(c=>c.Category).FirstOrDefault(c => c.Name == name);
                if (foundProduct == null)
                {
                    return null;
                }
                ProductView product = (new ProductView()
                {
                    Id = foundProduct.Id,
                    Name = foundProduct.Name,
                    Description = foundProduct.Description,
                    ImageUrl = foundProduct.ImageUrl,
                    CategoryName = foundProduct.Category.CategoryName.ToString(),
                    VendorName=foundProduct.Vendor.VendorName 
                });
                if (product.Quantity == 0)
                {
                    product.IsAvailable = false;
                    product.ProductAvailablity = "Product not available";
                }
                else
                {
                    product.IsAvailable = true;
                    product.ProductAvailablity = "Product avalaible";
                }
                return product;
            }
            return null;
        }


        public string UpdateProduct(EditProductRequest model)
        {
            var oldProduct = _context.Products.FirstOrDefault(c => c.Id == model.Id);

            if (oldProduct == null)
            {
                return "Product not found";
            } //Product not found

            //it'll work
            if (model.NewPrice != 0 && model.UpdatedQuantity != 0)
            {
                oldProduct.UnitPrice = model.NewPrice;
                oldProduct.Quantity = model.UpdatedQuantity;

            }
            else if (model.NewPrice == 0)
            {
                if (model.UpdatedQuantity != 0)
                {
                    oldProduct.Quantity = model.UpdatedQuantity;
                }
                else
                {
                    return "Please enter values to be updated";
                }

            }
            else if (model.UpdatedQuantity == 0)
            {
                if (model.NewPrice != 0)
                {
                    oldProduct.UnitPrice = model.NewPrice;
                }
                else
                {
                    return "Please enter values to be updated";
                }

            }
            oldProduct.DateModified = DateTime.UtcNow;
            _context.SaveChanges();
            return "Product updated";

        }

        public string DeleteProduct(List<string> names)
        {
            List<Product> productsToDelete = _context.Products.Where(c => names.Contains(c.Name)).ToList();

            if (productsToDelete.Count != 0)
            {
                _context.Products.RemoveRange(productsToDelete);
                _context.SaveChanges();

                return "Product Deleted Succesfully";
            }

            return "Product doesn't exist";
        }

    }


}

