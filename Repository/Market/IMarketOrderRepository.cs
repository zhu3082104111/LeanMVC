/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMarketOrderRepository.cs
// 文件功能描述：订单表Repository接口类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using System;
using Extensions;
using Model;
using Model.Market;
using System.Collections.Generic;


namespace Repository
{
    public interface IMarketOrderRepository : IRepository<MarketOrder> 
    {

        /// <summary>
        /// 根据客户订单号取得客户简称
        /// </summary>
        /// <param name="clientOrderNO">客户订单号</param>
        /// <returns>客户名称</returns>
        /// 创建者：朱静波
        String GetClientName(String clientOrderNO);

        /// <summary>
        /// 获取表 MK_ORDER 查询记录
        /// </summary>
        /// <param name="paraMOFSTMO">VM_MarketOrderForSearchTableMarketOrder 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_MarketOrderForTableMarketOrder 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_MarketOrderForShowMarketOrderInfo> GetMarketOrderListRepository(VM_MarketOrderForSearchMarketOrderTable paraMOFSMOT, Paging paraPage);

        /// <summary>
        /// 根据 ClientOrderID、ClientVersion，获取相应对象
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <param name="paraClientVersion">版数</param>
        /// <returns>VM_MarketOrderForShowMarketOrderInfo 页面显示类</returns>
        /// 创建者：冯吟夷
        VM_MarketOrderForShowMarketOrderInfo GetMarketOrderInfoRepository(string paraClientOrderID, string paraClientVersion);
    }
}
