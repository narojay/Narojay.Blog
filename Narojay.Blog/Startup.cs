using Autofac;
using CSRedis;
using HealthChecks.UI.Client;
using IdentityModel.OidcClient;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Application;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Application.Service;
using Narojay.Blog.Extension;
using Narojay.Blog.Filter;
using Narojay.Blog.Infrastruct;
using Narojay.Blog.Infrastruct.NotificationHub;
using Narojay.Blog.Infrastruct.NotificationHub.Hub;
using Narojay.Blog.Middleware;
using Newtonsoft.Json;
using Serilog;

namespace Narojay.Blog;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(typeof(Startup));

        services.AddMapper();

        services.AddSignalR();

        services.AddHttpContextAccessor();

        services.AddCustomizedController();

        services.AddCustomizedWorkflow(_configuration, _env);

        services.AddCustomizedDbExtension(_configuration, _env);

        RedisHelper.Initialization(new CSRedisClient(_configuration["Redis"]));

        services.AddCustomizedSwagger(_configuration, _env);

        services.AddCustomizedAuthentication(_configuration, _env);
    
        services.AddHealthChecks();

        services.AddHealthChecksUI().AddInMemoryStorage();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterModule(new InfrastructModule());
        builder.RegisterModule(new ApplicationModule());
        builder.RegisterModule(new AutofacModule());
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWarmUpEfCoreService warmUpEfCoreService)
    {
        app.UseSerilogRequestLogging();
        //app.ApplicationServices.GetService<IRabbitMQPersistentConnection>();
        app.UseMiddleware<ExceptionMiddleware>();
        //Console.WriteLine(_env.EnvironmentName);
        app.UseCors(x => x.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
        //app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Narojay.Blog v1"));
        //app.HangFireStart();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<BlogHub>("/bloghub");
            app.UseEndpoints(x =>
            {
                x.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                x.MapHealthChecksUI(); 
            });
        });
        app.UseHealthChecksUI();
        //var host = app.ApplicationServices.GetService<IWorkflowHost>();
        //host.RegisterWorkflow<TestWorkflow, MyDataClass>();
        //host.Start();
        //warmUpEfCoreService.WarmUp();
    }
}