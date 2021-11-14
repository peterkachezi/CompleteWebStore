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
        public async Task<bool> Create(ExpenseDTO expenseDTO)
        {
            try
            {
              
                using (var db = new WinkadStoreEntities())
                {
                    var s = new Expens
                    {
                        Id = expenseDTO.Id,

                        ExpenseTypeId = expenseDTO.ExpenseTypeId,

                        Amount = expenseDTO.Amount,

                        ExpenseDate = expenseDTO.ExpenseDate,

                        IsRecurring = expenseDTO.IsRecurring,

                        RecurringExpense = expenseDTO.RecurringExpense,

                        Description = expenseDTO.Description,

                        ReceiptAttachment = expenseDTO.ReceiptAttachment,

                        ReceiptAttachmentDesc = expenseDTO.ReceiptAttachmentDesc,

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

        public async Task<bool> Delete(Guid Id)
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

        public async Task<bool> Update(ExpenseDTO expenseDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.Expenses.FindAsync(expenseDTO.Id);
                        {


                            s.ExpenseDate = expenseDTO.ExpenseDate;

                            s.IsRecurring = expenseDTO.IsRecurring;

                            s.RecurringExpense = expenseDTO.RecurringExpense;

                            s.Amount = expenseDTO.Amount;

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

        public async Task<List<ExpenseDTO>> GetAll()
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

                            Amount= item.Amount,

                            ExpenseDate = item.ExpenseDate,

                            Description = item.Description,

                            ReceiptAttachment = item.ReceiptAttachment,

                            ReceiptAttachmentDesc = item.ReceiptAttachmentDesc,

                            IsRecurring = item.IsRecurring,

                            RecurringExpense = item.RecurringExpense,

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

        public async Task<ExpenseDTO> GetById(Guid Id)
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

                        Amount = s.Amount,

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
