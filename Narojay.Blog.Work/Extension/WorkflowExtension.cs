namespace Narojay.Blog.Work.Extension;

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