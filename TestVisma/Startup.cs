using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestVisma
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           string connectionString = Configuration["connectionStrings:countryDBConnectionString"];
           
            services.AddDbContext<CountryContext>(option =>option.UseSqlServer(connectionString));

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CountryContext countryContext)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            //countryContext.SeedDataToDB();
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<CountryDto, Country>().ReverseMap();
                config.CreateMap<List<CountryDto>, List<Country>>();
            });
            app.UseMvc();

        }
    }
}
