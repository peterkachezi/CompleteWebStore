using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ElectronicsModule;

namespace WebStore.Data.Services.ElectronicsModule
{
    public interface IElectronicService
    {
        Task<List<ElectronicDTO>> GetAll();
        Task<ElectronicDTO> GetById(Guid Id);
        Task<ElectronicDTO> Create(ElectronicDTO electronicDTO);
        Task<ElectronicDTO> Update(ElectronicDTO electronicDTO);
        Task<bool> Delete(Guid Id);
    }
}