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
        Task<bool> AddExpense(ExpenseDTO expenseDTO);
        Task<bool> EditExpense(Guid Id,ExpenseDTO expenseDTO);
        Task<bool> DeleteExpense(Guid Id);
        Task<List<ExpenseDTO>> GetAllExpenses();
        Task<ExpenseDTO> GetExpenseById(Guid Id);
    }
}
