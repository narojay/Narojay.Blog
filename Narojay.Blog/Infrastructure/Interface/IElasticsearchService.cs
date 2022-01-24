using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IElasticsearchService
    {

        IElasticClient GetClient();
    }
}
