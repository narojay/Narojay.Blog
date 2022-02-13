using System;
using Narojay.Blog.Infrastructure.Interface;
using System.Diagnostics;
using System.Linq;
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
            var _ = _dataContext.Users.AsNoTracking().FirstOrDefault();
            Console.WriteLine("warm up ef core ");
        }
    }
}
