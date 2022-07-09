using Narojay.Blog.Work.Filter;
using Newtonsoft.Json;

namespace Narojay.Blog.Work.Extension;

public static class ControllerExtension
{
    public static IServiceCollection AddCustomizedController(this IServiceCollection services)
    {
        services
            .AddControllers(x =>
            {
                x.Filters.Add<FormatResponseAttribute>();
                x.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
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