
using Aduaba.Data;
using Aduaba.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class FindShippingAddress
    {
        public string ShippingID { get; set; }
        public List<ShippingAddressService> ShippingAddressess { get; set; }
        private readonly ApplicationDbContext _context;
        public FindShippingAddress(ApplicationDbContext context)
        {
            _context = context;
        }
        public static FindShippingAddress GetShippingAddressId(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string shippingId = session.GetString("ShippingId") ?? Guid.NewGuid().ToString();

            session.SetString("ShippingId", shippingId);
            
            return new FindShippingAddress(context) {ShippingID = shippingId };
        }

    }
}
