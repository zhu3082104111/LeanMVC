/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MarketOrderDetailPrintRepositoryImp.cs
// 文件功能描述：订单明细表打字Repository接口实现类
//     
// 修改履历：2013/11/26 朱静波 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Model;
using Model.Market;
using Repository.database;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Repository
{
    class MarketOrderDetailPrintRepositoryImp : AbstractRepository<DB, MarketOrderDetailPrint>, IMarketOrderDetailPrintRepository
    {
        public IEnumerable<MarketOrderDetailPrint> GetTyping(string productID, string customerOrderID, string customerOrderDetails)
        {
            return
                base.GetAvailableList<MarketOrderDetailPrint>().Where(
                    a =>
                        a.ClientOrderID == customerOrderID && a.ClientOrderDetail == customerOrderDetails &&
                        a.ProductID == productID);
        }


        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable> GetMarketOrderDetailPrintListRepository(string paraClientOrderID)
        {
            IQueryable<MarketOrderDetailPrint> marketOrderDetailPrintIQ = base.GetList().Where(modp => modp.ClientOrderID.Equals(paraClientOrderID)); //根据客户订单号，获取表 MK_ORDER_DTL_PRINT 结果集
            IQueryable<ProdInfo> productInformationIQ = base.GetList<ProdInfo>(); //获取表 PD_PROD_INFO 结果集
            IQueryable<ProduceGeneralPlan> produceGeneralPlanIQ = base.GetList<ProduceGeneralPlan>(); //获取表 PD_GENERAL_PLAN 结果集
            //多连接
            IQueryable<VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable> resultIQ = from modpIQ in marketOrderDetailPrintIQ
                                                                                           join piIQ in productInformationIQ on modpIQ.ProductID equals piIQ.ProductId into JoinMODPPI
                                                                                           from piIQ in JoinMODPPI.DefaultIfEmpty()
                                                                                           join pgpIQ in produceGeneralPlanIQ on new { COID = modpIQ.ClientOrderID, COD = modpIQ.ClientOrderDetail } equals new { COID = pgpIQ.ClientOrderID, COD = pgpIQ.ClientOrderDetail } into JoinMODPPGP
                                                                                           from pgpIQ in JoinMODPPGP.DefaultIfEmpty()
                                                                                           select new VM_MarketOrderDetailPrintForMarketOrderDetailPrintTable
                                                                                           {
                                                                                               ClientOrderID = modpIQ.ClientOrderID,
                                                                                               ClientOrderDetailPrint = modpIQ.ClientOrderDetail,
                                                                                               NO = modpIQ.NO,
                                                                                               MODPProductID = modpIQ.ProductID,
                                                                                               ProductAbbreviation = piIQ.ProdAbbrev,
                                                                                               Position = modpIQ.Position,
                                                                                               Content = modpIQ.Content,
                                                                                               ImageName = modpIQ.ImageName,
                                                                                               Status = pgpIQ.Status,
                                                                                               StatusName = null //暂时不需要写函数获取
                                                                                           };
            return resultIQ.ToList().AsEnumerable(); //返回结果集
                                                                        
        } //end GetMarketOrderDetailPrintListRepository

        /// <summary>
        /// 根据客户订单号，删除表 MK_ORDER_DTL_PRINT 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public bool DeleteMarketOrderDetailPrintListRepository(string paraClientOrderID)
        {
            StringBuilder modpDeleteCommandSB = new StringBuilder();
            modpDeleteCommandSB.Append("delete from modp from MK_ORDER_DTL_PRINT as modp ");
            //modpDeleteCommandSB.Append("join PD_GENERAL_PLAN as pgp on modp.CLN_ODR_ID=pgp.CLN_ODR_ID and modp.CLN_ODR_DTL=pgp.CLN_ODR_DTL ");
            modpDeleteCommandSB.AppendFormat("where modp.CLN_ODR_ID='{0}'", paraClientOrderID);

            return base.ExecuteStoreCommand(modpDeleteCommandSB.ToString());
        }


    } //end MarketOrderDetailPrintRepositoryImp
}
