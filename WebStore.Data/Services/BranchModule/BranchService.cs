using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.BranchModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.BranchModule
{
    public class BranchService : IBranchService
    {
        public async Task<bool> AddBranch(BranchDTO storeDTO)
        {

            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = new Branch
                    {
                        Id = Guid.NewGuid(),

                        StoreName = storeDTO.StoreName.Substring(0, 1).ToUpper() + storeDTO.StoreName.Substring(1).ToLower().Trim(),

                        Town = storeDTO.Town.Substring(0, 1).ToUpper() + storeDTO.Town.Substring(1).ToLower().Trim(),

                        CountyId = storeDTO.CountyId,

                        CreatedBy = storeDTO.CreatedBy,

                        CreateDate = DateTime.Now,
                    };

                    db.Branches.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> DeleteBranch(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = await db.Branches.FindAsync(Id);

                    bool result = false;

                    if (s != null)
                    {
                        db.Branches.Remove(s);

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

        public async Task<bool> EditBranch(Guid Id, BranchDTO storeDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var s = await db.Branches.FindAsync(Id);
                        {
                            s.StoreName = storeDTO.StoreName;

                            s.Town = storeDTO.Town;

                            s.CountyId = storeDTO.CountyId;

                            db.SaveChanges();

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

        public async Task<List<BranchDTO>> GetAllBranches()
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var store = await db.Branches.ToListAsync();

                    var Branches = new List<BranchDTO>();

                    foreach (var item in store)
                    {
                        var data = new BranchDTO
                        {
                            Id = item.Id,

                            StoreName = item.StoreName,

                            Town = item.Town,

                            CountyId = item.CountyId,

                            CreatedBy = item.CreatedBy,

                            CreateDate = item.CreateDate,
                        };

                        Branches.Add(data);
                    }
                    return Branches;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<BranchDTO> GetBranchById(Guid Id)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var store = await db.Branches.FindAsync(Id);

                    return new BranchDTO
                    {
                        Id = store.Id,

                        StoreName = store.StoreName,

                        Town = store.Town,

                        CountyId = store.CountyId,

                        CreatedBy = store.CreatedBy,

                        CreateDate = store.CreateDate,
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
