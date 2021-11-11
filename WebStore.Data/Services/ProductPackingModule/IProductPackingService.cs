using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductPackingModule;

namespace WebStore.Data.Services.ProductPackingModule
{
   public interface IProductPackingService
    {
        Task<ProductPackingDTO> Create(ProductPackingDTO productPackingDTO);
        Task<bool> Delete(Guid Id);
        Task<ProductPackingDTO> Update(ProductPackingDTO productPackingDTO);
        Task<ProductPackingDTO> GetById(Guid Id);
        Task<List<ProductPackingDTO>> GetAll();


    }
}
