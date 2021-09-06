using System.Reflection;
using Autofac;

namespace Narojay.Blog.Configs
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
         
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") || t.Name.EndsWith("Controller") || t.Name.EndsWith("Attribute"))
                .PropertiesAutowired().AsSelf().AsImplementedInterfaces().InstancePerDependency();

            //builder.RegisterType<DataContext>().AsSelf()
            //    .InstancePerLifetimeScope();


        }
    }

}
