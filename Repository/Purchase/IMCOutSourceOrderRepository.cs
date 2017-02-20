/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IpurchaseOrderRepository.cs
// 文件功能描述：外购产品计划单画面的Repository接口
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

namespace Repository
{
    /// <summary>
    /// 外购产品计划单画面的Repository接口
    /// </summary>
    public interface IMCOutSourceOrderRepository : IRepository<MCOutSourceOrder>
    {
        /// <summary>
        /// 对外购单的删除处理（外购单表的处理）
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="delUID">删除人UserID</param>
        /// <param name="delDate">删除时间</param>
        /// <returns>删除处理结果</returns>
        bool Delete(string outOrderID, string delUID, DateTime delDate);

        /// <summary>
        /// 对外购单的审核处理
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="verifyUID">审核人UserID</param>
        /// <param name="verifyDate">审核时间</param>
        /// <returns>审核处理结果</returns>
        bool Audit(string outOrderID, string verifyUID, DateTime verifyDate);

        /// <summary>
        /// 对外购单的批准处理
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="approveUID">批准人UserID</param>
        /// <param name="approveDate">批准时间</param>
        /// <returns>批准处理结果</returns>
        bool Approve(string outOrderID, string approveUID, DateTime approveDate);

        /// <summary>
        /// 根据外购单号取得外购单详细信息
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>外购单详细信息</returns>
        IEnumerable GetPurchaseOrderInfoByID(string outOrderID);

        /// <summary>
        /// 得到外购单表中符合条件的数据
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        MCOutSourceOrder getOrderById(string outOrderID);

        /// <summary>
        /// 更新可以修改的数据
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        bool UpdateOrder(MCOutSourceOrder purchaseOrder,string uId);

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <param name="printUID">当前登录用户ID</param>
        /// <returns></returns>
        bool Print(string outOrderID, string printUID);




        MCOutSourceOrder GetMCOutSourceOrderById(String outOrderID);


    }
}
