/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMarketOrderService.cs
// 文件功能描述：订单表Service接口类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Model.Market;
using Repository;
using System.Collections.Generic;
using System.Web;


namespace BLL
{
    public interface IMarketOrderService
    {
        /// <summary>
        /// 添加表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraMarketOrder">MarketOrder 对象</param>
        /// <param name="paraMarketOrderDetailList"> MarketOrderDetail 泛型结果集</param>
        /// <param name="paraMarketOrderDetailPrintList"> MarketOrderDetailPrint 泛型结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [TransactionAop]
        bool AddMarketOrderManagementService(MarketOrder paraMarketOrder, List<MarketOrderDetail> paraMarketOrderDetailList, List<MarketOrderDetailPrint> paraMarketOrderDetailPrintList, List<ProduceGeneralPlan> paraProduceGeneralPlanList);

        /// <summary>
        /// 根据 ClientOrderID、ClientVersion，获取相应对象
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <param name="paraClientVersion">版数</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        VM_MarketOrderForShowMarketOrderInfo GetMarketOrderInfoService(string paraClientOrderID, string paraClientVersion);

        /// <summary>
        /// 获取表 MK_ORDER 查询记录
        /// </summary>
        /// <param name="paraPIFSTPI">VM_MarketOrderForSearchTableMarketOrder 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_MarketOrderForTableMarketOrder 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_MarketOrderForShowMarketOrderInfo> GetMarketOrderListService(VM_MarketOrderForSearchMarketOrderTable paraMOFSMOT, Paging paraPage);

        /// <summary>
        /// 更新表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraMarketOrder">MarketOrder 对象</param>
        /// <param name="paraMarketOrderDetailList"> MarketOrderDetail 泛型结果集</param>
        /// <param name="paraMarketOrderDetailPrintList"> MarketOrderDetailPrint 泛型结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [TransactionAop]
        string UpdateMarketOrderManagementService(MarketOrder paraMarketOrder, List<MarketOrderDetail> paraMarketOrderDetailList, List<MarketOrderDetailPrint> paraMarketOrderDetailPrintList, List<ProduceGeneralPlan> paraProduceGeneralPlanList);

    } //end IMarketOrderService
}
