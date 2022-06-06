using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Module = Autofac.Module;

namespace Narojay.Blog.Infrastruct
{
    public class InfrastructModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                  .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service") ||
                            t.Name.EndsWith("Controller") || t.Name.EndsWith("Attribute"))
                .PropertiesAutowired().AsSelf().AsImplementedInterfaces().InstancePerDependency();
        }
    }
}
