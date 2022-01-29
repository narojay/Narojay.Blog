using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Narojay.Blog.Infrastructure.Interface;
using System;

namespace Narojay.Blog.Configs
{
    public static class HangFireConfig
    {
        private static BackgroundJobClient Client { get; } = new BackgroundJobClient();
        public static void HangFireStart(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/blog.job", new DashboardOptions()
            {
                Authorization = new[]
                {
                    new MyRestrictiveAuthorizationFilter()
                }
            }); //配置hangfire
            RecurringJob.AddOrUpdate<IHangfireBackJob>(x => x.StatisticLeaveMessageCount(), "0/10 * * * * ? ", TimeZoneInfo.Local, "blog_job"); //每5h检查友链
        }


        //public static string CheckLinks(params dynamic[] args)
        //{
        //    var job = new Job(typeof(IHangfireBackJob), typeof(IHangfireBackJob).GetMethod(nameof(HangfireBackJob.StatisticLeaveMessageCount)));
        //    return string.IsNullOrEmpty("blog_job") ? Client.Create(job, new EnqueuedState()) : Client.Create(job, new EnqueuedState("blog_job"));
        //}
    }

    /// <summary>
    /// hangfire授权拦截器
    /// </summary>
    public class MyRestrictiveAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// 授权校验
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
