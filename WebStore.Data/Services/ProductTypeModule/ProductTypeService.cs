using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductTypeModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.ProductTypeModule
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly WinkadStoreEntities context;
        public ProductTypeService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<ProductTypeDTO> Create(ProductTypeDTO productTypeDTO)
        {
            try
            {
                var s = new ProductType
                {
                    Id = Guid.NewGuid(),

                    Name = productTypeDTO.Name,

                    UnitOfMeasureId = productTypeDTO.UnitOfMeasureId,

                    CreateDate = DateTime.Now,

                    UpdatedDate = DateTime.Now,

                    CreatedBy = productTypeDTO.CreatedBy,

                    UpdatedBy = productTypeDTO.CreatedBy,

                };
                context.ProductTypes.Add(s);

                await context.SaveChangesAsync();

                return productTypeDTO;
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

                var s = await context.ProductTypes.FindAsync(Id);
                if (s == null)
                {
                    return false;
                }


                if (s != null)
                {
                    context.ProductTypes.Remove(s);

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

        public async Task<List<ProductTypeDTO>> GetAll()
        {
            try
            {
                var product = await context.ProductTypes.ToListAsync();

                var ProductTypes = new List<ProductTypeDTO>();

                foreach (var item in product)
                {
                    var data = new ProductTypeDTO
                    {
                        Id = item.Id,

                        Name = item.Name +" "+ item.UnitOfMeasure.Name + " " + item.UnitOfMeasure.Unit,
     
                        CreateDate = item.CreateDate,

                        UnitOfMeasureId = item.UnitOfMeasureId,

                        UnitOfMeasureName = item.UnitOfMeasure.Name +" "+ item.UnitOfMeasure.Unit,

                        CreatedBy = item.CreatedBy,

                        CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                    };

                    ProductTypes.Add(data);
                }
                return ProductTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductTypeDTO> GetById(Guid Id)
        {
            try
            {
                var product = await context.ProductTypes.FindAsync(Id);

                return new ProductTypeDTO
                {
                    Id = product.Id,

                    Name = product.Name,

                    UnitOfMeasureId = product.UnitOfMeasureId,

                    UnitOfMeasureName = product.UnitOfMeasure.Name,

                    CreateDate = product.CreateDate,

                    CreatedBy = product.CreatedBy,

                    CreatedByName = product.AspNetUser.FirstName + " " + product.AspNetUser.LastName,

                };

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<bool> Update(ProductTypeDTO productTypeDTO)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.ProductTypes.FindAsync(productTypeDTO.Id);
                    {
                        s.Name = productTypeDTO.Name;

                        s.UpdatedBy = productTypeDTO.UpdatedBy;

                        s.UpdatedDate = DateTime.Now;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }

        }
    }
}
