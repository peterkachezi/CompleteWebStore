using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.VehicleModule;

namespace WebStore.Data.Services.VehicleModule
{
   public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAll();
        Task<List<VehicleMakeDTO>> GetAllMakes();
        Task<VehicleMakeDTO> GetMakesById(Guid Id);
        Task<VehicleDTO> GetById(Guid Id);
        Task<VehicleDTO> Create(VehicleDTO vehicleDTO);
        Task<VehicleDTO> Update(VehicleDTO vehicleDTO);
        Task<bool> Delete(Guid Id);
    }
}