using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductCategoryModule;

namespace WebStore.Data.Services.ProductCategoryModule
{
   public interface IProductCategoryService
    {
        Task<bool> CreateCategory(ProductCategoryDTO productCategoryDTO);
        Task<bool> UdateCategory(Guid Id ,ProductCategoryDTO productCategoryDTO);
        Task<bool> DeleteCategory(Guid Id);
        Task<List<ProductCategoryDTO>> GetAllCategory();
        Task<ProductCategoryDTO> GetCategoryById(Guid Id);
    }
}
