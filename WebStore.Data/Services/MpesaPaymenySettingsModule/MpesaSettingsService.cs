using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.MpesaPaymenySettingsModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.MpesaPaymenySettingsModule
{
    public class MpesaSettingsService : IMpesaSettingsService
    {
        private readonly WinkadStoreEntities context;

        public MpesaSettingsService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<MpesaPaymentSettingDTO> Create(MpesaPaymentSettingDTO mpesaPaymentSettingDTO)
        {
            try
            {
                var s = new MpesaPaymentSetting
                {
                    Id = Guid.NewGuid(),

                    MpesaKey = mpesaPaymentSettingDTO.MpesaKey,

                    Secrete = mpesaPaymentSettingDTO.Secrete,

                    BusinessShortCode = mpesaPaymentSettingDTO.BusinessShortCode,

                    passkey = mpesaPaymentSettingDTO.passkey,

                    CreateDate = DateTime.Now,

                    CreatedBy = mpesaPaymentSettingDTO.CreatedBy,

                    UpdatedBy = mpesaPaymentSettingDTO.CreatedBy

                };

                context.MpesaPaymentSettings.Add(s);

                await context.SaveChangesAsync();

                return mpesaPaymentSettingDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool results = false;

                var s = await context.MpesaPaymentSettings.FindAsync(Id);

                if (s != null)
                {
                    context.MpesaPaymentSettings.Remove(s);

                    await context.SaveChangesAsync();

                    return true;
                }
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<MpesaPaymentSettingDTO>> GetAll()
        {
            try
            {
                var setting = await context.MpesaPaymentSettings.ToListAsync();

                var settings = new List<MpesaPaymentSettingDTO>();

                foreach (var item in setting)
                {
                    var data = new MpesaPaymentSettingDTO
                    {
                        Id = item.Id,

                        MpesaKey = item.MpesaKey,

                        Secrete = item.Secrete,

                        BusinessShortCode = item.BusinessShortCode,

                        passkey = item.passkey,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy
                    };
                    settings.Add(data);
                }
                return settings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<MpesaPaymentSettingDTO> GetById(Guid Id)
        {
            try
            {
                var setting = await context.MpesaPaymentSettings.FindAsync(Id);

                return new MpesaPaymentSettingDTO
                {
                    Id = setting.Id,

                    MpesaKey = setting.MpesaKey,

                    Secrete = setting.Secrete,

                    BusinessShortCode = setting.BusinessShortCode,

                    passkey = setting.passkey,

                    CreateDate = setting.CreateDate,

                    CreatedBy = setting.CreatedBy
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<MpesaPaymentSettingDTO> Update(MpesaPaymentSettingDTO mpesaPaymentSettingDTO)
        {
            try
            {
               using(var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.MpesaPaymentSettings.FindAsync(mpesaPaymentSettingDTO.Id);
                    {

                        s.MpesaKey = mpesaPaymentSettingDTO.MpesaKey;

                        s.Secrete = mpesaPaymentSettingDTO.Secrete;

                        s.BusinessShortCode = mpesaPaymentSettingDTO.BusinessShortCode;

                        s.passkey = mpesaPaymentSettingDTO.passkey;

                        s.UpdatedBy = mpesaPaymentSettingDTO.UpdatedBy;

                    };
                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return mpesaPaymentSettingDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
