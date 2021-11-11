using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.FurnitureModule;
using WebStore.Data.EDMX;
using static WebStore.Data.Utils.Enumerations;

namespace WebStore.Data.Services.FurnitureModule
{


    public class FurnitureService : IFurnitureService
    {
        private readonly WinkadStoreEntities context;

        public FurnitureService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<FurnitureDTO> Create(FurnitureDTO furnitureDTO)
        {
            try
            {
                var s = new Furniture
                {
                    Id = Guid.NewGuid(),

                    ItemName = furnitureDTO.ItemName,

                    ItemNumber = furnitureDTO.ItemNumber,

                    Manufacturer = furnitureDTO.Manufacturer,

                    Description = furnitureDTO.Description,

                    Quantity = furnitureDTO.Quantity,

                    CreateDate = DateTime.Now,
                              
                    CreatedBy = furnitureDTO.CreatedBy,

                    Status = furnitureDTO.Status,

                    UpdatedBy = furnitureDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                };

                context.Furnitures.Add(s);

                await context.SaveChangesAsync();

                return furnitureDTO;
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

                var s = await context.Furnitures.FindAsync(Id);

                if (s != null)
                {
                    context.Furnitures.Remove(s);

                    await context.SaveChangesAsync();

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

        public async Task<List<FurnitureDTO>> GetAll()
        {
            try
            {
                var furniture = await context.Furnitures.ToListAsync();

                var furnitures = new List<FurnitureDTO>();

                foreach (var item in furniture)
                {
                    var data = new FurnitureDTO
                    {
                        Id = item.Id,

                        ItemName = item.ItemName,

                        ItemNumber = item.ItemNumber,

                        Manufacturer = item.Manufacturer,

                        Description = item.Description,

                        Quantity = item.Quantity,

                        CreateDate = item.CreateDate,
                                
                        CreatedBy = item.CreatedBy,

                        CreatedByName = item.AspNetUser.FirstName +" "+ item.AspNetUser.LastName,

                        Status = item.Status,

                        StatusDescription = GetDescription((AssetStatus)item.Status),

                        UpdatedBy = item.UpdatedBy,

                        UpdatedDate = item.UpdatedDate,
                    };

                    furnitures.Add(data);
                }
                return furnitures;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<FurnitureDTO> GetById(Guid Id)
        {
            try
            {
                var furniture = await context.Furnitures.FindAsync(Id);

                return new FurnitureDTO
                {
                    Id = furniture.Id,

                    ItemName = furniture.ItemName,

                    ItemNumber = furniture.ItemNumber,

                    Manufacturer = furniture.Manufacturer,

                    Description = furniture.Description,

                    Quantity = furniture.Quantity,

                    CreateDate = furniture.CreateDate,

                    CreatedBy = furniture.CreatedBy,

                    Status = furniture.Status,

                    StatusDescription = GetDescription((AssetStatus)furniture.Status),

                    UpdatedBy = furniture.UpdatedBy,

                    UpdatedDate = furniture.UpdatedDate,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<FurnitureDTO> Update(FurnitureDTO furnitureDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Furnitures.FindAsync(furnitureDTO.Id);
                    {

                        s.ItemName = furnitureDTO.ItemName;

                        s.ItemNumber = furnitureDTO.ItemNumber;

                        s.Manufacturer = furnitureDTO.Manufacturer;

                        s.Description = furnitureDTO.Description;

                        s.Quantity = furnitureDTO.Quantity;
                                     
                        s.UpdatedBy = furnitureDTO.UpdatedBy;

                        s.Status = furnitureDTO.Status;

                        s.UpdatedDate = DateTime.Now;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return furnitureDTO;


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
