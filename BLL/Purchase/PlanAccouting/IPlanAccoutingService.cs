﻿/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPlanAccoutingService.cs
// 文件功能描述：
//          外协和外购计划台帐的Service接口类
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace BLL
{
    /// <summary>
    /// 外协和外购计划台帐的Service接口类
    /// </summary>
    public interface IPlanAccoutingService
    {
        /// <summary>
        /// 外购计划台帐一览画面显示数据的查询方法
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>外购计划台帐一览信息</returns>
        IEnumerable GetPurchaseAccoutingListBySearchByPage(VM_PurchaseAccoutingListForSearch searchCondition, Paging paging);

        /// <summary>
        /// 外购计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns>外购计划台帐详细信息</returns>
        IEnumerable GetPurchaseAccoutingDetailByNo(string OutOrderNo);

        /// <summary>
        /// 外购单的基本信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns>外购单的基本信息</returns>
        VM_PurchaseAccoutingDetail SelectOrderInfo(string OutOrderNo);


        /// <summary>
        /// 外协计划台帐一览画面显示数据的查询方法
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>外协计划台帐一览信息</returns>
        IEnumerable GetSupplierAccoutingListBySearchByPage(VM_SupplierAccoutingList4Search searchCondition, Paging paging);

        /// <summary>
        /// 外协计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协计划台帐详细信息</returns>
        IEnumerable GetSupplierAccoutingDetailByNo(string supOrderNo);

        /// <summary>
        /// 外协单的基本信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协单的基本信息</returns>
        VM_SupplierAccoutingDetailInfo SelectSupplierOrderInfo(string supOrderNo);
    }
}