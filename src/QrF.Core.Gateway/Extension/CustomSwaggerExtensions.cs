using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    public class AddTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;

            //先判断是否是匿名访问,
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                //非匿名的方法,链接中添加accesstoken值
                if (!isAnonymous)
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "token",
                        In = "header",//query header body path formData
                        Type = "string",
                        Required = false //是否必选
                    });
                }
            }
        }
    }
}
