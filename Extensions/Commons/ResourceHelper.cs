using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class ResourceHelper
    {

        public static string ConvertMessage(string name, string[] param)
        {
            return ConvertText(name, param);
        }

        public static string ConvertText(string name, string[] param)
        {
            if (param == null)
            {
                return name;
            }
            string result = name;
            const string str1 = "{";
            const string str2 = "}";
            for (int i = 0; i < param.Length; i++)
            {
                string str = str1 + i + str2;
                if (name.IndexOf(str) >= 0)
                {
                   result= result.Replace(str,param[i]);
                }

            }

            return result;
        }
    }
}
