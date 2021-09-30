using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Order
{
    public class ViewPassedOrder
    {
        public string OrderId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
