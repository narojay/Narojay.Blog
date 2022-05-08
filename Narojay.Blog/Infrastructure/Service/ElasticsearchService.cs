﻿using System;
using Narojay.Blog.Infrastructure.Interface;
using Nest;

namespace Narojay.Blog.Infrastructure.Service
{
    public class ElasticsearchService : IElasticsearchService
    {
        public IElasticClient GetClient()
        {
            var connectStrings = new ConnectionSettings(new Uri("http://localhost:9200"));
            return new ElasticClient(connectStrings);
        }
    }
}