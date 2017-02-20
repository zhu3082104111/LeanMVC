/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ISupplierOrderRepository.cs
// 文件功能描述：
//          外协调度单的Repository接口类
//      
// 修改履历：2013/11/4 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    /// <summary>
    /// 外协调度单的Repository接口类
    /// </summary>
    public interface ISupplierOrderRepository:IRepository<MCSupplierOrder>
    {
        /// <summary>
        /// 获取外协调度单一览画面数据方法
        /// </summary>
        /// <param name="searchCondition">获取条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>调度单List</returns>
        IEnumerable<VM_SupplierOrderList> GetSupplierOrderListForSearch(VM_SupplierOrderListForSearch searchCondition, Extensions.Paging page);

        /// <summary>
        /// 删除,修改外协调度单表的Delete标志
        /// </summary>
        /// <param name="s">外协调度单实体</param>
        /// <returns>执行结果</returns>
        bool DeleteSupplierOrder(MCSupplierOrder s);

        /// <summary>
        /// 审核,外协单的审核
        /// </summary>
        /// <param name="s">外协调度单实体</param>
        /// <returns>执行结果</returns>
        bool AuditSupplierOrder(MCSupplierOrder s);

        #region  根据外协加工调度单号得到外协加工调度单表对应数据
        /// <summary>
        /// 根据外协加工调度单号得到外协加工调度单表对应数据
        /// </summary>
        /// <param name="supOrderID">外协加工调度单号</param>
        /// <returns></returns>
        MCSupplierOrder GetMCSupplierOrderById(string supOrderID);
        #endregion
    }
}
