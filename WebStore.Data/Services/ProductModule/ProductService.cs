using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using WebStore.Data.DTOs.ProductModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.Services.ProductModule
{
    public class ProductService : IProductService
    {

        private readonly WinkadStoreEntities context;

        public ProductService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            try
            {
                string productcode = CustomerNumber.GenerateUniqueNumber();

                productDTO.ProductCode = productcode;

                var profit = productDTO.SellingPrice - productDTO.CostPrice;

                var s = new Product
                {
                    Id = Guid.NewGuid(),

                    Name = productDTO.Name,

                    ProductCode = productDTO.ProductCode,

                    CostPrice = productDTO.CostPrice,

                    SellingPrice = productDTO.SellingPrice,

                    ReOrderLevel = productDTO.ReOrderLevel,

                    ExpectedProfit = profit,

                    CreateDate = DateTime.Now,

                    CreatedBy = productDTO.CreatedBy,

                    UpdateBy = productDTO.CreatedBy,

                    UpdatedDate = DateTime.Now,

                    Status = true,

                    PackagingId = productDTO.PackagingId,

                };

                context.Products.Add(s);

                await context.SaveChangesAsync();

                return productDTO;
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

                var s = await context.Products.FindAsync(Id);

                if (s != null)
                {
                    context.Products.Remove(s);

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

        public async Task<List<ProductDTO>> GetAll()
        {
            try
            {
                var product = await context.Products.ToListAsync();

                var products = new List<ProductDTO>();

                foreach (var item in product)
                {
                    var data = new ProductDTO
                    {
                        Id = item.Id,

                        PackagingId = item.PackagingId,

                        Name = item.Name,

                        ProductCode = item.ProductCode,

                        Quantity = item.Quantity,

                        CostPrice = item.CostPrice,

                        SellingPrice = item.SellingPrice,

                        ReOrderLevel = item.ReOrderLevel,

                        ExpectedProfit = item.ExpectedProfit,

                        CreateDate = item.CreateDate,

                        CreatedBy = item.CreatedBy,

                        UpdateBy = item.CreatedBy,

                        UpdatedDate = item.UpdatedDate,

                        Status = item.Status,

                        CreatedByName = item.AspNetUser.FirstName + " " + item.AspNetUser.LastName,

                        ProductName=item.Name +" "+ item.ProductPackaging.Name + " " + item.ProductPackaging.Unit,
                    };

                    products.Add(data);
                }

                return products;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductDTO> GetById(Guid Id)
        {
            try
            {
                var product = await context.Products.FindAsync(Id);

                return new ProductDTO
                {
                    Id = product.Id,

                    Name = product.Name,

                    ProductCode = product.ProductCode,

                    Quantity = product.Quantity,

                    CostPrice = product.CostPrice,

                    SellingPrice = product.SellingPrice,

                    ReOrderLevel = product.ReOrderLevel,

                    ExpectedProfit = product.ExpectedProfit,

                    CreateDate = product.CreateDate,

                    CreatedBy = product.CreatedBy,

                    UpdateBy = product.CreatedBy,

                    UpdatedDate = product.UpdatedDate,

                    Status = product.Status,

                    CreatedByName = product.AspNetUser.FirstName + " " + product.AspNetUser.LastName,

                    ProductName = product.Name + " " + product.ProductPackaging.Name + " " + product.ProductPackaging.Unit,
                };
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            try
            {
                var profit = productDTO.SellingPrice - productDTO.CostPrice;

                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Products.FindAsync(productDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }

                    else

                    {
                        s.CostPrice = productDTO.CostPrice;

                        s.SellingPrice = productDTO.SellingPrice;

                        s.ReOrderLevel = productDTO.ReOrderLevel;

                        s.ExpectedProfit = profit;

                        s.UpdateBy = productDTO.UpdateBy;

                        s.UpdatedDate = DateTime.Now;
                                       

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();

                    return productDTO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ProductDTO> UpdateStock(ProductDTO productDTO)
        {
            try
            {    

                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Products.FindAsync(productDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }

                    else

                    {
                        s.Quantity = s.Quantity +  productDTO.NewQuantity;
                                
                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();

                    return productDTO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ProductDTO> RemoveStock(ProductDTO productDTO)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var s = await context.Products.FindAsync(productDTO.Id);

                    if (s == null)
                    {
                        return null;
                    }

                    else

                    {
                        s.Quantity = s.Quantity - productDTO.NewQuantity;

                    };

                    transaction.Commit();

                    await context.SaveChangesAsync();

                    return productDTO;
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
