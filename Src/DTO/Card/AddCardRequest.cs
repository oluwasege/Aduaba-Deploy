using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Card
{
    public class AddCardRequest
    {
        [Required]
        public string CardHolderName { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        [MaxLength(3)]
       
        public string CCV { get; set; }
    }
}
