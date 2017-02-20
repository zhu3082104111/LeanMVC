/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMarketOrderDetailService.cs
// 文件功能描述：订单明细表Service接口类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Model.Market;
using System.Collections.Generic;


namespace BLL
{
    public interface IMarketOrderDetailService
    {
        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailForMarketOrderDetailTable 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> GetMarketOrderDetailListService(string paraClientOrderID);
    
    } //end IMarketOrderDetailService
}
