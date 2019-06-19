using LogParser.DAL.Context;
using LogParser.Server.Extensions;
using LogParser.Server.MiddleWares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogParser.Server
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
            services.AddDbContext<ApacheLogDbContext>(
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("ApacheLogs")));
            services.RegisterAutoMapper();
            services.RegisterCustomServices();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.ConfigureCustomValidationErrors();

            services.AddTransient<UnhandledExceptionMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<UnhandledExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
