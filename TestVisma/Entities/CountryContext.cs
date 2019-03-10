using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestVisma
{
    public class CountryContext : DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options) : base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }
        public DbSet<Country> Countries { get; set; }
    }
}
