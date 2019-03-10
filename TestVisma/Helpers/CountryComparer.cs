using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    class CountryComparer : IEqualityComparer<Country>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Country x, Country y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Code == y.Code && x.Name == y.Name;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Country country)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(country, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashCountrytName = country.Name == null ? 0 : country.Name.GetHashCode();

            //Get hash code for the Code field.
            int hashCountryCode = country.Code.GetHashCode();

            //Calculate the hash code for the product.
            return hashCountrytName ^ hashCountryCode;
        }
        public List<Country> GetDistinctCountries(List<Country> dbCountries, List<Country> apiCountries)
        {
            List<Country> newCountries = new List<Country>();
            foreach (Country country in apiCountries)
            {
                if (!dbCountries.Exists(c => c.Code == country.Code))
                //if (dbCountries.Contains(country))
                {
                    newCountries.Add(country);
                }
            }
            return newCountries;
        }

    }
}
