using Nest;

namespace Narojay.Blog.Application.Interface;

public interface IElasticsearchService
{
    IElasticClient GetClient();
}