/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProduceBill.cs
// 文件功能描述：masterInfo
// 
// 创建标识：代东泽 20131122
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
using Model;
namespace BLL
{
    public interface IMasterInfoService
    {
        /// <summary>
        /// 
        /// 获取一个section的所有数据
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        IEnumerable<MasterDefiInfo> GetOneSection(string section);

        MasterDefiInfo GetMasterDefiInfo(string section,string value);
    }
}
