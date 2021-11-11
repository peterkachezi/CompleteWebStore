using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.SupplierModule;

namespace WebStore.Data.Services.SupplierModule
{
    public class SupplierService : ISupplierService
    {

        public async Task<bool> AddSupplier(SupplierDTO supplierDTO)
        {
            try
            {

                string supplier_number = CustomerNumber.GenerateUniqueNumber();

                supplierDTO.SupplierNumber = "SUP" +""+ supplier_number;

                using (var db = new WinkadStoreEntities())
                {

                    var s = new Supplier
                    {
                        Id = Guid.NewGuid(),

                        FirstName = supplierDTO.FirstName.Substring(0, 1).ToUpper() + supplierDTO.FirstName.Substring(1).ToLower().Trim(),

                        MiddleName = supplierDTO.MiddleName.Substring(0, 1) + supplierDTO.MiddleName.Substring(1).ToLower().Trim(),

                        LastName = supplierDTO.LastName.Substring(0, 1) + supplierDTO.LastName.Substring(1).ToLower().Trim(),

                        Email = supplierDTO.Email.ToLower().Trim(),

                        PhoneNumber = supplierDTO.PhoneNumber,

                        Company = supplierDTO.Company.Substring(0, 1) + supplierDTO.Company.Substring(1).ToLower().Trim(),

                        Website = supplierDTO.Website.ToLower().Trim(),

                        AttachmentFileName = supplierDTO.AttachmentFileName,

                        CreatedDate = DateTime.Today,

                        CountryId = supplierDTO.CountryId,

                        CreatedBy = supplierDTO.CreatedBy,

                        SupplierNumber = supplierDTO.SupplierNumber,
                    };

                    db.Suppliers.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<List<SupplierDTO>> GetAllSuppliers()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var supplier = await db.Suppliers.ToListAsync();

                    var suppliers = new List<SupplierDTO>();

                    foreach (var item in supplier)
                    {
                        var data = new SupplierDTO
                        {
                            Id = item.Id,

                            FirstName = item.FirstName,

                            MiddleName = item.MiddleName,

                            LastName = item.LastName,

                            Email = item.Email,

                            PhoneNumber = item.PhoneNumber,

                            Company = item.Company,

                            Website = item.Website,

                            AttachmentFileName = item.AttachmentFileName,

                            CreatedDate = item.CreatedDate,

                            CountryId = item.CountryId,

                            CreatedBy = item.CreatedBy,

                            SupplierNumber = item.SupplierNumber,

                            CreateByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,
                        };

                        suppliers.Add(data);
                    }
                    return suppliers;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<SupplierDTO> GetSupplierById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var supplier = await db.Suppliers.FindAsync(Id);

                    return new SupplierDTO
                    {
                        Id = supplier.Id,

                        FirstName = supplier.FirstName,

                        MiddleName = supplier.MiddleName,

                        LastName = supplier.LastName,

                        Email = supplier.Email,

                        PhoneNumber = supplier.PhoneNumber,

                        Company = supplier.Company,

                        Website = supplier.Website,

                        AttachmentFileName = supplier.AttachmentFileName,

                        CreatedDate = supplier.CreatedDate,

                        CountryId = supplier.CountryId,

                        CreatedBy = supplier.CreatedBy,

                        SupplierNumber = supplier.SupplierNumber,

                        CreateByName = supplier.AspNetUser.FirstName + " " + supplier.AspNetUser.LastName,
                    };


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> DeleteSupplier(Guid Id)
        {
            using (var db = new WinkadStoreEntities())
            {
                bool result = false;

                var s = await db.Suppliers.FindAsync(Id);

                if (s != null)
                {
                    db.Suppliers.Remove(s);

                    await db.SaveChangesAsync();

                    return true;
                };

                return result;
            }
        }

        public async Task<bool> UpdateSuppliers(Guid Id, SupplierDTO supplierDTO)
        {
            using (var db = new WinkadStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var s = await db.Suppliers.FindAsync(Id);
                    {
                        s.FirstName = supplierDTO.FirstName.Substring(0, 1).ToUpper() + supplierDTO.FirstName.Substring(1).ToLower().Trim();

                        s.MiddleName = supplierDTO.MiddleName.Substring(0, 1) + supplierDTO.MiddleName.Substring(1).ToLower().Trim();

                        s.LastName = supplierDTO.LastName.Substring(0, 1) + supplierDTO.LastName.Substring(1).ToLower().Trim();

                        s.Email = supplierDTO.Email.ToLower().Trim();

                        s.PhoneNumber = supplierDTO.PhoneNumber;

                        s.Company = supplierDTO.Company.Substring(0, 1) + supplierDTO.Company.Substring(1).ToLower().Trim();

                        s.Website = supplierDTO.Website.ToLower().Trim();

                        s.AttachmentFileName = supplierDTO.AttachmentFileName;

                        await db.SaveChangesAsync();

                        transaction.Commit();

                    }
                    return true;
                }
            }
        }
    }
}
