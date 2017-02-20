using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class IOCHelper
    {

        public static T ResolverClass<T>() {

            return (T)AutofacDependencyResolver.Current.GetService(typeof(T));
        }
    }
}
