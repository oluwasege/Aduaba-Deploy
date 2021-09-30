using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Product
{
    public class EditProductRequest
    {
        public string Id { get; set; }

        public decimal NewPrice { get; set; }

        public int UpdatedQuantity { get; set; }

        
    }
}
