using System;
using Autofac;
using CSRedis;
using EventBusRabbitMQ;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Application;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Application.Service;
using Narojay.Blog.Extension;
using Narojay.Blog.Filter;
using Narojay.Blog.Infrastruct;
using Narojay.Blog.Infrastruct.DataBase;
using Narojay.Blog.Middleware;
using Newtonsoft.Json;
using RabbitMQ.Client;
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
        //services.AddHangfire(x => x.UseStorage(new MySqlStorage(AppConfig.ConnString, new MySqlStorageOptions
        //{
        //    TablesPrefix = "blog"
        //}))).AddHangfireServer(x =>
        //{
        //    x.ServerName = "blog.server";
        //    x.Queues = new[] { EnqueuedState.DefaultQueue, "blog_job" };
        //    x.WorkerCount = 10;
        //});
        services.AddHttpContextAccessor();
        RedisHelper.Initialization(new CSRedisClient(_configuration["Redis"]));
        services.AddControllers(x => x.Filters.Add<FormatResponseAttribute>()).AddControllersAsServices()
            .AddNewtonsoftJson(option =>
                {
                    //忽略循环引用
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }
            );

        services.AddDbContext<BlogContext>(opt =>
        {
            opt.UseMySql(_configuration["ConnString"], ServerVersion.AutoDetect(_configuration["ConnString"]));
        });

        services.AddCustomizedSwagger(_configuration, _env);

        services.AddCustomizedAuthentication(_configuration, _env);

        services.AddHealthChecks();

        services.AddHealthChecksUI().AddInMemoryStorage();

        //services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        //{
        //    var factory = new ConnectionFactory
        //    {
        //        HostName = _configuration["RabbitMqConfig:Host"],
        //        UserName = _configuration["RabbitMqConfig:UserName"],
        //        Password = _configuration["RabbitMqConfig:Password"],
        //        DispatchConsumersAsync = true
        //    };
        //    var a = new DefaultRabbitMQPersistentConnection(factory);
        //    return a;
        //});
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
        Console.WriteLine(_env.EnvironmentName);
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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
            endpoints.MapHub<TestHub>("/testHub");
            app.UseEndpoints(x =>
            {
                x.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }); //要进行该站点检测应添加此代码
                x.MapHealthChecksUI(); //添加UI界面支持
            });
        });
        app.UseHealthChecksUI();
        warmUpEfCoreService.WarmUp();
    }
}