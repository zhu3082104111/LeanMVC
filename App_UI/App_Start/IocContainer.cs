using Autofac;
using Autofac.Integration.Mvc;
using Extensions;
using Repository;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace App_UI
{
    public class IocContainer
    {

        public static void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();

            var dlls = from a in AppDomain.CurrentDomain.GetAssemblies() where a.FullName.Contains("BLL") || a.FullName.Contains("Repository") || a.FullName.Contains("BLL-InsideInterface") select a;

            builder.RegisterAssemblyTypes(dlls.ToArray<Assembly>()).Where(t => t.Name.EndsWith("RepositoryImp") || t.Name.EndsWith("ServiceImp"))
                 .AsImplementedInterfaces();

            builder.RegisterType<DB>().InstancePerHttpRequest();
            builder.RegisterType<UnitOfWork<DB>>().As<IUnitOfWork>().InstancePerHttpRequest();
   
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

        }

    }
}