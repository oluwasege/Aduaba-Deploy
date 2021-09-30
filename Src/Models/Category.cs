using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class Category
    {
        [Key]

        public string Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string ProductId { get; set; }
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }



    }
}
