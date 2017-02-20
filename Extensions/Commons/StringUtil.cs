/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：StringUtil.cs
// 文件功能描述：
//          字符串Util类
//      
// 修改履历：2014/01/10 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class StringUtil
    {
        // 编码规则
        private const string ENCODING_CODE = "UTF-8";

        public static int getByteCnt(string s)
        {
            // 将参数字符串根据指定编码格式转换为数组
            byte[] bt = Encoding.GetEncoding(ENCODING_CODE).GetBytes(s);

            // 返回数组长度
            return bt.Length;
        }
    }
}
