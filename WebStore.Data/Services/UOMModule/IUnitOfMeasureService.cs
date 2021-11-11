using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.UOMModule;

namespace WebStore.Data.Services.UOMModule
{
    public interface IUnitOfMeasureService
    {
        Task<bool> Create(UnitOfMeasureDTO unitOfMeasureDTO);
        Task<bool> Update(UnitOfMeasureDTO unitOfMeasureDTO);
        Task<bool> Delete(Guid Id);
        Task<List<UnitOfMeasureDTO>> GetAll();
        Task<UnitOfMeasureDTO> GetById(Guid Id);
    }
}
