using Aduaba.Data;
using Aduaba.DTO.ShippingAddress;
using Aduaba.DTOPresentation.ShippingAddress;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class ShippingAddressService:IShippingAddressService
    {
        //public string ShippingID { get; set; }
        public List<ShippingAddressService> ShippingAddressess { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShippingAddressService(ApplicationDbContext context,IAuthenticatedUserService authenticatedUser,UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _authenticatedUser = authenticatedUser;
            _userManager = userManager;
        }

        public async Task<string>  AddShippingAddress(AddShippingAddressRequest request)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);

            var UserAddress = new ShippingAddress
            {
                Id = Guid.NewGuid().ToString(),
                ContactPersonsName = request.ContactPersonsName,
                Address = request.Address,
                City = request.City == null ? "" : request.City,
                PhoneNumber = request.PhoneNumber,
                AlternatePhoneNumber = request.AlternatePhoneNumber == null ? "" : request.AlternatePhoneNumber,
                Landmark = request.Landmark == null ? "" : request.Landmark,
                ApplicationUserId = currentUser.Id
            };

            await _context.ShippingAddresses.AddAsync(UserAddress);
            await _context.SaveChangesAsync();
            return "Address Added";
        }



        public async Task<List<ShippingAddressResponse>> GetAllShippingAddresses()
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            var response = new List<ShippingAddressResponse>();
            var shippingAddresses = await _context.ShippingAddresses.Where(s => s.ApplicationUserId == currentUser.Id)
                                                                    .ToListAsync();

            if (shippingAddresses.Count != 0)
            {
                foreach (var addy in shippingAddresses)
                {
                    response.Add(new ShippingAddressResponse()
                    {
                        ContactPersonsName = addy.ContactPersonsName,
                        
                        Address = addy.Address,
                        City = addy.City,
                        PhoneNumber = addy.PhoneNumber,
                        AlternatePhoneNumber = addy.AlternatePhoneNumber,
                        Landmark=addy.Landmark,
                        ShippingAddressId=addy.Id,
                        ApplicationUserId=addy.ApplicationUserId
                        

                    });
                }
                return response;
            }
            return null;
        }

        public async Task<string> DeleteShippingAddress(string shipAdddressId)
        {
            var currentUser = await _userManager.FindByIdAsync(_authenticatedUser.UserId);
            // var shippingAddresses = await _context.ShippingAddresses.FirstOrDefaultAsync(c => c.Id == ShippingID);
            var shippingAddresses = _context.ShippingAddresses.Where(s => s.ApplicationUserId == currentUser.Id).FirstOrDefault(c=>c.Id==shipAdddressId);
            if (shippingAddresses!= null)
            {
                _context.ShippingAddresses.RemoveRange(shippingAddresses);
                await _context.SaveChangesAsync();
                return "Your address has been deleted successfully";
            }
            return null;
        }
    }

    
}
