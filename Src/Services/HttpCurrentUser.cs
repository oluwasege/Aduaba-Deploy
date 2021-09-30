using Aduaba.Data;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class HttpCurrentUser:IHttpCurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

    

        public HttpCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }

        public string GetUserAuthentication()
        {

            var userAuthentication = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Authentication);
            return userAuthentication;
        }

        public string GetUserPhone()
        {
            var userPhone = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.MobilePhone);

            return userPhone;
        }

    }
}



