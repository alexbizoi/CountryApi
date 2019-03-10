using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    public class CountryRepository : ICountryRepository
    {
        private static CountryContext _context;
        public CountryRepository(CountryContext context)
        {
            _context = context;
        }
        public async Task<Country> GetCountryAsyncByCode(string Code)
        {
            try
            {
                return await _context.Countries.Where(country => country.Code == Code).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw new DatabaseAccesException(); 
            }
        }
        public List<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }
        public void AddCountry(List<Country> countries)
        {

            if (countries == null)
            {
                throw new ArgumentNullException(nameof(countries));
            }
            try
            {
                _context.Countries.AddRange(countries);
            }
            catch(Exception exception)
            {
                throw new DbUpdateException("Error while trying to save data", exception);
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception exception)
            {
                throw new DbUpdateException("Error while trying to save data", exception);
            }
        }
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_context != null)
        //        {
        //            _context.Dispose();
        //            _context = null;
        //        }
        //    }
        //}

    }
}
