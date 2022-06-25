using Autofac;

namespace Narojay.Blog.Infrastruct;

public class InfrastructModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //var c = Assembly.GetExecutingAssembly();
        //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
        //    .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") ||
        //                t.Name.EndsWith("Controller") || t.Name.EndsWith("Attribute"))
        //    .PropertiesAutowired().AsSelf().AsImplementedInterfaces().InstancePerDependency();
    }
}