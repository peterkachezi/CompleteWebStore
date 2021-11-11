using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ElectronicsModule;
using WebStore.Data.EDMX;
using static WebStore.Data.Utils.Enumerations;

namespace WebStore.Data.Services.ElectronicsModule
{
    public class ElectronicService : IElectronicService
    {
        private readonly WinkadStoreEntities contex;
        public ElectronicService(WinkadStoreEntities contex)
        {
            this.contex = contex;
        }
        public async Task<ElectronicDTO> Create(ElectronicDTO electronicDTO)
        {
            try
            {
                var s = new Electronic
                {
                    Id = Guid.NewGuid(),

                    Name = electronicDTO.Name,

                    Description = electronicDTO.Description,

                    ModelNumber = electronicDTO.ModelNumber,

                    SerialNumber = electronicDTO.SerialNumber,

                    Quantity = electronicDTO.Quantity,

                    Status = electronicDTO.Status,

                    CreateDate = DateTime.Now,
                              
                    CreatedBy = electronicDTO.CreatedBy,

                    UpdatedBy = electronicDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                };
                contex.Electronics.Add(s);

                await contex.SaveChangesAsync();

                return electronicDTO;
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
                bool result = false;

                var s = await contex.Electronics.FindAsync(Id);

                if (s != null)
                {
                    contex.Electronics.Remove(s);

                    await contex.SaveChangesAsync();

                    return true;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<ElectronicDTO>> GetAll()
        {
            try
            {
                var electronic = await contex.Electronics.ToListAsync();

                var electronics = new List<ElectronicDTO>();

                foreach (var item in electronic)
                {
                    var data = new ElectronicDTO
                    {
                        Id =item.Id,

                        Name = item.Name,

                        Description = item.Description,

                        ModelNumber = item.ModelNumber,

                        SerialNumber = item.SerialNumber,

                        Quantity = item.Quantity,

                        Status = item.Status,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        UpdatedBy = item.CreatedBy,

                        UpdatedDate = item.UpdatedDate,

                        CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                        StatusDescription = GetDescription((AssetStatus)item.Status),

                    };

                    electronics.Add(data);
                }

                return electronics;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ElectronicDTO> GetById(Guid Id)
        {
            try
            {
                var data = await contex.Electronics.FindAsync(Id);

                return new ElectronicDTO
                {
                    Id = data.Id,

                    Name = data.Name,

                    Description = data.Description,

                    ModelNumber = data.ModelNumber,

                    SerialNumber = data.SerialNumber,

                    Quantity = data.Quantity,

                    Status = data.Status,

                    StatusDescription = GetDescription((AssetStatus)data.Status),

                    CreateDate = data.CreateDate,

                    CreatedBy = data.CreatedBy,

                    UpdatedBy = data.CreatedBy,

                    UpdatedDate = data.UpdatedDate,

                    CreatedByName = data.AspNetUser.FirstName + " " + data.AspNetUser.LastName,
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ElectronicDTO> Update(ElectronicDTO electronicDTO)
        {
            try
            {
                using (var transaction = contex.Database.BeginTransaction())
                {
                    var s = await contex.Electronics.FindAsync(electronicDTO.Id);
                    {
                        s.Name = electronicDTO.Name;

                        s.Description = electronicDTO.Description;

                        s.ModelNumber = electronicDTO.ModelNumber;

                        s.SerialNumber = electronicDTO.SerialNumber;

                        s.Quantity = electronicDTO.Quantity;

                        s.UpdatedBy = electronicDTO.UpdatedBy;

                        s.UpdatedDate = DateTime.Now;

                        s.Status = electronicDTO.Status;

                    };
                    transaction.Commit();

                    await contex.SaveChangesAsync();
                }

                return electronicDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        private static object SyncObj = new object();
        static Dictionary<Enum, string> _enumDescriptionCache = new Dictionary<Enum, string>();
        public static string GetDescription(Enum value)
        {
            if (value == null) return string.Empty;

            lock (SyncObj)
            {
                if (!_enumDescriptionCache.ContainsKey(value))
                {
                    var description = (from m in value.GetType().GetMember(value.ToString())
                                       let attr = (DescriptionAttribute)m.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()
                                       select attr == null ? value.ToString() : attr.Description).FirstOrDefault();

                    _enumDescriptionCache.Add(value, description);
                }
            }

            return _enumDescriptionCache[value];
        }
    }
}
