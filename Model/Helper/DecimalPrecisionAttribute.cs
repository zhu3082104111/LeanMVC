using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DecimalPrecisionAttribute : Attribute
    {
        public DecimalPrecisionAttribute() : this(10, 2) 
        {
        }

        public DecimalPrecisionAttribute(byte precision, byte scale,bool isZeroUpdate=false)
        {
            Precision = precision;
            Scale = scale;

        }

        public byte Precision { get; set; }
        public byte Scale { get; set; }

        public bool IsZeroUpdate { get; set; }

    }
}
