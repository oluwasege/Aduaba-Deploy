using Aduaba.DTO.ShippingAddress;
using Aduaba.DTOPresentation.ShippingAddress;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IShippingAddressService
    {
        Task<string> AddShippingAddress(AddShippingAddressRequest form);
        Task<List<ShippingAddressResponse>> GetAllShippingAddresses();
        //Task <ShippingAddress> GetShippingAddressById();
        Task<string> DeleteShippingAddress(string shipAdddressId);

    }
}
