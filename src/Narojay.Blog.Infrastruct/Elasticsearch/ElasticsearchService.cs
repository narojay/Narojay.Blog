using Nest;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public class ElasticsearchService : IElasticsearchService
{
    public IElasticClient GetClient()
    {
        var connectStrings = new ConnectionSettings(new Uri("http://localhost:9200"));
        return new ElasticClient(connectStrings);
    }
}