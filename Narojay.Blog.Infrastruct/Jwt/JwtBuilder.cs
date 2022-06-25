using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Narojay.Blog.Infrastruct.Jwt;

public interface IjwtBuilder
{
    IServiceCollection ServiceCollection { get; }


    IjwtBuilder ConfigOptions(JwtOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped);
}

internal class JwtBuilder : IjwtBuilder
{
    public JwtBuilder(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }

    public IServiceCollection ServiceCollection { get; }

    public IjwtBuilder ConfigOptions(JwtOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        AddProviderService(options);

        ServiceCollection.TryAdd(new ServiceDescriptor(typeof(IJwtService), typeof(JwtService), lifetime));

        return this;
    }

    private void AddProviderService(JwtOptions options)
    {
        var provider = new JwtProvider(options);
        ServiceCollection.TryAddSingleton<IJwtProvider>(provider);
    }
}