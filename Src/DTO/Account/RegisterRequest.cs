using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("FirstName"), StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("LastName"), StringLength(50)]
        public string LastName { get; set; }

        
        public string AvataUrl { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [StringLength(11)]
        public string PhoneNumber { get; set; }

    }
}
