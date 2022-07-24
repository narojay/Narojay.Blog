﻿using System;
using Nest;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public class ElasticsearchProvider : IElasticsearchProvider
{
    private IElasticClient _client;

    private ElasticsearchOption ElasticsearchOption { get; }

    public ElasticsearchProvider(ElasticsearchOption option)
    {
        ElasticsearchOption = option;
    }
    private void InitClient()
    {
        var connectStrings = new ConnectionSettings(new Uri($"http://{ElasticsearchOption.Host}:{ElasticsearchOption.Port}"));
        _client = new ElasticClient(connectStrings);
    }

    public IElasticClient GetClient()
    {
        if (_client != null)
        {
            return _client;
        }

        InitClient();
        return _client;
    }
}