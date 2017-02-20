/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IOrderDetailRepository.cs
// 文件功能描述：订单明细表Repository接口类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Model.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Produce;


namespace Repository
{
    /// <summary>
    /// 梁龙飞建，订单明细资源库
    /// </summary>
    public interface IOrderDetailRepository : IRepository<MarketOrderDetail>
    {
        //检索出符合条件的所有MarketOrderDetail
        IEnumerable<MarketOrderDetail> GetOrderDetailList(MarketOrderDetail searchCondition, Paging pagex);
         //检索出第一个符合条件MarketOrderDetail
        MarketOrderDetail GetOrderDetail(MarketOrderDetail searchCondition);
        //获取没有排产的订单详细列表 
        IEnumerable<VM_OrderAcceptShow> GetDetailNotScheduling(VM_OrderAcceptSearch searchConditon, Paging pagex);

        #region 基本维护

        bool addOrderDetail(MarketOrderDetail target);

        bool updateOrderDetail(MarketOrderDetail target);

        bool deleteOrderDetail(MarketOrderDetail target);
        #endregion

        ///  <summary>
        ///  取得订单明细表对应产品ID的交货日期
        ///  </summary>
        /// <param name="productID">产品型号</param>
        /// <param name="customerOrderID">客户订单号</param>
        /// <param name="customerOrderDetails">客户订单详细</param>
        /// <returns>交货日期</returns>
        /// 创建者：朱静波
        DateTime? GetOrderDetailDeliveryDate(String productID, String customerOrderID, String customerOrderDetails);

        /// <summary>
        /// 取得单条订单明细表记录
        /// </summary>
        /// <param name="customerOrderID">客户订单号</param>
        /// <param name="customerOrderDetails">客户订单详细</param>
        /// <returns>订单明细记录</returns>
        /// 创建者：朱静波
        MarketOrderDetail GetOrderDetail(String customerOrderID, String customerOrderDetails);


        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailForMarketOrderDetailTable 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> GetMarketOrderDetailListRepository(string paraClientOrderID);

        /// <summary>
        /// 取得产品ID和略称
        /// </summary>
        /// <param name="clientOrderID">客户订单号</param>
        /// <returns>进度查询标题头数据</returns>
        /// 创建者：20131214 朱静波 
        IEnumerable<VM_ProduceType> GetProduceType(string clientOrderID);

        /// <summary>
        /// 根据客户订单号，删除表 MK_ORDER_DTL 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        bool DeleteMarketOrderDetailListRepository(string paraClientOrderID);

    } //end IOrderDetailRepository
}
