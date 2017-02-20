/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPurchaseSupplierAccoutingListRepositoryImp.cs
// 文件功能描述：
//        外购外协计划台账一览的Repository接口
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using Extensions;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 外购外协计划台帐画面的Repository接口
    /// </summary>
    public interface IPurchaseSupplierAccoutingListRepository : IRepository<MCOutSourceOrder>
    {
        /// <summary>
        /// 得到将要在外购计划台账一览画面上显示的数据
        /// </summary>
        /// <param name="SupplierAccoutingList">查询类的视图model</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        IEnumerable GetPurchaseAccoutingListBySearchByPage(VM_PurchaseAccoutingListForSearch searchCondition, Paging paging);

        /// <summary>
        /// 外购计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        IEnumerable GetPurchaseAccoutingDetailByNo(string OutOrderNo);

        /// <summary>
        /// 外购单的基本信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        VM_PurchaseAccoutingDetail GetOrderByNo(string OutOrderNo);
        

        /// <summary>
        /// 得到将要在外协计划台账一览画面上显示的数据
        /// </summary>
        /// <param name="searchCondition">查询类的视图model</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
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
        VM_SupplierAccoutingDetailInfo GetSupplierOrderByNo(string supOrderNo);
    }
}
