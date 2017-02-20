/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISchedulingService.cs
// 文件功能描述：
//          外购排产和外协排产的Service接口类
//      
// 修改履历：2013/12/23 陈阵 新建
/*****************************************************************************/
using Extensions;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 外购排产和外协排产的Service接口类
    /// </summary>
    public interface ISchedulingService
    {
        /// <summary>
        /// 取得外购排产画面的显示信息
        /// </summary>
        /// <param name="insturctions">外购指示List</param>
        /// <returns>外购排产画面的显示信息</returns>
        IEnumerable<VM_PurchaseScheduling> GetPurchaseSchedulingInfo(string[] instructions);

        /// <summary>
        /// 外购排产 - 生成订单
        /// </summary>
        /// <param name="orderInfoList">排产订单信息</param>
        /// <param name="userID">排产用户ID</param>
        /// <param name="date">排产时间</param>
        /// <returns>排产结果（true:排产成功  false:排产失败）</returns>
        Boolean MakeOrder4Purchase(Dictionary<string, string>[] orderInfoList, String userID, DateTime date);

        /// <summary>
        /// 取得外协排产画面的显示信息
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="paging"></param>
        /// <param name="total"></param>
        /// <returns>外协排产画面的显示信息</returns>
        IEnumerable<VM_SupplierScheduling> GetSupplierSchedulingInfo(string[] schedulings);

        /// <summary>
        /// 外协排产 - 生成订单
        /// </summary>
        /// <param name="orderInfoList">排产订单信息</param>
        /// <param name="userID">排产用户ID</param>
        /// /// <returns>排产结果（true:排产成功  false:排产失败）</returns>
        Boolean MakeOrder4Supplier(Dictionary<string, string>[] orderInfoList, String userID, DateTime date);
    }
}
