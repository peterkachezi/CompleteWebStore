using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CompanyModule;


namespace WebStore.Data.Services.CompanyModule
{
    public interface ICompanyService
    {
        Task<bool> CreateCompany(CompanyDTO companyDTO);
        Task<bool> EditCompany(Guid Id, CompanyDTO companyDTO);
        Task<bool> DeleteCompany(Guid Id);
        Task<List<CompanyDTO>> GetAllCompany();
        Task<CompanyDTO> GetCompanyById();
    }
}
