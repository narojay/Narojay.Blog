using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Application.Workflow;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Extension
{
    public static class WorkflowExtension
    {
        public static IServiceCollection AddCustomizedWorkflow(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddLogging();
            services.AddWorkflow();
            return services;
        }
    }
}
