using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Books.Model;
using System.Data.Entity;
using Books.Repository.UnitOfWork;
using Books.Repository;

namespace Books.Web.Module
{
    public class EFModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());

            builder.RegisterType(typeof(BookSampleEntities)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
        }
    }
}