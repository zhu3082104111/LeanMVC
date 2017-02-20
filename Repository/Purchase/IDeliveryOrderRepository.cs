/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IDeliveryOrderRepository.cs
// 文件功能描述：
//           送货单表的Repository接口类
//      
// 修改履历：2013/11/13 姬思楠 新建
/*****************************************************************************/
using Model;
using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 送货单表的Repository接口类
    /// </summary>
    public interface IDeliveryOrderRepository : IRepository<MCDeliveryOrder>
    {
        /// <summary>
        /// 取得送货单一览信息
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>送货单信息List</returns>
        IEnumerable GetDeliveryOrderListBySearchByPage(VM_DeliveryOrderListForSearch searchCondition, Paging paging);

        /// <summary>
        /// 删除送货单（更新送货单表的信息）
        /// </summary>
        /// <param name="deliveryOrder">送货单实体</param>
        /// <param name="uId">用户</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        bool Delete(MCDeliveryOrder deliveryOrder, string uId, DateTime systime);


        /// <summary>
        /// 审核送货单
        /// </summary>
        /// <param name="deliveryOrder">送货单实体</param>
        /// <param name="uId">用户</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        bool Audit(MCDeliveryOrder deliveryOrder, string uId, DateTime systime);

        /// <summary>
        /// 更新送货单（更新送货单表的信息）
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <returns></returns>
        bool UpdateOrder(MCDeliveryOrder deliveryOrder);

        
        /// <summary>
        /// 通过送货单号得到对应送货单表数据
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <returns></returns>
        MCDeliveryOrder getDeliveryOrderById(string deliveryOrderID);

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">当前登录用户ID</param>
        /// <returns></returns>
        bool printInfo(string deliveryOrderID, string uId);

    }
}
