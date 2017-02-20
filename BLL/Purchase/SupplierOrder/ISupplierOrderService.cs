/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ISupplierOrderService.cs
// 文件功能描述：
//               外协调度单的Service接口
// 
// 创建标识：2013/11/14  廖齐玉 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    /// <summary>
    /// 外协调度单的Service接口
    /// </summary>
    public interface ISupplierOrderService
    {
        /// <summary>
        /// 获得调度单明细表数据
        /// </summary>
        /// <param name="supplierOrder"></param>
        /// <returns></returns>
        IEnumerable<VM_SupplierOrder> GetSupplierOrderByIdForSearch(VM_SupplierOrder supplierOrder,Paging page);

        /// <summary>
        /// 获取单号的相关信息
        /// 紧急状态，客户订单号，客户订单明细号，部门，调度单种类，还有调入单位
        /// </summary>
        /// <param name="supplierOrderId">单号</param>
        /// <returns>一维数组</returns>
        VM_SupplierOrderInfor GetDetailInformation(string supplierOrderId);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="uId">用户ID</param>
        /// <param name="s">数据视图</param>
        /// <param name="orderDetailData">Table表数据集合</param>
        /// <returns></returns>
        [TransactionAop]
        bool UpdateSupplierOrderDetail(string uId, VM_SupplierOrderInfor s, Dictionary<string, string>[] orderDetailData);

        /// <summary>
        /// 数据添加
        /// </summary>
        /// <param name="supOdrId">外协单号</param>
        /// <param name="uId">用户ID</param>
        /// <param name="s">数据视图</param>
        /// <param name="orderDetailData">Table表数据集合</param>
        /// <returns></returns>
        [TransactionAop]
        bool Add(string supOdrId, string uId,VM_SupplierOrderInfor s,Dictionary<string,string>[] orderDetailData);

        /// <summary>
        /// 判断是否存在调度单实体
        /// </summary>
        /// <param name="supOrderId"></param>
        /// <returns></returns>
        bool GetSupplierOrderById(string supOrderId);

        /// <summary>
        /// 获取外协调度单一览画面数据方法
        /// </summary>
        /// <param name="searchCondition">获取条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>调度单List</returns>
        IEnumerable<VM_SupplierOrderList> GetSupplierOrderListBySearchByPage(VM_SupplierOrderListForSearch searchCondition, Paging paging);

        /// <summary>
        /// 删除外协进度单  单表删除 
        /// </summary>
        /// <param name="list">待删除Id的集合</param>
        /// <param name="uId">删除者的Id</param>
        /// <returns>执行结果</returns>
        [TransactionAop]
        bool DeleteSupplierOrder(List<string> list, string uId);

        /// <summary>
        /// 外协调度单的审核
        /// </summary>
        /// <param name="list">待审核的Id集合</param>
        /// <param name="uId">审核者的Id</param>
        /// <returns>执行结果</returns>
        [TransactionAop]
        bool AuditSupplierOrder(List<string> list, string uId);

        /// <summary>
        /// 外协调度单的打印
        /// </summary>
        /// <param name="uId">登陆者的ID</param>
        /// <param name="supplierOrderId">需要打印的调度单ID</param>
        /// <returns></returns>
        [TransactionAop]
        bool PrintSupplierOrder(string uId,string supplierOrderId);
    }
}
