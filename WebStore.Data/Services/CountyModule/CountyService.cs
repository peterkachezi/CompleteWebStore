using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CountyModule;
using WebStore.Data.EDMX;


namespace WebStore.Data.Services.CountyModule
{
    public class CountyService : ICountyService
    {
        public Task<bool> AddCounty(CountyDTO countyDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCounty(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditCounty(Guid Id, CountyDTO countyDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CountyDTO>> GetAllCounties()
        {
            try
            {
                using(var db = new WinkadStoreEntities())
                {
                    var county = await db.Counties.ToListAsync();

                    var counties = new List<CountyDTO>();

                    foreach (var item in county)
                    {
                        var data = new CountyDTO
                        {
                            Id = item.Id,

                            Name = item.Name,

                            CreateDate = item.CreateDate
                        };
                        counties.Add(data);
                    }

                    return counties;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
          
            }
        }

        public Task<CountyDTO> GetCountyById()
        {
            throw new NotImplementedException();
        }
    }
}
