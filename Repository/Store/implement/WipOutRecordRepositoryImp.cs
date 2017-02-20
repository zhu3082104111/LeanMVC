/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipOutRecordRepositoryImp.cs
// 文件功能描述：
//            在制品出库履历及出库相关业务Repository实现
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
using System.Collections;
using Extensions;

namespace Repository
{
    public class WipOutRecordRepositoryImp : AbstractRepository<DB, WipOutRecord>, IWipOutRecordRepository
    {
        //在制品库仓库编码
        public String wipWhID = "0102";
        //当前用户
        public string LoginUserID = "201228";
        //让步区分正常品
        public string norMalGiCls = "999";


        #region 待出库一览(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)待出库一览结果集
        /// </summary>
        /// <param name="wipOutStoreForSearch">VM_WipOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetWipOutStoreBySearchByPage(VM_WipOutStoreForSearch wipOutStoreForSearch, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(pmdl => pmdl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmdl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && pmdl.ReceQty == 0); //获取生产领料单详细表(PD_PROD_MATER_DETAIL)结果集
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(mcscil => mcscil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mcscil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mcscil.ReceiveQuantity == 0); //获取外协领料单信息表(MC_SUPPLIER_CNSM_INFO)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集

            if (string.IsNullOrEmpty(wipOutStoreForSearch.MaterReqNO) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.MaterReqNo.Equals(wipOutStoreForSearch.MaterReqNO));
            }
            if (string.IsNullOrEmpty(wipOutStoreForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(wipOutStoreForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(wipOutStoreForSearch.MaterielID) == false)
            {
                produceMaterDetailList = produceMaterDetailList.Where(pmdl => pmdl.MaterialID.Equals(wipOutStoreForSearch.MaterielID));
                mcSupplierCnsmInfoList = mcSupplierCnsmInfoList.Where(mcscil => mcscil.MaterialID.Equals(wipOutStoreForSearch.MaterielID));
            }
            if (wipOutStoreForSearch.StartRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate >= wipOutStoreForSearch.StartRequestDate);
            }
            if (wipOutStoreForSearch.EndRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate <= wipOutStoreForSearch.EndRequestDate);
            }

            //
            IQueryable<VM_WipOutStoreForTableShow> produceMaterDetailQuery = from pmdIQ in produceMaterDetailList
                                                                             join piIQ in partInfoList on pmdIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on pmdIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join mdilIQ in masterDefiInfoList on pmrlIQ.DeptID equals mdilIQ.AttrCd into JoinPMRMDI
                                                                             from mdilIQ in JoinPMRMDI.DefaultIfEmpty()
                                                                             select new VM_WipOutStoreForTableShow
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
            IQueryable<VM_WipOutStoreForTableShow> mcSupplierCnsmInfoQuery = from mcscilIQ in mcSupplierCnsmInfoList
                                                                             join piIQ in partInfoList on mcscilIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on mcscilIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join ciIQ in compInfoList on pmrlIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                                             from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                                             select new VM_WipOutStoreForTableShow
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
            IQueryable<VM_WipOutStoreForTableShow> resultIQ = produceMaterDetailQuery.Union(mcSupplierCnsmInfoQuery);
            paging.total = resultIQ.Count();
            IEnumerable<VM_WipOutStoreForTableShow> ieWipOutStoreForTableShow = resultIQ.ToPageList<VM_WipOutStoreForTableShow>("MaterReqNO asc", paging);

