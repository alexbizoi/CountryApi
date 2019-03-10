using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryAsyncByCode(string Code);
        List<Country> GetCountries();
        void AddCountry(List<Country> country);
        Task<bool> SaveChangesAsync();
    }
}
