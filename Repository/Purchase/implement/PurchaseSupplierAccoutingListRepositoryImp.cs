/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseSupplierAccoutingListRepositoryImp.cs
// 文件功能描述：
//          外购外协计划台账一览接口的实现类
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository.database;
using Repository.Purchase;

namespace Repository
{
    /// <summary>
    /// 外购外协计划台帐一览画面的Repository实现
    /// </summary>
    public class PurchaseSupplierAccoutingListRepositoryImp : AbstractRepository<DB, MCOutSourceOrder>, IPurchaseSupplierAccoutingListRepository
    {
        /// <summary>
        /// 外购计划台账一览的数据显示
        /// </summary>
        /// <param name="searchCondition">查询条件</param>
        /// <param name="paging">分页显示</param>
        /// <returns>外购计划台账一览的显示数据</returns>
        public IEnumerable GetPurchaseAccoutingListBySearchByPage(VM_PurchaseAccoutingListForSearch searchCondition, Paging paging)
        {
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>().FilterBySearch(searchCondition);

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>().FilterBySearch(searchCondition);

            // 供货商信息表
            IQueryable<CompInfo> CompList = base.GetAvailableList<CompInfo>().FilterBySearch(searchCondition);

            // 外购单表
            IQueryable<MCOutSourceOrder> purchaseAccoutingListInfo = base.GetAvailableList<MCOutSourceOrder>().FilterBySearch(searchCondition);

            // 外购单详细表
            IQueryable<MCOutSourceOrderDetail> purchaseDetailInfo = base.GetAvailableList<MCOutSourceOrderDetail>().FilterBySearch(searchCondition);

            // Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>().FilterBySearch(searchCondition);

            #region  成品零件表
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            #endregion

            #region 过滤查询条件
            // 查询功能 对客户订单号的判断  外购单详细表的客户订单号+客户订单明细号
            if (!String.IsNullOrEmpty(searchCondition.CustomerOrder))
            {
                // 把传进的客户订单号送到外购单详细对象中并检索出其中数据
                purchaseDetailInfo = purchaseDetailInfo.Where(p => (p.CustomerOrderID + "-" + p.CustomerOrderDetailID).Contains(searchCondition.CustomerOrder));
            }

            // 查询功能   物料编号
            if (!String.IsNullOrEmpty(searchCondition.MaterielNo))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.abbrev.Contains(searchCondition.MaterielNo));
            }

