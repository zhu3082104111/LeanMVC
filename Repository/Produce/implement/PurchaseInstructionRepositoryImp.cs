/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseInstructionRepositoryImp.cs
// 文件功能描述：
//          外购指示表的Repository接口类的实现
//      
// 修改履历：2014/1/8 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 外购指示表的Repository接口类的实现
    /// </summary>
    public class PurchaseInstructionRepositoryImp : AbstractRepository<DB, PurchaseInstruction>, IPurchaseInstructionRepository
    {
        /// <summary>
        /// 获取外购指示List的查询方法
        /// </summary>
        /// <param name="searchItem">查询条件</param>
        /// <param name="paging">分页排序信息</param>
        /// <returns>外购指示List信息</returns>
        public IEnumerable<VM_PurchaseInstructionList4Table> GetPurchaseInstructionListWithPaging(VM_PurchaseInstructionList4Search searchItem, Paging paging)
        {
            #region 数据源
            // 外购指示表数据
            IQueryable<PurchaseInstruction> purchaseInstructionList = base.GetAvailableList<PurchaseInstruction>().FilterBySearch(searchItem);

            // 物料分解表数据
            IQueryable<MaterialDecompose> materialDecomposeList = base.GetAvailableList<MaterialDecompose>().FilterBySearch(searchItem);

            // 成品信息表数据
            IQueryable<ProdInfo> prodInfoList = base.GetAvailableList<ProdInfo>().FilterBySearch(searchItem);

            // 零部件表数据
            IQueryable<PartInfo> partInfoList = base.GetAvailableList<PartInfo>().FilterBySearch(searchItem);

            // 外购单明细表数据
            IQueryable<MCOutSourceOrderDetail> outSourceOrderDetailList = base.GetAvailableList<MCOutSourceOrderDetail>();

            // 供货商表
            IQueryable<CompInfo> compInfoList = base.GetAvailableList<CompInfo>().FilterBySearch(searchItem);

            // 供货商供货信息表
            IQueryable<CompMaterialInfo> compMaterialInfoList = base.GetAvailableList<CompMaterialInfo>().FilterBySearch(searchItem);

            // 生产单元Master表
            IQueryable<MasterDefiInfo> deptMaster = base.GetList<MasterDefiInfo>().Where(m => m.SectionCd == Constant.MasterSection.DEPT);
            #endregion

            #region 成品零件信息
            // 成品&&零件信息表
            var prodAndPartsList = (
                                        from prod in prodInfoList
                                        select new
                                        {
                                            id = prod.ProductId,
                                            abbrev = prod.ProdAbbrev,
                                            name = prod.ProdName
                                        }
                                    ).Union
                                    (
                                        from part in partInfoList
                                        select new
                                        {
                                            id = part.PartId,
                                            abbrev = part.PartAbbrevi,
                                            name = part.PartName
                                        }
                                    );
            #endregion

            #region 根据查询条件 过滤数据
            // 客户订单号
            if (!String.IsNullOrEmpty(searchItem.CustOrderNo))
            {
                purchaseInstructionList = purchaseInstructionList.Where(p => (p.ClientOrderID + Constant.SPLICE_MARK + p.ClientOrderDetail).Contains(searchItem.CustOrderNo));
            }
            // 物料编号
            if (!String.IsNullOrEmpty(searchItem.MaterialNo))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.abbrev.Contains(searchItem.MaterialNo));
            }
            // 物料名称
            if (!String.IsNullOrEmpty(searchItem.MaterialName))
            {
                prodAndPartsList = prodAndPartsList.Where(pp => pp.name.Contains(searchItem.MaterialName));
            }
            // 供货商名
            if (!String.IsNullOrEmpty(searchItem.CompName))
            {
                //对供货商供货信息的判断(指定为外购供货商)
                compMaterialInfoList = compMaterialInfoList.Where(cmi => cmi.CompType == Constant.CompanyType.OUT_SOURCING || cmi.CompType == Constant.CompanyType.BOTH);

                // 供货商可提供的物料（成品 或 零件）
                prodAndPartsList = (from pp in prodAndPartsList
                                    from co in compInfoList
                                    join cm in compMaterialInfoList on co.CompId equals cm.CompId
                                    where pp.id.Contains(cm.PdtId)
                                    select pp).Distinct();
            }
            #endregion

            // 抽取有效数据
            IQueryable<VM_PurchaseInstructionList4Table> query = from pi in purchaseInstructionList
                                                                 // 关联物料分解表
                                                                 join md in materialDecomposeList
                                                                 on new { pi.ClientOrderID, pi.ClientOrderDetail, pi.ProductsPartsID }
                                                                 equals new { md.ClientOrderID, md.ClientOrderDetail, md.ProductsPartsID }
                                                                 // 关联产品表
                                                                 join prod in prodInfoList on pi.ProductID equals prod.ProductId
                                                                 // 关联产品零件信息
                                                                 join pp in prodAndPartsList on pi.ProductsPartsID equals pp.id
                                                                 // 关联生产单元信息
                                                                 join ds in deptMaster on pi.DepartmentID equals ds.AttrCd
                                                                 into aa
                                                                 from ds in aa.DefaultIfEmpty()
                                                                 select new VM_PurchaseInstructionList4Table
                                                                 {
                                                                     CustOrderNo = pi.ClientOrderID + Constant.SPLICE_MARK + pi.ClientOrderDetail,     // 客户订单号
                                                                     PrdtType = prod.ProdName,                                                         // 产品型号(名称)
                                                                     ProductPartID = pp.id,                                                            // 产品零件ID
                                                                     MaterialNo = pp.abbrev,                                                           // 物料编号
                                                                     MaterialName = pp.name,                                                           // 物料名称
                                                                     MaterialsSpecReq = md.Specifica,                                                  // 材料规格和要求
                                                                     PlanQuantity = md.PurchNeedQuantity,                                              // 计划数量
                                                                     PlanDateS = md.StartDate,                                                         // 开始日
                                                                     PlanDateE = md.ProvideDate,                                                       // 结束日
                                                                     WaitingQuantity = md.PurchNeedQuantity - pi.ScheduledQtt,                         // 待购数量
                                                                     DeptName = ds.AttrValue,                                                          // 生产部门名称
                                                                     OutOrderList = from osd in outSourceOrderDetailList                               // 外购计划单号的List
                                                                            where (osd.CustomerOrderID == pi.ClientOrderID 
                                                                                    && osd.CustomerOrderDetailID == pi.ClientOrderDetail
                                                                                    && osd.ProductPartID == pi.ProductsPartsID)
                                                                            select new VM_OutSourceOrderNo
                                                                            {
                                                                                OutOrderID = osd.OutOrderID                                            // 外购计划单号
                                                                            }
                                                                 };

            // 统计结果数量            
            paging.total = query.Count();

            // 将结果返回给视图Model，默认按结束日期降序
            IEnumerable<VM_PurchaseInstructionList4Table> result = query.ToPageList<VM_PurchaseInstructionList4Table>("PlanDateE asc", paging);

            return result;
        }

        /// <summary>
        /// 取得外购排产画面的显示信息
        /// </summary>
        /// <param name="insturctions">外购指示List</param>
        /// <returns>外购排产画面的显示信息</returns>
        public IEnumerable<VM_PurchaseScheduling> GetPurchaseSchedulingInfo(string[] insturctions)
        {
            #region 数据源
            // 外购指示表数据
            IQueryable<PurchaseInstruction> purchaseInstructionList = base.GetAvailableList<PurchaseInstruction>();

            // 物料分解表数据
            IQueryable<MaterialDecompose> materialDecomposeList = base.GetAvailableList<MaterialDecompose>();

            // 成品信息表数据
            IQueryable<ProdInfo> prodInfoList = base.GetAvailableList<ProdInfo>();

            // 零部件表数据
            IQueryable<PartInfo> partInfoList = base.GetAvailableList<PartInfo>();

            // 订单明细表
            IQueryable<MarketOrderDetail> marketOrderDetailList = base.GetAvailableList<MarketOrderDetail>();
            #endregion

            #region 成品零件信息
            // 成品&&零件信息表
            var prodAndPartsList = (
                                        from prod in prodInfoList
                                        select new
                                        {
                                            id = prod.ProductId,
                                            abbrev = prod.ProdAbbrev,
                                            name = prod.ProdName
                                        }
                                    ).Union
                                    (
                                        from part in partInfoList
                                        select new
                                        {
                                            id = part.PartId,
                                            abbrev = part.PartAbbrevi,
                                            name = part.PartName
                                        }
                                    );
            #endregion

            // 抽取排产对象（将客户订单号、客户订单明细号、产品零件ID拼接成字符串，匹配参数的排产对象）
            purchaseInstructionList = (from pi in purchaseInstructionList
                                       where (insturctions.Contains(pi.ClientOrderID + pi.ClientOrderDetail + pi.ProductsPartsID))
                                       select pi);

            // 抽取有效数据
            IQueryable<VM_PurchaseScheduling> query = from pi in purchaseInstructionList
                                                      // 关联物料分解表
                                                      join md in materialDecomposeList
                                                      on new { pi.ClientOrderID, pi.ClientOrderDetail, pi.ProductsPartsID }
                                                      equals new { md.ClientOrderID, md.ClientOrderDetail, md.ProductsPartsID }
                                                      // 关联产品表
                                                      join prod in prodInfoList on pi.ProductID equals prod.ProductId
                                                      // 关联产品零件信息
                                                      join pp in prodAndPartsList on pi.ProductsPartsID equals pp.id
                                                      // 关联订单明细表
                                                      join mod in marketOrderDetailList
                                                      on new { pi.ClientOrderID, pi.ClientOrderDetail }
                                                      equals new { mod.ClientOrderID, mod.ClientOrderDetail } into modl
                                                      from mod in modl.DefaultIfEmpty()
                                                      select new VM_PurchaseScheduling
                                                      {
                                                          CustOrderNo = pi.ClientOrderID + Constant.SPLICE_MARK + pi.ClientOrderDetail,    // 客户订单号
                                                          ProductPartID = pp.id,                                                           // 产品零件ID
                                                          ProductID = pi.ProductID,                                                        // 产品ID
                                                          PrdtType = prod.ProdName,                                                        // 产品型号(名称)
                                                          DeptID = pi.DepartmentID,                                                        // 生产部门ID
                                                          MaterialNo = pp.abbrev,                                                          // 物料编号
                                                          MaterialName = pp.name,                                                          // 物料名称
                                                          MaterialsSpecReq = md.Specifica,                                                 // 材料规格和要求
                                                          PlanQuantity = md.PurchNeedQuantity,                                             // 计划数量
                                                          PlanDateS = md.StartDate,                                                        // 开始日
                                                          PlanDateE = md.ProvideDate,                                                      // 结束日
                                                          WaitingQuantity = md.PurchNeedQuantity - pi.ScheduledQtt,                        // 待购数量
                                                          UrgencyStatus = mod.Urgency,                                                     // 紧急状态Code
                                                          RequestQuantity = md.PurchNeedQuantity - pi.ScheduledQtt,                        //采购数量（默认等于待购数量）
                                                          UnitPrice = 0,                                                                   //单价（默认为0）
                                                          EstiPrice = 0                                                                    //估价（默认为0）
                                                      };

            IEnumerable<VM_PurchaseScheduling> queryResult = query.AsEnumerable<VM_PurchaseScheduling>();

            List<VM_PurchaseScheduling> result = queryResult.ToList<VM_PurchaseScheduling>();
            for (int i = 0; i < result.Count(); i++)
            {
                // 结束日期
                DateTime EndDate = (DateTime)result.ElementAt(i).PlanDateE;
                // 交货日期
                result.ElementAt(i).DeliveryDate = EndDate.AddDays(-3.0);
            }
            return result;
        }
    }
}
