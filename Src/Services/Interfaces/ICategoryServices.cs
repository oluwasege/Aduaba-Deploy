using Aduaba.DTO.Product;
using Aduaba.DTOPresentation.DTOProduct;
using Aduaba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aduaba.Services.Interfaces
{
    public interface ICategoryServices
    {
        public string AddCategory(AddCategoryRequest model);      

        public List<CategoryView> GetAllCategories();
        public CategoryView GetCategoryByName(string name);
       
        public string DeleteCategory(List<string> names);
    

        public string EditCategory(EditCategoryRequest editCategoryRequest);
        

    }
}
