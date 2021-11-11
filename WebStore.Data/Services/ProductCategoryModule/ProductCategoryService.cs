using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.ProductCategoryModule;

namespace WebStore.Data.Services.ProductCategoryModule
{
    public class ProductCategoryService : IProductCategoryService
    {
        //public async Task<bool> CreateCategory(ProductCategoryDTO productCategoryDTO)
        //{
        //    try
        //    {


        //        using (var db = new WinkadStoreEntities())
        //        {
        //            var s = new ProductCategory
        //            {
        //                Id = Guid.NewGuid(),

        //                Name = productCategoryDTO.Name.Substring(0, 1).ToUpper() + productCategoryDTO.Name.Substring(1).ToLower().Trim(),

        //                CreateDate = DateTime.Now,

        //                CreatedBy = productCategoryDTO.CreatedBy,

        //            };

        //            db.ProductCategories.Add(s);

        //            return await db.SaveChangesAsync() > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return false;
        //    }
        //}

        //public async Task<bool> DeleteCategory(Guid Id)
        //{
        //    try
        //    {
        //        using (var db = new WinkadStoreEntities())
        //        {
        //            bool result = false;

        //            var s = await db.ProductCategories.FindAsync(Id);

        //            if (s != null)
        //            {
        //                db.ProductCategories.Remove(s);

        //                await db.SaveChangesAsync();

        //                return true;
        //            }

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return false;
        //    }
        //}

        //public async Task<List<ProductCategoryDTO>> GetAllCategory()
        //{
        //    try
        //    {
        //        using (var db = new WinkadStoreEntities())
        //        {
        //            var product_category = await db.ProductCategories.ToListAsync();

        //            var product_categories = new List<ProductCategoryDTO>();

        //            foreach (var item in product_category)
        //            {
        //                var data = new ProductCategoryDTO
        //                {
        //                    Id = item.Id,

        //                    Name = item.Name,

        //                    CreateDate = item.CreateDate,

        //                    CreatedBy = item.CreatedBy
        //                };

        //                product_categories.Add(data);

        //            }
        //            return product_categories;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }
        //}

        //public async Task<ProductCategoryDTO> GetCategoryById(Guid Id)
        //{
        //    try
        //    {
        //        using (var db = new WinkadStoreEntities())
        //        {
        //            var product_category = await db.ProductCategories.FindAsync(Id);

        //            return new ProductCategoryDTO
        //            {
        //                Id = product_category.Id,

        //                Name = product_category.Name,

        //                CreateDate = product_category.CreateDate,

        //                CreatedBy = product_category.CreatedBy,
        //            };

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }
        //}

        //public async Task<bool> UdateCategory(Guid Id, ProductCategoryDTO productCategoryDTO)
        //{
        //    try
        //    {
        //        using (var db = new WinkadStoreEntities())
        //        {
        //            using (var transaction = db.Database.BeginTransaction())
        //            {
        //                var product_category = await db.ProductCategories.FindAsync(Id);
        //                {
        //                    product_category.Name = productCategoryDTO.Name;
        //                };

        //                db.SaveChanges();

        //                transaction.Commit();

        //                return true;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return false;
        //    }
        //}
        public Task<bool> CreateCategory(ProductCategoryDTO productCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductCategoryDTO>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategoryDTO> GetCategoryById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UdateCategory(Guid Id, ProductCategoryDTO productCategoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
