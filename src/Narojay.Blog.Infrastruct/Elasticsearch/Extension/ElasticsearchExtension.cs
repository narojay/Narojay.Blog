using System;
using Microsoft.Extensions.DependencyInjection;

namespace Narojay.Blog.Infrastruct.Elasticsearch.Extension;

public static class ElasticsearchExtension
{
    public static void UseElasticsearch(this IServiceCollection service, Action<IElasticsearchBuilder> action)
    {
        action(new ElasticsearchBuilder(service));
    }
}