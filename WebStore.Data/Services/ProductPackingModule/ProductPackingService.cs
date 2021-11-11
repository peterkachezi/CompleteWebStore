using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.ProductPackingModule;

namespace WebStore.Data.Services.ProductPackingModule
{

    public class ProductPackingService : IProductPackingService
    {
        private readonly WinkadStoreEntities context;

        public ProductPackingService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                bool result = false;

                var s = await context.ProductPackagings.FindAsync(Id);

                if (s != null)
                {
                    context.ProductPackagings.Remove(s);

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

        public async Task<ProductPackingDTO> Create(ProductPackingDTO productPackingDTO)
        {
            try
            {
                var s = new ProductPackaging
                {
                    Id = Guid.NewGuid(),

                    Name = productPackingDTO.Name,

                    Unit = productPackingDTO.Unit,

                    CreateDate = DateTime.Now,

                    CreatedBy = productPackingDTO.CreatedBy,
                };

                context.ProductPackagings.Add(s);

                await context.SaveChangesAsync();

                return productPackingDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductPackingDTO> Update(ProductPackingDTO productPackingDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.ProductPackagings.FindAsync(productPackingDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }
                    else
                    {
                        s.Name = productPackingDTO.Name;

                        s.Unit = productPackingDTO.Unit;

                        await context.SaveChangesAsync();

                        transaction.Commit();

                    }

                }
                return productPackingDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductPackingDTO> GetById(Guid Id)
        {
            try
            {
                var packing = await context.ProductPackagings.FindAsync(Id);

                return new ProductPackingDTO
                {
                    Id = packing.Id,

                    Name = packing.Name,

                    Unit = packing.Unit,

                    CreateDate = packing.CreateDate,

                    CreatedBy = packing.CreatedBy
                };


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<ProductPackingDTO>> GetAll()
        {
            try
            {
                var packing = await context.ProductPackagings.ToListAsync();

                var packings = new List<ProductPackingDTO>();

                foreach (var item in packing)
                {
                    var data = new ProductPackingDTO
                    {
                        Id = item.Id,

                        Name = item.Name,
                 
                        Unit = item.Unit,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        CreatedByName = item.AspNetUser.FirstName +" " + item.AspNetUser.LastName,

                    };

                    packings.Add(data);
                }

                return packings;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
