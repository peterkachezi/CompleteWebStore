using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebStore.Data.DTOs.ElectronicsModule;
using WebStore.Data.DTOs.FurnitureModule;
using WebStore.Data.DTOs.VehicleModule;
using WebStore.Data.Services.ElectronicsModule;
using WebStore.Data.Services.FurnitureModule;
using WebStore.Data.Services.VehicleModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class AssetsManagerController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IFurnitureService furnitureService;
        private readonly IElectronicService electronicService;

        public AssetsManagerController(IVehicleService vehicleService, IFurnitureService furnitureService, IElectronicService electronicService)
        {
            this.vehicleService = vehicleService;
            this.furnitureService = furnitureService;
            this.electronicService = electronicService;
        }
        public ActionResult Index()
        {
            return View();
        }

        #region Furnitures
        public ActionResult Furnitures()
        {

            return View();
        }
        public async Task<ActionResult> GetFurnitures()
        {
            var furnitures = await furnitureService.GetAll();

            return Json(new { data = furnitures }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CreateFurniture(FurnitureDTO furnitureDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                furnitureDTO.CreatedBy = user;

                var results = await furnitureService.Create(furnitureDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  has been successfully saved!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to save the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> UpdateFurniture(FurnitureDTO furnitureDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                furnitureDTO.UpdatedBy = user;

                var results = await furnitureService.Update(furnitureDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  has been successfully updated !" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> DeleteFurniture(Guid Id)
        {
            try
            {
                var results = await furnitureService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> GetFurnitureById(Guid Id)
        {
            var data = await furnitureService.GetById(Id);

            if (data != null)
            {
                FurnitureDTO file = new FurnitureDTO
                {
                    Id = data.Id,

                    ItemName = data.ItemName,

                    ItemNumber = data.ItemNumber,

                    Manufacturer = data.Manufacturer,

                    Description = data.Description,

                    CreateDate = data.CreateDate,

                    CreatedBy = data.CreatedBy,

                    Status = data.Status,

                    StatusDescription = data.StatusDescription,

                    UpdatedBy = data.UpdatedBy,

                    UpdatedDate = data.UpdatedDate,

                    Quantity = data.Quantity,
                };

                return Json(new { data = file }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Vehicles
        public async Task<ActionResult> Vehicles()
        {
            ViewBag.Makes = await vehicleService.GetAllMakes();

            return View();
        }
        public async Task<ActionResult> GetMakes()
        {
            var make = await vehicleService.GetAllMakes();

            return Json(make.ToList().Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetVehicles()
        {
            var vehicles = await vehicleService.GetAll();

            return Json(new { data = vehicles.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CreateVehicles(VehicleDTO vehicleDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                vehicleDTO.CreatedBy = user;

                var results = await vehicleService.Create(vehicleDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  has been successfully saved!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to save the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> UpdateVehicle(VehicleDTO vehicleDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                vehicleDTO.UpdatedBy = user;

                var results = await vehicleService.Update(vehicleDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> DeleteVehicle(Guid Id)
        {
            try
            {
                var results = await vehicleService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> GetVehicleById(Guid Id)
        {
            var data = await vehicleService.GetById(Id);

            if (data != null)
            {
                VehicleDTO file = new VehicleDTO
                {
                    Id = data.Id,

                    ModelName = data.ModelName,

                    ModelYear = data.ModelYear,

                    PlateNumber = data.PlateNumber,

                    DatePurchased = data.DatePurchased,

                    MakeId = data.MakeId,

                    Capacity = data.Capacity,

                    Owner = data.Owner,

                    CreateDate = data.CreateDate,

                    CreatedBy = data.CreatedBy,

                    CreatedByName = data.CreatedByName,

                    UpdatedBy = data.UpdatedBy,

                    UpdatedDate = data.UpdatedDate,

                    Status = data.Status,

                    StatusDescription = data.StatusDescription,

                };

                return Json(new { data = file }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Electronics
        public ActionResult Electronics()
        {


            return View();
        }
        public async Task<ActionResult> GetElectronics()
        {
            var electronics = await electronicService.GetAll();

            return Json(new { data = electronics }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetElectronicByIdAsync(Guid Id)
        {
            var data = await electronicService.GetById(Id);

            if (data != null)
            {
                ElectronicDTO file = new ElectronicDTO
                {
                    Id = data.Id,

                    Name = data.Name,

                    Description = data.Description,

                    ModelNumber = data.ModelNumber,

                    SerialNumber = data.SerialNumber,

                    Quantity = data.Quantity,

                    Status = data.Status,

                    StatusDescription = data.StatusDescription,

                    CreateDate = data.CreateDate,

                    CreatedBy = data.CreatedBy,

                    UpdatedBy = data.CreatedBy,

                    UpdatedDate = data.UpdatedDate,

                    CreatedByName = data.CreatedByName,
                };

                return Json(new { data = file }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CreateElectronics(ElectronicDTO electronicDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                electronicDTO.CreatedBy = user;

                var results = await electronicService.Create(electronicDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  has been successfully saved!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to save the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> UpdateElectronics(ElectronicDTO electronicDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                electronicDTO.UpdatedBy = user;

                var results = await electronicService.Update(electronicDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "The record  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> DeleteElectronics(Guid Id)
        {
            try
            {
                var results = await electronicService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        #endregion
    }
}