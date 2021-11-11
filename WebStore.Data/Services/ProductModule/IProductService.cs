using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductModule;

namespace WebStore.Data.Services.Services.ProductModule
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(Guid Id);
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(Guid Id);
        Task<ProductDTO> UpdateStock(ProductDTO productDTO);
        Task<ProductDTO> RemoveStock(ProductDTO productDTO);
    }
}