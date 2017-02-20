/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IpurchaseOrderService.cs
// 文件功能描述：
//      外购产品计划的Service接口类
//      
// 修改履历：2013/11/1 刘云 新建
/*****************************************************************************/
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using BLL_InsideInterface;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 外购产品计划的Service接口类
    /// </summary>
    public interface IPurchaseOrderService
    {
        /// <summary>
        /// 取得外购一览画面显示数据
        /// </summary>
        /// <param name="purchaseOrderList">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        IEnumerable GetPurchaseOrderListInfoByPage(VM_PurchaseOrderListForSearch searchConditon, Paging paging);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pOrderList">外购单号List</param>
        /// <param name="delUID">删除人UserID</param>
        /// <returns>删除处理结果</returns>
        [TransactionAop]
        bool Delete(List<String> pOrderList, string delUID);

        /// <summary>
        /// 批准
        /// </summary>
        /// <param name="pOrderList">外购单号的List</param>
        /// <param name="approveUID">批准人的UserID</param>
        /// <returns>批准处理结果</returns>
        [TransactionAop]
        bool Approve(List<String> pOrderList, string approveUID);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="pOrderList">外购单号List</param>
        /// <param name="verifyUID">审核人UserID</param>
        /// <returns>审核处理结果</returns>
        [TransactionAop]
        bool Audit(List<String> pOrderList, string verifyUID);

        /// <summary>
        /// 根据外购单号取得外购单详细信息
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>外购单详细信息</returns>
        IEnumerable GetPurchaseOrderDetailInfoByID(string outOrderID);

        /// <summary>
        /// 得到外购单表中符合条件的数据
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        MCOutSourceOrder GetOrderInfoByID(string outOrderID);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="purchaseOrder">筛选条件</param>
        /// <param name="orderList">筛选条件</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        [TransactionAop]
        bool Add(VM_PurchaseOrderForShow purchaseOrder, Dictionary<string, string>[] orderList,string uId);

        /// <summary>
        /// 对更新操作进行检查并更新
        /// </summary>
        /// <param name="purchaseOrder">筛选条件</param>
        /// <param name="orderList">筛选条件</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        [TransactionAop]
        bool Update(VM_PurchaseOrderForShow purchaseOrder, Dictionary<string, string>[] orderList,string uId);

        /// <summary>
        /// 打印（更新外购单表的打印flg）
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns>处理结果</returns>
        [TransactionAop]
        bool PrintInfo(string outOrderID, string uId);
    }
}
