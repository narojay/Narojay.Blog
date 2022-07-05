using System.Reflection;
using System.Text;
using System.Text.Json;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using CSRedis;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using HealthChecks.UI.Client;
using Markdig.Helpers;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Narojay.Blog;
using Narojay.Blog.Application;
using Narojay.Blog.Application.Events;
using Narojay.Blog.Domain;
using Narojay.Blog.Infrastruct;
using Narojay.Blog.Infrastruct.NotificationHub.Hub;
using Narojay.Blog.Work;
using Narojay.Blog.Work.Extension;
using Narojay.Blog.Work.Handler;
using Narojay.Blog.Work.Middleware;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
{
    builder.RegisterModule(new InfrastructModule());
    builder.RegisterModule(new ApplicationModule());
    builder.RegisterModule(new WorkerModule());
})).UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMapper();

builder.Services.AddSignalR();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomizedController();

builder.Services.AddCustomizedWorkflow(builder.Configuration, builder.Environment);

builder.Services.AddCustomizedDbExtension(builder.Configuration, builder.Environment);

builder.Services.AddCustomizedRabbitMq(builder.Configuration, builder.Environment);

RedisHelper.Initialization(new CSRedisClient(builder.Configuration["Redis"]));

builder.Services.AddCustomizedSwagger(builder.Configuration, builder.Environment);

builder.Services.AddCustomizedAuthentication(builder.Configuration, builder.Environment);

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecksUI().AddInMemoryStorage();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

//app.ApplicationServices.GetService<IRabbitMQPersistentConnection>();
app.UseSerilogRequestLogging();
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
        x.MapHealthChecks("/Warm", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        x.MapHealthChecksUI();
    });
});
app.UseHealthChecksUI();
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

app.UseRabbitMqWork();

app.Run();