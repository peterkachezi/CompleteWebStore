using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ExpenseModule;

namespace WebStore.Data.Services.ExpenseModule
{
    public interface IExpenseService
    {
        Task<bool> Create(ExpenseDTO expenseDTO);
        Task<bool> Update(ExpenseDTO expenseDTO);
        Task<bool> Delete(Guid Id);
        Task<List<ExpenseDTO>> GetAll();
        Task<ExpenseDTO> GetById(Guid Id);
    }
}
