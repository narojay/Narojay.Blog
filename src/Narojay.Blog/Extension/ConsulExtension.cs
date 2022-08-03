using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Narojay.Blog.Extension;

public static class ConsulExtension
{
    public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
    {
        if(services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        services.AddSingleton<IConsulClient>(consul 
        
          => new ConsulClient(options =>
            {
                options.Address = new Uri(configuration["Consul:Address"]); // Consul客户端地址
            })

        
        );

        return services;
    }
    
    public static IApplicationBuilder UserConsul(this IApplicationBuilder app, IConfiguration configuration,IHostApplicationLifetime applicationLifetime)
    {
        if(app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        var client = app.ApplicationServices.GetService<IConsulClient>();
        if (client == null)
        {
            throw new  ArgumentException(nameof(client));
        }

        var registration = new AgentServiceRegistration
        {
            ID = Guid.NewGuid().ToString(), // 唯一Id
            Name = configuration["Consul:Name"], // 服务名
            Address = configuration["Consul:Ip"], // 服务绑定IP
            Port = Convert.ToInt32(Environment.GetEnvironmentVariable("ASPNETCORE_URLS").Split(":")[2]), // 服务绑定端口
            // Check = new AgentServiceCheck
            // {
            //     DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), // 服务启动多久后注册
            //     Interval = TimeSpan.FromSeconds(10), // 健康检查时间间隔
            //     HTTP = $"http://{configuration["Consul:Ip"]}:{configuration["Consul:Port"]}{configuration["Consul:HealthCheck"]}", // 健康检查地址
            //     Timeout = TimeSpan.FromSeconds(5) // 超时时间
            // }
        };
        client.Agent.ServiceRegister(registration).Wait();
        client.Agent.ServiceDeregister("02ad3f0a-de12-4d28-9740-d4971a60ef89").Wait();
        applicationLifetime.ApplicationStopping.Register(() =>
        {
            client.Agent.ServiceDeregister(registration.ID).Wait();
        });
        return app;
    }
}