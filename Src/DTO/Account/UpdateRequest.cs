using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.DTO
{
    public class UpdateRequest
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
    }
}
