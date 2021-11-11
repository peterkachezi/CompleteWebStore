using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.SupplierModule;

namespace WebStore.Data.Services.SupplierModule
{
    public interface ISupplierService
    {
        Task<bool> AddSupplier(SupplierDTO supplierDTO);
        Task<List<SupplierDTO>> GetAllSuppliers();

        Task<SupplierDTO> GetSupplierById(Guid Id);
        Task<bool> DeleteSupplier(Guid Id);
        Task<bool> UpdateSuppliers(Guid Id, SupplierDTO supplierDTO);

    }
}
