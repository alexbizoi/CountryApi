using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestVisma
{
    //  [Produces("application/json")]
    [Route("api/Country")]
    public class CountriesController : Controller
    {
        private ICountryRepository _context;
        public CountriesController(ICountryRepository context)
        {
            _context = context;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                Country country = await _context.GetCountryAsyncByCode(code);
                if (country == null)
                {
                    return NotFound($"Country with the code {code} does not exist in system");

                }
                return Ok(country);
            }
            catch (DatabaseAccesException exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unknown error");
            }
        }
        [HttpPost()]
        [ActionName("RefreshList")]
        public async Task<IActionResult> Post()
        {
            try
            {
                HttpRequestsToAPI httpServices = new HttpRequestsToAPI();
                CountryComparer countryComparer = new CountryComparer();
                List<Country> countries = await httpServices.GetResources();
                List<Country> dbCountries = _context.GetCountries();
                countries.Sort((x, y) => x.Code.CompareTo(y.Code));
                var newCountries = new List<Country>();

                var countriesAlreadyInDB = dbCountries.SequenceEqual(countries, countryComparer);
                if (!countriesAlreadyInDB)
                {
                    newCountries = countryComparer.GetDistinctCountries(dbCountries, countries);
                    if (newCountries.Count > 0)
                    {
                        _context.AddCountry(newCountries);
                        if (await _context.SaveChangesAsync())
                        {
                            return Ok(newCountries);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, "Error while trying to save data");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status204NoContent);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }
            }
            catch (DbUpdateException exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            catch (FetchDataFromAPIException exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unknown error");
            }

        }
    }
}
