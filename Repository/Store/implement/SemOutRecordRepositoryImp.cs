/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemOutRecordRepositoryImp.cs
// 文件功能描述：
//            半成品库出库履历及出库相关业务Repository实现
//      
// 修改履历：2013/11/13 杨灿 新建
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Collections;

namespace Repository
{
    public class SemOutRecordRepositoryImp : AbstractRepository<DB, SemOutRecord>, ISemOutRecordRepository
    {
        //半成品仓库编码
        public String semWhID = "0202";

        #region ISemOutRecordRepository 成员（半成品库出库履历一览初始化页面（yc添加））
        public IEnumerable GetSemOutRecordBySearchByPage(VM_SemOutRecordStoreForSearch semOutRecordStoreForSearch, Paging paging)
        {
            IQueryable<SemOutRecord> semOutRecordList = null;
            IQueryable<SemOutDetailRecord> semOutDetailRecordList = null;
            IQueryable<UnitInfo> unitInfo = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;
            IQueryable<Process> processList = null;
            IQueryable<ConcInfo> concInfoList = null;
            IQueryable<Department> departmentList = null;
            IQueryable<CompInfo> compInfoList = null;

            //外协外购（加工单位）取得供货商信息表
            compInfoList = base.GetList<CompInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //自生产（加工单位）取得部门信息表
            departmentList = base.GetList<Department>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得让步信息表
            concInfoList = base.GetList<ConcInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得工序信息表
            processList = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的出库履历表数据
            semOutRecordList = base.GetList<SemOutRecord>().Where(a => a.DelFlag == "0" && a.WhId == semWhID && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            semOutDetailRecordList = base.GetList<SemOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取的单位信息表
            unitInfo = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");

           // bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(semOutRecordStoreForSearch.PickListID))
            {
                semOutRecordList = semOutRecordList.Where(a => a.PickListId == semOutRecordStoreForSearch.PickListID);
                //isPaging = false;
            }
          
            if (!String.IsNullOrEmpty(semOutRecordStoreForSearch.SaeetID))
            {
                semOutRecordList = semOutRecordList.Where(a => a.TecnPdtOutId.Contains(semOutRecordStoreForSearch.SaeetID));
            }
            if (!String.IsNullOrEmpty(semOutRecordStoreForSearch.WhID))
            {
                semOutRecordList = semOutRecordList.Where(a => a.WhId.Contains(semOutRecordStoreForSearch.WhID));
            }
            if (!String.IsNullOrEmpty(semOutRecordStoreForSearch.BthID))
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(a => a.BatchID.Contains(semOutRecordStoreForSearch.BthID));
            }
            //出库日期
            if (semOutRecordStoreForSearch.StartOutDate != null)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(a => a.OutDate >= semOutRecordStoreForSearch.StartOutDate);
            }
            if (semOutRecordStoreForSearch.EndOutDate != null)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(a => a.OutDate <= semOutRecordStoreForSearch.EndOutDate);
            }
            if (!String.IsNullOrEmpty(semOutRecordStoreForSearch.GiCls))
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(a => a.GiCls.Contains(semOutRecordStoreForSearch.GiCls));
            }
            

            //出库与出库履历中的数据
            IQueryable<VM_SemOutRecordStoreForTableShow> semOutRecordListQuery = from a in semOutRecordList
                                                                                 join b in semOutDetailRecordList on a.TecnPdtOutId equals b.TecnProductOutID
                                                                                 join c in partInfoList on b.ProductID equals c.PartId
                                                                                 join d in unitInfoList on c.UnitId equals d.UnitId
                                                                                 join e in processList on b.TecnProcess equals e.ProcessId
                                                                                 select new VM_SemOutRecordStoreForTableShow
                                                                                 {
                                                                                     //领料单号
                                                                                     PickListID = a.PickListId,
                                                                                     //领料单类型
                                                                                     PickListTypeID = a.PickListTypeID,
                                                                                     //领料单详细号
                                                                                     PickListDetNo = b.PickListDetNo,
                                                                                     //加工产品出库单
                                                                                     SaeetID = a.TecnPdtOutId,
                                                                                     //请领单位（来自外协外购）
                                                                                     CallinUnitID = (compInfoList.Where(g => g.CompId == a.CallinUnitId)).FirstOrDefault().CompName,
                                                                                     //请领单位（来自生产）
                                                                                     CallinUnitIDs = (departmentList.Where(g => g.DeptId == a.CallinUnitId)).FirstOrDefault().DeptName,
                                                                                     //物料ID
                                                                                     MaterielID = b.ProductID,
                                                                                     //物料名称
                                                                                     MaterielName = b.ProductName,
                                                                                     //批次号
                                                                                     BthID = b.BatchID,
                                                                                     //让步区分
                                                                                     GiCls = b.GiCls,
                                                                                     //规格型号
                                                                                     PdtSpec = b.ProductSpec,
                                                                                     //加工工艺
                                                                                     TecnProcess = e.ProcName,
                                                                                     //数量
                                                                                     Qty = b.Quantity,
                                                                                     //单位
                                                                                     Unit = d.UnitName,
                                                                                     //单价
                                                                                     PrchsUp=b.PrchsUp,
                                                                                     //出库价格
                                                                                     SellPrc = b.PrchsUp,
                                                                                     //金额
                                                                                     NotaxAmt = b.NotaxAmt,
                                                                                     //出库日期
                                                                                     OutDate = b.OutDate,
                                                                                     //备注
                                                                                     Rmrs = b.Remarks

                                                                                 };

            //if (isPaging)
            //{
            //    paging.total = semOutRecordListQuerys.Count();
            //    IEnumerable<VM_SemOutRecordStoreForTableShow> resultForFirst = semOutRecordListQuerys.AsQueryable().ToPageList<VM_SemOutRecordStoreForTableShow>("OutDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}

            paging.total = semOutRecordListQuery.Count();
            IEnumerable<VM_SemOutRecordStoreForTableShow> result = semOutRecordListQuery.AsQueryable().ToPageList<VM_SemOutRecordStoreForTableShow>("OutDate desc", paging);

            //加工单位取得
            var semOutRecordListQuerys = result.ToList();
            for (int j = 0; j < semOutRecordListQuerys.Count; j++)
            {
                if (semOutRecordListQuerys[j].CallinUnitID == "" || semOutRecordListQuerys[j].CallinUnitID == null)
                {
                    semOutRecordListQuerys[j].CallinUnitID = semOutRecordListQuerys[j].CallinUnitIDs;
                }
            }
            return semOutRecordListQuerys;
        }

        #endregion


        #region 待出库一览(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)待出库一览结果集
        /// </summary>
        /// <param name="semOutStoreForSearch">VM_SemOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetSemOutStoreBySearchByPage(VM_SemOutStoreForSearch semOutStoreForSearch, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(pmdl => pmdl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmdl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && pmdl.ReceQty == 0); //获取生产领料单详细表(PD_PROD_MATER_DETAIL)结果集
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(mcscil => mcscil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mcscil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mcscil.ReceiveQuantity == 0); //获取外协领料单信息表(MC_SUPPLIER_CNSM_INFO)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集

            if (string.IsNullOrEmpty(semOutStoreForSearch.MaterReqNO) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.MaterReqNo.Equals(semOutStoreForSearch.MaterReqNO));
            }
            if (string.IsNullOrEmpty(semOutStoreForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(semOutStoreForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(semOutStoreForSearch.MaterielID) == false)
            {
                produceMaterDetailList = produceMaterDetailList.Where(pmdl => pmdl.MaterialID.Equals(semOutStoreForSearch.MaterielID));
                mcSupplierCnsmInfoList = mcSupplierCnsmInfoList.Where(mcscil => mcscil.MaterialID.Equals(semOutStoreForSearch.MaterielID));
            }
            if (semOutStoreForSearch.StartRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate >= semOutStoreForSearch.StartRequestDate);
            }
            if (semOutStoreForSearch.EndRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate <= semOutStoreForSearch.EndRequestDate);
            }

            //
            IQueryable<VM_SemOutStoreForTableShow> produceMaterDetailQuery = from pmdIQ in produceMaterDetailList
                                                                             join piIQ in partInfoList on pmdIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on pmdIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join mdilIQ in masterDefiInfoList on pmrlIQ.DeptID equals mdilIQ.AttrCd into JoinPMRMDI
                                                                             from mdilIQ in JoinPMRMDI.DefaultIfEmpty()
                                                                             select new VM_SemOutStoreForTableShow
                                                                             {
                                                                                 MaterReqNO = pmdIQ.MaterReqNo,
                                                                                 MaterReqDetailNO = pmdIQ.MaterReqDetailNo,
                                                                                 DeptID = pmrlIQ.DeptID,
                                                                                 DeptName = mdilIQ.AttrValue,
                                                                                 AppoQuantity = pmdIQ.AppoQty,
                                                                                 MaterielID = pmdIQ.MaterialID,
                                                                                 MaterielName = piIQ.PartName,
                                                                                 RequestDate = pmrlIQ.RequestDate
                                                                             };
            IQueryable<VM_SemOutStoreForTableShow> mcSupplierCnsmInfoQuery = from mcscilIQ in mcSupplierCnsmInfoList
                                                                             join piIQ in partInfoList on mcscilIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on mcscilIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join ciIQ in compInfoList on pmrlIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                                             from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                                             select new VM_SemOutStoreForTableShow
                                                                             {
                                                                                 MaterReqNO = mcscilIQ.MaterReqNo,
                                                                                 MaterReqDetailNO = mcscilIQ.No,
                                                                                 DeptID = pmrlIQ.DeptID,
                                                                                 DeptName = ciIQ.CompName,
                                                                                 AppoQuantity = mcscilIQ.ApplyQuantity,
                                                                                 MaterielID = mcscilIQ.MaterialID,
                                                                                 MaterielName = piIQ.PartName,
                                                                                 RequestDate = pmrlIQ.RequestDate
                                                                             };
            IQueryable<VM_SemOutStoreForTableShow> resultIQ = produceMaterDetailQuery.Union(mcSupplierCnsmInfoQuery);
            paging.total = resultIQ.Count();
            IEnumerable<VM_SemOutStoreForTableShow> ieSemOutStoreForTableShow = resultIQ.ToPageList<VM_SemOutStoreForTableShow>("MaterReqNO asc", paging);

            return ieSemOutStoreForTableShow; //返回结果集
        } //end GetSemOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(半成品库)(fyy修改)

        /// <summary>
        /// 获取(半成品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipInPrintForSearch">VM_SemOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_SemOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetSemOutPrintBySearchByPage(VM_SemOutPrintForSearch semOutPrintForSearch, Paging paging)
        {
            IQueryable<SemOutRecord> semOutRecordList = base.GetList().Where(sorl => sorl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && sorl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(半成品库)出库履历表(MC_WH_SEM_OUT_RECORD)结果集
            IQueryable<SemOutDetailRecord> semOutDetailRecordList = base.GetList<SemOutDetailRecord>().Where(sodrl => sodrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && sodrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(半成品库)出库履历详细表(MC_WH_SEM_OUT_DETAIL_RECORD)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> mdiPrintList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.PRINT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)打印区分结果集
            IQueryable<MasterDefiInfo> mdiDeptList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集
            IQueryable<Process> processList = base.GetList<Process>().Where(p => p.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && p.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取工序信息表(PD_PROCESS)结果集
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(ui => ui.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && ui.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取单位信息表(BI_UNIT_INFO)结果集

            if (string.IsNullOrEmpty(semOutPrintForSearch.PickListID) == false) 
            {
                semOutRecordList = semOutRecordList.Where(sorl => sorl.PickListId.Equals(semOutPrintForSearch.PickListID));
            }
            if (string.IsNullOrEmpty(semOutPrintForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(semOutPrintForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(semOutPrintForSearch.TecnProductOutID) == false)
            {
                //semOutRecordList = semOutRecordList.Where(sorl => sorl.TecnPdtOutId.Equals(semOutPrintForSearch.TecnProductOutID));
                semOutDetailRecordList = semOutDetailRecordList.Where(sodrl => sodrl.TecnProductOutID.Equals(semOutPrintForSearch.TecnProductOutID));
            }
            if (string.IsNullOrEmpty(semOutPrintForSearch.ProductID) == false)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(sodrl => sodrl.ProductID.Equals(semOutPrintForSearch.ProductID));
            }
            if (semOutPrintForSearch.StartOutDate != null)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(sodrl => sodrl.OutDate >= semOutPrintForSearch.StartOutDate);
            }
            if (semOutPrintForSearch.EndOutDate != null)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(sodrl => sodrl.OutDate <= semOutPrintForSearch.EndOutDate);
            }
            if (string.IsNullOrEmpty(semOutPrintForSearch.PrintFlg) == false)
            {
                semOutDetailRecordList = semOutDetailRecordList.Where(sodrl => sodrl.PrintFlg.Equals(semOutPrintForSearch.PrintFlg));
            }
            if (string.IsNullOrEmpty(semOutPrintForSearch.WhID) == false)
            {
                semOutRecordList = semOutRecordList.Where(sorl => sorl.WhId.Equals(semOutPrintForSearch.WhID));
            }
            //
            IQueryable<VM_SemOutPrintForTableShow> resultIQ = from sodrIQ in semOutDetailRecordList
                                                              join sorIQ in semOutRecordList on sodrIQ.TecnProductOutID equals sorIQ.TecnPdtOutId
                                                              join pmrIQ in produceMaterRequestList on sorIQ.PickListId equals pmrIQ.MaterReqNo into JoinSORPMR
                                                              from pmrIQ in JoinSORPMR.DefaultIfEmpty()
                                                              join mdiPrintIQ in mdiPrintList on sodrIQ.PrintFlg equals mdiPrintIQ.AttrCd into JoinSODRMDI
                                                              from mdiPrintIQ in JoinSODRMDI.DefaultIfEmpty()
                                                              join mdiDeptIQ in mdiDeptList on pmrIQ.DeptID equals mdiDeptIQ.AttrCd into JoinPMRMDI
                                                              from mdiDeptIQ in JoinPMRMDI.DefaultIfEmpty()
                                                              join ciIQ in compInfoList on pmrIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                              from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                              join pIQ in processList on sodrIQ.TecnProcess equals pIQ.ProcessId into JoinSODRP
                                                              from pIQ in JoinSODRP.DefaultIfEmpty()
                                                              join piIQ in partInfoList on sodrIQ.ProductID equals piIQ.PartId into JoinSODRPI
                                                              from piIQ in JoinSODRPI.DefaultIfEmpty()
                                                              join uiIQ in unitInfoList on piIQ.UnitId equals uiIQ.UnitId into JoinPIUI
                                                              from uiIQ in JoinPIUI.DefaultIfEmpty()
                                                              select new VM_SemOutPrintForTableShow
                                                              {
                                                                  PrintFlg = sodrIQ.PrintFlg,
                                                                  PrintFlgName = mdiPrintIQ.AttrValue,
                                                                  PickListID = sorIQ.PickListId,
                                                                  TecnProductOutID = sodrIQ.TecnProductOutID,
                                                                  BatchID = sodrIQ.BatchID,
                                                                  DeptID = pmrIQ.DeptID,
                                                                  DeptName = mdiDeptIQ.AttrValue,
                                                                  CompName = ciIQ.CompName,
                                                                  PickListDetailNO = sodrIQ.PickListDetNo,
                                                                  ProductID = sodrIQ.ProductID,
                                                                  ProductName = sodrIQ.ProductName,
                                                                  TecnProcessID = sodrIQ.TecnProcess,
                                                                  TecnProcessName = pIQ.ProcName,
                                                                  Quantity = sodrIQ.Quantity,
                                                                  UnitID = piIQ.UnitId,
                                                                  UnitName = uiIQ.UnitName,
                                                                  PrchsUp = sodrIQ.PrchsUp,
                                                                  NotaxAmt = sodrIQ.NotaxAmt,
                                                                  OutDate = sodrIQ.OutDate,
                                                                  Remarks = sodrIQ.Remarks
                                                              };

            IEnumerable<VM_SemOutPrintForTableShow> resultIE = resultIQ.ToList().AsEnumerable();
            foreach (var semOutPrintForTableShow in resultIE) //遍历，设置属性 DeptName 值
            {
                var compName = string.IsNullOrEmpty(semOutPrintForTableShow.CompName) ? null : semOutPrintForTableShow.CompName; //设置属性 compName
                semOutPrintForTableShow.DeptName = string.IsNullOrEmpty(semOutPrintForTableShow.DeptName) ? compName : semOutPrintForTableShow.DeptName; //设置属性 DeptName
            }

            paging.total = resultIE.AsQueryable().Count();
            IEnumerable<VM_SemOutPrintForTableShow> ieSemOutPrintForTableShow = resultIE.AsQueryable().ToPageList<VM_SemOutPrintForTableShow>("PickListID asc", paging);

            return ieSemOutPrintForTableShow; //返回结果集
        } //end GetSemOutPrintBySearchByPage
       
        #endregion

        #region 材料领用出库单(半成品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_SemOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_SemOutPrintIndexForInfoShow GetSemOutPrintForInfoShow(string pickListID)
        {
            SemOutRecord semOutRecord = new SemOutRecord();
            semOutRecord.PickListId = pickListID;
            semOutRecord = base.Find(semOutRecord);

            CompInfo compInfo = new CompInfo();
            compInfo.CompId = semOutRecord.CallinUnitId;
            compInfo = base.Find<CompInfo>(compInfo);

            MasterDefiInfo masterDefiInfo = null;
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT) && mdi.AttrCd.Equals(semOutRecord.CallinUnitId));
            if (masterDefiInfoList.Count() > 0) 
            {
                masterDefiInfo = masterDefiInfoList.First();
            }

            ProduceMaterRequest produceMaterRequest=new ProduceMaterRequest();
            produceMaterRequest.MaterReqNo = pickListID;
            produceMaterRequest = base.Find<ProduceMaterRequest>(produceMaterRequest);

            UserInfo userInfo = new UserInfo();
            userInfo.UId = produceMaterRequest.MaterHandlerID;
            userInfo = base.Find<UserInfo>(userInfo);
            
            VM_SemOutPrintIndexForInfoShow semOutPrintIndexForInfoShow = new VM_SemOutPrintIndexForInfoShow();
            semOutPrintIndexForInfoShow.TecnProductOutID = semOutRecord.TecnPdtOutId;
            if (masterDefiInfo != null)
            {
                semOutPrintIndexForInfoShow.CallinUnitName = masterDefiInfo.AttrValue;
            }
            if (compInfo != null)
            {
                semOutPrintIndexForInfoShow.CallinUnitName = compInfo.CompName;
            }
            semOutPrintIndexForInfoShow.MaterHandlerName = userInfo.UName;

            return semOutPrintIndexForInfoShow;
            
        } //end GetSemOutPrintByInfoShow

        /// <summary>
        /// 根据 SemOutRecord 类，获取相关信息
        /// </summary>
        /// <param name="semOutRecord">SemOutRecord 实体类</param>
        /// <returns>VM_SemOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public VM_SemOutPrintIndexForTableShow GetSemOutPrintForTableShow(string pickListID, SemOutDetailRecord semOutDetailRecord)
        {
            MasterDefiInfo masterDefiInfo = null;
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdil => mdil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdil.SectionCd.Equals(Constant.MasterSection.DEPT) && mdil.AttrCd.Equals(semOutDetailRecord.PrintFlg));
            if (masterDefiInfoList.Count() > 0)
            {
                masterDefiInfo = masterDefiInfoList.First();
            }

            UnitInfo unitInfo=null;
            PartInfo partInfo = null;
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pil => pil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && pil.PartId.Equals(semOutDetailRecord.ProductID));
            if (partInfoList.Count() > 0)
            {
                partInfo = partInfoList.First();
                IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(uil => uil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && uil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && uil.UnitId.Equals(partInfo.UnitId));
                if(unitInfoList.Count()>0)
                {
                    unitInfo=unitInfoList.First();
                }
            }

            ProduceMaterDetail produceMaterDetail = null;
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(pmdl => pmdl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmdl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && pmdl.MaterReqNo.Equals(pickListID) && pmdl.MaterReqDetailNo.Equals(semOutDetailRecord.PickListDetNo) && pmdl.BthID.Equals(semOutDetailRecord.BatchID) && pmdl.MaterialID.Equals(semOutDetailRecord.ProductID));
            if (produceMaterDetailList.Count() > 0)
            {
                produceMaterDetail = produceMaterDetailList.First();
            }

            VM_SemOutPrintIndexForTableShow semOutPrintIndexForTableShow = new VM_SemOutPrintIndexForTableShow();
            semOutPrintIndexForTableShow.TecnProductOutID = semOutDetailRecord.TecnProductOutID;
            semOutPrintIndexForTableShow.PickListDetailNO = semOutDetailRecord.PickListDetNo;
            semOutPrintIndexForTableShow.BatchID = semOutDetailRecord.BatchID;
            semOutPrintIndexForTableShow.PrintFlg = semOutDetailRecord.PrintFlg;
            semOutPrintIndexForTableShow.PrintFlgName = masterDefiInfo == null ? null : masterDefiInfo.AttrValue;
            semOutPrintIndexForTableShow.ProductID = semOutDetailRecord.ProductID;
            semOutPrintIndexForTableShow.ProductName = semOutDetailRecord.ProductName;
            semOutPrintIndexForTableShow.ProductSpec = semOutDetailRecord.ProductSpec;
            semOutPrintIndexForTableShow.UnitID = partInfo == null ? null : partInfo.UnitId;
            semOutPrintIndexForTableShow.UnitName = unitInfo == null ? null : unitInfo.UnitName;
            semOutPrintIndexForTableShow.AppoQuantity = produceMaterDetail == null ? 0 : produceMaterDetail.AppoQty;
            semOutPrintIndexForTableShow.ReceQuantity = produceMaterDetail == null ? 0 : produceMaterDetail.ReceQty;
            semOutPrintIndexForTableShow.Remarks = semOutDetailRecord.Remarks;

            return semOutPrintIndexForTableShow;
        } //end GetSemOutPrintForTableShow

        #endregion

        #region ISemOutRecordRepository 成员（半成品出库登录画面数据表示）

        /// <summary>
        /// 半成品出库登录画面数据表示 陈健
        /// </summary>
        /// <param name="materReqNO">领料单号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        public IEnumerable GetSemOutStoreForLoginBySearchByPage(string materReqNO, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = null;
            IQueryable<ProduceMaterDetail> produceMaterDetailList = null;
            //IQueryable<SemOutRecord> semOutRecordList = null;
            //IQueryable<SemOutDetailRecord> semOutDetailRecordList = null;
            IQueryable<UnitInfo> unitInfo = null;
            IQueryable<Process> process = null;
            IQueryable<MasterDefiInfo> masterInfo = null;
            IQueryable<PartInfo> partInfo = null;
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfo = null;

            //取得满足条件的生产领料单信息表数据
            produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(p => p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的生产领料单详细表数据
            produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(p => p.AppoQty > p.ReceQty && p.DelFlag == "0" && p.EffeFlag == "0");

            ////取得满足条件的出库履历表数据
            //semOutRecordList = base.GetList<SemOutRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            ////取得满足条件的出库履历详细表数据
            //semOutDetailRecordList = base.GetList<SemOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取的单位信息表
            unitInfo = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的工序信息表
            process = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的master表
            masterInfo = base.GetList<MasterDefiInfo>().Where(u => u.SectionCd == "00009" && u.DelFlag == "0" && u.EffeFlag == "0");
            //取的零件信息表
            partInfo = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的外协领料单信息表
            mcSupplierCnsmInfo = base.GetList<MCSupplierCnsmInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");

            ////出库与出库履历中的数据
            //IQueryable<VM_SemOutLoginStoreForTableShow> semOutRecordListQuery = from a in semOutRecordList
            //                                                                    join b in semOutDetailRecordList on a.TecnPdtOutId equals b.TecnProductOutID
            //                                                                    join n in partInfo on b.ProductID equals n.PartId
            //                                                                    join c in unitInfo on n.UnitId equals c.UnitId
            //                                                                    join m in masterInfo on a.CallinUnitId equals m.AttrCd
            //                                                                    where (materReqNO.Contains(a.PickListId))
            //                                                                    select new VM_SemOutLoginStoreForTableShow
            //                                                                    {
            //                                                                        //领料单号
            //                                                                        PickListID = a.PickListId,
            //                                                                        //加工产品出库单号
            //                                                                        SaeetID = a.TecnPdtOutId,
            //                                                                        //请领单位
            //                                                                        CallinUnitID = m.AttrValue,
            //                                                                        //物料ID
            //                                                                        MaterielID = b.ProductID,
            //                                                                        //物料名称
            //                                                                        MaterielName = b.ProductName,
            //                                                                        //加工工艺
            //                                                                        TecnProcess = b.TecnProcess,
            //                                                                        //数量
            //                                                                        Qty = b.Quantity,
            //                                                                        //请领数量
            //                                                                        CallinQty = b.Quantity,
            //                                                                        //单位
            //                                                                        Unit = c.UnitName,
            //                                                                        //出库单价
            //                                                                        SellPrc = b.PrchsUp,
            //                                                                        //金额
            //                                                                        NotaxAmt = b.NotaxAmt,
            //                                                                        //出库日期
            //                                                                        OutDate = b.OutDate,
            //                                                                        //备注
            //                                                                        Rmrs = b.Remarks,
            //                                                                        //标识
            //                                                                        SemLoginFlg = "ForLogin",

            //                                                                        //规格型号
            //                                                                        PdtSpec = b.ProductSpec,

            //                                                                        //让步区分
            //                                                                        GiCls = b.GiCls,

            //                                                                        //领料单详细号
            //                                                                        MaterReqDetailNo = b.PickListDetNo,
            //                                                                        //外协、外购、自生产区分
            //                                                                        OsSupProFlg = "",
            //                                                                        //批次号
            //                                                                        BthID = b.BatchID
            //                                                                    };

            //var ERER = semOutRecordListQuery.ToList();

            //来自外协
            IQueryable<VM_SemOutLoginStoreForTableShow> mcSupplierCnsmInfoListQuery = from m in mcSupplierCnsmInfo
                                                                                      join p in produceMaterRequestList on m.MaterReqNo equals p.MaterReqNo
                                                                                      join a in masterInfo on p.DeptID equals a.AttrCd
                                                                                      join n in partInfo on m.MaterialID equals n.PartId into pname
                                                                                      from n in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                      join d in process on m.ProcID equals d.ProcessId
                                                                                      join c in unitInfo on n.UnitId equals c.UnitId

                                                                                      where (materReqNO.Contains(m.MaterReqNo))
                                                                                      select new VM_SemOutLoginStoreForTableShow
                                                                                      {
                                                                                          //领料单号
                                                                                          PickListID = m.MaterReqNo,
                                                                                          //加工产品出库单号
                                                                                          SaeetID = m.MaterReqNo + semWhID,
                                                                                          //请领单位
                                                                                          CallinUnitID = a.AttrValue,
                                                                                          //物料ID
                                                                                          MaterielID = m.MaterialID,
                                                                                          //物料名称
                                                                                          MaterielName = n.PartName,
                                                                                          //加工工艺
                                                                                          TecnProcess = d.ProcName,
                                                                                          //数量
                                                                                          Qty = 0,
                                                                                          //请领数量
                                                                                          CallinQty = m.ApplyQuantity,
                                                                                          //单位
                                                                                          Unit = c.UnitName,
                                                                                          //出库单价
                                                                                          SellPrc = 0,
                                                                                          //金额
                                                                                          NotaxAmt = 0,
                                                                                          //出库日期
                                                                                          OutDate = DateTime.Today,
                                                                                          //备注
                                                                                          Rmrs = m.Remarks,
                                                                                          //标识
                                                                                          //SemLoginFlg = "",

                                                                                          //规格型号
                                                                                          PdtSpec = m.MaterialsSpecReq,

                                                                                          //让步区分
                                                                                          GiCls = m.SpecFlg,

                                                                                          //领料单详细号
                                                                                          MaterReqDetailNo = m.No,
                                                                                          //外协、外购、自生产区分
                                                                                          OsSupProFlg = "002",
                                                                                          //批次号
                                                                                          BthID = m.BatchID
                                                                                      };
            var mcs = mcSupplierCnsmInfoListQuery.ToList();

            //来自生产
            IQueryable<VM_SemOutLoginStoreForTableShow> produceMaterRequestListQuery = from p in produceMaterRequestList
                                                                                       join q in produceMaterDetailList on p.MaterReqNo equals q.MaterReqNo
                                                                                       join d in process on q.ProcessID equals d.ProcessId
                                                                                       join m in masterInfo on p.DeptID equals m.AttrCd
                                                                                       join n in partInfo on q.MaterialID equals n.PartId into pname
                                                                                       from n in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                       join c in unitInfo on n.UnitId equals c.UnitId
                                                                                       where (materReqNO.Contains(p.MaterReqNo))
                                                                                       select new VM_SemOutLoginStoreForTableShow
                                                                                       {
                                                                                           //领料单号
                                                                                           PickListID = p.MaterReqNo,
                                                                                           //加工产品出库单号
                                                                                           SaeetID = p.MaterReqNo + semWhID,
                                                                                           //请领单位
                                                                                           CallinUnitID = m.AttrValue,
                                                                                           //物料ID
                                                                                           MaterielID = q.MaterialID,
                                                                                           //物料名称
                                                                                           MaterielName = n.PartName,
                                                                                           //加工工艺
                                                                                           TecnProcess = d.ProcName,
                                                                                           //数量
                                                                                           Qty = 0,
                                                                                           //请领数量
                                                                                           CallinQty = q.AppoQty,
                                                                                           //单位
                                                                                           Unit = c.UnitName,
                                                                                           //出库单价
                                                                                           SellPrc = 0,
                                                                                           //金额
                                                                                           NotaxAmt = 0,
                                                                                           //出库日期
                                                                                           OutDate = DateTime.Today,
                                                                                           //备注
                                                                                           Rmrs = p.Remark,
                                                                                           //标识
                                                                                           //SemLoginFlg = "",

                                                                                           //规格型号
                                                                                           PdtSpec = q.PdtSpec,

                                                                                           //让步区分
                                                                                           GiCls = q.SpecFlag,

                                                                                           //领料单详细号
                                                                                           MaterReqDetailNo = q.MaterReqDetailNo,
                                                                                           //外协、外购、自生产区分
                                                                                           OsSupProFlg = "000",
                                                                                           //批次号
                                                                                           BthID = q.BthID
                                                                                       };

            var mj = produceMaterRequestListQuery.ToList();
            //var query = semOutRecordListQuery.Union(mcSupplierCnsmInfoListQuery);
            var queryxx = mcSupplierCnsmInfoListQuery.Union(produceMaterRequestListQuery);
            paging.total = queryxx.Count();
            IEnumerable<VM_SemOutLoginStoreForTableShow> result = queryxx.ToPageList<VM_SemOutLoginStoreForTableShow>("OutDate desc", paging);
            return result;
        }

        #endregion

        #region 半成品出库批次已出库（已作废）
        /// <summary>
        /// 半成品库出库批次选择画面数据（已出库）
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="pickListID"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable SelectSemOutRecordForBthSelect(decimal qty, string pickListID, Paging paging)
        {

            //取得满足条件的出库履历表数据
            IQueryable<SemOutRecord> wipOutRecordList = base.GetList<SemOutRecord>().Where(a => a.PickListId.Equals(pickListID) && a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            IQueryable<SemOutDetailRecord> wipOutDetailRecordList = base.GetList<SemOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_SemOutBthForTableShow> query = from b in wipOutRecordList
                                                         join w in wipOutDetailRecordList on b.TecnPdtOutId equals w.TecnProductOutID
                                                         select new VM_SemOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = qty,

                                                             //批次号
                                                             BthID = w.BatchID,

                                                             //让步区分
                                                             GiCls = w.GiCls,

                                                             //规格型号
                                                             PdtSpec = w.ProductSpec,

                                                             //单价
                                                             SellPrc = w.PrchsUp,

                                                             //使用数量
                                                             UserQty = w.Quantity
                                                         };
            paging.total = query.Count();
            IEnumerable<VM_SemOutBthForTableShow> result = query.ToPageList<VM_SemOutBthForTableShow>("BthID asc", paging);
            return result;
        } 
        #endregion

        /// <summary>
        /// 生产查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>生产领料数据集合</returns>
        public ProduceMaterDetail SemOutRecordInfo(string pickListID, string materReqDetailNo)
        {
            return base.First<ProduceMaterDetail>(a => a.MaterReqNo == pickListID && a.MaterReqDetailNo == materReqDetailNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 半成品库出库批次选择画面数据（生产未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产未指定批次选择画面数据集合</returns>
        public IEnumerable SelectSemOutRecordProNForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging)
        {

            //取得满足条件的半成品库入库履历表数据
            IQueryable<SemInRecord> semInRecordList = base.GetList<SemInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的半成品库入库履历详细表数据
            IQueryable<SemInDetailRecord> semInDetailRecordList = base.GetList<SemInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_SemOutBthForTableShow> query = from w in bthStockListList
                                                         join p in produceMaterDetailList on w.PdtID equals p.MaterialID
                                                         join i in semInRecordList on w.PrhaOdrID equals i.DlvyListId
                                                         join d in semInDetailRecordList on i.TecnPdtInId equals d.TecnPdtInId
                                                         where d.PdtId == pdtID && w.BillType == "01" && w.WhID == semWhID
                                                          && p.MaterReqNo == pickListID && p.MaterReqDetailNo == materReqDetailNo
                                                          && w.PdtID == pdtID
                                                         select new VM_SemOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = p.AppoQty,

                                                             //批次号
                                                             BthID = w.BthID,

                                                             //让步区分
                                                             GiCls = w.GiCls,

                                                             //规格型号
                                                             PdtSpec = w.PdtSpec,

                                                             //单价
                                                             SellPrc = d.PrchsUp,

                                                             //估价
                                                             ValuatUp = d.ValuatUp,

                                                             //单位标识
                                                             SemLoginPriceFlg = "1",

                                                             //可用数量
                                                             UseableQty = w.Qty,

                                                             //入库日期
                                                             InDate = w.InDate
                                                         };
            //单价取得方式
            var queryList = query.ToList();
            for (int i = 0; i < queryList.Count; i++)
            {
                var PrchsUp = queryList[i].SellPrc;
                var ValuatUp = queryList[i].ValuatUp;
                if (PrchsUp == 0 && ValuatUp != 0)
                {
                    //用估价
                    queryList[i].SellPrc = ValuatUp;
                    queryList[i].SemLoginPriceFlg = "0";
                }
                else
                {

                }

            }

            paging.total = queryList.Count();
            IEnumerable<VM_SemOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_SemOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 半成品库出库批次选择画面数据（生产指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产指定批次选择画面数据集合</returns>
        public IEnumerable SelectSemOutRecordProForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging)
        {

            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_SemOutBthForTableShow> query = from p in produceMaterDetailList
                                                         where p.MaterReqNo == pickListID && p.MaterReqDetailNo == materReqDetailNo
                                                         select new VM_SemOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = qty,

                                                             //批次号
                                                             BthID = p.BthID,

                                                             //让步区分
                                                             GiCls = p.SpecFlag,

                                                             //规格型号
                                                             PdtSpec = p.PdtSpec,

                                                             //单价
                                                             SellPrc = p.UnitPrice,

                                                             //使用数量
                                                             UserQty = p.AppoQty
                                                         };
            paging.total = query.Count();
            IEnumerable<VM_SemOutBthForTableShow> result = query.ToPageList<VM_SemOutBthForTableShow>("BthID asc", paging);
            return result;
        }

        /// <summary>
        /// 外协查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>外协领料数据集合</returns>
        public MCSupplierCnsmInfo SemOutRecordSInfo(string pickListID, string materReqDetailNo)
        {
            return base.First<MCSupplierCnsmInfo>(a => a.MaterReqNo == pickListID && a.No == materReqDetailNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 半成品库出库批次选择画面数据（外协未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协未指定批次选择画面数据集合</returns>
        public IEnumerable SelectSemOutRecordSupNForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging)
        {

            //取得满足条件的半成品库入库履历表数据
            IQueryable<SemInRecord> semInRecordList = base.GetList<SemInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的半成品库入库履历详细表数据
            IQueryable<SemInDetailRecord> semInDetailRecordList = base.GetList<SemInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_SemOutBthForTableShow> query = from w in bthStockListList
                                                         join m in mcSupplierCnsmInfoList on w.PdtID equals m.MaterialID
                                                         join i in semInRecordList on w.PrhaOdrID equals i.DlvyListId
                                                         join d in semInDetailRecordList on i.TecnPdtInId equals d.TecnPdtInId
                                                         where d.PdtId == pdtID && w.BillType == "03" && w.WhID == semWhID
                                                          && m.MaterReqNo == pickListID && m.MaterialsSpecReq == materReqDetailNo
                                                          && w.PdtID == pdtID

                                                         select new VM_SemOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = m.ApplyQuantity,

                                                             //批次号
                                                             BthID = w.BthID,

                                                             //让步区分
                                                             GiCls = w.GiCls,

                                                             //规格型号
                                                             PdtSpec = w.PdtSpec,

                                                             //单价
                                                             SellPrc = d.PrchsUp,

                                                             //估价
                                                             ValuatUp = d.ValuatUp,

                                                             //单位标识
                                                             SemLoginPriceFlg = "1",

                                                             //可用数量
                                                             UseableQty = w.Qty,

                                                             //入库日期
                                                             InDate = w.InDate
                                                         };

            //单价取得方式
            var queryList = query.ToList();
            for (int i = 0; i < queryList.Count; i++)
            {
                var PrchsUp = queryList[i].SellPrc;
                var ValuatUp = queryList[i].ValuatUp;
                if (PrchsUp == 0 && ValuatUp != 0)
                {
                    //用估价
                    queryList[i].SellPrc = ValuatUp;
                    queryList[i].SemLoginPriceFlg = "0";
                }
                else
                {

                }

            }
            paging.total = queryList.Count();
            IEnumerable<VM_SemOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_SemOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 半成品库出库批次选择画面数据（外协指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协指定批次选择画面数据集合</returns>
        public IEnumerable SelectSemOutRecordSupForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging)
        {

            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_SemOutBthForTableShow> query = from p in mcSupplierCnsmInfoList
                                                         where p.MaterReqNo == pickListID && p.No == materReqDetailNo
                                                         select new VM_SemOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = qty,

                                                             //批次号
                                                             BthID = p.BatchID,

                                                             //让步区分
                                                             GiCls = p.SpecFlg,

                                                             //规格型号
                                                             PdtSpec = p.MaterialsSpecReq,

                                                             //单价
                                                             SellPrc = p.UnitPrice,

                                                             //使用数量
                                                             UserQty = p.ReceiveQuantity
                                                         };
            paging.total = query.Count();
            IEnumerable<VM_SemOutBthForTableShow> result = query.ToPageList<VM_SemOutBthForTableShow>("BthID asc", paging);
            return result;
        }

        /// <summary>
        /// 半成品库出库登录插入出库履历表查询是否已存在 陈健
        /// </summary>
        /// <param name="semOutRecord">半成品出库登录数据集合</param>
        /// <returns>数据集合</returns>
        public SemOutRecord SelectSemOutRecord(SemOutRecord semOutRecord)
        {
            return base.First(a => a.PickListId == semOutRecord.PickListId && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        /// <summary>
        /// Desc取得生产领料单详细表List 陈健
        /// </summary>
        /// <param name="produceMaterDetail">生产领料单数据</param>
        /// <returns>生产领料单数据集合</returns>
        public IEnumerable<ProduceMaterDetail> GetProduceMaterDetailForListDesc(ProduceMaterDetail produceMaterDetail)
        {
            return base.GetList<ProduceMaterDetail>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0" && a.MaterReqNo == produceMaterDetail.MaterReqNo && a.MaterReqDetailNo == produceMaterDetail.MaterReqDetailNo).OrderByDescending(n => n.CreDt);
        }

        /// <summary>
        /// 半成品库出库登录修改外协领料单信息表 陈健
        /// </summary>
        /// <param name="mcSupplierCnsmInfo">更新外协领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateSupplierCnsmInfoForOut(MCSupplierCnsmInfo mcSupplierCnsmInfo)
        {
            return base.ExecuteStoreCommand("update MC_SUPPLIER_CNSM_INFO set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and NO={5}", mcSupplierCnsmInfo.ReceiveQuantity, mcSupplierCnsmInfo.UpdUsrID, mcSupplierCnsmInfo.UpdDt,"1", mcSupplierCnsmInfo.MaterReqNo, mcSupplierCnsmInfo.MaterialsSpecReq);
        }

        /// <summary>
        /// 半成品库出库登录修改生产领料单详细表 陈健
        /// </summary>
        /// <param name="produceMaterDetail">更新生产领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateProduceMaterDetailForOut(ProduceMaterDetail produceMaterDetail)
        {
            return base.ExecuteStoreCommand("update PD_PROD_MATER_DETAIL set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and MATER_REQ_DET_NO={5}", produceMaterDetail.ReceQty, produceMaterDetail.UpdUsrID, produceMaterDetail.UpdDt,"1", produceMaterDetail.MaterReqNo, produceMaterDetail.MaterReqDetailNo);
        }
        
    } // SemOutRecordRepositoryImp
}
