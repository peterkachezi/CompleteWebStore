using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductTypeModule;

namespace WebStore.Data.Services.ProductTypeModule
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeDTO>> GetAll();
        Task<ProductTypeDTO> GetById(Guid Id);
        Task<ProductTypeDTO> Create(ProductTypeDTO  productTypeDTO);
        Task<bool> Update(ProductTypeDTO productTypeDTO);
        Task<bool> Delete(Guid Id);
    }
}
