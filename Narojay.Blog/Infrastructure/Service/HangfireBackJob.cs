using System;
using System.Linq;
using Narojay.Blog.Infrastructure.Interface;

namespace Narojay.Blog.Infrastructure.Service
{
    public class HangfireBackJob : IHangfireBackJob
    {
        private readonly DataContext _dbContext;

        public HangfireBackJob(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <summary>
        /// 检查友链
        /// </summary>
        public void StatisticLeaveMessageCount()
        {
            var count = _dbContext.LeaveMessages.Count();
            Console.WriteLine($"留言数量统计共{count}");
        }
    }
}