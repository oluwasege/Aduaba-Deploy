using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aduaba.Models
{
    public class WishList
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }


        public int ApplicationUserId { get; set; }


        public virtual IEnumerable<WishListItem> WishListItems { get; set; }
    }
}