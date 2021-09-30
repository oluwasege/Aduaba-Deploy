using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aduaba.Models
{
    public class Order
    {

        [Key]
        public string OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }


        public int OrderStatusId { get; set; }

        
        public string PaymentType { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        public string OrderReferenceNumber { get; set; }
        public string OrderType { get; set; }
        public List<CartItemSeg> OrderItems { get; set; } = new List<CartItemSeg>();

        public virtual ShippingAddress ShippingAddress { get; set; }


        public string ShippingAddressId { get; set; }

        public virtual BillingAddress BillingAddress { get; set; }


        public string BillingAddressId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public string ShoppingCartId { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}