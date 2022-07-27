using System;
using Nest;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public class ElasticsearchProvider : IElasticsearchProvider
{
    private IElasticClient _client;

    public ElasticsearchProvider(ElasticsearchOption option)
    {
        ElasticsearchOption = option;
    }

    private ElasticsearchOption ElasticsearchOption { get; }

    public IElasticClient GetClient()
    {
        if (_client != null) return _client;

        InitClient();
        return _client;
    }

    private void InitClient()
    {
        var uri = new Uri($"http://{ElasticsearchOption.Host}:{ElasticsearchOption.Port}");
        var connectStrings = new ConnectionSettings(uri)
            .BasicAuthentication(ElasticsearchOption.UserName, ElasticsearchOption.Password);
        _client = new ElasticClient(connectStrings);
    }
}