using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO.Account
{
    public class ResetPassword
    {
       public string Email { get; set; }
        public string Token { get; set; }
        //public DateTime LastModifiedDate { get; set; }

        //public string RefreshToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
