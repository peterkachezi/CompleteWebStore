using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using WebStore.Data.DTOs.TaxModule;

namespace WebStore.Data.Services.TaxModule
{
    public class TaxService : ITaxService
    {
        public Task<bool> AddTax(TaxDTO taxDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTax(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditTax(Guid Id, TaxDTO taxDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaxDTO>> GetAllTaxt()
        {
            throw new NotImplementedException();
        }

        public Task<TaxDTO> GetTaxById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
