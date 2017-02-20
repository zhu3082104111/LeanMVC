using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IntegerAttribute : Attribute
    {
        public IntegerAttribute() {
            IsZeroUpdate = false;
        }
        public IntegerAttribute(bool isZeroUpdate)
        {
            IsZeroUpdate = isZeroUpdate;
        }

        public bool IsZeroUpdate { get; set; }
    }
}
