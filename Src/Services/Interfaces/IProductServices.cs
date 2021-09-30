using Aduaba.DTO.Product;
using Aduaba.DTOPresentation.DTOProduct;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface IProductServices
    {
        public string AddProduct(AddProductRequest model);
        public Task<ProductView> GetProductById(string id);
        public string UpdateProduct(EditProductRequest model);
        public string DeleteProduct(List<string> productIds);
        public ProductView GetProductByName(string name);
        public Task<List<ProductView>> GetAllProducts();





    }
}
