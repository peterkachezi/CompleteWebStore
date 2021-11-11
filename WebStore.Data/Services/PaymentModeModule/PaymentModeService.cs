using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.PaymentModeModule;

namespace WebStore.Data.Services.PaymentModeModule
{
    public class PaymentModeService : IPaymentModeService
    {
        public async Task<bool> AddPaymentMode(PaymentModeDTO paymentModeDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = new PaymentMode
                    {
                        Id = Guid.NewGuid(),

                        Name = paymentModeDTO.Name,

                        CreatedBy = paymentModeDTO.CreatedBy,

                        CreateDate = DateTime.Now,
                    };

                    db.PaymentModes.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> DeletePaymentMode(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.PaymentModes.FindAsync(Id);

                    if (s != null)
                    {
                        db.PaymentModes.Remove(s);

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

        public async Task<bool> EditPaymentMode(Guid Id, PaymentModeDTO paymentModeDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.PaymentModes.FindAsync(Id);
                        {

                            s.Name = paymentModeDTO.Name;

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

        public async Task<List<PaymentModeDTO>> GetAllPaymentMode()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var paymentmode = await db.PaymentModes.ToListAsync();

                    var paymentmodes = new List<PaymentModeDTO>();

                    foreach (var item in paymentmode)
                    {
                        var data = new PaymentModeDTO
                        {
                            Id = item.Id,

                            Name = item.Name,

                            CreatedBy = item.CreatedBy,

                            //CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                            CreateDate = item.CreateDate,
                        };

                        paymentmodes.Add(data);
                    }
                    return paymentmodes;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<PaymentModeDTO> GetPaymentModeById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var paymentmode = await db.PaymentModes.FindAsync(Id);

                    return new PaymentModeDTO
                    {
                        Id = paymentmode.Id,

                        Name = paymentmode.Name,

                        CreatedBy = paymentmode.CreatedBy,

                       // CreatedByName = paymentmode.AspNetUser.FirstName + " "+ paymentmode./AspNetUser.LastName,

                        CreateDate = paymentmode.CreateDate,
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
