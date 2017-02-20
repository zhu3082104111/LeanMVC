/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IInstructionsService.cs
// 文件功能描述：
//          外购、外协计划一览画面的Service接口类
//      
// 修改履历：2013/12/06 宋彬磊 新建
/*****************************************************************************/
using Extensions;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace BLL
{
    /// <summary>
    /// 外购、外协计划一览画面的Service接口类
    /// </summary>
    public interface IInstructionsService
    {
        /// <summary>
        /// 获取外购指示List的方法
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外购指示List</returns>
        IEnumerable<VM_PurchaseInstructionList4Table> GetPurchaseInstructionList(VM_PurchaseInstructionList4Search searchItem, Paging paging);

        /// <summary>
        /// 获取外协指示List的方法
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外协指示List</returns>
        IEnumerable<VM_SupplierInstructionList4Table> GetSupplierInstructionList(VM_SupplierInstructionList4Search searchItem, Paging paging);
    }
}
