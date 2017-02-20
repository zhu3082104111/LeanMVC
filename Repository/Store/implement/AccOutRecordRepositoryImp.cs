/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccOutRecordRepositoryImp.cs
// 文件功能描述：
//            附件库出库履历及出库相关业务Repository实现
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
    public class AccOutRecordRepositoryImp : AbstractRepository<DB, AccOutRecord>, IAccOutRecordRepository
    {  
        //附件库仓库编码
        public String accWhID = "0002";
        //让步区分正常品
        public string norMalGiCls = "999";


        #region 待出库一览(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)待出库一览结果集
        /// </summary>
        /// <param name="accOutStoreForSearch">VM_AccOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutStoreForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetAccOutStoreBySearchByPage(VM_AccOutStoreForSearch accOutStoreForSearch, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(pmdl => pmdl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmdl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && pmdl.ReceQty == 0); //获取生产领料单详细表(PD_PROD_MATER_DETAIL)结果集
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(mcscil => mcscil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mcscil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mcscil.ReceiveQuantity == 0); //获取外协领料单信息表(MC_SUPPLIER_CNSM_INFO)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集

            if (string.IsNullOrEmpty(accOutStoreForSearch.MaterReqNO) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.MaterReqNo.Equals(accOutStoreForSearch.MaterReqNO));
            }
            if (string.IsNullOrEmpty(accOutStoreForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(accOutStoreForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(accOutStoreForSearch.MaterielID) == false)
            {
                produceMaterDetailList = produceMaterDetailList.Where(pmdl => pmdl.MaterialID.Equals(accOutStoreForSearch.MaterielID));
                mcSupplierCnsmInfoList = mcSupplierCnsmInfoList.Where(mcscil => mcscil.MaterialID.Equals(accOutStoreForSearch.MaterielID));
            }
            if (accOutStoreForSearch.StartRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate >= accOutStoreForSearch.StartRequestDate);
            }
            if (accOutStoreForSearch.EndRequestDate != null)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.RequestDate <= accOutStoreForSearch.EndRequestDate);
            }

            //
            IQueryable<VM_AccOutStoreForTableShow> produceMaterDetailQuery = from pmdIQ in produceMaterDetailList
                                                                             join piIQ in partInfoList on pmdIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on pmdIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join mdilIQ in masterDefiInfoList on pmrlIQ.DeptID equals mdilIQ.AttrCd into JoinPMRMDI
                                                                             from mdilIQ in JoinPMRMDI.DefaultIfEmpty()
                                                                             select new VM_AccOutStoreForTableShow
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
            IQueryable<VM_AccOutStoreForTableShow> mcSupplierCnsmInfoQuery = from mcscilIQ in mcSupplierCnsmInfoList
                                                                             join piIQ in partInfoList on mcscilIQ.MaterialID equals piIQ.PartId into JoinPMDPI
                                                                             from piIQ in JoinPMDPI.DefaultIfEmpty()
                                                                             join pmrlIQ in produceMaterRequestList on mcscilIQ.MaterReqNo equals pmrlIQ.MaterReqNo
                                                                             join ciIQ in compInfoList on pmrlIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                                             from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                                             select new VM_AccOutStoreForTableShow
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
            IQueryable<VM_AccOutStoreForTableShow> resultIQ = produceMaterDetailQuery.Union(mcSupplierCnsmInfoQuery);
            paging.total = resultIQ.Count();
            IEnumerable<VM_AccOutStoreForTableShow> ieAccOutStoreForTableShow = resultIQ.ToPageList<VM_AccOutStoreForTableShow>("MaterReqNO asc", paging);

            return ieAccOutStoreForTableShow; //返回结果集
        } //end GetAccOutStoreBySearchByPage

        #endregion

        #region 出库单打印选择(附件库)(fyy修改)

        /// <summary>
        /// 获取(附件库)出库单打印选择结果集
        /// </summary>
        /// <param name="accOutPrintForSearch">VM_AccOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns>VM_AccOutPrintForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public IEnumerable GetAccOutPrintBySearchByPage(VM_AccOutPrintForSearch accOutPrintForSearch, Paging paging)
        {
            IQueryable<AccOutRecord> accOutRecordList = base.GetList().Where(aorl => aorl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && aorl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(附件库)出库履历表(MC_WH_ACC_OUT_RECORD)结果集
            IQueryable<AccOutDetailRecord> accOutDetailRecordList = base.GetList<AccOutDetailRecord>().Where(aodrl => aodrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && aodrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(附件库)出库履历详细表(MC_WH_ACC_OUT_DETAIL_RECORD)结果集
            IQueryable<CompInfo> compInfoList = base.GetList<CompInfo>().Where(cil => cil.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && cil.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取供货商信息表(PD_COMP_INFO)结果集
            IQueryable<MasterDefiInfo> mdiPrintList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.PRINT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)打印区分结果集
            IQueryable<MasterDefiInfo> mdiDeptList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)部门ID结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集
            IQueryable<Process> processList = base.GetList<Process>().Where(p => p.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && p.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取工序信息表(PD_PROCESS)结果集
            IQueryable<ProduceMaterRequest> produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(pmrl => pmrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pmrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取生产领料单信息表(PD_PROD_MATER_REQU)结果集
            IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(ui => ui.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && ui.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取单位信息表(BI_UNIT_INFO)结果集

            if (string.IsNullOrEmpty(accOutPrintForSearch.PickListID) == false)
            {
                accOutRecordList = accOutRecordList.Where(aorl => aorl.PickListID.Equals(accOutPrintForSearch.PickListID));
            }
            if (string.IsNullOrEmpty(accOutPrintForSearch.DeptID) == false)
            {
                produceMaterRequestList = produceMaterRequestList.Where(pmrl => pmrl.DeptID.Equals(accOutPrintForSearch.DeptID));
            }
            if (string.IsNullOrEmpty(accOutPrintForSearch.TecnProductOutID) == false)
            {
                //accOutRecordList = accOutRecordList.Where(aorl => aorl.SaeetID.Equals(accOutPrintForSearch.TecnProductOutID));
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.SaeetID.Equals(accOutPrintForSearch.TecnProductOutID));
            }
            if (string.IsNullOrEmpty(accOutPrintForSearch.ProductID) == false)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.PdtID.Equals(accOutPrintForSearch.ProductID));
            }
            if (accOutPrintForSearch.StartOutDate != null)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.OutDate >= accOutPrintForSearch.StartOutDate);
            }
            if (accOutPrintForSearch.EndOutDate != null)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.OutDate <= accOutPrintForSearch.EndOutDate);
            }
            if (string.IsNullOrEmpty(accOutPrintForSearch.PrintFlg) == false)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.PrintFlg.Equals(accOutPrintForSearch.PrintFlg));
            }
            if (string.IsNullOrEmpty(accOutPrintForSearch.WhID) == false)
            {
                accOutRecordList = accOutRecordList.Where(aorl => aorl.WhID.Equals(accOutPrintForSearch.WhID));
            }
            //
            IQueryable<VM_AccOutPrintForTableShow> resultIQ = from aodrIQ in accOutDetailRecordList
                                                              join aorIQ in accOutRecordList on aodrIQ.SaeetID equals aorIQ.SaeetID
                                                              join pmrIQ in produceMaterRequestList on aorIQ.PickListID equals pmrIQ.MaterReqNo into JoinSORPMR
                                                              from pmrIQ in JoinSORPMR.DefaultIfEmpty()
                                                              join mdiPrintIQ in mdiPrintList on aodrIQ.PrintFlg equals mdiPrintIQ.AttrCd into JoinSODRMDI
                                                              from mdiPrintIQ in JoinSODRMDI.DefaultIfEmpty()
                                                              join mdiDeptIQ in mdiDeptList on pmrIQ.DeptID equals mdiDeptIQ.AttrCd into JoinPMRMDI
                                                              from mdiDeptIQ in JoinPMRMDI.DefaultIfEmpty()
                                                              join ciIQ in compInfoList on pmrIQ.DeptID equals ciIQ.CompId into JoinPMRCI
                                                              from ciIQ in JoinPMRCI.DefaultIfEmpty()
                                                              join pIQ in processList on aodrIQ.TecnProcess equals pIQ.ProcessId into JoinSODRP
                                                              from pIQ in JoinSODRP.DefaultIfEmpty()
                                                              join piIQ in partInfoList on aodrIQ.PdtID equals piIQ.PartId into JoinSODRPI
                                                              from piIQ in JoinSODRPI.DefaultIfEmpty()
                                                              join uiIQ in unitInfoList on piIQ.UnitId equals uiIQ.UnitId into JoinPIUI
                                                              from uiIQ in JoinPIUI.DefaultIfEmpty()
                                                              select new VM_AccOutPrintForTableShow
                                                              {
                                                                  PrintFlg = aodrIQ.PrintFlg,
                                                                  PrintFlgName = mdiPrintIQ.AttrValue,
                                                                  PickListID = aorIQ.PickListID,
                                                                  TecnProductOutID = aodrIQ.SaeetID,
                                                                  BatchID = aodrIQ.BthID,
                                                                  DeptID = pmrIQ.DeptID,
                                                                  DeptName = mdiDeptIQ.AttrValue,
                                                                  CompName = ciIQ.CompName,
                                                                  PickListDetailNO = aodrIQ.PickListDetNo,
                                                                  ProductID = aodrIQ.PdtID,
                                                                  ProductName = aodrIQ.PdtName,
                                                                  TecnProcessID = aodrIQ.TecnProcess,
                                                                  TecnProcessName = pIQ.ProcName,
                                                                  Quantity = aodrIQ.Qty,
                                                                  UnitID = piIQ.UnitId,
                                                                  UnitName = uiIQ.UnitName,
                                                                  PrchsUp = aodrIQ.PrchsUp,
                                                                  NotaxAmt = aodrIQ.NotaxAmt,
                                                                  OutDate = aodrIQ.OutDate,
                                                                  Remarks = aodrIQ.Rmrs
                                                              };

            IEnumerable<VM_AccOutPrintForTableShow> resultIE = resultIQ.ToList().AsEnumerable();
            foreach (var accOutPrintForTableShow in resultIE) //遍历，设置属性 DeptName 值
            {
                var compName = string.IsNullOrEmpty(accOutPrintForTableShow.CompName) ? null : accOutPrintForTableShow.CompName; //设置属性 compName
                accOutPrintForTableShow.DeptName = string.IsNullOrEmpty(accOutPrintForTableShow.DeptName) ? compName : accOutPrintForTableShow.DeptName; //设置属性 DeptName
            }

            paging.total = resultIE.AsQueryable().Count();
            IEnumerable<VM_AccOutPrintForTableShow> ieAccOutPrintForTableShow = resultIE.AsQueryable().ToPageList<VM_AccOutPrintForTableShow>("PickListID asc", paging);

            return ieAccOutPrintForTableShow; //返回结果集
        }

        #endregion

        #region 材料领用出库单(附件库)(fyy修改)

        /// <summary>
        /// 根据领料单号，获取相关信息
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <returns>VM_AccOutPrintIndexForInfoShow 信息显示类</returns>
        /// 修改者：冯吟夷
        public VM_AccOutPrintIndexForInfoShow GetAccOutPrintForInfoShow(string pickListID)
        {
            AccOutRecord accOutRecord = new AccOutRecord();
            accOutRecord.PickListID = pickListID;
            accOutRecord = base.Find(accOutRecord);

            CompInfo compInfo = new CompInfo();
            compInfo.CompId = accOutRecord.CallinUnitID;
            compInfo = base.Find<CompInfo>(compInfo);

            MasterDefiInfo masterDefiInfo = null;
            IQueryable<MasterDefiInfo> masterDefiInfoList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.DEPT) && mdi.AttrCd.Equals(accOutRecord.CallinUnitID));
            if (masterDefiInfoList.Count() > 0)
            {
                masterDefiInfo = masterDefiInfoList.First();
            }

            ProduceMaterRequest produceMaterRequest = new ProduceMaterRequest();
            produceMaterRequest.MaterReqNo = pickListID;
            produceMaterRequest = base.Find<ProduceMaterRequest>(produceMaterRequest);

            UserInfo userInfo = new UserInfo();
            if (produceMaterRequest == null)
            {
                userInfo = null;
            }
            else
            {
                userInfo.UId = produceMaterRequest.MaterHandlerID;
                userInfo = base.Find<UserInfo>(userInfo);
            }

            VM_AccOutPrintIndexForInfoShow accOutPrintIndexForInfoShow = new VM_AccOutPrintIndexForInfoShow();
            accOutPrintIndexForInfoShow.SaeetID = accOutRecord.SaeetID;
            if (masterDefiInfo != null)
            {
                accOutPrintIndexForInfoShow.CallinUnitName = masterDefiInfo.AttrValue;
            }
            if (compInfo != null)
            {
                accOutPrintIndexForInfoShow.CallinUnitName = compInfo.CompName;
            }
            accOutPrintIndexForInfoShow.MaterHandlerName = userInfo == null ? null : userInfo.UName;

            return accOutPrintIndexForInfoShow;

        } //end GetAccOutPrintByInfoShow

        /// <summary>
        /// 根据 AccOutRecord 类，获取相关信息
        /// </summary>
        /// <param name="accOutRecord">AccOutRecord 实体类</param>
        /// <returns>VM_AccOutPrintIndexForTableShow 表格显示类</returns>
        /// 修改者：冯吟夷
        public VM_AccOutPrintIndexForTableShow GetAccOutPrintForTableShow(AccOutDetailRecord accOutDetailRecord)
        {
            IQueryable<AccOutDetailRecord> accOutDetailRecordList = base.GetList<AccOutDetailRecord>().Where(aodrl => aodrl.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && aodrl.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取(附件库)出库履历详细表(MC_WH_Acc_OUT_DETAIL_RECORD)结果集
            IQueryable<MasterDefiInfo> mdiPrintList = base.GetList<MasterDefiInfo>().Where(mdi => mdi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && mdi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON) && mdi.SectionCd.Equals(Constant.MasterSection.PRINT)); //获取Master数据管理表(BI_MASTER_DEFI_INFO)打印区分结果集
            IQueryable<PartInfo> partInfoList = base.GetList<PartInfo>().Where(pi => pi.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && pi.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取零件信息表(PD_PART_INFO)结果集
            IQueryable<Process> processList = base.GetList<Process>().Where(p => p.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && p.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取工序信息表(PD_PROCESS)结果集
            IQueryable<UnitInfo> unitInfoList = base.GetList<UnitInfo>().Where(ui => ui.DelFlag.Equals(Constant.GLOBAL_DELFLAG_ON) && ui.EffeFlag.Equals(Constant.GLOBAL_EffEFLAG_ON)); //获取单位信息表(BI_UNIT_INFO)结果集

            if (accOutDetailRecord != null)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(aodrl => aodrl.SaeetID.Equals(accOutDetailRecord.SaeetID) && aodrl.PickListDetNo.Equals(accOutDetailRecord.PickListDetNo) && aodrl.BthID.Equals(accOutDetailRecord.BthID));
            }

            IQueryable<VM_AccOutPrintIndexForTableShow> resultIQ = from aodrIQ in accOutDetailRecordList
                                                                   join piIQ in partInfoList on aodrIQ.PdtID equals piIQ.PartId into JoinAODRPI
                                                                   from piIQ in JoinAODRPI.DefaultIfEmpty()
                                                                   join uiIQ in unitInfoList on piIQ.UnitId equals uiIQ.UnitId into JoinPIUI
                                                                   from uiIQ in JoinPIUI.DefaultIfEmpty()
                                                                   join pIQ in processList on aodrIQ.TecnProcess equals pIQ.ProcessId into JoinAODRP
                                                                   from pIQ in JoinAODRP.DefaultIfEmpty()
                                                                   join mdipIQ in mdiPrintList on aodrIQ.PrintFlg equals mdipIQ.AttrCd into JoinAODRMDIP
                                                                   from mdipIQ in JoinAODRMDIP.DefaultIfEmpty()
                                                                   select new VM_AccOutPrintIndexForTableShow
                                                                   {
                                                                       SaeetID = aodrIQ.SaeetID,
                                                                       PickListDetNo = aodrIQ.PickListDetNo,
                                                                       BthID = aodrIQ.BthID,
                                                                       ProductID = aodrIQ.PdtID,
                                                                       ProductName = aodrIQ.PdtName,
                                                                       TecnProcess = aodrIQ.TecnProcess,
                                                                       TecnProcessName = pIQ.ProcName,
                                                                       UnitID = piIQ.UnitId,
                                                                       UnitName = uiIQ.UnitName,
                                                                       Quantity = aodrIQ.Qty,
                                                                       PrintFlg = aodrIQ.PrintFlg,
                                                                       PrintFlgName = mdipIQ.AttrValue,
                                                                       Remarks = aodrIQ.Rmrs
                                                                   };
            return resultIQ.First();
        } //end GetAccOutPrintForTableShow

        #endregion

        #region IAccOutRecordRepository 成员(附件库出库登录画面数据表示)

        /// <summary>
        /// 附件库出库登录画面初始化 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录画面数据</returns>
        public IEnumerable GetAccOutStoreForLoginBySearchByPage(string pickListID, string materielID, string pickListDetNo, Paging paging)
        {
            IQueryable<ProduceMaterRequest> produceMaterRequestList = null;
            IQueryable<ProduceMaterDetail> produceMaterDetailList = null;
            //IQueryable<AccOutRecord> accOutRecordList = null;
            //IQueryable<AccOutDetailRecord> accOutDetailRecordList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;
            IQueryable<Process> processList = null;
            IQueryable<MasterDefiInfo> masterInfoList = null;
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = null;

            //取的工序信息表
            processList = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的生产领料单信息表数据
            produceMaterRequestList = base.GetList<ProduceMaterRequest>().Where(p => p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的生产领料单详细表数据
            produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(p => p.AppoQty > p.ReceQty && p.DelFlag == "0" && p.EffeFlag == "0");

            ////取得满足条件的出库履历表数据
            //accOutRecordList = base.GetList<AccOutRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            ////取得满足条件的出库履历详细表数据
            //accOutDetailRecordList = base.GetList<AccOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            //取的master表
            masterInfoList = base.GetList<MasterDefiInfo>().Where(u => u.SectionCd == "00009" && u.DelFlag == "0" && u.EffeFlag == "0");
            //取的外协领料单信息表
            mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");

            ////出库与出库履历中的数据
            //IQueryable<VM_AccOutLoginStoreForTableShow> accOutRecordListQuery = from a in accOutRecordList
            //                                                                  join b in accOutDetailRecordList on a.SaeetID equals b.SaeetID
            //                                                                  join c in partInfoList on b.PdtID equals c.PartId
            //                                                                  join d in unitInfoList on c.UnitId equals d.UnitId
            //                                                                  join m in masterInfoList on a.CallinUnitID equals m.AttrCd
            //                                                                  where (pickListID.Contains(a.PickListID))
            //                                                                  select new VM_AccOutLoginStoreForTableShow
            //                                                                  {
            //                                                                      //领料单号
            //                                                                      PickListID = a.PickListID,
            //                                                                      //加工产品出库单号
            //                                                                      SaeetID = a.SaeetID,
            //                                                                      //请领单位
            //                                                                      CallinUnitID = m.AttrValue,
            //                                                                      //物料编号
            //                                                                      MaterielID = b.PdtID,
            //                                                                      //物料名称
            //                                                                      MaterielName = b.PdtName,
            //                                                                      //加工工艺
            //                                                                      TecnProcess =b.TecnProcess,
            //                                                                      //数量
            //                                                                      Qty = b.Qty,
            //                                                                      //请领数量
            //                                                                      CallinQty = b.Qty,
            //                                                                      //单位
            //                                                                      Unit = d.UnitName,
            //                                                                      //出库单价
            //                                                                      SellPrc = b.PrchsUp,
            //                                                                      //金额
            //                                                                      NotaxAmt = b.NotaxAmt,
            //                                                                      //出库日期
            //                                                                      OutDate = b.OutDate,
            //                                                                      //备注
            //                                                                      Rmrs = b.Rmrs,
            //                                                                      //标识
            //                                                                      AccLoginFlg = "ForLogin",

            //                                                                      //规格型号
            //                                                                      PdtSpec = b.PdtSpec,

            //                                                                      //让步区分
            //                                                                      GiCls = b.GiCls,

            //                                                                      //领料单详细号
            //                                                                      PickListDetNo = b.PickListDetNo,
            //                                                                      //外协、外购、自生产区分
            //                                                                      OsSupProFlg = "",
            //                                                                      //批次号
            //                                                                      BthID = b.BthID
            //                                                                  };
            //var ERER = accOutRecordListQuery.ToList();

            //来自生产
            IQueryable<VM_AccOutLoginStoreForTableShow> produceMaterRequestListQuery = from p in produceMaterRequestList
                                                                                join q in produceMaterDetailList on p.MaterReqNo equals q.MaterReqNo
                                                                                join c in partInfoList on q.MaterialID equals c.PartId into pname
                                                                                from c in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                join d in unitInfoList on c.UnitId equals d.UnitId
                                                                                join e in processList on q.ProcessID equals e.ProcessId
                                                                                join m in masterInfoList on p.DeptID equals m.AttrCd
                                                                                where (pickListID.Contains(p.MaterReqNo) && materielID.Contains(q.MaterialID) && pickListDetNo.Contains(q.MaterReqDetailNo))
                                                                                select new VM_AccOutLoginStoreForTableShow
                                                                                {
                                                                                    //领料单号
                                                                                    PickListID = p.MaterReqNo,
                                                                                    //加工产品出库单号
                                                                                    SaeetID = p.MaterReqNo + accWhID,
                                                                                    //请领单位
                                                                                    CallinUnitID = m.AttrValue,
                                                                                    //物料编号
                                                                                    MaterielID = q.MaterialID,
                                                                                    //物料名称
                                                                                    MaterielName = c.PartName,
                                                                                    //加工工艺
                                                                                    TecnProcess = e.ProcName,
                                                                                    //数量
                                                                                    Qty = 0,
                                                                                    //请领数量
                                                                                    CallinQty = q.AppoQty,
                                                                                    //单位
                                                                                    Unit = d.UnitName,
                                                                                    //出库单价
                                                                                    SellPrc = 0,
                                                                                    //金额
                                                                                    NotaxAmt = 0,
                                                                                    //出库日期
                                                                                    OutDate = DateTime.Today,
                                                                                    //备注
                                                                                    Rmrs = p.Remark,
                                                                                    //标识
                                                                                    //AccLoginFlg = "",
                                                                                    //规格型号
                                                                                    PdtSpec = q.PdtSpec,

                                                                                    //让步区分
                                                                                    GiCls = q.SpecFlag,

                                                                                    //领料单详细号
                                                                                    PickListDetNo = q.MaterReqDetailNo,
                                                                                    //外协、外购、自生产区分
                                                                                    OsSupProFlg = "000",
                                                                                    //批次号
                                                                                    BthID = q.BthID
                                                                                };
            var mj = produceMaterRequestListQuery.ToList();

            //来自外协
            IQueryable<VM_AccOutLoginStoreForTableShow> mcSupplierCnsmInfoListQuery = from m in mcSupplierCnsmInfoList
                                                                                      join p in produceMaterRequestList on m.MaterReqNo equals p.MaterReqNo
                                                                                      join a in masterInfoList on p.DeptID equals a.AttrCd
                                                                                      join n in partInfoList on m.MaterialID equals n.PartId into pname
                                                                                      from n in pname.DefaultIfEmpty() //找出pname中为空的值，将其赋值为Null（外连接）
                                                                                      join d in processList on m.ProcID equals d.ProcessId
                                                                                      join c in unitInfoList on n.UnitId equals c.UnitId

                                                                                      where (pickListID.Contains(m.MaterReqNo) && materielID.Contains(m.MaterialID) && pickListDetNo.Contains(m.No))
                                                                                      select new VM_AccOutLoginStoreForTableShow
                                                                                      {
                                                                                          //领料单号
                                                                                          PickListID = m.MaterReqNo,
                                                                                          //加工产品出库单号
                                                                                          SaeetID = m.MaterReqNo + accWhID,
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
                                                                                          //AccLoginFlg = "",

                                                                                          //规格型号
                                                                                          PdtSpec = m.MaterialsSpecReq,

                                                                                          //让步区分
                                                                                          GiCls = m.SpecFlg,

                                                                                          //领料单详细号
                                                                                          PickListDetNo = m.No,
                                                                                          //外协、外购、自生产区分
                                                                                          OsSupProFlg = "002",
                                                                                          //批次号
                                                                                          BthID = m.BatchID
                                                                                      };
            var mcs = mcSupplierCnsmInfoListQuery.ToList();

            //var query = accOutRecordListQuery.Union(produceMaterRequestListQuery);
            var queryxx = produceMaterRequestListQuery.Union(mcSupplierCnsmInfoListQuery);

            var xx = queryxx.ToList();
            paging.total = queryxx.Count();
            IEnumerable<VM_AccOutLoginStoreForTableShow> result = queryxx.ToPageList<VM_AccOutLoginStoreForTableShow>("OutDate desc", paging);
            return result;

        }

        #endregion

        #region IAccOutRecordRepository 成员（附件库出库履历一览初始化页面）


        public IEnumerable GetAccOutRecordBySearchByPage(VM_AccOutRecordStoreForSearch accOutRecordStoreForSearch, Paging paging)
        {
            IQueryable<AccOutRecord> accOutRecordList = null;
            IQueryable<AccOutDetailRecord> accOutDetailRecordList = null;
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
            accOutRecordList = base.GetList<AccOutRecord>().Where(a => a.DelFlag == "0" && a.WhID == accWhID && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            accOutDetailRecordList = base.GetList<AccOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            //bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(accOutRecordStoreForSearch.PickListID))
            {
                accOutRecordList = accOutRecordList.Where(a => a.PickListID == accOutRecordStoreForSearch.PickListID);
                //isPaging = false;
            }        
            if (!String.IsNullOrEmpty(accOutRecordStoreForSearch.SaeetID))
            {
                accOutRecordList = accOutRecordList.Where(a => a.SaeetID.Contains(accOutRecordStoreForSearch.SaeetID));
            }
            if (!String.IsNullOrEmpty(accOutRecordStoreForSearch.WhID))
            {
                accOutRecordList = accOutRecordList.Where(a => a.WhID.Contains(accOutRecordStoreForSearch.WhID));
            }
            if (!String.IsNullOrEmpty(accOutRecordStoreForSearch.BthID))
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(a => a.BthID.Contains(accOutRecordStoreForSearch.BthID));
            }
            //出库日期
            if (accOutRecordStoreForSearch.StartOutDate != null)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(a => a.OutDate >= accOutRecordStoreForSearch.StartOutDate);
            }
            if (accOutRecordStoreForSearch.EndOutDate != null)
            {
                accOutDetailRecordList = accOutDetailRecordList.Where(a => a.OutDate <= accOutRecordStoreForSearch.EndOutDate);
            }
            if (!String.IsNullOrEmpty(accOutRecordStoreForSearch.GiCls))
            {
                if (accOutRecordStoreForSearch.GiCls == norMalGiCls)
                {
                    accOutDetailRecordList = accOutDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (accOutRecordStoreForSearch.GiCls != norMalGiCls)
                {
                    accOutDetailRecordList = accOutDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
         

            //出库与出库履历中的数据
            IQueryable<VM_AccOutRecordStoreForTableShow> accOutRecordListQuery = from a in accOutRecordList
                                                                               join b in accOutDetailRecordList on a.SaeetID equals b.SaeetID
                                                                               join c in partInfoList on b.PdtID equals c.PartId
                                                                               join d in unitInfoList on c.UnitId equals d.UnitId
                                                                               join e in processList on b.TecnProcess equals e.ProcessId
                                                                               select new VM_AccOutRecordStoreForTableShow
                                                                               {
                                                                                    //领料单号
                                                                                    PickListID=a.PickListID,
                                                                                    //领料单类型
                                                                                    PickListTypeID=a.PickListTypeID,
                                                                                    //领料单详细号
                                                                                    PickListDetNo=b.PickListDetNo,
                                                                                    //加工产品出库单
                                                                                    SaeetID=a.SaeetID,
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
                                                                                    SellPrc =b.SellPrc,
                                                                                    //金额
                                                                                    NotaxAmt=b.NotaxAmt,
                                                                                    //出库日期
                                                                                    OutDate =b.OutDate,
                                                                                    //备注
                                                                                    Rmrs=b.Rmrs

                                                                               };
            //if (isPaging)
            //{
            //    paging.total = accOutRecordListQuery.Count();
            //    IEnumerable<VM_AccOutRecordStoreForTableShow> resultForFirst = accOutRecordListQuery.ToPageList<VM_AccOutRecordStoreForTableShow>("OutDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //} 

            paging.total = accOutRecordListQuery.Count();
            IEnumerable<VM_AccOutRecordStoreForTableShow> result = accOutRecordListQuery.ToPageList<VM_AccOutRecordStoreForTableShow>("OutDate desc", paging);
            return result;
        }

        #endregion

        #region IAccOutRecordRepository 成员


        public bool AccOutRecordForDel(AccOutRecord accOutRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_ACC_OUT_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where PICK_LIST_ID={2} ", DateTime.Now, accOutRecord.DelUsrID, accOutRecord.PickListID);
        }

        #endregion

        /// <summary>
        /// 附件库出库登录插入出库履历表查询是否已存在 陈健
        /// </summary>
        /// <param name="accOutRecord">附件库出库登录数据集合</param>
        /// <returns>数据集合</returns>
        public AccOutRecord SelectAccOutRecord(AccOutRecord accOutRecord)
        {
            return base.First(a => a.PickListID == accOutRecord.PickListID && a.DelFlag == "0" && a.EffeFlag == "0");
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

        #region 附件库出库批次选择已出库（作废）
        /// <summary>
        /// 附件库出库批次选择画面数据（已出库）
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="pickListID"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable SelectAccOutRecordForBthSelect(decimal qty, string pickListID, Paging paging)
        {

            //取得满足条件的出库履历表数据
            IQueryable<AccOutRecord> accOutRecordList = base.GetList<AccOutRecord>().Where(a => a.PickListID.Equals(pickListID) && a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            IQueryable<AccOutDetailRecord> accOutDetailRecordList = base.GetList<AccOutDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_AccOutBthForTableShow> query = from b in accOutRecordList
                                                         join w in accOutDetailRecordList on b.SaeetID equals w.SaeetID
                                                         select new VM_AccOutBthForTableShow
                                                         {
                                                             //数量
                                                             Qty = qty,

                                                             //批次号
                                                             BthID = w.BthID,

                                                             //让步区分
                                                             GiCls = w.GiCls,

                                                             //规格型号
                                                             PdtSpec = w.PdtSpec,

                                                             //单价
                                                             SellPrc = w.PrchsUp,

                                                             //使用数量
                                                             UserQty = w.Qty
                                                         };
            paging.total = query.Count();
            IEnumerable<VM_AccOutBthForTableShow> result = query.ToPageList<VM_AccOutBthForTableShow>("BthID asc", paging);
            return result;
        } 
        #endregion

        /// <summary>
        /// 生产查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <returns>生产领料数据集合</returns>
        public ProduceMaterDetail AccOutRecordInfo(string pickListID, string pickListDetNo)
        {
            return base.First<ProduceMaterDetail>(a => a.MaterReqNo == pickListID && a.MaterReqDetailNo == pickListDetNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 附件库出库批次选择画面数据（生产未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产未指定批次选择画面数据集合</returns>
        public IEnumerable SelectAccOutRecordProNForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging)
        {

            //取得满足条件的附件库入库履历表数据
            IQueryable<AccInRecord> accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的附件库入库履历详细表数据
            IQueryable<AccInDetailRecord> accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_AccOutBthForTableShow> query = from w in bthStockListList
                                                         join p in produceMaterDetailList on w.PdtID equals p.MaterialID
                                                         join i in accInRecordList on w.PrhaOdrID equals i.DlvyListID
                                                         join d in accInDetailRecordList on i.McIsetInListID equals d.McIsetInListID
                                                         where d.PdtID == pdtID && w.BillType == "01" && w.WhID == accWhID
                                                          && p.MaterReqNo == pickListID && p.MaterReqDetailNo == pickListDetNo
                                                          && w.PdtID == pdtID
                                                         select new VM_AccOutBthForTableShow
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
                                                             AccLoginPriceFlg = "1",

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
                    queryList[i].AccLoginPriceFlg = "0";
                }
                else
                {

                }

            }

            paging.total = queryList.Count();
            IEnumerable<VM_AccOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_AccOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 附件库出库批次选择画面数据（生产指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>生产指定批次选择画面数据集合</returns>
        public IEnumerable SelectAccOutRecordProForBthSelect(decimal qty, string pickListID, string pickListDetNo, Paging paging)
        {

            //取得满足条件的生产领料单详细表数据
            IQueryable<ProduceMaterDetail> produceMaterDetailList = base.GetList<ProduceMaterDetail>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_AccOutBthForTableShow> query = from p in produceMaterDetailList
                                                         where p.MaterReqNo == pickListID && p.MaterReqDetailNo == pickListDetNo
                                                         select new VM_AccOutBthForTableShow
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
            IEnumerable<VM_AccOutBthForTableShow> result = query.ToPageList<VM_AccOutBthForTableShow>("BthID asc", paging);
            return result;
        }

        /// <summary>
        /// 外协查询领料单是否指定批次 陈健
        /// </summary>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <returns>外协领料数据集合</returns>
        public MCSupplierCnsmInfo AccOutRecordSInfo(string pickListID, string pickListDetNo)
        {
            return base.First<MCSupplierCnsmInfo>(a => a.MaterReqNo == pickListID && a.No == pickListDetNo && a.EffeFlag == "0" && a.DelFlag == "0");
        }

        /// <summary>
        /// 附件库出库批次选择画面数据（外协未指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协未指定批次选择画面数据集合</returns>
        public IEnumerable SelectAccOutRecordSupNForBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging)
        {

            //取得满足条件的附件库入库履历表数据
            IQueryable<AccInRecord> accInRecordList = base.GetList<AccInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的附件库入库履历详细表数据
            IQueryable<AccInDetailRecord> accInDetailRecordList = base.GetList<AccInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的批次别库存表数据
            IQueryable<BthStockList> bthStockListList = base.GetList<BthStockList>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_AccOutBthForTableShow> query = from w in bthStockListList
                                                         join m in mcSupplierCnsmInfoList on w.PdtID equals m.MaterialID
                                                         join i in accInRecordList on w.PrhaOdrID equals i.DlvyListID
                                                         join d in accInDetailRecordList on i.McIsetInListID equals d.McIsetInListID
                                                         where d.PdtID == pdtID && w.BillType == "03" && w.WhID == accWhID
                                                          && m.MaterReqNo == pickListID && m.MaterialsSpecReq == pickListDetNo
                                                          && w.PdtID == pdtID

                                                         select new VM_AccOutBthForTableShow
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
                                                             AccLoginPriceFlg = "1",

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
                    queryList[i].AccLoginPriceFlg = "0";
                }
                else
                {

                }

            }
            paging.total = queryList.Count();
            IEnumerable<VM_AccOutBthForTableShow> result = queryList.AsQueryable().ToPageList<VM_AccOutBthForTableShow>("InDate asc", paging);
            return result;
        }

        /// <summary>
        /// 附件库出库批次选择画面数据（外协指定批次） 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="paging">分页参数</param>
        /// <returns>外协指定批次选择画面数据集合</returns>
        public IEnumerable SelectAccOutRecordSupForBthSelect(decimal qty, string pickListID, string pickListDetNo, Paging paging)
        {

            //取得满足条件的外协领料单信息表数据
            IQueryable<MCSupplierCnsmInfo> mcSupplierCnsmInfoList = base.GetList<MCSupplierCnsmInfo>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            IQueryable<VM_AccOutBthForTableShow> query = from p in mcSupplierCnsmInfoList
                                                         where p.MaterReqNo == pickListID && p.No == pickListDetNo
                                                         select new VM_AccOutBthForTableShow
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
            IEnumerable<VM_AccOutBthForTableShow> result = query.ToPageList<VM_AccOutBthForTableShow>("BthID asc", paging);
            return result;
        }

        /// <summary>
        /// 附件库出库登录修改外协领料单信息表 陈健
        /// </summary>
        /// <param name="mcSupplierCnsmInfo">更新外协领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateSupplierCnsmInfoForOut(MCSupplierCnsmInfo mcSupplierCnsmInfo)
        {
            return base.ExecuteStoreCommand("update MC_SUPPLIER_CNSM_INFO set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and NO={5}", mcSupplierCnsmInfo.ReceiveQuantity, mcSupplierCnsmInfo.UpdUsrID, mcSupplierCnsmInfo.UpdDt,"1", mcSupplierCnsmInfo.MaterReqNo, mcSupplierCnsmInfo.MaterialsSpecReq);
        }

        /// <summary>
        /// 附件库出库登录修改生产领料单详细表 陈健
        /// </summary>
        /// <param name="produceMaterDetail">更新生产领料单数据集合</param>
        /// <returns>更新结果</returns>
        public bool UpdateProduceMaterDetailForOut(ProduceMaterDetail produceMaterDetail)
        {
            return base.ExecuteStoreCommand("update PD_PROD_MATER_DETAIL set RECE_QTY={0},UPD_USR_ID={1},UPD_DT={2},DEL_FLG={3} where MATER_REQ_NO={4} and MATER_REQ_DET_NO={5}", produceMaterDetail.ReceQty, produceMaterDetail.UpdUsrID, produceMaterDetail.UpdDt, "1", produceMaterDetail.MaterReqNo, produceMaterDetail.MaterReqDetailNo);
        }

    } //end AccOutRecordRepositoryImp
}
