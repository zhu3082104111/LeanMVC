using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Globalization;
using System.Resources;
namespace Extensions
{
    public class Message
    {
        private static IOIniFile file = new IOIniFile(System.AppDomain.CurrentDomain.BaseDirectory + "Resource\\Message.ini");

        private static string getMessage(string key) 
        {

           
            return file.IniReadValue("Message",key);
        }

        private static string getError(string key)
        {
            return file.IniReadValue("Error", key);

        }
        public static string GetMsgWithParams(string name,string [] param) 
        {
            if (param == null) {
                return name;
            }
            string result = name;
            const string str1 = "{";
            const string str2 = "}";
            for (int i = 0; i < param.Length; i++) 
            {
                string str=str1 + "i" + str2;
                if (name.IndexOf(str)>0) 
                {
                    result.Replace(str, param[i]);
                }
            
            }

            return result;
        }

        private static void  GenJsMessage() 
        {

            //ResourceSet set = Produce.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            /*foreach (var s in set)
            {
                System.Collections.DictionaryEntry en = (System.Collections.DictionaryEntry)s;
                string key=(string)en.Key;
                string value = (string)en.Value;
            }*/
        }
    }
}
