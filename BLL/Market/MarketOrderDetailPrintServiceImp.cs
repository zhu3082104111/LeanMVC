/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderDetailPrintServiceImp.cs
// 文件功能描述：订单明细表打字Service接口实现类
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
    class MarketOrderDetailPrintServiceImp: AbstractService,IMarketOrderDetailPrintService
    {
        private IMarketOrderDetailPrintRepository iMODPR;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIMODPR">IMarketOrderDetailPrintRepository 接口实现类</param>
        /// 创建者：冯吟夷
        public MarketOrderDetailPrintServiceImp(IMarketOrderDetailPrintRepository paraIMODPR)
        {
            this.iMODPR = paraIMODPR;
        } //end MarketOrderDetailPrintServiceImp

        #region 修改订单

        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable> GetMarketOrderDetailPrintListService(string paraClientOrderID)
        {
            return iMODPR.GetMarketOrderDetailPrintListRepository(paraClientOrderID);
        } //end GetMarketOrderDetailPrintListService

        #endregion


    } //end MarketOrderDetailPrintServiceImp
}
