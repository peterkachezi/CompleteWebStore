using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.BranchModule;

namespace WebStore.Data.Services.BranchModule
{
    public interface IBranchService
    {
        Task<bool> AddBranch(BranchDTO storeDTO);
        Task<bool> EditBranch(Guid Id,BranchDTO storeDTO);
        Task<bool> DeleteBranch(Guid Id);
        Task<List<BranchDTO>> GetAllBranches();
        Task<BranchDTO> GetBranchById(Guid Id);
    }
}
