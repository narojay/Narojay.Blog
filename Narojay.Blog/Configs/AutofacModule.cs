using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure;

namespace Narojay.Blog.Config
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
