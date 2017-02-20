// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：CommonConstant.cs
// 文件功能描述：公共常量类
// 
// 创建标识：代东泽 20131224
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
    /// 代东泽 20131224
    /// 公共变量，可以在任意层中使用，主要存放那些没有实际意义的常量，
    /// 比如ui层中使用的“0”表识一种状态，然后在service层中使用此状态进行了判断等，
    /// 纯粹是为了写代码方便而使用。
    /// </summary>
    public class CommonConstant
    {
        /// <summary>
        /// Value="0"
        /// </summary>
        public const string ZERO_ST = "0";
        /// <summary>
        /// Value="1"
        /// </summary>
        public const string ONE_ST = "1";
        /// <summary>
        /// Value="2"
        /// </summary>
        public const string TWO_STR = "2";
        /// <summary>
        /// Value="3"
        /// </summary>
        public const string THREE_ST = "3";
        /// <summary>
        /// Value=0
        /// </summary>
        public const int ZERO_INT = 0;
        /// <summary>
        /// Value=1
        /// </summary>
        public const int ONE_INT = 1;
        /// <summary>
        /// Value=2
        /// </summary>
        public const int TWO_INT = 2;
        /// <summary>
        /// Value=3
        /// </summary>
        public const int THREE_INT = 3;
        /// <summary>
        /// Value="1"
        /// </summary>
        public const string OFF = "1";
        /// <summary>
        /// Value="0"
        /// </summary>
        public const string ON = "0";
        /// <summary>
        /// Value="1"
        /// </summary>
        public const string ADD = "1";
        /// <summary>
        /// Value="0"
        /// </summary>
        public const string UPDATE = "0";
        /// <summary>
        /// Value="1"
        /// </summary>
        public const string NEW = "1";
        /// <summary>
        /// Value="0"
        /// </summary>
        public const string OLD = "0";


    }
}
