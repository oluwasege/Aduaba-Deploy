using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.Order
{
    public class ViewCustomerOrder
    {
        public class Detail
        {
            public string ProductID { get; set; }
            public string Product { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }
        public IEnumerable<Detail> Details { get; set; }
    }
}
