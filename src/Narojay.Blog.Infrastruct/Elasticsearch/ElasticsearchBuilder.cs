using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public interface IElasticsearchBuilder
{
    IServiceCollection ServiceCollection { get; }

    IElasticsearchBuilder ConfigOptions(ElasticsearchOption option, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped);
}

public class ElasticsearchBuilder : IElasticsearchBuilder
{
    public IServiceCollection ServiceCollection { get; }

    public ElasticsearchBuilder(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }


    public IElasticsearchBuilder ConfigOptions(ElasticsearchOption option, ServiceLifetime serviceLifetime)
    {
        var elasticsearchProvider = new ElasticsearchProvider(option);
        
        ServiceCollection.TryAddSingleton<IElasticsearchProvider>(elasticsearchProvider);

        return this;
    }
}