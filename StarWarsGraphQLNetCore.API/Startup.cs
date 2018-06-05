using GraphQL;
using GraphQL.Types;
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
        public Startup(IHostingEnvironment env)
        {
            // creating ConfigurationBuilder using appsettings and enviroment based appsettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Env = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<StarWarsQuery>();
            services.AddTransient<IDroidRepository, DroidRepository>();


            if (Env.IsEnvironment("Test"))
            {
                services.AddDbContext<StarWarsContext>(options => options.UseInMemoryDatabase(databaseName: "StarWars"));
            }
            else
            {
                //config DI to run data seed
                services.AddDbContext<StarWarsContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:StarWarsDatabaseConnection"]));
            }

            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            ServiceProvider sp = services.BuildServiceProvider();
            services.AddTransient<ISchema>(_ => new Schema { Query = sp.GetService<StarWarsQuery>() });
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
