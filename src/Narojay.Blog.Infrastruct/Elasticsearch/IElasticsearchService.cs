using Nest;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public interface IElasticsearchService
{
    IElasticClient GetClient();
}