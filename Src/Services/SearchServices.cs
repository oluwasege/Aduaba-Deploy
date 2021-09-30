using Aduaba.Data;
using Aduaba.DTOPresentation;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class SearchServices : ISearchServices
    {
        private readonly ApplicationDbContext _context;

        public SearchServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> SearchProduct(string name)
        {
            IQueryable<Product> query = _context.Products;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name)
                || e.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Vendor>> SearchVendor(string name)
        {
            IQueryable<Vendor> query = _context.Vendors;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.VendorName.Contains(name)
                || e.VendorName.Contains(name));
            }

            return await query.ToListAsync();
        }

    }

}
