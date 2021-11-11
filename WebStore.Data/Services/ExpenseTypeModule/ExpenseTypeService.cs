using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ExpenseTypeModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.ExpenseTypeModule
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        public async Task<bool> AddExpenseType(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = new ExpenseType
                    {
                        Id = Guid.NewGuid(),

                        Name = expenseTypeDTO.Name.Substring(0, 1).ToUpper() + expenseTypeDTO.Name.Substring(1).ToLower().Trim(),

                        CreateDate = DateTime.Now,

                        CreatedBy = expenseTypeDTO.CreatedBy,
                    };

                    db.ExpenseTypes.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> DeleteExpenseType(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.ExpenseTypes.FindAsync(Id);

                    if (s != null)
                    {
                        db.ExpenseTypes.Remove(s);

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

        public async Task<bool> EditExpenseType(Guid Id, ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.ExpenseTypes.FindAsync(Id);
                        {
                            s.Name = expenseTypeDTO.Name;

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

        public async Task<List<ExpenseTypeDTO>> GetAllExpenseTypes()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var expensetype = await db.ExpenseTypes.ToListAsync();

                    var expensetypes = new List<ExpenseTypeDTO>();

                    foreach (var item in expensetype)
                    {
                        var data = new ExpenseTypeDTO()
                        {
                            Id = item.Id,

                            Name = item.Name,

                            CreatedBy = item.CreatedBy,

                            CreateDate = item.CreateDate
                        };

                        expensetypes.Add(data);
                    }
                    return expensetypes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ExpenseTypeDTO> GetExpenseById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = await db.ExpenseTypes.FindAsync(Id);

                    return new ExpenseTypeDTO
                    {
                        Id = s.Id,

                        Name = s.Name,

                        CreatedBy = s.CreatedBy,

                        CreateDate = s.CreateDate
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
