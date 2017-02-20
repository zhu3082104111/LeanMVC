using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class ConvertUtils
    {
        /// <summary>
        /// 把字符串数组转化为decimal类型
        /// 代东泽 20131202
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal[] convertDecimals(string[] str)
        {
            decimal[] de = new decimal[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (!"".Equals(str[i]))
                {
                    de[i] = decimal.Parse(str[i]);
                }
            }
            return de;
        }
        /// <summary>
        /// 把数组转换称日期
        /// 代东泽 20131202
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime[] convertDates(string[] str)
        {
            DateTime[] de = new DateTime[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (!"".Equals(str[i]))
                {
                    de[i] = DateTime.Parse(str[i]);
                }
            }
            return de;
        }
    }
}
