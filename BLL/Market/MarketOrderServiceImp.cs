/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderServiceImp.cs
// 文件功能描述：订单表Service接口实现类
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


namespace BLL
{
    class MarketOrderServiceImp : AbstractService, IMarketOrderService
    {
        private IMarketOrderRepository iMOR;
        private IOrderDetailRepository iMODR;
        private IMarketOrderDetailPrintRepository iMODPR;
        private IProduceGeneralPlanRepository iPGPR;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIMOR">IMarketOrderRepository 接口实现类</param>
        /// <param name="paraIODR">IOrderDetailRepository 接口实现类</param>
        /// <param name="paraIMODPR">IMarketOrderDetailPrintRepository 接口实现类</param>
        /// <param name="paraparaIPGPR">IProduceGeneralPlanRepository 接口实现类</param>
        /// 创建者：冯吟夷
        public MarketOrderServiceImp(IMarketOrderRepository paraIMOR, IOrderDetailRepository paraIODR, IMarketOrderDetailPrintRepository paraIMODPR, IProduceGeneralPlanRepository paraIPGPR)
        {
            this.iMOR = paraIMOR;
            this.iMODR = paraIODR;
            this.iMODPR = paraIMODPR;
            this.iPGPR = paraIPGPR;
        } //end MarketOrderServiceImp

        #region 客户订单一览

        /// <summary>
        /// 获取表 MK_ORDER 查询记录
        /// </summary>
        /// <param name="paraPIFSTPI">VM_MarketOrderForSearchTableMarketOrder 表单查询类</param>
        /// <param name="paraPage">Paging 分页排序属性类</param>
        /// <returns>VM_MarketOrderForTableMarketOrder 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderForShowMarketOrderInfo> GetMarketOrderListService(VM_MarketOrderForSearchMarketOrderTable paraMOFSMOT, Paging paraPage)
        {
            return iMOR.GetMarketOrderListRepository(paraMOFSMOT, paraPage);
        } //end GetMarketOrderListService

        #endregion

        #region 新增订单

        /// <summary>
        /// 添加表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraMarketOrder">MarketOrder 对象</param>
        /// <param name="paraMarketOrderDetailList"> MarketOrderDetail 泛型结果集</param>
        /// <param name="paraMarketOrderDetailPrintList"> MarketOrderDetailPrint 泛型结果集</param>
        /// <param name="paraProduceGeneralPlanList"> ProduceGeneralPlan 泛型结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public bool AddMarketOrderManagementService(MarketOrder paraMarketOrder, List<MarketOrderDetail> paraMarketOrderDetailList, List<MarketOrderDetailPrint> paraMarketOrderDetailPrintList, List<ProduceGeneralPlan> paraProduceGeneralPlanList)
        {
            //首先要判断是否有该订单号？还是根据主键唯一性原则，以异常的形式返回？
            
            iMOR.Add(paraMarketOrder); //添加表 MK_ORDER 记录

            if (paraMarketOrderDetailList != null && paraMarketOrderDetailList.Count > 0) //判断 MarketOrderDetail 泛型是否有值
            {
                foreach (MarketOrderDetail marketOrderDetail in paraMarketOrderDetailList) //遍历 MarketOrderDetail 泛型
                {
                    iMODR.Add(marketOrderDetail); //添加表 MK_ORDER_DTL 记录
                }
            } //end if

            if (paraMarketOrderDetailPrintList != null && paraMarketOrderDetailPrintList.Count > 0) //判断 MarketOrderDetailPrint 泛型是否有值
            {
                foreach (MarketOrderDetailPrint marketOrderDetailPrint in paraMarketOrderDetailPrintList) //遍历 MarketOrderDetail 泛型
                {
                    iMODPR.Add(marketOrderDetailPrint); //添加表 MK_ORDER_DTL_PRINT 记录
                }
            } //end if

            if (paraProduceGeneralPlanList != null && paraProduceGeneralPlanList.Count > 0) //判断 ProduceGeneralPlan 泛型是否有值
            {
                foreach (ProduceGeneralPlan produceGeneralPlan in paraProduceGeneralPlanList) //遍历 ProduceGeneralPlan 泛型
                {
                    iPGPR.Add(produceGeneralPlan); //添加表 PD_GENERAL_PLAN 记录
                }
            } //end if

            return true; //返回结果
        } //end AddMarketOrderManagementService

