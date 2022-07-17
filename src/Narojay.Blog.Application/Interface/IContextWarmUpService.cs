using System;
using System.Reflection;

namespace Narojay.Blog.Application.Interface;

public interface IContextWarmUpService
{
    void WarmUpAllContexts(params Assembly[] assembliesWithContexts);

    void WarmUpAllContexts(params Type[] types);
}