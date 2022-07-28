using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Narojay.Blog.Extension;

public static class SwaggerExtension
{
    public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Narojay.Blog", Version = "v1", Description = "fuck" });
            var file = Path.Combine(AppContext.BaseDirectory, "Narojay.Blog.xml");
            var path = Path.Combine(AppContext.BaseDirectory, file);
            c.IncludeXmlComments(path, true);
            c.OrderActionsBy(o => o.RelativePath);
            c.OperationFilter<AddResponseHeadersFilter>();
            c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            c.OperationFilter<SecurityRequirementsOperationFilter>();
            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "",
                Name = "Authorization"
            });
        });
        return services;
    }
}