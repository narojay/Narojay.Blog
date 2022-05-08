using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Configs;

namespace Narojay.Blog.Extensions
{
    public static class MiddlewareExtension
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(new MapperProfile()));
            return services;
        }
    }
}