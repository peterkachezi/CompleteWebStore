using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.TaxModule;

namespace WebStore.Data.Services.TaxModule
{
    public interface ITaxService
    {
        Task<bool> AddTax(TaxDTO taxDTO);
        Task<bool> EditTax(Guid Id, TaxDTO taxDTO);
        Task<bool> DeleteTax(Guid Id);
        Task<TaxDTO> GetTaxById(Guid Id);
        Task<List<TaxDTO>> GetAllTaxt();
    }
}
