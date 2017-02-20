using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public abstract class AbstractMethodAopAttribute:Attribute
    {
        public abstract void Before();

        public abstract void handleException(Exception e);

        public abstract void After();
    }
}
