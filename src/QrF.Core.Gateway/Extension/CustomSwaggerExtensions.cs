using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QrF.Core.Gateway.Extension
{
    public static class CustomSwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(configuration["Name"], new Info
                {
                    Title = configuration["Title"],
                    Version = configuration["Version"],
                    Description = configuration["Description"]
                });
                options.OperationFilter<AddTokenHeaderParameter>();
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                if (Directory.Exists(basePath))
                {
                    var xmlPath = Path.Combine(basePath, $"{configuration["Name"]}.xml");
                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                }
            });

            return services;
        }
    }
}
