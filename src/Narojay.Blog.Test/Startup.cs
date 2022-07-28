using System;
using System.Diagnostics;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Application;
using Xunit;
using Xunit.DependencyInjection;

namespace Narojay.Blog.Test;

public class Startup
{
    public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureAppConfiguration(lb => lb.AddJsonFile("appsettings.json", false, true))
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());

    public void ConfigureServices(IServiceCollection services)
    {
        services.Scan(sc =>
        {
            sc.FromAssembliesOf(typeof(AutofacModule),typeof(ApplicationModule)).
                AddClasses(c => c
                                                        .Where(t => t.Name.EndsWith("Repository") 
                                                         || t.Name.EndsWith("Service") 
                                                         || t.Name.EndsWith("Controller") 
                                                         || t.Name.EndsWith("Attribute")))
                .AsSelf()
                .AsImplementedInterfaces()
                .WithTransientLifetime();
        });
        services.TryAddSingleton<ITestService,TestService>();
        // services.AddSingleton<IAsyncExceptionFilter, DemystifyExceptionFilter>();
    }

    public void Configure(IServiceProvider provider)
    {
        // Assert.NotNull(accessor);

        // XunitTestOutputLoggerProvider.Register(provider);

        // var listener = new ActivityListener();
        //
        // listener.ShouldListenTo += _ => true;
        // listener.Sample += delegate { return ActivitySamplingResult.AllDataAndRecorded; };
        //
        // ActivitySource.AddActivityListener(listener);
    }
}