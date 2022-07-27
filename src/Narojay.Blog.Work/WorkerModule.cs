using System.Reflection;
using Autofac;
using EventBus.Abstractions;
using Module = Autofac.Module;

namespace Narojay.Blog.Work;

public class WorkerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>)).InstancePerLifetimeScope();
    }
}