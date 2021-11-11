using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.MpesaPaymenySettingsModule;

namespace WebStore.Data.Services.MpesaPaymenySettingsModule
{
    public interface IMpesaSettingsService
    {
        Task<List<MpesaPaymentSettingDTO>> GetAll();
        Task<MpesaPaymentSettingDTO> GetById(Guid Id);
        Task<MpesaPaymentSettingDTO> Create(MpesaPaymentSettingDTO mpesaPaymentSettingDTO);
        Task<MpesaPaymentSettingDTO> Update(MpesaPaymentSettingDTO mpesaPaymentSettingDTO);
        Task<bool> Delete(Guid Id);

    }
}