using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.VehicleModule;
using WebStore.Data.EDMX;
using static WebStore.Data.Utils.Enumerations;

namespace WebStore.Data.Services.VehicleModule
{
    public class VehicleService : IVehicleService
    {
        private readonly WinkadStoreEntities context;

        public VehicleService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<VehicleDTO> Create(VehicleDTO vehicleDTO)
        {
            try
            {
                var s = new Vehicle
                {

                    Id = Guid.NewGuid(),

                    ModelName = vehicleDTO.ModelName,

                    ModelYear = vehicleDTO.ModelYear,

                    PlateNumber = vehicleDTO.PlateNumber,

                    MakeId = vehicleDTO.MakeId,

                    Capacity = vehicleDTO.Capacity,

                    Owner = vehicleDTO.Owner,

                    DatePurchased = vehicleDTO.DatePurchased,

                    CreateDate = DateTime.Now,

                    CreatedBy = vehicleDTO.CreatedBy,

                    UpdatedBy = vehicleDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                    Status = vehicleDTO.Status,

                };

                context.Vehicles.Add(s);

                await context.SaveChangesAsync();

                return vehicleDTO;
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

                var s = await context.Vehicles.FindAsync(Id);

                if (s != null)
                {
                    context.Vehicles.Remove(s);

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

        public async Task<List<VehicleDTO>> GetAll()
        {
            try
            {
                var vehicle = await context.Vehicles.ToListAsync();

                var vehicles = new List<VehicleDTO>();

                foreach (var item in vehicle)
                {
                    var data = new VehicleDTO
                    {
                        Id = item.Id,

                        ModelName = item.ModelName,

                        ModelYear = item.ModelYear,

                        PlateNumber = item.PlateNumber,

                        MakeId = item.MakeId,

                        MakeName = item.VehicleMake.Name,

                        Capacity = item.Capacity,

                        Owner = item.Owner,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                        UpdatedBy = item.CreatedBy,

                        UpdatedDate = item.UpdatedDate,

                        Status = item.Status,

                        DatePurchased = item.DatePurchased,

                        StatusDescription = GetDescription((AssetStatus)item.Status),
                    };
                    vehicles.Add(data);
                }
                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<VehicleDTO> GetById(Guid Id)
        {
            try
            {
                var vehicle = await context.Vehicles.FindAsync(Id);

                return new VehicleDTO
                {
                    Id = vehicle.Id,

                    ModelName = vehicle.ModelName,

                    ModelYear = vehicle.ModelYear,

                    PlateNumber = vehicle.PlateNumber,

                    MakeId = vehicle.MakeId,

                    MakeName = vehicle.VehicleMake.Name,

                    Capacity = vehicle.Capacity,

                    Owner = vehicle.Owner,

                    CreateDate = vehicle.CreateDate,

                    CreatedBy = vehicle.CreatedBy,

                    CreatedByName = vehicle.AspNetUser.FirstName + " " + vehicle.AspNetUser.LastName,

                    UpdatedBy = vehicle.UpdatedBy,

                    UpdatedDate = vehicle.UpdatedDate,

                    Status = vehicle.Status,

                    DatePurchased = vehicle.DatePurchased,

                    StatusDescription = GetDescription((AssetStatus)vehicle.Status),
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<VehicleDTO> Update(VehicleDTO vehicleDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Vehicles.FindAsync(vehicleDTO.Id);
                    {
                        s.ModelName = vehicleDTO.ModelName;

                        s.ModelYear = vehicleDTO.ModelYear;

                        s.PlateNumber = vehicleDTO.PlateNumber;

                        s.MakeId = vehicleDTO.MakeId;

                        s.Capacity = vehicleDTO.Capacity;

                        s.Owner = vehicleDTO.Owner;

                        s.UpdatedBy = vehicleDTO.UpdatedBy;

                        s.UpdatedDate = DateTime.Now;

                        s.Status = vehicleDTO.Status;

                        s.DatePurchased = vehicleDTO.DatePurchased;

                    };
                    transaction.Commit();

                    await context.SaveChangesAsync();
                }

                return vehicleDTO;
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

        public async Task<List<VehicleMakeDTO>> GetAllMakes()
        {
            try
            {
                var make = await context.VehicleMakes.ToListAsync();

                var makes = new List<VehicleMakeDTO>();

                foreach (var item in make)
                {
                    var data = new VehicleMakeDTO
                    {
                        Id = item.Id,

                        Code = item.Code,

                        Name = item.Name,
                    };

                    makes.Add(data);
                }

                return makes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<VehicleMakeDTO> GetMakesById(Guid Id)
        {
            try
            {
                var make = await context.VehicleMakes.FindAsync(Id);

                return new VehicleMakeDTO
                {
                    Id = make.Id,

                    Code = make.Code,

                    Name = make.Name,
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
