using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace Extensions
{
    public class AopProxy : RealProxy
    {
        public AopProxy(Type serverType) 
            : base(serverType) { }

        public override IMessage Invoke(IMessage msg)
        {
            if (msg is IConstructionCallMessage)
            {
                IConstructionCallMessage constructCallMsg = msg as IConstructionCallMessage;
                IConstructionReturnMessage constructionReturnMessage = this.InitializeServerObject((IConstructionCallMessage)msg);
                RealProxy.SetStubData(this, constructionReturnMessage.ReturnValue);
                //Console.WriteLine("constructs is call");
                return constructionReturnMessage;
            }
            else
            {
                IMethodCallMessage callMsg = msg as IMethodCallMessage;
                IMessage message;
                Attribute[] atts = Attribute.GetCustomAttributes(callMsg.MethodBase, typeof(AbstractMethodAopAttribute));
                IEnumerable<AbstractMethodAopAttribute> aops = (IEnumerable<AbstractMethodAopAttribute>)atts.AsEnumerable();
                try
                {
                    object[] args = callMsg.Args;

                    
                    foreach (var aop in aops)
                    {
                        aop.Before();
                    }

                    object o = callMsg.MethodBase.Invoke(GetUnwrappedServer(), args);
                    foreach (var aop in aops)
                    {
                        aop.After();
                    }
                    message = new ReturnMessage(o, args, args.Length, callMsg.LogicalCallContext, callMsg);
                    
                }
                catch (Exception e)
                {
                    foreach (var aop in aops)
                    {
                        aop.handleException(e);
                    }
                    message = new ReturnMessage(e, callMsg);
                }


                return message;
            }
        }


    }
}
