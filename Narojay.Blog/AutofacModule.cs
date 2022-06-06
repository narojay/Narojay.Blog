using System;
using System.Reflection;
using Autofac;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Application.Service;
using Module = Autofac.Module;

namespace Narojay.Blog;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
       
        builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") ||
                        t.Name.EndsWith("Controller") || t.Name.EndsWith("Attribute"))
            .PropertiesAutowired().AsSelf().AsImplementedInterfaces().InstancePerDependency();
        //builder.RegisterType<BlogContext>().AsSelf()
        //    .InstancePerLifetimeScope();
        //builder.RegisterType<HangfireBackJob>().As<IHangfireBackJob>()
        //    .PropertiesAutowired(PropertyWiringOptions.PreserveSetValues).InstancePerDependency();
    }
}