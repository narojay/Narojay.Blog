using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Narojay.Blog.Extension;

public static class RabbitMqExtension
{
    public static IServiceCollection AddCustomizedRabbitMq(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var host = configuration["RabbitMqConfig:Host"];
            var userName = configuration["RabbitMqConfig:UserName"];
            var password = configuration["RabbitMqConfig:Password"];
            var factory = new ConnectionFactory
            {
                HostName = host,
                UserName = userName,
                Password = password,
                DispatchConsumersAsync = true
            };
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
            return new DefaultRabbitMQPersistentConnection(factory, logger);
        });


        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>();

        return services;
    }
}