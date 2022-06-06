using Narojay.Blog.Application.Interface;
using Nest;

namespace Narojay.Blog.Application.Service;

public class ElasticsearchService : IElasticsearchService
{
    public IElasticClient GetClient()
    {
        var connectStrings = new ConnectionSettings(new Uri("http://localhost:9200"));
        return new ElasticClient(connectStrings);
    }
}