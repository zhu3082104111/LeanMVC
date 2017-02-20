using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class NullValueKeyException:Exception
    {
        public NullValueKeyException(string keyName) 
        {
            this.KeyName = keyName;
        }

        public string KeyName { get; set; }

        public string Message { get { return "主键" + KeyName+"值为空"; } }
    }
}
