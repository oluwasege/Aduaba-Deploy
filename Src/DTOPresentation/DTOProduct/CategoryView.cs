using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTOPresentation.DTOProduct
{
    public class CategoryView
    {
        public string CategoryId { get; set; } = default;
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
