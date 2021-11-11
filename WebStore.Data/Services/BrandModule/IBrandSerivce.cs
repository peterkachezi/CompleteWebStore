using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.BrandModule;

namespace WebStore.Data.Services.BrandModule
{
    public interface IBrandSerivce
    {
        Task<bool> AddBrand(BrandDTO brandDTO);
        Task<bool> EditBrand(Guid Id,BrandDTO brandDTO);
        Task<bool> DeleteBrand(Guid Id);
        Task<List<BrandDTO>> GetAllBrands();
        Task <BrandDTO> GetBrandById(Guid Id);
    }
}
