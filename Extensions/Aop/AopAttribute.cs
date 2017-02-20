using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class AopAttribute:ProxyAttribute
    {
        
        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            AopProxy realProxy = new AopProxy(serverType);
            return realProxy.GetTransparentProxy() as MarshalByRefObject;
        }


    }
}