        #endregion

        #region 订单查看、修改订单

        /// <summary>
        /// 根据 ClientOrderID、ClientVersion，获取相应对象
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <param name="paraClientVersion">版数</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public VM_MarketOrderForShowMarketOrderInfo GetMarketOrderInfoService(string paraClientOrderID, string paraClientVersion)
        {
            return iMOR.GetMarketOrderInfoRepository(paraClientOrderID, paraClientVersion);
        } //end GetMarketOrderModelService

        #endregion

        #region 修改订单

        /// <summary>
        /// 更新表 MK_ORDER、MK_ORDER_DTL、MK_ORDER_DTL_PRINT 记录
        /// </summary>
        /// <param name="paraMarketOrder">MarketOrder 对象</param>
        /// <param name="paraMarketOrderDetailList"> MarketOrderDetail 泛型结果集</param>
        /// <param name="paraMarketOrderDetailPrintList"> MarketOrderDetailPrint 泛型结果集</param>
        /// <param name="paraProduceGeneralPlanList"> ProduceGeneralPlan 泛型结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public string UpdateMarketOrderManagementService(MarketOrder paraMarketOrder, List<MarketOrderDetail> paraMarketOrderDetailList, List<MarketOrderDetailPrint> paraMarketOrderDetailPrintList, List<ProduceGeneralPlan> paraProduceGeneralPlanList)
        {
            //首先要判断是否有该订单号？还是根据主键唯一性原则，以异常的形式返回？

            iMOR.Update(paraMarketOrder); //更新表 MK_ORDER 记录

            iMODR.DeleteMarketOrderDetailListRepository(paraMarketOrder.ClientOrderID); //根据客户订单号，删除表 MK_ORDER_DTL 相关记录
            if (paraMarketOrderDetailList != null && paraMarketOrderDetailList.Count > 0) //判断 MarketOrderDetail 泛型是否有值
            {
                foreach (MarketOrderDetail marketOrderDetail in paraMarketOrderDetailList) //遍历 MarketOrderDetail 泛型
                {
                    iMODR.Add(marketOrderDetail); //添加表 MK_ORDER_DTL 记录
                } //end foreach
            } //end if

            iMODPR.DeleteMarketOrderDetailPrintListRepository(paraMarketOrder.ClientOrderID); //根据客户订单号，删除表 MK_ORDER_DTL_PRINT 相关记录
            if (paraMarketOrderDetailPrintList != null && paraMarketOrderDetailPrintList.Count > 0) //判断 MarketOrderDetailPrint 泛型是否有值
            {
                foreach (MarketOrderDetailPrint marketOrderDetailPrint in paraMarketOrderDetailPrintList) //遍历 MarketOrderDetail 泛型
                {
                    iMODPR.Add(marketOrderDetailPrint); //添加表 MK_ORDER_DTL_PRINT 记录
                } //end foreach
            } //end if

            //iPGPR.DeleteProduceGeneralPlanListRepository(paraMarketOrder.ClientOrderID); //根据客户订单号，删除表 PD_GENERAL_PLAN 相关记录
            //if (paraProduceGeneralPlanList != null && paraProduceGeneralPlanList.Count > 0) //判断 ProduceGeneralPlan 泛型是否有值
            //{
            //    foreach (ProduceGeneralPlan produceGeneralPlan in paraProduceGeneralPlanList) //遍历 ProduceGeneralPlan 泛型
            //    {

            //        if (iPGPR.Find(produceGeneralPlan) == null) //判断表 PD_GENERAL_PLAN 是否有该条记录,防止插入重复
            //        {
            //            iPGPR.Add(produceGeneralPlan); //添加表 PD_GENERAL_PLAN 记录
            //        }
            //    } //end foreach
            //} //end if

            return "true"; //返回结果
        } //end UpdateMarketOrderManagementService

        #endregion


    } //end MarketOrderServiceImp
}
