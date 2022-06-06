using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Application.Service;

public class WarmUpEfCoreService : IWarmUpEfCoreService
{
    private readonly BlogContext _blogContext;

    public WarmUpEfCoreService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public void WarmUp()
    {
        WarmupThreadPool();
        var _ = _blogContext.Users.AsNoTracking().FirstOrDefault();
        Console.WriteLine("warm up ef core ");
    }

    private void WarmupThreadPool()
    {
        if (ThreadPool.SetMinThreads(50, 20)) Parallel.For(0, 50, a => Thread.Sleep(1000));
    }
}