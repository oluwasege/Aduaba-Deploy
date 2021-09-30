using Aduaba.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Aduaba.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string AvatarUrl { get; set; }
        public virtual IEnumerable<BillingAddress> BillingAddresses { get; set; }
        public virtual IEnumerable<ShippingAddress> ShippingAddresses { get; set; }
        public string ShippingAddressId { get; set; }
        public virtual IEnumerable<Card> Cards { get; set; }
        //public virtual Cart Cart { get; set; }
        public string ShoppingCartId { get; set; }

        public virtual List<CartItemSeg> ShoppingCart { get; set; }

        public virtual IEnumerable<WishList> Wishlist { get; set; }
        public virtual IEnumerable<Order> Orders { get; set; }
        public virtual IEnumerable<PaymentHistory> PaymentHistories { get; set; }
    }
}