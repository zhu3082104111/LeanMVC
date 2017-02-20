/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPurchaseInstructionRepository.cs
// 文件功能描述：
//          外购指示表的Repository接口类
//      
// 修改履历：2014/1/8 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 外购指示表的Repository接口类
    /// </summary>
    public interface IPurchaseInstructionRepository : IRepository<PurchaseInstruction>
    {
        /// <summary>
        /// 获取外购指示List的查询方法(采购部门的外购计划一览画面用)
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外购指示List信息</returns>
        IEnumerable<VM_PurchaseInstructionList4Table> GetPurchaseInstructionListWithPaging(VM_PurchaseInstructionList4Search searchItem, Paging paging);

        /// <summary>
        /// 取得外购排产画面的显示信息
        /// </summary>
        /// <param name="insturctions">外购指示List</param>
        /// <returns>外购排产画面的显示信息</returns>
        IEnumerable<VM_PurchaseScheduling> GetPurchaseSchedulingInfo(string[] insturctions);
    }
}
