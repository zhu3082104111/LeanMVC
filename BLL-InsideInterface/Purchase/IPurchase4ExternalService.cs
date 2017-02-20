/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IPurchase4ExternalService.cs
// 文件功能描述：
//          采购部门的外部共通的Service接口类
//      
// 修改履历：2013/12/11 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 采购部门的外部共通的Service接口类
    /// </summary>
    public interface IPurchase4ExternalService
    {
        /// <summary>
        /// 仓库部门入库时，更新外购单表的实际入库数量
        /// </summary>
        /// <param name="outSourceNo">外购单号</param>
        /// <param name="customerOrderNo">客户定单号</param>
        /// <param name="customerOrderDetailNo">客户订单详细号</param>
        /// <param name="prodPartID">产品零件ID</param>
        /// <param name="quantity">入库数量</param>
        /// <param name="userID">用户ID</param>
        /// <param name="updDate">更新时间</param>
        /// <returns>更新结果（true：更新成功； false：更新失败）</returns>
        bool UpdOutSource4Storage(string outSourceNo, string customerOrderNo, string customerOrderDetailNo, 
            string prodPartID, decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 仓库部门入库时，更新外协单表的实际入库数量
        /// </summary>
        /// <param name="supplierNo">外协单号</param>
        /// <param name="customerOrderNo">客户定单号</param>
        /// <param name="customerOrderDetailNo">客户订单详细号</param>
        /// <param name="prodPartID">产品零件ID</param>
        /// <param name="quantity">入库数量</param>
        /// <param name="userID">用户ID</param>
        /// <param name="updDate">更新时间</param>
        /// <returns>更新结果（true：更新成功； false：更新失败）</returns>
        bool UpdSupplier4Storage(string supplierNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 品保部门品保判断时，不合格和让步接收不用于本订单的判定时，更新外购单表的其他数量
        /// </summary>
        /// <param name="outSourceNo">外购单号</param>
        /// <param name="customerOrderNo">客户定单号</param>
        /// <param name="customerOrderDetailNo">客户订单详细号</param>
        /// <param name="prodPartID">产品零件ID</param>
        /// <param name="quantity">入库数量</param>
        /// <param name="userID">用户ID</param>
        /// <param name="updDate">更新时间</param>
        /// <returns>更新结果（true：更新成功； false：更新失败）</returns>
        bool UpdOutSource4Quality(string outSourceNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate);

        /// <summary>
        /// 品保部门品保判断时，不合格和让步接收不用于本订单的判定时，更新外协单表的其他数量
        /// </summary>
        /// <param name="supplierNo">外协单号</param>
        /// <param name="customerOrderNo">客户定单号</param>
        /// <param name="customerOrderDetailNo">客户订单详细号</param>
        /// <param name="prodPartID">产品零件ID</param>
        /// <param name="quantity">入库数量</param>
        /// <param name="userID">用户ID</param>
        /// <param name="updDate">更新时间</param>
        /// <returns>更新结果（true：更新成功； false：更新失败）</returns>
        bool UpdSupplier4Quality(string supplierNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate);

    }
}
