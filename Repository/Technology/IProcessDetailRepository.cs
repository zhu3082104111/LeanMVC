/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessDetailRepositoryImp.cs
// 文件功能描述：工序详细信息表的Repository接口
//     
// 修改履历：2013/11/29 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface IProcessDetailRepository : IRepository<ProcessDetail>
     {
         /// <summary>
         /// 根据工序号取得所有顺序号
         /// </summary>
         /// <param name="procId">工序号</param>
         /// <returns>工序顺序号数据集</returns>
         IEnumerable<int> GetSeqNo(string procId);
     }
}