            return ieWipOutStoreForTableShow; //返回结果集
        } //end GetWipOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(在制品库)(fyy修改)

        /// <summary>
        /// 获取(在制品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipOutPrintForSearch">VM_WipOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_WipOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetWipOutPrintBySearchByPage(VM_WipOutPrintForSearch wipOutPrintForSearch, Paging paging)
        {
            IQueryable<WipOutRecord> wipOutRecordList = base.GetList().Where(worl => worl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && worl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(在制品库)出库履历表(MC_WH_WIP_OUT_RECORD)结果集
            IQueryable<WipOutDetailRecord> wipOutDetailRecordList = base.GetList<WipOutDetailRecord>().Where(wodrl => wodrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && wodrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(在制品库)出库履历详细表(MC_WH_WIP_OUT_DETAIL_RECORD)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> mdiPrintList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.PRINT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)打印区分结果集
            IQueryable<MasterDefiInfo> mdiDeptList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集
            IQueryable<Process> processList = base.GetList<Process>().Where(p => p.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && p.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取工序信息表(PD_PROCESS)结果集
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(ui => ui.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && ui.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取单位信息表(BI_UNIT_INFO)结果集

            if (string.IsNullOrEmpty(wipOutPrintForSearch.PickListID) == false)
            {
                wipOutRecordList = wipOutRecordList.Where(worl => worl.PickListID.Equals(wipOutPrintForSearch.PickListID));
            }
            if (string.IsNullOrEmpty(wipOutPrintForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(wipOutPrintForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(wipOutPrintForSearch.TecnProductOutID) == false)
            {
                //wipOutRecordList = wipOutRecordList.Where(worl => worl.TecnPdtOutID.Equals(wipOutPrintForSearch.TecnProductOutID));
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.TecnPdtOutID.Equals(wipOutPrintForSearch.TecnProductOutID));
            }
            if (string.IsNullOrEmpty(wipOutPrintForSearch.ProductID) == false)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.PdtID.Equals(wipOutPrintForSearch.ProductID));
            }
            if (wipOutPrintForSearch.StartOutDate != null)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.OutDate >= wipOutPrintForSearch.StartOutDate);
            }
            if (wipOutPrintForSearch.EndOutDate != null)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.OutDate <= wipOutPrintForSearch.EndOutDate);
            }
            if (string.IsNullOrEmpty(wipOutPrintForSearch.PrintFlg) == false)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.PrintFlg.Equals(wipOutPrintForSearch.PrintFlg));
            }
            if (string.IsNullOrEmpty(wipOutPrintForSearch.WhID) == false)
            {
                wipOutRecordList = wipOutRecordList.Where(worl => worl.WhID.Equals(wipOutPrintForSearch.WhID));
            }
            //
            IQueryable<VM_WipOutPrintForTableShow> resultIQ = from wodrIQ in wipOutDetailRecordList
                                                              join worIQ in wipOutRecordList on wodrIQ.TecnPdtOutID equals worIQ.TecnPdtOutID
                                                              join pmrIQ in produceMaterRequestList on worIQ.PickListID equals pmrIQ.MaterReqNo into JoinWORPMR
                                                              from pmrIQ in JoinWORPMR.DefaultIfEmpty()
                                                              join mdiPrintIQ in mdiPrintList on wodrIQ.PrintFlg equals mdiPrintIQ.AttrCd into JoinWODRMDI
                                                              from mdiPrintIQ in JoinWODRMDI.DefaultIfEmpty()
                                                              join mdiDeptIQ in mdiDeptList on pmrIQ.DeptID equals mdiDeptIQ.AttrCd into JoinPMRMDI
                                                              from mdiDeptIQ in JoinPMRMDI.DefaultIfEmpty()
                                                              join ciIQ in compInfoList on pmrIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                              from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                              join pIQ in processList on wodrIQ.TecnProcess equals pIQ.ProcessId into JoinWODRP
                                                              from pIQ in JoinWODRP.DefaultIfEmpty()
                                                              join piIQ in partInfoList on wodrIQ.PdtID equals piIQ.PartId into JoinWODRPI //零件信息表用于关联单位表
                                                              from piIQ in JoinWODRPI.DefaultIfEmpty()
                                                              join uiIQ in unitInfoList on piIQ.UnitId equals uiIQ.UnitId into JoinPIUI
                                                              from uiIQ in JoinPIUI.DefaultIfEmpty()
                                                              select new VM_WipOutPrintForTableShow
                                                              {
                                                                  PrintFlg = wodrIQ.PrintFlg,
                                                                  PrintFlgName = mdiPrintIQ.AttrValue,
                                                                  PickListID = worIQ.PickListID,
                                                                  TecnProductOutID = wodrIQ.TecnPdtOutID,
                                                                  PickListDetailNO = wodrIQ.PickListDetNo,
                                                                  BatchID = wodrIQ.BthID,
                                                                  DeptID = pmrIQ.DeptID,
                                                                  DeptName = mdiDeptIQ.AttrValue,
                                                                  CompName = ciIQ.CompName,
                                                                  ProductID = wodrIQ.PdtID,
                                                                  ProductName = wodrIQ.PdtName,
                                                                  TecnProcessID = wodrIQ.TecnProcess,
                                                                  TecnProcessName = pIQ.ProcName,
                                                                  Quantity = wodrIQ.Qty,
                                                                  UnitID = piIQ.UnitId,
                                                                  UnitName = uiIQ.UnitName,
                                                                  PrchsUp = wodrIQ.PrchsUp,
                                                                  NotaxAmt = wodrIQ.NotaxAmt,
                                                                  OutDate = wodrIQ.OutDate,
                                                                  Remarks = wodrIQ.Rmrs
                                                              };

            IEnumerable<VM_WipOutPrintForTableShow> resultIE = resultIQ.ToList().AsEnumerable();
            foreach (var wipOutPrintForTableShow in resultIE) //遍历，设置属性 DeptName 值
            {
                var compName = string.IsNullOrEmpty(wipOutPrintForTableShow.CompName) ? null : wipOutPrintForTableShow.CompName; //设置属性 compName
                wipOutPrintForTableShow.DeptName = string.IsNullOrEmpty(wipOutPrintForTableShow.DeptName) ? compName : wipOutPrintForTableShow.DeptName; //设置属性 DeptName
            }

            paging.total = resultIE.AsQueryable().Count();
            IEnumerable<VM_WipOutPrintForTableShow> ieWipOutPrintForTableShow = resultIE.AsQueryable().ToPageList<VM_WipOutPrintForTableShow>("PickListID asc", paging);

            return ieWipOutPrintForTableShow; //返回结果集
        } //end GetWipOutPrintBySearchByPage

        #endregion

        #region 材料领用出库单(在制品库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_WipOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_WipOutPrintIndexForInfoShow GetWipOutPrintForInfoShow(string pickListID)
        {
            WipOutRecord wipOutRecord = new WipOutRecord();
            wipOutRecord.PickListID = pickListID;
            wipOutRecord = base.Find(wipOutRecord);

            CompInfo compInfo = new CompInfo();
            compInfo.CompId = wipOutRecord.CallinUnitID;
            compInfo = base.Find<CompInfo>(compInfo);

            MasterDefiInfo masterDefiInfo = null;
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT) && mdi.AttrCd.Equals(wipOutRecord.CallinUnitID));
            if (masterDefiInfoList.Count() > 0)
            {
                masterDefiInfo = masterDefiInfoList.First();
            }

            ProduceMaterRequest produceMaterRequest = new ProduceMaterRequest();
            produceMaterRequest.MaterReqNo = pickListID;
            produceMaterRequest = base.Find<ProduceMaterRequest>(produceMaterRequest);

            UserInfo userInfo = new UserInfo();
            userInfo.UId = produceMaterRequest.MaterHandlerID;
            userInfo = base.Find<UserInfo>(userInfo);

            VM_WipOutPrintIndexForInfoShow wipOutPrintIndexForInfoShow = new VM_WipOutPrintIndexForInfoShow();
            wipOutPrintIndexForInfoShow.TecnProductOutID = wipOutRecord.TecnPdtOutID;
            if (masterDefiInfo != null)
            {
                wipOutPrintIndexForInfoShow.CallinUnitName = masterDefiInfo.AttrValue;
            }
            if (compInfo != null)
            {
                wipOutPrintIndexForInfoShow.CallinUnitName = compInfo.CompName;
            }
            wipOutPrintIndexForInfoShow.MaterHandlerName = userInfo.UName;

            return wipOutPrintIndexForInfoShow;

        } //end GetWipOutPrintByInfoShow

        /// <summary>
        /// 根据 WipOutRecord 实体类，获取相关信息
        /// </summary>
        /// <param name="wipOutRecord">WipOutRecord 实体类</param>
        /// <returns>VM_WipOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public VM_WipOutPrintIndexForTableShow GetWipOutPrintForTableShow(WipOutDetailRecord wipOutDetailRecord)
        {
            IQueryable<WipOutDetailRecord> wipOutDetailRecordList = base.GetList<WipOutDetailRecord>().Where(wodrl => wodrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && wodrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(在制品库)出库履历详细表(MC_WH_Wip_OUT_DETAIL_RECORD)结果集
            IQueryable<MasterDefiInfo> mdiPrintList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.PRINT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)打印区分结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集
            IQueryable<Process> processList = base.GetList<Process>().Where(p => p.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && p.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取工序信息表(PD_PROCESS)结果集
            IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(ui => ui.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && ui.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取单位信息表(BI_UNIT_INFO)结果集

            if (wipOutDetailRecord != null)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(wodrl => wodrl.TecnPdtOutID.Equals(wipOutDetailRecord.TecnPdtOutID) && wodrl.PickListDetNo.Equals(wipOutDetailRecord.PickListDetNo) && wodrl.BthID.Equals(wipOutDetailRecord.BthID));
            }

            IQueryable<VM_WipOutPrintIndexForTableShow> resultIQ = from wodrIQ in wipOutDetailRecordList
                                                                   join piIQ in partInfoList on wodrIQ.PdtID equals piIQ.PartId into JoinWODRPI //零件信息表用于关联单位表
                                                                   from piIQ in JoinWODRPI.DefaultIfEmpty()
                                                                   join uiIQ in unitInfoList on piIQ.UnitId equals uiIQ.UnitId into JoinPIUI
                                                                   from uiIQ in JoinPIUI.DefaultIfEmpty()
                                                                   join pIQ in processList on wodrIQ.TecnProcess equals pIQ.ProcessId into JoinWODRP
                                                                   from pIQ in JoinWODRP.DefaultIfEmpty()
                                                                   join mdipIQ in mdiPrintList on wodrIQ.PrintFlg equals mdipIQ.AttrCd into JoinWODRMDI
                                                                   from mdipIQ in JoinWODRMDI.DefaultIfEmpty()
                                                                   select new VM_WipOutPrintIndexForTableShow
                                                                   {
                                                                       TecnProductOutID = wodrIQ.TecnPdtOutID,
                                                                       PickListDetailNO = wodrIQ.PickListDetNo,
                                                                       BatchID = wodrIQ.BthID,
                                                                       ProductID = wodrIQ.PdtID,
                                                                       ProductName = wodrIQ.PdtName,
                                                                       TecnProcessID = wodrIQ.TecnProcess,
                                                                       TecnProcessName = pIQ.ProcName,
                                                                       UnitID = piIQ.UnitId,
                                                                       UnitName = uiIQ.UnitName,
                                                                       Quantity = wodrIQ.Qty,
                                                                       PrintFlg = wodrIQ.PrintFlg,
                                                                       PrintFlgName = mdipIQ.AttrValue,
                                                                       Remarks = wodrIQ.Rmrs
                                                                   };
            return resultIQ.First();
        } //end GetWipOutPrintForTableShow

        #endregion

        #region IWipOutRecordRepository 成员（在制品出库登录画面数据表示）

        /// <summary>
        /// 在制品出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>在制品库出库登录画面数据集合</returns>
        public IEnumerable GetWipOutStoreForLoginBySearchByPage(string pickListID, string materielID,string materReqDetailNo, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = null;
            IQueryable<ProduceMaterDetail> produceMaterDetailList = null;
            //IQueryable<WipOutRecord> wipOutRecordList = null;
            //IQueryable<WipOutDetailRecord> wipOutDetailRecordList = null;
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
            //wipOutRecordList = base.GetList<WipOutRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            ////取得满足条件的出库履历详细表数据
            //wipOutDetailRecordList = base.GetList<WipOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取的单位信息表
            unitInfo = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的工序信息表
            process = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的master表
            masterInfo = base.GetList<MasterDefiInfo>().Where(u => u.SectionCd=="00009" && u.DelFlag == "0" && u.EffeFlag == "0");
            //取的零件信息表
            partInfo = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取的外协领料单信息表
            mcSupplierCnsmInfo = base.GetList<MCSupplierCnsmInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");

            ////出库与出库履历中的数据
            //IQueryable<VM_WipOutLoginStoreForTableShow> wipOutRecordListQuery = from a in wipOutRecordList
            //                                                                  join b in wipOutDetailRecordList on a.TecnPdtOutID equals b.TecnPdtOutID
            //                                                                  join n in partInfo on b.PdtID equals n.PartId
            //                                                                  join c in unitInfo on n.UnitId  equals c.UnitId
            //                                                                  join m in masterInfo on a.CallinUnitID equals m.AttrCd
            //                                                                    where (pickListID.Contains(a.PickListID))
            //                                                                   select new VM_WipOutLoginStoreForTableShow
            //                                                                  {
            //                                                                      //领料单号
            //                                                                      PickListID = a.PickListID,
            //                                                                      //加工产品出库单号
            //                                                                      SaeetID = a.TecnPdtOutID,
            //                                                                      //请领单位
            //                                                                      CallinUnitID = m.AttrValue,
            //                                                                      //物料ID
            //                                                                      MaterielID = b.PdtID,
            //                                                                      //物料名称
            //                                                                      MaterielName=b.PdtName,
            //                                                                      //加工工艺
            //                                                                      TecnProcess =b.TecnProcess,
            //                                                                      //数量
            //                                                                      Qty = b.Qty,
            //                                                                      //请领数量
            //                                                                      CallinQty=b.Qty,
            //                                                                      //单位
            //                                                                      Unit = c.UnitName,
            //                                                                      //出库单价
            //                                                                      SellPrc = b.PrchsUp,
            //                                                                      //金额
            //                                                                      NotaxAmt = b.NotaxAmt,
            //                                                                      //出库日期
            //                                                                      OutDate = b.OutDate,
            //                                                                      //备注
            //                                                                      Rmrs = b.Rmrs,
            //                                                                      //标识
            //                                                                      WipLoginFlg = "ForLogin",

            //                                                                      //规格型号
            //                                                                      PdtSpec = b.PdtSpec,

            //                                                                      //让步区分
            //                                                                      GiCls = b.GiCls,

            //                                                                      //领料单详细号
            //                                                                      MaterReqDetailNo=b.PickListDetNo,
            //                                                                      //外协、外购、自生产区分
            //                                                                      OsSupProFlg="",
            //                                                                      //批次号
            //                                                                      BthID = b.BthID
            //                                                                  };

            //var ERER= wipOutRecordListQuery.ToList();

            //来自外协
            IQueryable<VM_WipOutLoginStoreForTableShow> mcSupplierCnsmInfoListQuery = from m in mcSupplierCnsmInfo
                                                                                      join p in produceMaterRequestList on m.MaterReqNo equals p.MaterReqNo
                                                                                      join a in masterInfo on p.DeptID equals a.AttrCd
                                                                                      join n in partInfo on m.MaterialID equals n.PartId into pname
                                                                                      from n in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                      join d in process on m.ProcID equals d.ProcessId
                                                                                      join c in unitInfo on n.UnitId  equals c.UnitId

                                                                                      where (pickListID.Contains(m.MaterReqNo) && materielID.Contains(m.MaterialID) && materReqDetailNo.Contains(m.No))
                                                                                       select new VM_WipOutLoginStoreForTableShow
                                                                                       {
                                                                                           //领料单号
                                                                                           PickListID = m.MaterReqNo,
                                                                                           //加工产品出库单号
                                                                                           SaeetID = m.MaterReqNo + wipWhID,
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
                                                                                           ////标识
                                                                                           //WipLoginFlg = "",

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
            IQueryable<VM_WipOutLoginStoreForTableShow> produceMaterRequestListQuery = from p in produceMaterRequestList
                                                                                       join q in produceMaterDetailList on p.MaterReqNo equals q.MaterReqNo
                                                                                       join d in process on q.ProcessID equals d.ProcessId
                                                                                       join m in masterInfo on p.DeptID equals m.AttrCd
                                                                                       join n in partInfo on q.MaterialID equals n.PartId into pname
                                                                                       from n in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                       join c in unitInfo on n.UnitId  equals c.UnitId
                                                                                       where (pickListID.Contains(p.MaterReqNo) && materielID.Contains(q.MaterialID) && materReqDetailNo.Contains(q.MaterReqDetailNo))
                                                                                select new VM_WipOutLoginStoreForTableShow
                                                                                {
                                                                                    //领料单号
                                                                                    PickListID = p.MaterReqNo,
                                                                                    //加工产品出库单号
                                                                                    SaeetID = p.MaterReqNo + wipWhID,
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
                                                                                    OutDate =DateTime.Today,
                                                                                    //备注
                                                                                    Rmrs = p.Remark,
                                                                                    ////标识
                                                                                    //WipLoginFlg = "",

                                                                                    //规格型号
                                                                                    PdtSpec = q.PdtSpec,

                                                                                    //让步区分
                                                                                    GiCls = q.SpecFlag,

                                                                                    //领料单详细号
                                                                                    MaterReqDetailNo = q.MaterReqDetailNo,
                                                                                    //外协、外购、自生产区分
                                                                                    OsSupProFlg="000",
                                                                                    //批次号
                                                                                    BthID = q.BthID
                                                                                };

            var mj = produceMaterRequestListQuery.ToList();
            //var query = wipOutRecordListQuery.Union(mcSupplierCnsmInfoListQuery);
            var queryxx = mcSupplierCnsmInfoListQuery.Union(produceMaterRequestListQuery);
            paging.total = queryxx.Count();
            IEnumerable<VM_WipOutLoginStoreForTableShow> result = queryxx.ToPageList<VM_WipOutLoginStoreForTableShow>("OutDate desc", paging);
            return result;
        }

        #endregion

        #region IWipOutRecordRepository 成员（在制品出库履历一览初始化页面）

        public IEnumerable GetWipOutRecordBySearchByPage(VM_WipOutRecordStoreForSearch wipOutRecordStoreForSearch, Paging paging)
        {
             IQueryable<WipOutRecord> wipOutRecordList = null;
             IQueryable<WipOutDetailRecord> wipOutDetailRecordList = null;
             IQueryable<PartInfo> partInfoList = null;
             IQueryable<UnitInfo> unitInfoList = null;
             IQueryable<Process> processList = null;
             //部门表
             IQueryable<Department> departmentList = null;
             //供货商信息表
             IQueryable<CompInfo> compInfoList = null;

             //外协外购（加工单位）取得供货商信息表
             compInfoList = base.GetList<CompInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
             //自生产（加工单位）取得部门信息表
             departmentList = base.GetList<Department>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
             //取的工序信息表
             processList = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
             //取得零件信息表
             partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
             //取得单位信息表
             unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的出库履历表数据
            wipOutRecordList = base.GetList<WipOutRecord>().Where(a => a.DelFlag == "0" && a.WhID == wipWhID && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            wipOutDetailRecordList = base.GetList<WipOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            // bool isPaging = true;//按主键查询时(单条记录)，不分页
            //paging.total = 1;
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.PickListID))
            {
                wipOutRecordList = wipOutRecordList.Where(a => a.PickListID == wipOutRecordStoreForSearch.PickListID);
               // isPaging = false;
            }
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.SaeetID))
            {
                wipOutRecordList = wipOutRecordList.Where(a => a.TecnPdtOutID.Contains(wipOutRecordStoreForSearch.SaeetID));
            }
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.WhID))
            {
                wipOutRecordList = wipOutRecordList.Where(a => a.WhID.Contains(wipOutRecordStoreForSearch.WhID));
            }
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.BthID))
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.BthID.Contains(wipOutRecordStoreForSearch.BthID));
            }
            //出库日期
            if (wipOutRecordStoreForSearch.StartOutDate != null)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.OutDate >= wipOutRecordStoreForSearch.StartOutDate);
            }
            if (wipOutRecordStoreForSearch.EndOutDate != null)
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.OutDate <= wipOutRecordStoreForSearch.EndOutDate);
            }
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.GiCls))
            {
                if (wipOutRecordStoreForSearch.GiCls == norMalGiCls)
                {
                    wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (wipOutRecordStoreForSearch.GiCls != norMalGiCls)
                {
                    wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(wipOutRecordStoreForSearch.PickListID))
            {
                wipOutDetailRecordList = wipOutDetailRecordList.Where(a => a.PdtSpec.Contains(wipOutRecordStoreForSearch.PdtSpec));
            }
            

            //出库与出库履历中的数据
            IQueryable<VM_WipOutRecordStoreForTableShow> wipOutRecordListQuery = from a in wipOutRecordList
                                                                               join b in wipOutDetailRecordList on a.TecnPdtOutID equals b.TecnPdtOutID
                                                                               join c in partInfoList on b.PdtID equals c.PartId
                                                                               join d in unitInfoList on c.UnitId equals d.UnitId
                                                                               join e in processList on b.TecnProcess equals e.ProcessId
                                                                               select new VM_WipOutRecordStoreForTableShow
                                                                               {
                                                                                    //领料单号
                                                                                    PickListID=a.PickListID,
                                                                                   //领料单类型
                                                                                    PickListTypeID = a.PickListTypeID,
                                                                                   //领料单详细号
                                                                                    PickListDetNo = b.PickListDetNo,
                                                                                    //加工产品出库单
                                                                                    SaeetID=a.TecnPdtOutID,
                                                                                    //请领单位
                                                                                    CallinUnitID=a.CallinUnitID,
                                                                                    //物料ID
                                                                                    MaterielID=b.PdtID,
                                                                                    //物料名称
                                                                                    MaterielName=b.PdtName,
                                                                                    //批次号
                                                                                    BthID=b.BthID,
                                                                                    //让步区分
                                                                                    GiCls=b.GiCls, 
                                                                                    //规格型号
                                                                                    PdtSpec=b.PdtSpec,
                                                                                    //加工工艺
                                                                                    TecnProcess=e.ProcName,
                                                                                    //数量
                                                                                    Qty =b.Qty,
                                                                                    //单位
                                                                                    Unit = d.UnitName,
                                                                                    //单价
                                                                                    PrchsUp = b.PrchsUp,
                                                                                    //出库单价
                                                                                    SellPrc =b.PrchsUp,
                                                                                    //金额
                                                                                    NotaxAmt=b.NotaxAmt,
                                                                                    //出库日期
                                                                                    OutDate =b.OutDate,
                                                                                    //备注
                                                                                    Rmrs=b.Rmrs

                                                                               };
            //if (isPaging)
            //{
            //    paging.total = wipOutRecordListQuery.Count();
            //    IEnumerable<VM_WipOutRecordStoreForTableShow> resultForFirst = wipOutRecordListQuery.ToPageList<VM_WipOutRecordStoreForTableShow>("OutDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //} 

            paging.total = wipOutRecordListQuery.Count();
            IEnumerable<VM_WipOutRecordStoreForTableShow> result = wipOutRecordListQuery.ToPageList<VM_WipOutRecordStoreForTableShow>("OutDate desc", paging);
            return result;
        }
        

        #endregion

        /// <summary>
        /// 在制品库出库登录插入出库履历表查询是否已存在 陈健
        /// </summary>
        /// <param name="wipOutRecord">在制品出库登录数据集合</param>
        /// <returns>数据集合</returns>
        public WipOutRecord SelectWipOutRecord(WipOutRecord wipOutRecord)
        {
            return base.First(a => a.PickListID == wipOutRecord.PickListID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #region 批次选择画面已出库数据（废弃）
        ///// <summary>
        ///// 在制品库出库批次选择画面数据（已出库）
        ///// </summary>
        ///// <param name="qty"></param>
        ///// <param name="pickListID"></param>
        ///// <param name="paging"></param>
        ///// <returns></returns>
        //public IEnumerable SelectWipOutRecordForBthSelect(decimal qty, string pickListID, Paging paging)
        //{

        //    //取得满足条件的出库履历表数据
        //    IQueryable<WipOutRecord> wipOutRecordList = base.GetList<WipOutRecord>().Where(a => a.PickListID.Equals(pickListID) && a.DelFlag == "0" && a.EffeFlag == "0");
        //    //取得满足条件的出库履历详细表数据
        //    IQueryable<WipOutDetailRecord> wipOutDetailRecordList = base.GetList<WipOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

        //    IQueryable<VM_WipOutBthForTableShow> query = from b in wipOutRecordList
        //                                                 join w in wipOutDetailRecordList on b.TecnPdtOutID equals w.TecnPdtOutID
        //                                                 select new VM_WipOutBthForTableShow
        //                                                 {
        //                                                     //数量
        //                                                     Qty = qty,

        //                                                     //批次号
        //                                                     BthID = w.BthID,

        //                                                     //让步区分
        //                                                     GiCls = w.GiCls,

        //                                                     //规格型号
        //                                                     PdtSpec = w.PdtSpec,

        //                                                     //单价
        //                                                     SellPrc = w.PrchsUp,

        //                                                     //使用数量
        //                                                     UserQty = w.Qty
        //                                                 };
        //    paging.total = query.Count();
        //    IEnumerable<VM_WipOutBthForTableShow> result = query.ToPageList<VM_WipOutBthForTableShow>("BthID asc", paging);
        //    return result;
        //}

        #endregion

        /// <summary>
        /// 生产查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>生产领料数据集合</returns>
        public ProduceMaterDetail WipOutRecordInfo(string pickListID, string materReqDetailNo)
        {
            return base.First<ProduceMaterDetail>(a => a.MaterReqNo == pickListID && a.MaterReqDetailNo == materReqDetailNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 在制品库出库批次选择画面数据（生产未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产未指定批次选择画面数据集合</returns>
        public IEnumerable SelectWipOutRecordProNForBthSelect(decimal qty, string pdtID,string pickListID, string materReqDetailNo,string osSupProFlg, Paging paging)
        {

            //取得满足条件的在制品库入库履历表数据
            IQueryable<WipInRecord> wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的在制品库入库履历详细表数据
            IQueryable<WipInDetailRecord> wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_WipOutBthForTableShow> query = from w in bthStockListList
                                                         join p in produceMaterDetailList on w.PdtID equals p.MaterialID
                                                         join i in wipInRecordList on w.PrhaOdrID equals i.DlvyListID
                                                         join d in wipInDetailRecordList on i.TecnPdtInID equals d.TecnPdtInID
                                                         where d.PdtID == pdtID && w.BillType == "01" && w.WhID == wipWhID
                                                          && p.MaterReqNo == pickListID && p.MaterReqDetailNo == materReqDetailNo
                                                          && w.PdtID == pdtID 
                                                         select new VM_WipOutBthForTableShow
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
                                                             ValuatUp=d.ValuatUp,

                                                             //单位标识
                                                             WipLoginPriceFlg="1",
                                                             
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
                    queryList[i].WipLoginPriceFlg = "0";
                }
                else
                {

                }
            }

            paging.total = queryList.Count();
            IEnumerable<VM_WipOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_WipOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 在制品库出库批次选择画面数据（生产指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产指定批次选择画面数据集合</returns>
        public IEnumerable SelectWipOutRecordProForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging)
        {
            
            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_WipOutBthForTableShow> query = from p in produceMaterDetailList
                                                         where p.MaterReqNo == pickListID && p.MaterReqDetailNo == materReqDetailNo
                                                         select new VM_WipOutBthForTableShow
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
            IEnumerable<VM_WipOutBthForTableShow> result = query.ToPageList<VM_WipOutBthForTableShow>("BthID asc", paging);
            return result;
        }

        /// <summary>
        /// 外协查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>外协领料数据集合</returns>
        public MCSupplierCnsmInfo WipOutRecordSInfo(string pickListID, string materReqDetailNo)
        {
            return base.First<MCSupplierCnsmInfo>(a => a.MaterReqNo == pickListID && a.No == materReqDetailNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 在制品库出库批次选择画面数据（外协未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协未指定批次选择画面数据集合</returns>
        public IEnumerable SelectWipOutRecordSupNForBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging)
        {

            //取得满足条件的在制品库入库履历表数据
            IQueryable<WipInRecord> wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的在制品库入库履历详细表数据
            IQueryable<WipInDetailRecord> wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_WipOutBthForTableShow> query = from w in bthStockListList
                                                         join m in mcSupplierCnsmInfoList on w.PdtID equals m.MaterialID
                                                         join i in wipInRecordList on w.PrhaOdrID equals i.DlvyListID
                                                         join d in wipInDetailRecordList on i.TecnPdtInID equals d.TecnPdtInID
                                                         where d.PdtID == pdtID && w.BillType == "03" && w.WhID == wipWhID
                                                          && m.MaterReqNo == pickListID && m.MaterialsSpecReq == materReqDetailNo
                                                          && w.PdtID == pdtID 

                                                         select new VM_WipOutBthForTableShow
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
                                                             ValuatUp=d.ValuatUp,

                                                             //单位标识
                                                             WipLoginPriceFlg = "1",

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
                    queryList[i].WipLoginPriceFlg = "0";
                }
                else
                {

                }

            }
            paging.total = queryList.Count();
            IEnumerable<VM_WipOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_WipOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 在制品库出库批次选择画面数据（外协指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协指定批次选择画面数据集合</returns>
        public IEnumerable SelectWipOutRecordSupForBthSelect(decimal qty, string pickListID, string materReqDetailNo, Paging paging)
        {

            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_WipOutBthForTableShow> query = from p in mcSupplierCnsmInfoList
                                                         where p.MaterReqNo == pickListID && p.No == materReqDetailNo
                                                         select new VM_WipOutBthForTableShow
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
            IEnumerable<VM_WipOutBthForTableShow> result = query.ToPageList<VM_WipOutBthForTableShow>("BthID asc", paging);
            return result;
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

        #region IWipOutRecordRepository 成员（出履历数据删除方法(yc添加)）


        public bool WipOutRecordForDel(WipOutRecord wipOutRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_WIP_OUT_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where PICK_LIST_ID={2} ", DateTime.Now, wipOutRecord.DelUsrID, wipOutRecord.PickListID);
        }

        #endregion

        /// <summary>
        /// 在制品库出库登录修改外协领料单信息表 陈健
        /// </summary>
        /// <param name="mcSupplierCnsmInfo">更新外协领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateSupplierCnsmInfoForOut(MCSupplierCnsmInfo mcSupplierCnsmInfo)
        {
            return base.ExecuteStoreCommand("update MC_SUPPLIER_CNSM_INFO set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and NO={5}", mcSupplierCnsmInfo.ReceiveQuantity, mcSupplierCnsmInfo.UpdUsrID, mcSupplierCnsmInfo.UpdDt,"1", mcSupplierCnsmInfo.MaterReqNo, mcSupplierCnsmInfo.MaterialsSpecReq);
        }

        /// <summary>
        /// 在制品库出库登录修改生产领料单详细表 陈健
        /// </summary>
        /// <param name="produceMaterDetail">更新生产领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateProduceMaterDetailForOut(ProduceMaterDetail produceMaterDetail)
        {
            return base.ExecuteStoreCommand("update PD_PROD_MATER_DETAIL set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and MATER_REQ_DET_NO={5}", produceMaterDetail.ReceQty, produceMaterDetail.UpdUsrID, produceMaterDetail.UpdDt,"1", produceMaterDetail.MaterReqNo, produceMaterDetail.MaterReqDetailNo);
        }

    } //end WipOutRecordRepositoryImp

}
