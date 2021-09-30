using Aduaba.Data;
using Aduaba.DTO.Vendor;
using Aduaba.DTOPresentation.Vendor;
using Aduaba.Models;
using Aduaba.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services
{
    public class VendorServices : IVendorServices
    {
        private readonly ApplicationDbContext _context;
        public VendorServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public string AddVendor(AddVendorRequest model)
        {
            var vendorExits = _context.Vendors.FirstOrDefault(c => c.VendorName == model.VendorName);
            if (vendorExits != null)
            {
                return "Vendor already exist, Please check the name of the Vendor";
            }
            _context.Vendors.Add(new Vendor()
            {
                Id = Guid.NewGuid().ToString(),
                VendorName = model.VendorName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateJoined = DateTime.UtcNow
            });
            _context.SaveChanges();
            return "Vendor Added";
        }

        public List<VendorView> GetAllVendors()
        {
            List<VendorView> listOfVendors = new List<VendorView>();
            List<Vendor> availableVendors = _context.Vendors.ToList();

            foreach (var vendor in availableVendors)
            {
                listOfVendors.Add(new VendorView()
                {
                    Id = vendor.Id,
                    VendorName = vendor.VendorName,
                    PhoneNumber = vendor.PhoneNumber,
                    DateAdded = vendor.DateJoined,
                    ModifiedDate = vendor.ModifiedDate,
                    Email = vendor.Email,


                });

            }
            return listOfVendors;


        }

        public VendorView GetVendorId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var foundVendor = _context.Vendors.FirstOrDefault(p => p.Id == id);
                if (foundVendor == null)
                {
                    return null;
                }
                VendorView vendor = (new VendorView()
                {
                    Id = foundVendor.Id,
                    VendorName = foundVendor.VendorName,
                    Email = foundVendor.Email,
                    PhoneNumber = foundVendor.PhoneNumber,
                    DateAdded = foundVendor.DateJoined,
                    ModifiedDate = foundVendor.ModifiedDate

                });
                return vendor;
            }
            return null;

        }

        public VendorView GetVendorByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var foundVendor = _context.Vendors.FirstOrDefault(c => c.VendorName == name);
                if (foundVendor == null)
                {
                    return null;
                }
                VendorView vendor = (new VendorView()
                {
                    Id = Guid.NewGuid().ToString(),
                    VendorName = foundVendor.VendorName,
                    Email = foundVendor.Email,
                    PhoneNumber = foundVendor.PhoneNumber,
                    DateAdded = foundVendor.DateJoined,
                    ModifiedDate = foundVendor.ModifiedDate


                });
                return vendor;
            }
            return null;
        }

        public string UpdateVendor(EditVendorRequest model)
        {
            var oldVendor = _context.Vendors.FirstOrDefault(c => c.Id == model.Id);

            if (oldVendor == null)
            {
                return "Product not found";
            } //Vendor not found

            //it'll work
            if (model.VendorName != null && model.PhoneNumber != null)
            {
                oldVendor.VendorName = model.VendorName;
                oldVendor.PhoneNumber = model.PhoneNumber;


            }
            else if (model.VendorName == null)
            {
                if (model.PhoneNumber != null)
                {
                    oldVendor.PhoneNumber = model.PhoneNumber;
                }
                else
                {
                    return "Please enter values to be updated";
                }

            }
            else if (model.PhoneNumber != null)
            {
                if (model.VendorName != null)
                {
                    oldVendor.VendorName = model.VendorName;
                }
                else
                {
                    return "Please enter values to be updated";
                }

            }
            oldVendor.ModifiedDate = DateTime.UtcNow;
            _context.SaveChanges();
            return "Vendor updated";

        }

        public string DeleteVendors(List<string> names)
        {
            List<Vendor> vendorsToDelete = _context.Vendors.Where(c => names.Contains(c.VendorName)).ToList();

            if (vendorsToDelete.Count != 0)
            {
                _context.Vendors.RemoveRange(vendorsToDelete);
                _context.SaveChanges();

                return "Vendor Deleted Succesfully";
            }

            return "vendor doesn't exist";
        }



    }
}
