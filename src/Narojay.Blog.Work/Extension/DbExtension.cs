using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Work.Extension;

public static class DbExtension
{
    public static IServiceCollection AddCustomizedDbExtension(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<BlogContext>(opt =>
        {
            opt.UseMySql(configuration["ConnString"], ServerVersion.AutoDetect(configuration["ConnString"]));
        });
        return services;
    }
}