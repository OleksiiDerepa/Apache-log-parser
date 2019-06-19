using System;
using System.Linq;

using AutoMapper;

using LogParser.BLL.MappingProfiles;
using LogParser.BLL.Services;
using LogParser.BLL.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace LogParser.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IHostService, HostService>();
            services.AddScoped<ILogsService, LogsService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RequestUrlDtoProfile>();
            });
        }

        public static void ConfigureCustomValidationErrors(this IServiceCollection services)
        {
            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Message = "Validation errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });
        }
    }
}
