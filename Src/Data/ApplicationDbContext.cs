using Aduaba.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<Card> Cards { get; set; }

        //public DbSet<SubCategory> SubCategories { get; set; }

        // public DbSet<Cart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartSeg> ShoppingCartSegs { get; set; }
        public DbSet<CartItemSeg> CartItemSegs { get; set; }
        public DbSet<WishListItemSeg> WishListItemSegs { get; set; }
        public DbSet<OrderSimi> OrderSimis { get; set; }
        public DbSet<WishListSeg> WishListSegs { get; set; }
       // public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PaymentHistory> PaymentHistories { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }


        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }
    }
    
}
