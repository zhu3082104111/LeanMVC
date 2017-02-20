/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IDeliveryOrderDetailRepository.cs
// 文件功能描述：
//          送货单详细表的Repository接口类
//      
// 修改履历：2013/12/10 刘云 新建
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
    /// 送货单详细表的Repository接口类
    /// </summary>
    public interface IDeliveryOrderDetailRepository : IRepository<MCDeliveryOrderDetail>
    {
        /// <summary>
        /// 得到将要在页面上显示的数据
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <returns></returns>
        IEnumerable GetDeliveryOrderDetailBySearchById(VM_DeliveryOrderForShow searchConditon);

        /// <summary>
        /// 导入显示的数据
        /// </summary>
        /// <param name="orderNo">采购计划单号</param>
        /// <returns></returns>
        IEnumerable GetImportInfo(string orderNo);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <returns></returns>
        bool UpdateDetail(MCDeliveryOrderDetail deliveryOrder);

        /// <summary>
        /// 删除送货单（更新送货单详细表的信息）
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">登录用户ID</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        bool Delete(string deliveryOrderID, string uId, DateTime systime);
    }
}
