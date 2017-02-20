/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderDetailServiceImp.cs
// 文件功能描述：订单明细表Service接口实现类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Model.Market;
using Repository;
using System.Collections.Generic;


namespace BLL
{
    class MarketOrderDetailServiceImp : AbstractService, IMarketOrderDetailService
    {
        private IOrderDetailRepository iMODR;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIODR">IOrderDetailRepository 接口实现类</param>
        /// 创建者：冯吟夷
        public MarketOrderDetailServiceImp(IOrderDetailRepository paraIODR) 
        {
            this.iMODR = paraIODR;
        } //end MarketOrderDetailServiceImp

        #region 修改订单

        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailForMarketOrderDetailTable 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> GetMarketOrderDetailListService(string paraClientOrderID)
        {
            return iMODR.GetMarketOrderDetailListRepository(paraClientOrderID);
        } //end GetMarketOrderDetailListService

        #endregion

    } //end MarketOrderDetailServiceImp
}
