/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IMarketOrderDetailPrintRepository.cs
// 文件功能描述：订单明细表打字Repository接口类
//     
// 修改履历：2013/11/26 朱静波 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Model;
using Model.Market;
using System.Collections.Generic;


namespace Repository
{
    public interface IMarketOrderDetailPrintRepository : IRepository<MarketOrderDetailPrint>
    {
        /// <summary>
        /// 根据传入条件，查询记录
        /// </summary>
        /// <param name="productID">产品ID</param>
        /// <param name="customerOrderID">客户订单号</param>
        /// <param name="customerOrderDetails">客户订单详细</param>
        /// <returns>订单明细表打字数据集</returns>
        IEnumerable<MarketOrderDetailPrint> GetTyping(string productID, string customerOrderID,
            string customerOrderDetails);


        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable 表格显示类</returns>
        /// 创建者：冯吟夷
        IEnumerable<VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable> GetMarketOrderDetailPrintListRepository(string paraClientOrderID);

        /// <summary>
        /// 根据客户订单号，删除表 MK_ORDER_DTL_PRINT 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        bool DeleteMarketOrderDetailPrintListRepository(string paraClientOrderID);

    }
}
