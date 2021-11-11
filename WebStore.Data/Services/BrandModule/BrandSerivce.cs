using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.BrandModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.BrandModule
{
    public class BrandSerivce : IBrandSerivce
    {
        public async Task<bool> AddBrand(BrandDTO brandDTO)
        {
            try
            {

                using (var db = new WinkadStoreEntities())
                {
                    var brand = new Brand
                    {
                        Id = Guid.NewGuid(),

                        Name = brandDTO.Name.Substring(0, 1).ToUpper() + brandDTO.Name.Substring(1).ToLower().Trim(),

                        CreatedBy = brandDTO.CreatedBy,

                        CreateDate = DateTime.Now,
                    };

                    db.Brands.Add(brand);

                    return await db.SaveChangesAsync() > 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteBrand(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.Brands.FindAsync(Id);

                    if (s != null)
                    {
                        db.Brands.Remove(s);

                        await db.SaveChangesAsync();

                        return true;
                    }

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BrandDTO>> GetAllBrands()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var brand = await db.Brands.ToListAsync();

                    var brands = new List<BrandDTO>();

                    foreach (var item in brand)
                    {
                        var data = new BrandDTO()
                        {
                            Id = item.Id,

                            Name = item.Name,

                            CreateDate = item.CreateDate,

                            CreatedBy = item.CreatedBy,
                        };

                        brands.Add(data);
                    }
                    return brands;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> EditBrand(Guid Id, BrandDTO brandDTO)
        {
            try
            {

                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var brand = await db.Brands.FindAsync(Id);
                        {
                            brand.Name = brandDTO.Name.Substring(0, 1).ToUpper() + brandDTO.Name.Substring(1).ToLower().Trim();

                            await db.SaveChangesAsync();

                            transaction.Commit();

                            return true;

                        };

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BrandDTO> GetBrandById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var brand = await db.Brands.FindAsync(Id);

                    return new BrandDTO()
                    {
                        Id = brand.Id,

                        Name = brand.Name,

                        CreateDate = brand.CreateDate,

                        CreatedBy = brand.CreatedBy,
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
