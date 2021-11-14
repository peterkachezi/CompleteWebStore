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
        Task<bool> Create(ExpenseTypeDTO expenseTypeDTO);
        Task<bool> Update(ExpenseTypeDTO expenseTypeDTO);
        Task<bool> Delete(Guid Id);
        Task<List<ExpenseTypeDTO>> GetAll();
        Task<ExpenseTypeDTO> GetById(Guid Id);
    }
}
