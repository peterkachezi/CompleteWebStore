using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ExpenseModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.ExpenseModule
{
    public class ExpenseService : IExpenseService
    {
        public async Task<bool> AddExpense(ExpenseDTO expenseDTO)
        {
            try
            {
              
                using (var db = new WinkadStoreEntities())
                {
                    var s = new Expens
                    {
                        Id = Guid.NewGuid(),

                        ExpenseTypeId = expenseDTO.ExpenseTypeId,

                        ExpenseAmount = expenseDTO.ExpenseAmount,

                        ExpenseDate = expenseDTO.ExpenseDate,

                        Description = expenseDTO.Description,

                        ReceiptAttachment = expenseDTO.ReceiptAttachment,

                        CreateDate=DateTime.Now,

                        CreatedBy = expenseDTO.CreatedBy,

                    };

                    db.Expenses.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> DeleteExpense(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.Expenses.FindAsync(Id);

                    if (s != null)
                    {
                        db.Expenses.Remove(s);

                        await db.SaveChangesAsync();

                        return true;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> EditExpense(Guid Id, ExpenseDTO expenseDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.Expenses.FindAsync(Id);
                        {

                            s.ExpenseAmount = expenseDTO.ExpenseAmount;

                            s.ExpenseDate = expenseDTO.ExpenseDate;

                            s.Description = expenseDTO.Description;

                            s.ReceiptAttachment = expenseDTO.ReceiptAttachment;

                            await db.SaveChangesAsync();

                            transaction.Commit();

                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<ExpenseDTO>> GetAllExpenses()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var expense = await db.Expenses.ToListAsync();

                    var expenses = new List<ExpenseDTO>();

                    foreach (var item in expense)
                    {
                        var data = new ExpenseDTO()
                        {
                            Id = item.Id,

                            ExpenseTypeId = item.ExpenseTypeId,

                            ExpenseTypeName = item.ExpenseType.Name,

                            ExpenseAmount = item.ExpenseAmount,

                            ExpenseDate = item.ExpenseDate,

                            Description = item.Description,

                            ReceiptAttachment = item.ReceiptAttachment,

                            CreateDate = item.CreateDate,

                            CreatedBy = item.CreatedBy,

                        };

                        expenses.Add(data);
                    }
                    return expenses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ExpenseDTO> GetExpenseById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = await db.Expenses.FindAsync(Id);

                    return new ExpenseDTO
                    {
                        Id = s.Id,

                        ExpenseTypeId = s.ExpenseTypeId,

                        ExpenseAmount = s.ExpenseAmount,

                        ExpenseDate = s.ExpenseDate,

                        Description = s.Description,

                        ReceiptAttachment = s.ReceiptAttachment,

                        CreateDate = s.CreateDate,

                        CreatedBy = s.CreatedBy,

                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
