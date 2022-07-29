using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Application.Service;

public class WarmUpEfCoreService : IWarmUpEfCoreService
{
    private readonly BlogContext _blogContext;
    private readonly IServiceProvider _serviceProvider;

    public WarmUpEfCoreService(BlogContext blogContext,IServiceProvider serviceProvider)
    {
        _blogContext = blogContext;
        _serviceProvider = serviceProvider;
    }

    public void WarmUp()
    {
        WarmupThreadPool();
        var _ = _blogContext.Model;
    }

    private void WarmupThreadPool()
    {
        if (ThreadPool.SetMinThreads(50, 20)) 
            Parallel.For(0, 50, a => Thread.Sleep(1000));
    }
}