            // 查询功能   物料名称
            if (!String.IsNullOrEmpty(searchCondition.MaterielName))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.name.Contains(searchCondition.MaterielName));
            }
            #endregion

            #region 合并显示结果
            // 合并外购单详细表的相同物料求总数（按照外购单号，产品零件ID，规格型号，交货日期,完成状态，备注group by）
            var purchaseDetail = from p in purchaseDetailInfo
                                 group p by new
                                 {
                                     p.OutOrderID,
                                     p.ProductPartID,
                                     p.MaterialsSpecReq,
                                     p.DeliveryDate,
                                     p.CompleteStatus,
                                     p.Remarks
                                 } into de
                                 select new
                                 {
                                     de.Key,
                                     ActualQuantity = de.Sum(detail => detail.ActualQuantity + detail.OtherQuantity),
                                     RequestQuantity = de.Sum(detail => detail.RequestQuantity)
                                 };
            #endregion

            // 生成外购计划台账查询表
            IQueryable<VM_PurchaseAccoutingListForTableShow> query = from purdetail in purchaseDetail
                                                                     join s in purchaseAccoutingListInfo on purdetail.Key.OutOrderID equals s.OutOrderID
                                                                     join com in CompList on s.OutCompanyID equals com.CompId
                                                                     join pp in prodAndPartsList on purdetail.Key.ProductPartID equals pp.id
                                                                     select new VM_PurchaseAccoutingListForTableShow
                                                                     {
                                                                         CompletStatusCd = purdetail.Key.CompleteStatus,
                                                                         CompletStatus = (master.Where(m => m.AttrCd == purdetail.Key.CompleteStatus && m.SectionCd.Equals(Constant.MasterSection.COMPLETE_STATUS))).FirstOrDefault().AttrValue,  //完成状态 关联Master表
                                                                         UrgentStatus = (master.Where(m => m.AttrCd == s.UrgentStatus && m.SectionCd.Equals(Constant.MasterSection.URGENT_STATE))).FirstOrDefault().AttrValue,  //紧急状态 关联Master表                                                                      
                                                                         OutOrderNo = purdetail.Key.OutOrderID, //外购单号
                                                                         Remarks = purdetail.Key.Remarks, //备注
                                                                         CompName = com.CompName, //供货商名称
                                                                         MaterialsSpecReq = purdetail.Key.MaterialsSpecReq, //规格型号 
                                                                         DeliveryDate = purdetail.Key.DeliveryDate, //交货日期
                                                                         ArrivalQuantity = purdetail.ActualQuantity, //实收数量
                                                                         OrderedQuantity = purdetail.RequestQuantity, //订货数量
                                                                         MaterialNo = pp.abbrev, //物料编号
                                                                         MaterialName = pp.name, //物料名称
                                                                         MarginQuantity = purdetail.RequestQuantity - purdetail.ActualQuantity //订单差额
                                                                     };
            paging.total = query.Count();

            IEnumerable<VM_PurchaseAccoutingListForTableShow> queryResult = query.ToPageList<VM_PurchaseAccoutingListForTableShow>("OutOrderNo asc", paging);

            // 背景颜色Flg的设置
            List<VM_PurchaseAccoutingListForTableShow> result = queryResult.ToList<VM_PurchaseAccoutingListForTableShow>();
            for (int i = 0; i < result.Count(); i++)
            {
                // 交货日期
                DateTime deliveryDate = (DateTime)result.ElementAt(i).DeliveryDate;
                // 完成状态
                string completStatus = result.ElementAt(i).CompletStatusCd;
                // 设置背景颜色Flg
                result.ElementAt(i).BGColorFlag = getBGColorFlag(deliveryDate, completStatus);
            }

            return result;
        }

        /// <summary>
        /// 外购计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        public IEnumerable GetPurchaseAccoutingDetailByNo(string OutOrderNo)
        {
            #region 产品零件信息
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();

            // 合并产品信息和零件信息
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            # endregion

            #region 采购信息
            // 外购单详细表
            IQueryable<MCOutSourceOrderDetail> purchaseDetailInfo = base.GetAvailableList<MCOutSourceOrderDetail>();

            // 根据参数的外购单号关联外购详细表的主表信息
            purchaseDetailInfo = purchaseDetailInfo.Where(purdetail => purdetail.OutOrderID == OutOrderNo);

            // 合并外购单详细表的相同物料求总数（按照产品零件ID，规格型号及要求，交货日期group by）
            var purchaseDetailsObject = from de in purchaseDetailInfo
                                        group de by new
                                        {
                                            de.OutOrderID,
                                            de.ProductPartID,
                                            de.MaterialsSpecReq,
                                            de.DeliveryDate
                                        } into objDetails
                                        select new
                                        {
                                            objDetails.Key,
                                            TotalQuantity = objDetails.Sum(detail => detail.RequestQuantity)
                                        };
            #endregion

            #region 送货信息
            // 送货详细表
            IQueryable<MCDeliveryOrderDetail> deliveryOrderDetailInfo = base.GetAvailableList<MCDeliveryOrderDetail>();

            // 送货单表
            IQueryable<MCDeliveryOrder> deliveryOrderInfo = base.GetAvailableList<MCDeliveryOrder>();

            // 抽取有效的送货信息
            var deliveryInfo = from d in deliveryOrderInfo
                               join de in deliveryOrderDetailInfo on d.DeliveryOrderID equals de.DeliveryOrderID
                               select new
                               {
                                   d.DeliveryOrderID,
                                   d.OrderNo,
                                   d.DeliveryDate,
                                   de.MaterielID,
                                   de.ActualQuantity
                               };
            #endregion

            #region 品保信息
            // 进货检验单表
            IQueryable<PurChkList> purChkListInfo = base.GetAvailableList<PurChkList>();

            //Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();
            master = master.Where(m => m.SectionCd.Equals(Constant.MasterSection.LDK_PUR_CHK_RES));

            // 抽取有用的品保信息
            var purCheckInfoList = from chk in purChkListInfo
                                   join m in master on chk.LdkComRes equals m.AttrCd into chkRes
                                   from m in chkRes.DefaultIfEmpty()
                                   select new
                                   {
                                       chk.ChkListId,   // 质检单号
                                       chk.ChkDt,       // 质检日期
                                       chk.PartId,      // 零件ID
                                       chk.DlyOdrId,    // 送货单号
                                       chk.LdkComRes,   // 质检结果（总和判定）Code
                                       m.AttrValue      // 质检结果（总和判定）Value
                                   };
            #endregion

            #region 入库信息
            //附件库入库履历表
            IQueryable<AccInRecord> accInRecord = base.GetAvailableList<AccInRecord>();

            //附件库入库履历详细表
            IQueryable<AccInDetailRecord> accInDetailRecord = base.GetAvailableList<AccInDetailRecord>();

            //半成品入库履历表
            IQueryable<SemInRecord> semInRecord = base.GetAvailableList<SemInRecord>();

            //半成品入库履历详细表
            IQueryable<SemInDetailRecord> semInDetailRecord = base.GetAvailableList<SemInDetailRecord>();

            //在制品库入库履历表
            IQueryable<WipInRecord> wipInRecord = base.GetAvailableList<WipInRecord>();

            //在制品库入库履历详细表
            IQueryable<WipInDetailRecord> wipInDetailRecord = base.GetAvailableList<WipInDetailRecord>();




            // 合并三个入库详细表（送货单号，入库单号，入库日期，零件id，数量）
            var inStorageInfoList = (
                                   from aidr in accInDetailRecord
                                   join air in accInRecord on aidr.McIsetInListID equals air.McIsetInListID
                                   select new
                                   {
                                       deliveryOrderNo = air.DlvyListID,
                                       id = aidr.McIsetInListID,
                                       date = aidr.InDate,
                                       pdtId = aidr.PdtID,
                                       num = aidr.Qty
                                   }
                                ).Union(
                                   from sidr in semInDetailRecord
                                   join sir in semInRecord on sidr.TecnPdtInId equals sir.TecnPdtInId
                                   select new
                                   {
                                       deliveryOrderNo = sir.DlvyListId,
                                       id = sidr.TecnPdtInId,
                                       date = sidr.InDate,
                                       pdtId = sidr.PdtId,
                                       num = sidr.Qty
                                   }
                               ).Union
                               (
                                   from widr in wipInDetailRecord
                                   join wir in wipInRecord on widr.TecnPdtInID equals wir.TecnPdtInID
                                   select new
                                   {
                                       deliveryOrderNo = wir.DlvyListID,
                                       id = widr.TecnPdtInID,
                                       date = widr.InDate,
                                       pdtId = widr.PdtID,
                                       num = widr.Qty
                                   }
                               );

            #endregion

            //生成外购计划台账查询表
            IQueryable<VM_PurchaseAccoutingDetail4Table> query = from d in purchaseDetailsObject
                                                                           join pp in prodAndPartsList on d.Key.ProductPartID equals pp.id
                                                                           // 关联送货信息
                                                                           join dev in deliveryInfo
                                                                           on new { OUTORDERNO = d.Key.OutOrderID, PRODPARTID = d.Key.ProductPartID }
                                                                           equals new { OUTORDERNO = dev.OrderNo, PRODPARTID = dev.MaterielID }
                                                                           into delivery
                                                                           from dev in delivery.DefaultIfEmpty()
                                                                           // 关联质检信息
                                                                           join chk in purCheckInfoList
                                                                           on new { DELIVERYNO = dev.DeliveryOrderID, PRODPARTID = dev.MaterielID }
                                                                           equals new { DELIVERYNO = chk.DlyOdrId, PRODPARTID = chk.PartId }
                                                                           into chkInfo
                                                                           from chk in chkInfo.DefaultIfEmpty()
                                                                           // 关联入库信息
                                                                           join ins in inStorageInfoList
                                                                           on new { DELIVERYNO = chk.DlyOdrId, PROPARTID = chk.PartId }
                                                                           equals new { DELIVERYNO = ins.deliveryOrderNo, PROPARTID = ins.pdtId }
                                                                           into insInfo
                                                                           from ins in insInfo.DefaultIfEmpty()
                                                                           select new VM_PurchaseAccoutingDetail4Table
                                                                           {
                                                                               MaterialNo = pp.abbrev,                      // 物料编号
                                                                               MaterialName = pp.name,                      // 物料名称
                                                                               MaterialsSpecReq = d.Key.MaterialsSpecReq,   // 规格型号及要求
                                                                               OrderQuantity = d.TotalQuantity,             // 订单总数
                                                                               OrderDate = d.Key.DeliveryDate,              // 交货日期
                                                                               DeliveryOrderNo = dev.DeliveryOrderID,       // 送货单号
                                                                               DeliveryDate = dev.DeliveryDate,             // 送货日期
                                                                               DeliveryQuantity = dev.ActualQuantity,       // 送货数量
                                                                               QCOrderNo = chk.ChkListId,                   // 质检单号
                                                                               QCDate = chk.ChkDt,                          // 质检日期
                                                                               QCResult = chk.AttrValue,                    // 质检结果
                                                                               StorageOrderNo = ins.id,                     // 入库单号
                                                                               StorageDate = ins.date,                      // 入库日期
                                                                               StorageQuantity = ins.num                    // 入库数量
                                                                           };

            IEnumerable<VM_PurchaseAccoutingDetail4Table> result = query.AsEnumerable<VM_PurchaseAccoutingDetail4Table>();

            return result;
        }

        /// <summary>
        /// 外购单的基本信息的查询方法
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        public VM_PurchaseAccoutingDetail GetOrderByNo(string OutOrderNo)
        {
            // 实例化视图Model类
            VM_PurchaseAccoutingDetail order = new VM_PurchaseAccoutingDetail();
            
            // 取得外购单的实体类
            MCOutSourceOrder purchase = base.GetEntityById(OutOrderNo);
            
            // 供货商信息表
            IQueryable<CompInfo> CompList = base.GetAvailableList<CompInfo>();

            // Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();

            // 加载视图Model类
            // 外购单号
            order.OutOrderId = purchase.OutOrderID;
            // 供货商名
            order.CompName = CompList.Where(com => com.CompId == purchase.OutCompanyID).FirstOrDefault().CompName;
            // 紧急状态
            order.UrgentStatus = (master.Where(m => m.AttrCd == purchase.UrgentStatus && m.SectionCd == Constant.MasterSection.URGENT_STATE)).FirstOrDefault().AttrValue;
            // 下单日期
            order.EstablishDate = purchase.EstablishDate;
            // 生产部门
            order.DeptName = (master.Where(m => m.AttrCd == purchase.DepartmentID && m.SectionCd == Constant.MasterSection.DEPT)).FirstOrDefault().AttrValue;

            // 返回结果
            return order;
        }

        /// <summary>
        /// 外协单表台账一览的数据显示
        /// </summary>
        /// <param name="searchCondition">查询条件的视图</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public IEnumerable GetSupplierAccoutingListBySearchByPage(VM_SupplierAccoutingList4Search searchCondition, Paging paging)
        {
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>().FilterBySearch(searchCondition);

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>().FilterBySearch(searchCondition);

            // 供货商信息表
            IQueryable<CompInfo> companyList = base.GetAvailableList<CompInfo>().FilterBySearch(searchCondition);

            // Master数据管理表(完成状态)
            IQueryable<MasterDefiInfo> compStsMaster = base.GetList<MasterDefiInfo>().FilterBySearch(searchCondition);

            // Master数据管理表(紧急状态)
            IQueryable<MasterDefiInfo> urgtStsMaster = base.GetList<MasterDefiInfo>().FilterBySearch(searchCondition);

            // 外协加工调度单表
            IQueryable<MCSupplierOrder> supplierOrderInfo = base.GetAvailableList<MCSupplierOrder>().FilterBySearch(searchCondition);
            
            // 外协加工调度单详细表
            IQueryable<MCSupplierOrderDetail> supplierOrderDetailInfo = base.GetAvailableList<MCSupplierOrderDetail>().FilterBySearch(searchCondition);

            // 工序表
            IQueryable<Process> process = base.GetList<Process>().FilterBySearch(searchCondition);

            // 完成状态的数据
            compStsMaster = compStsMaster.Where(m => m.SectionCd.Equals(Constant.MasterSection.COMPLETE_STATUS));

            // 紧急状态的数据
            urgtStsMaster = urgtStsMaster.Where(m => m.SectionCd.Equals(Constant.MasterSection.URGENT_STATE));

            #region  成品零件表
            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               );
            #endregion

            #region 过滤查询条件
            // 客户订单号
            if (!String.IsNullOrEmpty(searchCondition.CustomerOrder))
            {
                //把传进的客户订单号送到外协单详细对象中并检索出其中数据
                supplierOrderDetailInfo = supplierOrderDetailInfo.Where(s => (s.CustomerOrderID + "-" + s.CustomerOrderDetailID).Contains(searchCondition.CustomerOrder));
            }

            // 物料编号
            if (!String.IsNullOrEmpty(searchCondition.MaterielNo))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.abbrev.Contains(searchCondition.MaterielNo));
            }

            // 物料名称
            if (!String.IsNullOrEmpty(searchCondition.MaterielName))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.name.Contains(searchCondition.MaterielName));
            }
            #endregion

            #region 外协单表的数据
            var supplierOrder = from s in supplierOrderInfo
                                join m in urgtStsMaster
                                on s.UrgentStatus equals m.AttrCd
                                select new
                                {
                                    UrgentStatus = m.AttrValue,
                                    SupOrderID = s.SupOrderID,
                                    InCompanyID = s.InCompanyID,
                                    Urgent = s.UrgentStatus
                                };
            #endregion

            #region 合并显示结果
            // 合并外协单详细表的相同物料求总数（按照外协单号,产品零件ID,规格型号,交货日期,完成状态,备注group by）
            var supplierDetail = from s in supplierOrderDetailInfo
                                 group s by new
                                 {
                                     s.SupOrderID,
                                     s.ProductPartID,
                                     s.MaterialsSpecReq,
                                     s.DeliveryDate,
                                     s.CompleteStatus,
                                     s.Remarks,
                                     s.PdProcID
                                 } into sus
                                 join m in compStsMaster
                                 on sus.Key.CompleteStatus equals m.AttrCd
                                 select new
                                 {
                                     sus.Key,
                                     ActualQuantity = sus.Sum(supplier => supplier.ActualQuantity + supplier.OtherQuantity),
                                     RequestQuantity = sus.Sum(supplier => supplier.RequestQuantity),
                                     CompleteStatus = m.AttrValue                                    
                                 };
            #endregion

            //生成外协计划台账查询表
            IQueryable<VM_SupplierAccoutingList4Table> query = from sd in supplierDetail
                                                                     join s1 in supplierOrder on sd.Key.SupOrderID equals s1.SupOrderID
                                                                     join c in companyList on s1.InCompanyID equals c.CompId
                                                                     join pp in prodAndPartsList on sd.Key.ProductPartID equals pp.id
                                                                     join pro in process on sd.Key.PdProcID equals pro.ProcessId
                                                                     select new VM_SupplierAccoutingList4Table
                                                                     {
                                                                         CompletStatusCd = sd.Key.CompleteStatus,                   // 完成状态Code
                                                                         CompletStatus = sd.CompleteStatus,                         // 完成状态
                                                                         UrgentStatus = s1.UrgentStatus,                            // 紧急状态                                                                    
                                                                         SupOrderNo = sd.Key.SupOrderID,                            // 外协单号
                                                                         SupCompName = c.CompName,                                  // 外协单位名称
                                                                         MaterialNo = pp.abbrev,                                    // 物料编号
                                                                         MaterialName = pp.name,                                    // 物料名称
                                                                         PlanQuantity = sd.RequestQuantity,                         // 订货数量
                                                                         ArrivalQuantity = sd.ActualQuantity,                       // 实收数量
                                                                         MarginQuantity = sd.RequestQuantity - sd.ActualQuantity,   // 订单差额
                                                                         DeliveryDate = sd.Key.DeliveryDate,                        // 交货日期
                                                                         Remarks = sd.Key.Remarks,                                  // 备注
                                                                         ProcessID = pro.ProcName,                                  // 加工工艺
                                                                         MaterialsSpecReq = sd.Key.MaterialsSpecReq                 // 规格要求
                                                                     };

            paging.total = query.Count();

            IEnumerable<VM_SupplierAccoutingList4Table> queryResult = query.ToPageList<VM_SupplierAccoutingList4Table>("SupOrderNo asc", paging);

            // 背景颜色Flg的设置
            List<VM_SupplierAccoutingList4Table> result = queryResult.ToList<VM_SupplierAccoutingList4Table>();
            for (int i = 0; i < result.Count(); i++)
            {
                // 交货日期
                DateTime deliveryDate = (DateTime)result.ElementAt(i).DeliveryDate;
                // 完成状态
                string completStatus = result.ElementAt(i).CompletStatusCd;
                // 设置背景颜色Flg
                result.ElementAt(i).BGColorFlag = getBGColorFlag(deliveryDate, completStatus);
            }

            return result;
        }



        /// <summary>
        /// 外协计划台帐详细信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协计划台帐详细信息</returns>
        public IEnumerable GetSupplierAccoutingDetailByNo(string supOrderNo)
        {

            #region 外协详细
            IQueryable<MCSupplierOrderDetail> supplierOrderDetail = base.GetAvailableList<MCSupplierOrderDetail>().Where(s => s.SupOrderID == supOrderNo);
            // 合并外协单详细表的相同物料求总数（按照产品零件ID、规格型号及要求、加工工艺和交货日期 group by）
            var supplierOrderDetailObject = from s in supplierOrderDetail
                                            group s by new
                                            {
                                                s.SupOrderID,         //调度单号
                                                s.ProductPartID,      //零件ID  
                                                s.MaterialsSpecReq,   //规格型号及要求
                                                s.PdProcID,           //加工工艺
                                                s.DeliveryDate        //交货日期
                                            } into sdo
                                            select new
                                            {
                                                sdo.Key,
                                                //调度单的需对该件产品的总数，（有可能不同的客户对同一件商品的需要）
                                                TotalQuantity = sdo.Sum(de => de.RequestQuantity)
                                            }; 
            #endregion

            #region 产品零件信息
            // 成品表
            IQueryable<ProdInfo> prodList = base.GetAvailableList<ProdInfo>();

            // 零件表
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();

            // 成品 && 零件信息表
            var prodAndPartsList = (
                                    from prod in prodList
                                    select new
                                    {
                                        id = prod.ProductId,
                                        abbrev = prod.ProdAbbrev,
                                        name = prod.ProdName
                                    }
                                ).Union
                               (
                                   from part in partList
                                   select new
                                   {
                                       id = part.PartId,
                                       abbrev = part.PartAbbrevi,
                                       name = part.PartName
                                   }
                               ); 
            #endregion

            //加工工艺
            IQueryable<Process> process = base.GetAvailableList<Process>(); 
            
            #region 送货详细
            //送货
            IQueryable<MCDeliveryOrder> deliveryOrder = base.GetAvailableList<MCDeliveryOrder>().Where(d => d.OrderNo == supOrderNo);
            IQueryable<MCDeliveryOrderDetail> deliveryOrderDetail = base.GetAvailableList<MCDeliveryOrderDetail>();
            var delivery = from d in deliveryOrder
                           join dd in deliveryOrderDetail on d.DeliveryOrderID equals dd.DeliveryOrderID
                           select new
                           {
                               orderId = d.OrderNo,                 //调度单号
                               deliveryOrderNo = d.DeliveryOrderID, //送货单号
                               deliveryDate = d.DeliveryDate,       //送货日期
                               materialId=dd.MaterielID,            //零件名称
                               deliveryQuantity = dd.ActualQuantity //送货数量
                           }; 
            #endregion

            #region 质检信息
            //质检
            IQueryable<PurChkList> purcheck = base.GetAvailableList<PurChkList>();
            //Master数据管理表
            IQueryable<MasterDefiInfo> master = base.GetList<MasterDefiInfo>();
            master = master.Where(m => m.SectionCd.Equals(Constant.MasterSection.LDK_PUR_CHK_RES));

            //质检与送货单连接，查出质检信息
            var check = from c in purcheck
                        //join d in delivery on c.DlyOdrId equals d.deliveryOrderNo
                        join m in master on c.LdkComRes equals m.AttrCd into chkRes
                        from m in chkRes.DefaultIfEmpty()
                        select new
                        {
                            //orderNo = d.orderId,          //调度单号
                            dlyOdrId=c.DlyOdrId,            //送货单号
                            checkNo = c.ChkListId,          //质检单号
                            partId = c.PartId,              //零件ID
                            checkDate = c.ChkDt,            //质检日期
                            checkResult = c.LdkComRes,      //质检结果Code
                            crValue = m.AttrValue           //质检结果Value
                        }; 
            #endregion

            #region 入库信息 + receiveIn(表名)
            //入库
            //在制品库选出外协加工入库
            IQueryable<WipInRecord> wipInRecord = base.GetAvailableList<WipInRecord>().Where(s => s.PlanCls == "002");
            IQueryable<WipInDetailRecord> wipInDetailRecord = base.GetAvailableList<WipInDetailRecord>();

            var wipIn = from wir in wipInRecord
                        join widr in wipInDetailRecord on wir.TecnPdtInID equals widr.TecnPdtInID
                        select new
                        {
                            wipInNo = wir.TecnPdtInID,//入库单号
                            deliveryOrderNo = wir.DlvyListID,//送货单号
                            checkProNo = widr.IsetRepID,//质检单号
                            partId=widr.PdtID,//零件Id
                            wipInDate = widr.InDate,//入库日期
                            wipInQuantity = widr.Qty,//入库数量

                        };

            //半成品库选出外协单入库信息
            IQueryable<SemInRecord> semInRecord = base.GetAvailableList<SemInRecord>().Where(s => s.PlanCls == "002");
            IQueryable<SemInDetailRecord> semInDetailRecord = base.GetAvailableList<SemInDetailRecord>();

            var semIn = from sir in semInRecord
                        join sidr in semInDetailRecord on sir.TecnPdtInId equals sidr.TecnPdtInId
                        select new
                        {
                            semInNo = sir.TecnPdtInId,//入库单号
                            deliveryOrderNo = sir.DlvyListId,//送货单号
                            checkProNo = sidr.IsetRepId,//质检单号
                            partId =sidr.PdtId,//零件Id
                            semInDate = sidr.InDate,//入库日期
                            semInQuantity = sidr.Qty,//入库数量
                        };

            //在制品库 & 半成品库 入库信息
            var receiveIn = (
                from w in wipIn
                select new
                {
                    inNo = w.wipInNo,//入库单号
                    deliveryNo = w.deliveryOrderNo,//送货单号
                    checkNo = w.checkProNo,//质检单号
                    partId =w.partId,//零件ID
                    inDate = w.wipInDate,//入库日期
                    inQuantity = w.wipInQuantity,//入库数量
                }
                ).Union
                (
                from s in semIn
                select new
                {
                    inNo = s.semInNo,
                    deliveryNo = s.deliveryOrderNo,
                    checkNo = s.checkProNo,
                    partId= s.partId,
                    inDate = s.semInDate,
                    inQuantity = s.semInQuantity,
                }
                ); 
            #endregion

            IQueryable<VM_SupplierAccoutingDetail4Table> query = from s in supplierOrderDetailObject
                                                                    //产品零件信息
                                                                    join pp in prodAndPartsList on s.Key.ProductPartID equals pp.id
                                                                    //关联送货信息
                                                                    join d in delivery
                                                                    on new { SUPORDERNO = s.Key.SupOrderID, PRODPARTID = s.Key.ProductPartID }
                                                                    equals new { SUPORDERNO = d.orderId, PRODPARTID = d.materialId }
                                                                    into dev
                                                                    from d in dev.DefaultIfEmpty()
                                                                    //关联质检信息
                                                                    join c in check
                                                                    on new { DELIVERYNO = d.deliveryOrderNo, PRODPARTID = d.materialId }
                                                                    equals new { DELIVERYNO = c.dlyOdrId, PRODPARTID = c.partId }
                                                                    into chkInfo
                                                                    from c in chkInfo.DefaultIfEmpty()
                                                                    //关联入库信息
                                                                    join r in receiveIn
                                                                    on new { CHECKNO = c.checkNo, PRODPARTID = c.partId }
                                                                    equals new { CHECKNO = r.checkNo, PRODPARTID = r.partId }
                                                                    into recInfo
                                                                    from r in recInfo.DefaultIfEmpty()
                                                                    select new VM_SupplierAccoutingDetail4Table
                                                                    {
                                                                        MaterialNo = pp.abbrev,                     // 物料编号
                                                                        MaterialName = pp.name,                     // 物料名称
                                                                        MaterialsSpecReq = s.Key.MaterialsSpecReq,  // 规格型号及要求
                                                                        ProcessingNo = (process.Where(p => p.ProcessId == s.Key.PdProcID)).FirstOrDefault().ProcName,// 加工工艺
                                                                        OrderQuantity = s.TotalQuantity,            // 订单总数
                                                                        OrderDate = s.Key.DeliveryDate,             // 交货日期
                                                                        DeliveryOrderNo = d.deliveryOrderNo,        // 送货单号
                                                                        DeliveryDate = d.deliveryDate,              // 送货日期
                                                                        DeliveryQuantity = d.deliveryQuantity,      // 送货数量
                                                                        QCOrderNo = c.checkNo,                      // 质检单号
                                                                        QCDate = c.checkDate,                       // 质检日期
                                                                        QCResult = c.crValue,                       // 质检结果
                                                                        StorageOrderNo = r.inNo,                    // 入库单号
                                                                        StorageDate = r.inDate,                     // 入库日期
                                                                        StorageQuantity = r.inQuantity,             // 入库数量                                                             
                                                                    };

            IEnumerable<VM_SupplierAccoutingDetail4Table> detail = query.AsEnumerable<VM_SupplierAccoutingDetail4Table>();

            return detail;
        }

        /// <summary>
        /// 外协单的基本信息的查询方法
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns>外协单的基本信息</returns>
        public VM_SupplierAccoutingDetailInfo GetSupplierOrderByNo(string supOrderNo)
        {
            // 返回值初始化
            VM_SupplierAccoutingDetailInfo supplierInfo = new VM_SupplierAccoutingDetailInfo();

            // 获取实体
            MCSupplierOrder supplierOrder = base.GetEntityById<MCSupplierOrder>(supOrderNo);

            // 获取Master表数据
            IQueryable<MasterDefiInfo> master = base.GetAvailableList<MasterDefiInfo>();

            // 获取外协单位
            IQueryable<CompInfo> companyName = base.GetAvailableList<CompInfo>();

            // 紧急状态
            supplierInfo.UrgentStatus = (master.Where(m => m.AttrCd == supplierOrder.UrgentStatus && m.SectionCd == Constant.MasterSection.URGENT_STATE)).FirstOrDefault().AttrValue;
            // 制单日期
            supplierInfo.MarkDate = supplierOrder.MarkSignDate;
            // 生产部门
            supplierInfo.Department = (master.Where(m => m.AttrCd == supplierOrder.DepartmentID && m.SectionCd == Constant.MasterSection.DEPT)).FirstOrDefault().AttrValue;
            // 外协单位
            supplierInfo.SupCompName = (companyName.Where(c => c.CompId == supplierOrder.InCompanyID)).FirstOrDefault().CompName;

            return supplierInfo;
        }

        /// <summary>
        /// 取得背景颜色的Flag
        /// </summary>
        /// <param name="deliveryDate">送货日期</param>
        /// <param name="completeStatus">完成状态</param>
        /// <returns>背景颜色flg</returns>
        private string getBGColorFlag(DateTime deliveryDate, string completeStatus)
        {
            // 返回结果
            string ret = "";

            // 若完成状态 = 已完成时，不设置背景颜色
            if (completeStatus.Equals("1"))
            {
                return ret;
            }

            // 获取当前日期
            DateTime systime = System.DateTime.Now;

            // 比较交货日期和当前日期
            int comp = DateTimeUtil.compareDate(deliveryDate, systime);
            // 交货日期 < 当前日期时，背景颜色设置为红色
            if (comp < 0)
            {
                ret = "R";
            }

            // 交货日期 没有比当前日期小时
            if (comp >= 0)
            {
                // 当前时间加3天
                DateTime sysAdd3Day = systime.AddDays(3.0);
                // 比较交货日期和当前时间加3天的日期
                comp = DateTimeUtil.compareDate(deliveryDate, sysAdd3Day);
                if (comp <= 0)
                {
                    ret = "Y";
                }
            }
            return ret;
        }
    }
}


