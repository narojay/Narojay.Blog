using Microsoft.Extensions.DependencyInjection;

namespace Narojay.Blog.Infrastruct.Jwt.Extension;

public static class ServiceCollectionExtension
{
    public static void UseJwtService(this IServiceCollection service, Action<IjwtBuilder> action)
    {
        action(new JwtBuilder(service));
    }
}