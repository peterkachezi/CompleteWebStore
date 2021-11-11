using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CountryModule;

namespace WebStore.Data.Services.CountryModule
{
  public  interface ICountryService
    {
        Task<List<CountryDTO>> GetAllCountries();
    }
}
