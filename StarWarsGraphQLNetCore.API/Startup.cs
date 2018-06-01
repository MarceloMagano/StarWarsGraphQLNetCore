using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarWarsGraphQLNetCore.API.Models;
using StarWarsGraphQLNetCore.Core.Data;
using StarWarsGraphQLNetCore.Data.EntityFramework;
using StarWarsGraphQLNetCore.Data.EntityFramework.Seed;
using StarWarsGraphQLNetCore.Data.InMemory;

namespace StarWarsGraphQLNetCore.API
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
            services.AddMvc();

            services.AddTransient<StarWarsQuery>();
            services.AddTransient<IDroidRepository, DroidRepository>();
            //config DI to run data seed
            services.AddDbContext<StarWarsContext>(options=> options.UseSqlServer(Configuration["ConnectionStrings:StarWarsDatabaseConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, StarWarsContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}
