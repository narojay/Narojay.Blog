using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Filter;
using Newtonsoft.Json;

namespace Narojay.Blog.Extension;

public static class ControllerExtension
{
    public static IServiceCollection AddCustomizedController(this IServiceCollection services)
    {
        services
            .AddControllers(x => { x.Filters.Add<FormatResponseAttribute>(); })
            .AddControllersAsServices()
            .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }
            );
        return services;
    }
}