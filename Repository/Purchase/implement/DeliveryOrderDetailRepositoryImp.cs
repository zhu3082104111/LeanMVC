/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：DeliveryOrderDetailRepositoryImp.cs
// 文件功能描述：
//          送货单详细表的Repository接口的实现类
//      
// 修改履历：2013/12/10 刘云 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 送货单详细表的Repository接口的实现类
    /// </summary>
    public class DeliveryOrderDetailRepositoryImp : AbstractRepository<DB, MCDeliveryOrderDetail>, IDeliveryOrderDetailRepository
    {
        /// <summary>
        /// 得到将要在页面上显示的数据
        /// </summary>
        /// <param name="searchConditon">筛选条件</param>
        /// <returns></returns>
        public IEnumerable GetDeliveryOrderDetailBySearchById(VM_DeliveryOrderForShow searchConditon)
        {
            //MCDeliveryOrder表数据
            IQueryable<MCDeliveryOrder> order = base.GetList<MCDeliveryOrder>();
            //MCDeliveryOrderDetail表数据
            IQueryable<MCDeliveryOrderDetail> detail=base.GetList();
            //单位表
            IQueryable<UnitInfo> uInfo = base.GetList<UnitInfo>();
            //成品信息表
            IQueryable<ProdInfo> prodInfo = base.GetList<ProdInfo>();
            //零件信息表
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>();
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodInfo
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName,
                                        unit = prod.UnitId
                                    }
                                ).Union
                               (
                                   from part in partInfo
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName,
                                       unit = part.UnitId
                                   }
                               );
            //对送货单号的判断
            if (!String.IsNullOrEmpty(searchConditon.DeliveryOrderID))
            {
                detail = detail.Where(d => d.DeliveryOrderID == searchConditon.DeliveryOrderID);
            }
            var query = from o in order
                        join d in detail on o.DeliveryOrderID equals d.DeliveryOrderID
                        join p in prodAndPartsList on d.MaterielID equals p.id
                        join u in uInfo on p.unit equals u.UnitId
                        select new {
                            OrderNo=o.OrderNo,
                            MaterielID=p.abbrev,
                            MaterielName = p.name,
                            //ProductPartID deliveryOrderType 0外购 1外协
                            MaterialsSpec=d.MaterialsSpec,
                            WarehouseID=d.WarehouseID,
                            Unit=u.UnitName,
                            Quantity=d.Quantity,
                            PriceWithTax=d.PriceWithTax,
                            CkPriceWithTax=d.CkPriceWithTax,
                            ActualQuantity=d.ActualQuantity,
                            Remarks=d.Remarks,
                            InnumQuantity=d.InnumQuantity,
                            Num=d.Num,
                            Scrap=d.Scrap,
                            DeliveryDate=o.DeliveryDate,
                            DeliveryUID=o.DeliveryUID,
                            DeliveryCompanyID=o.DeliveryCompanyID,
                            TelNo=o.TelNo
                        };
            return query;
        }

        /// <summary>
        /// 导入显示的数据
        /// </summary>
        /// <param name="orderNo">采购计划单号</param>
        /// <returns></returns>
        public IEnumerable GetImportInfo(string orderNo)
        {
            //成品信息表
            IQueryable<ProdInfo> prodInfo = base.GetList<ProdInfo>();
            //零件信息表
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>();
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodInfo
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName,
                                        unit = prod.UnitId
                                    }
                                ).Union
                               (
                                   from part in partInfo
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName,
                                       unit = part.UnitId
                                   }
                               );
            IQueryable<UnitInfo> uInfo = base.GetList<UnitInfo>();
            //得到第7、8位，判断是外购还是外协
            string check = orderNo.Substring(6, 2);
            if (check == "WG")
            {
                //物料编码、物料名称、规格、仓库编号、单位、数量、含税价格、核实含税单价
                IQueryable<MCOutSourceOrderDetail> detail = base.GetList<MCOutSourceOrderDetail>();
                IQueryable<MCOutSourceOrder> order = base.GetList<MCOutSourceOrder>();
                IQueryable<CompInfo> comp = base.GetList<CompInfo>();
                if (!String.IsNullOrEmpty(orderNo))
                {
                    detail = detail.Where(d => d.OutOrderID == orderNo);
                    order = order.Where(o => o.OutOrderID == orderNo);
                }
                var query = from d in detail
                            join o in order on d.OutOrderID equals o.OutOrderID
                            join p in prodAndPartsList on d.ProductPartID equals p.id
                            join u in uInfo on p.unit equals u.UnitId
                            select new
                            {
                                DeliveryCompanyName=comp.Where(c=>c.CompId==o.OutCompanyID).FirstOrDefault().CompName,
                                MaterielID = p.abbrev,
                                Materiel = p.id,
                                MaterielName = p.name,
                                MaterialsSpec = d.MaterialsSpecReq,
                                WarehouseID = d.WarehouseID,
                                Unit =u.UnitName,
                                Quantity = d.RequestQuantity,
                                UnitID=p.unit
                                //PriceWithTax  CkPriceWithTax
                            };
                return query;
            }
            else
            {
                IQueryable<MCSupplierOrderDetail> detail = base.GetList<MCSupplierOrderDetail>();
                IQueryable<MCSupplierOrder> order = base.GetList<MCSupplierOrder>();
                IQueryable<CompInfo> comp = base.GetList<CompInfo>();
                if (!String.IsNullOrEmpty(orderNo))
                {
                    detail = detail.Where(d => d.SupOrderID == orderNo);
                    order = order.Where(o => o.SupOrderID == orderNo);
                }
                
                var query = from d in detail
                            join o in order on d.SupOrderID equals o.SupOrderID
                            join p in prodAndPartsList on d.ProductPartID equals p.id
                            join u in uInfo on p.unit equals u.UnitId
                            select new
                            {
                                DeliveryCompanyName = comp.Where(c => c.CompId == o.OutCompanyID).FirstOrDefault().CompName,
                                MaterielID = p.abbrev,
                                Materiel = p.id,
                                PdtName= p.name,
                                MaterialsSpec = d.MaterialsSpecReq,
                                WarehouseID = d.WarehouseID,
                                Unit=u.UnitName,
                                Quantity = d.RequestQuantity,
                                UnitID=p.unit
                                //PriceWithTax  CkPriceWithTax
                            };
                return query;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="deliveryOrder"></param>
        /// <returns></returns>
        public bool UpdateDetail(MCDeliveryOrderDetail deliveryOrder)
        {
            //成品信息表
            IQueryable<ProdInfo> prodInfo = base.GetList<ProdInfo>();
            //零件信息表
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>();
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodInfo
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partInfo
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            string deliveryOrderID = deliveryOrder.DeliveryOrderID;
            string materielID = prodAndPartsList.Where(p => p.abbrev == deliveryOrder.MaterielID).FirstOrDefault().id;
            decimal innumQuantity = deliveryOrder.InnumQuantity;
            decimal num = deliveryOrder.Num;
            decimal scrap = deliveryOrder.Scrap;
            decimal actualQuantity = deliveryOrder.ActualQuantity;
            string remarks = deliveryOrder.Remarks;
            return base.ExecuteStoreCommand("UPDATE MC_DELIVERY_ORDER_DTL SET PKG_INNUM_QTY={0},PKG_NUM={1},PKG_SCRAP={2},ACT_REC_QTY={3},RMRS={4},UPD_USR_ID={5},UPD_DT={6} WHERE DLY_ODR_ID={7} AND PDT_ID={8}", innumQuantity, num, scrap, actualQuantity, remarks, deliveryOrder.UpdUsrID, deliveryOrder.UpdDt, deliveryOrderID, materielID);
        }

        /// <summary>
        /// 删除送货单（更新送货单详细表的信息）
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <param name="uId">登录用户ID</param>
        /// <param name="systime">系统时间</param>
        /// <returns>处理结果</returns>
        public bool Delete(string deliveryOrderID, string uId, DateTime systime)
        {
            // SQL语句
            string sql = "UPDATE MC_DELIVERY_ORDER_DTL " +
                         "SET DEL_FLG = {0}, " + 
                         "UPD_USR_ID = {1}, " +
                         "UPD_DT = {2}, " +
                         "DEL_USR_ID = {3}, " + 
                         "DEL_DT = {4} " +
                         "WHERE DLY_ODR_ID = {5} ";
            
            // 返回执行结果
            return base.ExecuteStoreCommand(sql, Constant.CONST_FIELD.DELETED, uId, systime, uId, systime, deliveryOrderID);
        }

    }
}
