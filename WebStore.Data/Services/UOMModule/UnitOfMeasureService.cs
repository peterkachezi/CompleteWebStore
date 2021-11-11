using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.UOMModule;

namespace WebStore.Data.Services.UOMModule
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        public async Task<bool> Create(UnitOfMeasureDTO unitOfMeasureDTO)
        {
            try

            {       
                using (var db = new WinkadStoreEntities())
                {
                    var s = new UnitOfMeasure
                    {
                        Id = Guid.NewGuid(),

                        Name = unitOfMeasureDTO.Name.ToUpper(),

                        Unit = unitOfMeasureDTO.Unit,

                        CreatedBy = unitOfMeasureDTO.CreatedBy,

                        CreateDate = DateTime.Now,
                    };

                    db.UnitOfMeasures.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> Delete(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    bool result = false;

                    var s = await db.UnitOfMeasures.FindAsync(Id);

                    if (s != null)
                    {
                        db.UnitOfMeasures.Remove(s);

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

        public async Task<bool> Update(UnitOfMeasureDTO unitOfMeasureDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using(var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.UnitOfMeasures.FindAsync(unitOfMeasureDTO.Id);

                        {
                            s.Name = unitOfMeasureDTO.Name;

                            s.Unit = unitOfMeasureDTO.Unit;

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

        public async Task<List<UnitOfMeasureDTO>> GetAll()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var UnitOfMeasure = await db.UnitOfMeasures.ToListAsync();

                    var UnitOfMeasures = new List<UnitOfMeasureDTO>();

                    foreach (var item in UnitOfMeasure)
                    {
                        var data = new UnitOfMeasureDTO
                        {
                            Id = item.Id,

                            Name = item.Name,

                            FullName = item.Name + " " + item.Unit,

                            Unit = item.Unit,

                            CreatedBy = item.CreatedBy,

                            CreatedByName = item.AspNetUser.FirstName +" "+ item.AspNetUser.LastName,

                            CreateDate = item.CreateDate,
                        };

                        UnitOfMeasures.Add(data);
                    }

                    return UnitOfMeasures;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<UnitOfMeasureDTO> GetById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var UnitOfMeasure = await db.UnitOfMeasures.FindAsync(Id);

                    return new UnitOfMeasureDTO
                    {
                        Id = UnitOfMeasure.Id,

                        Name = UnitOfMeasure.Name,

                        Unit = UnitOfMeasure.Unit,

                        CreatedByName = UnitOfMeasure.AspNetUser.FirstName + " " + UnitOfMeasure.AspNetUser.LastName,

                        CreatedBy = UnitOfMeasure.CreatedBy,

                        CreateDate = UnitOfMeasure.CreateDate,
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
