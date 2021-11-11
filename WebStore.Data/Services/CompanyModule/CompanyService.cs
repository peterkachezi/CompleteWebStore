using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CompanyModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.CompanyModule
{
    public class CompanyService : ICompanyService
    {
        public async Task<bool> CreateCompany(CompanyDTO companyDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = new Company
                    {
                        Id = Guid.NewGuid(),

                        Name = companyDTO.Name,

                        Address = companyDTO.Address,

                        PhoneNumber = companyDTO.PhoneNumber,

                        Email = companyDTO.Email,

                        Website = companyDTO.Website,

                        PhysicalAddress = companyDTO.PhysicalAddress,

                        CreateDate = DateTime.Now,

                        CreatedBy = companyDTO.CreatedBy,

                        CountryId = companyDTO.CountryId,

                        IsActive = true,
                    };
                    db.Companies.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> DeleteCompany(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.Companies.FindAsync(Id);

                    if (s != null)
                    {
                        db.Companies.Remove(s);

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

        public async Task<bool> EditCompany(Guid Id, CompanyDTO companyDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.Companies.FindAsync(Id);
                        {
                            s.Name = companyDTO.Name;

                            s.Address = companyDTO.Address;

                            s.PhoneNumber = companyDTO.PhoneNumber;

                            s.Email = companyDTO.Email;

                            s.Website = companyDTO.Website;

                            s.PhysicalAddress = companyDTO.PhysicalAddress;

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

        public async Task<List<CompanyDTO>> GetAllCompany()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var company =await (from C in db.Companies

                                  // join U in db.AspNetUsers on C.CreatedBy equals U.Id

                                   select new CompanyDTO
                                   {

                                       Id = C.Id,

                                       Name = C.Name,

                                       Address = C.Address,

                                       PhoneNumber = C.PhoneNumber,

                                       Email = C.Email,

                                       Website = C.Website,

                                       PhysicalAddress = C.PhysicalAddress,

                                       CreateDate = C.CreateDate,

                                       CreatedBy = C.CreatedBy,

                                      // CreatedByName= U.FirstName + " " + U.LastName,

                                       CountryId = C.CountryId,

                                       IsActive = C.IsActive,

                                   }).ToListAsync();

                    return company;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task <CompanyDTO> GetCompanyById()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var company = await (from C in db.Companies

                                   //join U in db.AspNetUsers on C.CreatedBy equals U.Id

                                   join d in db.Countries on C.CountryId equals d.Id
                                                   
                                   select new CompanyDTO
                                   {

                                       Id = C.Id,

                                       Name = C.Name,

                                       Address = C.Address,

                                       PhoneNumber = C.PhoneNumber,

                                       Email = C.Email,

                                       Website = C.Website,

                                       PhysicalAddress = C.PhysicalAddress,

                                       CreateDate = C.CreateDate,

                                       CreatedBy = C.CreatedBy,

                                       //CreatedByName = U.FirstName + " " + U.LastName,

                                       CountryId = C.CountryId,

                                       CountryName =d.Name,

                                       IsActive = C.IsActive,

                                   }).FirstOrDefaultAsync();

                    return company;

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
