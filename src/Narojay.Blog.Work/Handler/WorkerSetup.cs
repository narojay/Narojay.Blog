using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Narojay.Blog.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Narojay.Blog.Work.Handler;

public static class WorkerSetup
{
    public static void UseRabbitMqWork(this WebApplication app)
    {
        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.IsClosedTypeOf(typeof(IIntegrationEventHandler<>)));
        var rabbitMqPersistentConnection = app.Services.GetService<IRabbitMQPersistentConnection>();
        var lifetimeScope = app.Services.GetService<ILifetimeScope>();
        foreach (var item in types)
        {
            var channel = rabbitMqPersistentConnection.CreateModel();
            channel.BasicQos(0, 1, false);
            var queueName = NarojayPlatform.BlogWork.ToString();
            channel.QueueDeclare(queueName, true, false, false, null);
            var routingKey = item.GetMethod("Handle")?.GetParameters()[0].ParameterType.Name;
            var a = item.GetMethod("Handle")?.GetParameters()[0].ParameterType;
            channel.QueueBind(queueName, EventBusRabbitMQ.EventBusRabbitMQ.BROKER_NAME, routingKey, null);

            var asyncEventingBasicConsumer = new AsyncEventingBasicConsumer(channel);

            async Task OnAsyncEventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs eventArgs)
            {
                try
                {
                    var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
                    var creatOrderEvent = JsonSerializer.Deserialize(message, a);
                    await using var scope = lifetimeScope.BeginLifetimeScope();
                    var handler = scope.ResolveOptional(item);
                    await (Task)item.GetMethod("Handle").Invoke(handler, new[] { creatOrderEvent });
                }
                catch (Exception e)
                {
                    Log.Error(e, "消费失败" + Encoding.UTF8.GetString(eventArgs.Body.Span));
                }

                channel.BasicAck(eventArgs.DeliveryTag, false);
            }

            asyncEventingBasicConsumer.Received += OnAsyncEventingBasicConsumerOnReceived;
            channel.BasicConsume(
                queueName,
                false,
                asyncEventingBasicConsumer);
        }
    }
}