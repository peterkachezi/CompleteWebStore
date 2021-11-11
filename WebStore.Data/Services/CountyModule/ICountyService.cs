using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CountyModule;

namespace WebStore.Data.Services.CountyModule
{
    public interface ICountyService
    {
        Task<bool> AddCounty(CountyDTO countyDTO);
        Task<bool> EditCounty(Guid Id,CountyDTO countyDTO);
        Task<bool> DeleteCounty(Guid Id);
        Task<List<CountyDTO>> GetAllCounties();
        Task<CountyDTO> GetCountyById();
    }
}
