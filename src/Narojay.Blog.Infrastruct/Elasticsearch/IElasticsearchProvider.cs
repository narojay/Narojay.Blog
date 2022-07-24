using Nest;

namespace Narojay.Blog.Infrastruct.Elasticsearch;

public interface IElasticsearchProvider
{
   IElasticClient GetClient();
}