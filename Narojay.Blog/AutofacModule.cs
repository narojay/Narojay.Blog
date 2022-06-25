using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Narojay.Blog;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") ||
                        t.Name.EndsWith("Controller") || t.Name.EndsWith("Attribute"))
            .PropertiesAutowired().AsSelf().AsImplementedInterfaces().InstancePerDependency();
        //builder.RegisterType<BlogContext>().AsSelf()
        //    .InstancePerLifetimeScope();
        //builder.RegisterType<HangfireBackJob>().As<IHangfireBackJob>()
        //    .PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();
    }
}