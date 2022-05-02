using Nest;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IElasticsearchService
    {
        IElasticClient GetClient();
    }
}