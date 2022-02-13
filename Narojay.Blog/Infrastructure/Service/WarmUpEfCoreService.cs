using System;
using Narojay.Blog.Infrastructure.Interface;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Narojay.Blog.Infrastructure.Service
{
    public class WarmUpEfCoreService : IWarmUpEfCoreService
    {
        private readonly DataContext _dataContext;

        public WarmUpEfCoreService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void WarmUp()
        {
            WarmupThreadPool();
            var _ = _dataContext.Users.AsNoTracking().FirstOrDefault();
            Console.WriteLine("warm up ef core ");
        }

        private void WarmupThreadPool()
        {
            if (ThreadPool.SetMinThreads(50, 20))
            {
                Parallel.For(0, 50, a => Thread.Sleep(1000));
            }
        }
    }
}
