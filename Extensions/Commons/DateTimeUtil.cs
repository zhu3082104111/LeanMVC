using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class DateTimeUtil
    {
        /// <summary>
        /// 日期比较函数
        /// </summary>
        /// <param name="date1">第一个日期</param>
        /// <param name="date2">第二个日期</param>
        /// <returns>0：两时间相等； -1：第一个时间小于第二个时间； 1：第一个时间大于第二个时间 </returns>
        public static int compareDate(DateTime date1, DateTime date2)
        {
            string str1 = date1.ToString("yyyyMMdd");
            string str2 = date2.ToString("yyyyMMdd");
            return str1.CompareTo(str2);
        }
    }
}
