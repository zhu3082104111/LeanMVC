/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：OrderDetailRepositoryImp.cs
// 文件功能描述：订单明细表Repository接口实现类
//     
// 修改履历：2013/12/20 冯吟夷 新建
//
// 修改标识：
// 修改描述：
/*****************************************************************************/
using Extensions;
using Model;
using Model.Market;
using Model.Produce;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    /// <summary>
    /// 20131113 梁龙飞
    /// 订单详细资源库实现类
    /// </summary>
    class OrderDetailRepositoryImp : AbstractRepository<DB, MarketOrderDetail>, IOrderDetailRepository
    {
        /// <summary>
        /// 预设时间初始化值,提取到通用部分
        /// </summary>

        #region IOrderDetailRepository 成员

        public IEnumerable<MarketOrderDetail> GetOrderDetailList(MarketOrderDetail searchCondition, Extensions.Paging page)
        {
            throw new NotImplementedException();
        }

        public MarketOrderDetail GetOrderDetail(MarketOrderDetail searchCondition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取没有排产的订单详细
        /// </summary>
        /// <param name="searchConditon">搜索条件</param>
        /// <param name="pagex">分页信息</param>
        /// <returns></returns>
        public IEnumerable<VM_OrderAcceptShow> GetDetailNotScheduling(VM_OrderAcceptSearch searchConditon, Extensions.Paging pagex)
        {
            ////获取审核通过没有生产的订单:不规范的“3”，系统应当提供enum
            //IQueryable<MarketOrder> orderNotAccept = MarketOrder.TestDate().Where(u => (u.StateFlag == "3")).AsQueryable<MarketOrder>();
            //IQueryable<MarketOrderDetail> orderDetail = MarketOrderDetail.TestData().AsQueryable<MarketOrderDetail>();
            ////客户订单号
            //if (!string.IsNullOrEmpty(searchConditon.ClientOrderID))
            //{
            //    orderNotAccept = orderNotAccept.Where(u => u.ClientOrderID.Contains(searchConditon.ClientOrderID));
            //}
            ////规定时间内，注意规避逻辑漏洞：界面传回的搜索视图要格式化边界,newDateTime应当用CONST代替
            //if (searchConditon.DeliveryDateBegin != CONST_FIELD.INIT_DATETIME)
            //{
            //    orderNotAccept = orderNotAccept.Where(u => (u.DeliveryDate >= searchConditon.DeliveryDateBegin));
            //    if (searchConditon.DeliveryDateEnd != CONST_FIELD.INIT_DATETIME)
            //    {
            //        orderNotAccept = orderNotAccept.Where(u => (u.DeliveryDate < searchConditon.DeliveryDateEnd.AddDays(1)));
            //    }
            //}
            //IQueryable<VM_OrderAcceptShow> resultDate = from order in orderNotAccept
            //                                              join detail in orderDetail
            //                                              on order.ClientOrderID equals detail.ClientOrderID
            //                                              orderby order.ClientOrderID,detail.ProductID
            //                                              select new VM_OrderAcceptShow
            //                                              {
            //                                                  ClientOrderID=order.ClientOrderID,
            //                                                  DeliveryDate = (DateTime)detail.DeliveryDate,//界面显示是strDeliveryDate
            //                                                  ClientOrderDetail=detail.ClientOrderDetail,
            //                                                  ProductID=detail.ProductID,
            //                                                  Quantity=detail.Quantity,
            //                                                  PackageQuantity=detail.PackageQuantity,
            //                                                  PackageSize=detail.PackageSize
            //                                              };
            ////产品,检索出包含此种产品的订单的详细信息
            //if (!string.IsNullOrEmpty(searchConditon.ProductID))
            //{

            //}
            ////分页信息
            //pagex.total = resultDate.Count();
            //IEnumerable<VM_OrderAcceptShow> result = resultDate.ToPageList<VM_OrderAcceptShow>("ClientOrderID asc", pagex);
            //return result;
            return null;
        }

        public bool addOrderDetail(MarketOrderDetail target)
        {
            throw new NotImplementedException();
        }

        public bool updateOrderDetail(MarketOrderDetail target)
        {
            throw new NotImplementedException();
        }

        public bool deleteOrderDetail(MarketOrderDetail target)
        {
            throw new NotImplementedException();
        }

        public DateTime? GetOrderDetailDeliveryDate(string productID, string customerOrderID, string customerOrderDetails)
        {
            MarketOrderDetail entity = base.GetAvailableList<MarketOrderDetail>().FirstOrDefault(a => a.ProductID.Equals(productID) && a.ClientOrderID.Equals(customerOrderID) && a.ClientOrderDetail.Equals(customerOrderDetails));
            if (entity == null)
            {
                return null;
            }
            return entity.DeliveryDate;
        }

        public MarketOrderDetail GetOrderDetail(string customerOrderID, string customerOrderDetails)
        {
            MarketOrderDetail entity =
                base.GetAvailableList<MarketOrderDetail>()
                    .FirstOrDefault(
                        a => a.ClientOrderID.Equals(customerOrderID) && a.ClientOrderDetail.Equals(customerOrderDetails));
            return entity;
        }

        public IEnumerable<VM_ProduceType> GetProduceType(string clientOrderID)
        {
            IQueryable<MarketOrderDetail> marOrdDets =
                base.GetAvailableList<MarketOrderDetail>().Where(t => t.ClientOrderID.Equals(clientOrderID));
            IQueryable<ProdInfo> proInfs = base.GetAvailableList<ProdInfo>();

            IQueryable<VM_ProduceType> proType = from mod in marOrdDets
                                                 join pif in proInfs on mod.ProductID equals pif.ProductId into gif
                                                 from prodInfo in gif.DefaultIfEmpty()
                                                 select new VM_ProduceType
                                                 {
                                                     ProductIdCOD = mod.ProductID + "," + mod.ClientOrderDetail,
                                                     ProdAbbrev = prodInfo != null ? prodInfo.ProdAbbrev + "("+mod.ClientOrderDetail+")" : ""
                                                 };

            return proType;
        }

        #endregion


        /// <summary>
        /// 根据客户订单号，获取结果集
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns>VM_MarketOrderDetailForMarketOrderDetailTable 表格显示类</returns>
        /// 创建者：冯吟夷
        public IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> GetMarketOrderDetailListRepository(string paraClientOrderID)
        {
            IQueryable<MarketOrderDetail> marketOrderDetailIQ = base.GetList().Where(mod => mod.ClientOrderID.Equals(paraClientOrderID)); //根据客户订单号，获取表 MK_ORDER_DTL 结果集
            IQueryable<ProdInfo> productInformationIQ=base.GetList<ProdInfo>(); //获取表 PD_PROD_INFO 结果集
            IQueryable<ProduceGeneralPlan> produceGeneralPlanIQ = base.GetList<ProduceGeneralPlan>(); //获取表 PD_GENERAL_PLAN 结果集
            IQueryable<MasterDefiInfo> mdiPCNIQ = base.GetList<MasterDefiInfo>().Where(mdi => mdi.SectionCd.Equals("00009")); //获取表 BI_MASTER_DEFI_INFO 部门ID结果集
            IQueryable<MasterDefiInfo> mdiSCNIQ = base.GetList<MasterDefiInfo>().Where(mdi => mdi.SectionCd.Equals("00051")); //获取表 BI_MASTER_DEFI_INFO 颜色分类结果集
            //多连接
            IQueryable<VM_MarketOrderDetailForMarketOrderDetailTable> resultIQ = from modIQ in marketOrderDetailIQ
                                                                                 join piIQ in productInformationIQ on modIQ.ProductID equals piIQ.ProductId into JoinMODPI
                                                                                 from piIQ in JoinMODPI.DefaultIfEmpty()
                                                                                 join pcnIQ in mdiPCNIQ on modIQ.ProduceCellID equals pcnIQ.AttrCd into JoinMODMDIPCN
                                                                                 from pcnIQ in JoinMODMDIPCN.DefaultIfEmpty()
                                                                                 join scnIQ in mdiSCNIQ on modIQ.SealColorID equals scnIQ.AttrCd into JoinMODMDISCN
                                                                                 from scnIQ in JoinMODMDISCN.DefaultIfEmpty()
                                                                                 join pgpIQ in produceGeneralPlanIQ on new { COID = modIQ.ClientOrderID, COD = modIQ.ClientOrderDetail } equals new { COID = pgpIQ.ClientOrderID, COD = pgpIQ.ClientOrderDetail } into JoinMODPGP
                                                                                 from pgpIQ in JoinMODPGP.DefaultIfEmpty()
                                                                                 select new VM_MarketOrderDetailForMarketOrderDetailTable
                                                                                 {
                                                                                     ClientOrderID = modIQ.ClientOrderID,
                                                                                     ClientOrderDetail = modIQ.ClientOrderDetail,
                                                                                     ProductID = modIQ.ProductID,
                                                                                     ProductAbbreviation = piIQ.ProdAbbrev,
                                                                                     MODDeliveryDate = modIQ.DeliveryDate,
                                                                                     ProduceCellID = modIQ.ProduceCellID,
                                                                                     ProduceCellName = pcnIQ.AttrValue,
                                                                                     Quantity = modIQ.Quantity,
                                                                                     ClientProductID = modIQ.ClientProductID,
                                                                                     PackageQuantity = modIQ.PackageQuantity,
                                                                                     PackageSize = modIQ.PackageSize,
                                                                                     OriginalEquipmentManufacturerID = modIQ.OriginalEquipmentManufacturerID,
                                                                                     OriginalEquipmentManufacturerName = null, //暂时不需要写函数获取
                                                                                     ImageName = modIQ.ImageName,
                                                                                     SealColorID = modIQ.SealColorID,
                                                                                     SealColorName = scnIQ.AttrValue,
                                                                                     SealRequire = modIQ.SealRequire,
                                                                                     SealPicture = modIQ.SealPicture,
                                                                                     Urgency = modIQ.Urgency,
                                                                                     UrgencyName = null, //暂时不需要写函数获取
                                                                                     Status=pgpIQ.Status,
                                                                                     StatusName = null //暂时不需要写函数获取
                                                                                 };
            IEnumerable<VM_MarketOrderDetailForMarketOrderDetailTable> resultIE = resultIQ.ToList().AsEnumerable(); //执行操作，获取查询数据至内存

            return resultIE; //返回结果集

        } //end GetMarketOrderDetailListRepository

        /// <summary>
        /// 根据客户订单号，删除表 MK_ORDER_DTL 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public bool DeleteMarketOrderDetailListRepository(string paraClientOrderID) 
        {
            StringBuilder modDeleteCommandSB = new StringBuilder();
            modDeleteCommandSB.Append("delete from mkod from MK_ORDER_DTL as mkod ");
            //modDeleteCommandSB.Append("join PD_GENERAL_PLAN as pgp on mkod.CLN_ODR_ID=pgp.CLN_ODR_ID and mkod.CLN_ODR_DTL=pgp.CLN_ODR_DTL ");
            modDeleteCommandSB.AppendFormat("where mkod.CLN_ODR_ID='{0}'", paraClientOrderID);

            return base.ExecuteStoreCommand(modDeleteCommandSB.ToString());
        }


    } //end OrderDetailRepositoryImp
}
