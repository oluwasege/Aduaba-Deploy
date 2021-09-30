using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class WishListSeg
    {
        [Key]
        public string Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual List<WishListItemSeg> WishListItems { get; set; }
    }
}
