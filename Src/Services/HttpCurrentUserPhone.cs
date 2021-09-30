using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class HttpCurrentUserPhone:IHttpCurrentUserPhone
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpCurrentUserPhone(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

       
        public string GetUserPhone()
        {
            var userPhone = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);

            return userPhone;
        }
        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

    }
}
