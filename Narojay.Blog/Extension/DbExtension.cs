using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Extension
{
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
}
