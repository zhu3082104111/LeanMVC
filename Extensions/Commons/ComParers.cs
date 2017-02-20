// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ComParers.cs
// 文件功能描述：各种类型比较器
// 
// 创建标识：代东泽 20131223
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// 代东泽 20131223
    /// </summary>
    public  class ComParers
    {
        /// <summary>
        /// 数值字符串比较
        /// 代东泽 20131202
        /// </summary>
        public class StringComparer : IComparer<string>
        {

            public int Compare(string x, string y)
            {
                int k = int.Parse(x);
                int l = int.Parse(y);
                if (k > l)
                {
                    return 1;
                }
                else if (k < l)
                {
                    return -1;
                }
                return 0;
            }
        };
        /// <summary>
        /// 数值字符串比较
        /// 代东泽 20131202
        /// </summary>
        public class IntComparer : IComparer<int>
        {

            public int Compare(int k, int l)
            {
                if (k > l)
                {
                    return 1;
                }
                else if (k < l)
                {
                    return -1;
                }
                return 0;
            }
        };
    }
}
