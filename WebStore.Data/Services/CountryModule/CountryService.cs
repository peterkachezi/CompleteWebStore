using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CountryModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.CountryModule
{
    public class CountryService : ICountryService
    {
        public async Task<List<CountryDTO>> GetAllCountries()
        {
           using(var db = new WinkadStoreEntities())
            {
                var country = await db.Countries.ToListAsync();

                var countries = new List<CountryDTO>();

                foreach (var item in country)
                {
                    var data = new CountryDTO()
                    {
                        Id=item .Id,

                        Name=item .Name ,

                        CreateDate=item.CreateDate.Value,
                    };

                    countries.Add(data);
                }
                return countries;
            }
        }
    }
}
