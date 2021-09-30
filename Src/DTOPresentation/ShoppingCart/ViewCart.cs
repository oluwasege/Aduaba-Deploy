using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.ShoppingCart
{
    public class ViewCart
    {
        public  string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ProductVendor { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }


    }
}
