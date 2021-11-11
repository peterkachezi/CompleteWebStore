using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.FurnitureModule;

namespace WebStore.Data.Services.FurnitureModule
{
    public interface IFurnitureService
    {
        Task<List<FurnitureDTO>> GetAll();
        Task<FurnitureDTO> GetById(Guid Id);
        Task<bool> Delete(Guid Id);
        Task<FurnitureDTO> Create(FurnitureDTO furnitureDTO);
        Task<FurnitureDTO> Update(FurnitureDTO furnitureDTO);
    }
}