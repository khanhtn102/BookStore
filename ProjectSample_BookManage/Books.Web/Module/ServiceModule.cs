using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using System.Reflection;

namespace Books.Web.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Books.Business"))
                .Where(n => n.Name.EndsWith("Business"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}