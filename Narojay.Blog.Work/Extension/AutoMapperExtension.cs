using Narojay.Blog.Application.AutoMapper;

namespace Narojay.Blog.Work.Extension;

public static class MiddlewareExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(x => x.AddProfile(new MapperProfile()));
        return services;
    }
}