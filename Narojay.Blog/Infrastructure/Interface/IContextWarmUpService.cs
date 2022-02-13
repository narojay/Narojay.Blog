using System;
using System.Reflection;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IContextWarmUpService
    {
        void WarmUpAllContexts(params Assembly[] assembliesWithContexts);

        void WarmUpAllContexts(params Type[] types);
    }
}
