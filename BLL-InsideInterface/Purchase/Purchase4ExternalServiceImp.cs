/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：Purchase4ExternalServiceImp.cs
// 文件功能描述：
//          采购部门的外部共通的Service接口的实现类
//      
// 修改履历：2013/12/11 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;

namespace BLL_InsideInterface
{
    /// <summary>
    /// 采购部门的外部共通的Service接口的实现类
    /// </summary>
    public class Purchase4ExternalServiceImp : IPurchase4ExternalService 
    {
        // 外购单详细表Repository类
        private IMCOutSourceOrderDetailRepository outOrderDetailRepos;

        // 外协单详细表的Repository类
        private ISupplierOrderDetailRepository supOrderDetailRepos;

        /// <summary>
        /// 采购部门的外部共通的Service接口的实现类的构造函数
        /// </summary>
        /// <param name="outOrderDetailRepos">外购单详细表Repository类</param>
        /// <param name="supOrderDetailRepos">外协单详细表的Repository类</param>
        public Purchase4ExternalServiceImp(IMCOutSourceOrderDetailRepository outOrderDetailRepos, ISupplierOrderDetailRepository supOrderDetailRepos)
        {
            this.outOrderDetailRepos = outOrderDetailRepos;
            this.supOrderDetailRepos = supOrderDetailRepos;
        }

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
        public bool UpdOutSource4Storage(string outSourceNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate)
        {
            // 返回值初始化
            bool result = false;

            // 实体初始化
            MCOutSourceOrderDetail outSourceOrderDetail = new MCOutSourceOrderDetail();
            // 主键赋值
            outSourceOrderDetail.OutOrderID = outSourceNo;
            outSourceOrderDetail.CustomerOrderID = customerOrderNo;
            outSourceOrderDetail.CustomerOrderDetailID = customerOrderDetailNo;
            outSourceOrderDetail.ProductPartID = prodPartID;
            // 通过主键查找实体
            outSourceOrderDetail= outOrderDetailRepos.Find(outSourceOrderDetail);
            // 实体存在 ， 则进行相关操作 
            if (outSourceOrderDetail != null)
            {
                // 更新数据库实际数量 
                outSourceOrderDetail.ActualQuantity = outSourceOrderDetail.ActualQuantity + quantity;

                // 实际入库总量 初始化
                decimal amount = 0;
                // 总量计算 = 数据库库里的 actQ + othQ  
                amount = outSourceOrderDetail.ActualQuantity + outSourceOrderDetail.OtherQuantity;

                // 当实际接收的数量与其他数量之和为单据要求数量时，更新数据库单据完成状态
                if (outSourceOrderDetail.RequestQuantity == amount)
                {
                    // 更新数据状态 为 【已完成】
                    outSourceOrderDetail.CompleteStatus = Constant.PurchaseCompleteStatus.COMPLETED;
                }

                // 更新 修改人 和 修改时间 信息
                outSourceOrderDetail.UpdUsrID = userID;
                outSourceOrderDetail.UpdDt = updDate;

                // 更新数据库
                if (outOrderDetailRepos.Update(outSourceOrderDetail) == true)
                {
                    // 更新成功
                    result = true;
                };
            }

            return result;
        }

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
        public bool UpdSupplier4Storage(string supplierNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate)
        {
            // 返回值初始化
            bool result = false;

            // 实体初始化
            MCSupplierOrderDetail supplierOrderDetail = new MCSupplierOrderDetail();
            // 主键赋值
            supplierOrderDetail.SupOrderID = supplierNo;
            supplierOrderDetail.CustomerOrderID = customerOrderNo;
            supplierOrderDetail.CustomerOrderDetailID = customerOrderDetailNo;
            supplierOrderDetail.ProductPartID = prodPartID;
            // 通过主键查找实体
            supplierOrderDetail = supOrderDetailRepos.Find(supplierOrderDetail);
            // 实体存在 ， 则进行相关操作 
            if (supplierOrderDetail != null)
            {
                // 更新数据库实际数量 
                supplierOrderDetail.ActualQuantity = supplierOrderDetail.ActualQuantity + quantity;

                // 实际入库总量 初始化
                decimal amount = 0;
                // 总量计算 = 数据库库里的 actQ + othQ  
                amount = supplierOrderDetail.ActualQuantity + supplierOrderDetail.OtherQuantity;

                // 当实际接收的数量与其他数量之和为单据要求数量时，更新数据库单据完成状态
                if (supplierOrderDetail.RequestQuantity == amount)
                {
                    // 更新数据状态 为 【已完成】
                    supplierOrderDetail.CompleteStatus = Constant.PurchaseCompleteStatus.COMPLETED;
                }

                // 更新 修改人 和修改时间 信息
                supplierOrderDetail.UpdUsrID = userID;
                supplierOrderDetail.UpdDt = updDate;

                // 更新数据库
                if (supOrderDetailRepos.Update(supplierOrderDetail) == true)
                {
                    // 更新成功
                    result = true;
                };
            }

            return result;
        }

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
        public bool UpdOutSource4Quality(string outSourceNo, string customerOrderNo, string customerOrderDetailNo,
            string prodPartID, decimal quantity, string userID, DateTime updDate)
        {
            // 返回值初始化
            bool result = false;

            // 实体初始化
            MCOutSourceOrderDetail outSourceOrderDetail = new MCOutSourceOrderDetail();
            // 主键赋值
            outSourceOrderDetail.OutOrderID = outSourceNo;
            outSourceOrderDetail.CustomerOrderID = customerOrderNo;
            outSourceOrderDetail.CustomerOrderDetailID = customerOrderDetailNo;
            outSourceOrderDetail.ProductPartID = prodPartID;
            // 通过主键查找实体
            outSourceOrderDetail = outOrderDetailRepos.Find(outSourceOrderDetail);
            // 实体存在 ， 则进行相关操作 
            if (outSourceOrderDetail != null)
            {
                // 更新数据库其他数量 
                outSourceOrderDetail.OtherQuantity = outSourceOrderDetail.OtherQuantity + quantity;

                // 实际入库总量 初始化
                decimal amount = 0;
                // 总量计算 = 数据库库里的 actQ + othQ  
                amount = outSourceOrderDetail.ActualQuantity + outSourceOrderDetail.OtherQuantity;

                // 当实际接收的数量与其他数量之和为单据要求数量时，更新数据库单据完成状态
                if (outSourceOrderDetail.RequestQuantity == amount)
                {
                    // 更新数据状态 为 【已完成】
                    outSourceOrderDetail.CompleteStatus = Constant.PurchaseCompleteStatus.COMPLETED;
                }

                // 更新 修改人 和 修改时间 信息
                outSourceOrderDetail.UpdUsrID = userID;
                outSourceOrderDetail.UpdDt = updDate;

                // 更新数据库
                if (outOrderDetailRepos.Update(outSourceOrderDetail) == true)
                {
                    // 更新成功
                    result = true;
                };
            }

            return result;
        }

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
        public bool UpdSupplier4Quality(string supplierNo, string customerOrderNo, string customerOrderDetailNo, 
            string prodPartID, decimal quantity, string userID, DateTime updDate)
        {
            // 返回值初始化
            bool result = false;

            // 实体初始化
            MCSupplierOrderDetail supplierOrderDetail = new MCSupplierOrderDetail();
            // 主键赋值
            supplierOrderDetail.SupOrderID = supplierNo;
            supplierOrderDetail.CustomerOrderID = customerOrderNo;
            supplierOrderDetail.CustomerOrderDetailID = customerOrderDetailNo;
            supplierOrderDetail.ProductPartID = prodPartID;
            // 通过主键查找实体
            supplierOrderDetail = supOrderDetailRepos.Find(supplierOrderDetail);
            // 实体存在 ， 则进行相关操作 
            if (supplierOrderDetail != null)
            {
                // 更新数据库其他数量 
                supplierOrderDetail.OtherQuantity = supplierOrderDetail.OtherQuantity + quantity;

                // 实际入库总量 初始化
                decimal amount = 0;
                // 总量计算 = 数据库库里的 actQ + othQ  
                amount = supplierOrderDetail.ActualQuantity + supplierOrderDetail.OtherQuantity;

                // 当实际接收的数量与其他数量之和为单据要求数量时，更新数据库单据完成状态
                if (supplierOrderDetail.RequestQuantity == amount)
                {
                    // 更新数据状态 为 【已完成】
                    supplierOrderDetail.CompleteStatus = Constant.PurchaseCompleteStatus.COMPLETED;
                }

                // 更新 修改人 和修改时间 信息
                supplierOrderDetail.UpdUsrID = userID;
                supplierOrderDetail.UpdDt = updDate;

                // 更新数据库
                if (supOrderDetailRepos.Update(supplierOrderDetail) == true)
                {
                    // 更新成功
                    result = true;
                };
            }

            return result;
        }
    }
}
