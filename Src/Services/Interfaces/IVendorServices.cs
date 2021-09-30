using Aduaba.DTO.Vendor;
using Aduaba.DTOPresentation.Vendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IVendorServices
    {
        public string AddVendor(AddVendorRequest model);
        public List<VendorView> GetAllVendors();
        public VendorView GetVendorId(string id);
        public VendorView GetVendorByName(string name);
        public string UpdateVendor(EditVendorRequest model);
        public string DeleteVendors(List<string> names);
    }
}
