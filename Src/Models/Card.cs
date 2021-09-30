using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Models
{
    public class Card
    {
         [Key]
        public string Id { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        
        public string CCV { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public string ApplicationUserId { get; set; }

        public string CardType { get; set; }
    }
}
