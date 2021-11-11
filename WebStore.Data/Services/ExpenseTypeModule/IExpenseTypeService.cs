using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ExpenseTypeModule;

namespace WebStore.Data.Services.ExpenseTypeModule
{
  public  interface IExpenseTypeService
    {
        Task<bool> AddExpenseType(ExpenseTypeDTO expenseTypeDTO);
        Task<bool> EditExpenseType(Guid Id,ExpenseTypeDTO expenseTypeDTO);
        Task<bool> DeleteExpenseType(Guid Id);
        Task<List<ExpenseTypeDTO>> GetAllExpenseTypes();
        Task<ExpenseTypeDTO> GetExpenseById(Guid Id);
    }
}
