/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInRecordRepositoryImp.cs
// 文件功能描述：
//            在制品库入库履历及入库相关业务Repository实现
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
    public class WipInRecordRepositoryImp : AbstractRepository<DB, WipInRecord>, IWipInRecordRepository
    {
        //在制品库仓库编码
        public String wipWhID = "0102";
        //当前用户
        public string LoginUserID = "201228";
        //让步区分正常品
        public string norMalGiCls = "999";


        #region IWipInRecordRepository 成员(在制品库待入库一览画面初始化 （yc添加）)

        public IEnumerable GetWipInStoreBySearchByPage(VM_WipInStoreForSearch wipInStoreForSearch, Paging paging)
        {
            //进货检验单表(入库物资来自外协外购时)
            IQueryable<PurChkList> purChkList = null;
            //送货单详细表(入库物资来自外协外购时)
            IQueryable<MCDeliveryOrderDetail> mCDeliveryOrderDetailList = null;
            //过程检验单(入库物资来自生产时)
            IQueryable<ProcChkList> procChkList = null;
            //加工送货单详细表(入库物资来自生产时)
            IQueryable<ProcessDeliveryDetail> processDeliveryDetailList = null;
            //部门表
            IQueryable<Department> departmentList = null;
            //供货商信息表
            IQueryable<CompInfo> compInfoList = null;
            //过程检验单记录表（获取生产部门的ID）
            //IQueryable<ProcChkReco> procChkRecoList = null;(假注释，当前model没建立)

            //自生产获取加工部门ID(假注释，当前model没建立)
            //procChkRecoList = base.GetList<ProcChkReco>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //外协外购（加工单位）取得供货商信息表
            compInfoList = base.GetList<CompInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //自生产（加工单位）取得部门信息表
            departmentList = base.GetList<Department>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的加工送货单详细表数据
            processDeliveryDetailList = base.GetList<ProcessDeliveryDetail>().Where(p => p.WarehouseID == wipWhID && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的过程检验单表数据
            procChkList = base.GetList<ProcChkList>().Where(p => p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的送货单详细表数据
            mCDeliveryOrderDetailList = base.GetList<MCDeliveryOrderDetail>().Where(m => m.DelFlag == "0" && m.WarehouseID == wipWhID && m.EffeFlag == "0");
            //取得满足条件的进货检验单表数据
            purChkList = base.GetList<PurChkList>().Where(p => p.OsSupFlg == "1" && p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");




            //bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(wipInStoreForSearch.IsetRepID))
            {
                purChkList = purChkList.Where(c => c.ChkListId.Contains(wipInStoreForSearch.IsetRepID));
                //isPaging = false;
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.DeliveryOrderID))
            {
                purChkList = purChkList.Where(c => c.DlyOdrId.Contains(wipInStoreForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.DeliveryCompanyID))
            {
                purChkList = purChkList.Where(c => c.CompName.Contains(wipInStoreForSearch.DeliveryCompanyID));
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.MaterielID))
            {
                purChkList = purChkList.Where(c => c.PartId.Contains(wipInStoreForSearch.MaterielID));
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.MaterielName))
            {
                purChkList = purChkList.Where(c => c.PartName.Contains(wipInStoreForSearch.MaterielName));
            }
            //质检日期
            if (wipInStoreForSearch.StartDt != null)
            {
                purChkList = purChkList.Where(c => c.ChkDt >= wipInStoreForSearch.StartDt);
            }
            if (wipInStoreForSearch.EndDt != null)
            {
                purChkList = purChkList.Where(c => c.ChkDt <= wipInStoreForSearch.EndDt);
            }

            //搜索2

            if (!String.IsNullOrEmpty(wipInStoreForSearch.IsetRepID))
            {
                procChkList = procChkList.Where(p => p.ChkListId == wipInStoreForSearch.IsetRepID);
                //isPaging = false;
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.DeliveryOrderID))
            {
                procChkList = procChkList.Where(p => p.DlyOdrId.Contains(wipInStoreForSearch.DeliveryOrderID));
            }
            //if (!String.IsNullOrEmpty(wipInStoreForSearch.DeliveryCompanyID))
            //{
            //    procChkList = procChkList.Where(p => p.Contains(wipInStoreForSearch.DeliveryCompanyID));
            //}
            if (!String.IsNullOrEmpty(wipInStoreForSearch.MaterielID))
            {
                procChkList = procChkList.Where(p => p.PartId.Contains(wipInStoreForSearch.MaterielID));
            }
            if (!String.IsNullOrEmpty(wipInStoreForSearch.MaterielName))
            {
                procChkList = procChkList.Where(p => p.PartName.Contains(wipInStoreForSearch.MaterielName));
            }
            //质检日期
            if (wipInStoreForSearch.StartDt != null)
            {
                procChkList = procChkList.Where(p => p.ChkDt >= wipInStoreForSearch.StartDt);
            }
            if (wipInStoreForSearch.EndDt != null)
            {
                procChkList = procChkList.Where(p => p.ChkDt <= wipInStoreForSearch.EndDt);
            }
            //来自外购外协
            IQueryable<VM_WipInStoreForTableShow> purChkListQuery = from p in purChkList
                                                                    join m in mCDeliveryOrderDetailList on p.DlyOdrId equals m.DeliveryOrderID
                                                                    where p.PartId == m.MaterielID //方法1
                                                                    //on new { p.DlyOdrId, p.PartId } equals new { m.DeliveryOrderID, m.MaterielID }//方法2

                                                                    select new VM_WipInStoreForTableShow
                                                                    {
                                                                        //送货单号
                                                                        DeliveryOrderID = p.DlyOdrId,
                                                                        //检验报告单号
                                                                        IsetRepID = p.ChkListId,
                                                                        //供货商名称
                                                                        DeliveryCompanyID = p.CompName,
                                                                        //物料名称
                                                                        MaterielID = p.PartName,
                                                                        //质检日期
                                                                        ChkDt = p.ChkDt
                                                                    };
            var aaa = purChkListQuery.ToList();

            //自生产
            IQueryable<VM_WipInStoreForTableShow> procChkListQuery = from p in procChkList
                                                                     join q in processDeliveryDetailList on p.DlyOdrId equals q.ProcessDeliveryID
                                                                     // join a in procChkRecoList on p.ChkListId equals a.ChkListId //假注释
                                                                     //join b in departmentList on a.DepartId equals b.DeptId //假注释
                                                                     where p.PartId == q.PartID //方法1
                                                                     //on new { p.DlyOdrId, p.PartId } equals new { m.DeliveryOrderID, m.MaterielID }//方法2

                                                                     select new VM_WipInStoreForTableShow
                                                                      {
                                                                          //送货单号
                                                                          DeliveryOrderID = p.DlyOdrId,
                                                                          //检验报告单号
                                                                          IsetRepID = p.ChkListId,
                                                                          //供货商名称
                                                                          DeliveryCompanyID = "暂无数据表对应",//b.DeptName //假注释
                                                                          //物料名称
                                                                          MaterielID = p.PartName,
                                                                          //质检日期
                                                                          ChkDt = p.ChkDt
                                                                      };


            var query = purChkListQuery.Union(procChkListQuery);
            //if (isPaging)
            //{
            //    paging.total = query.Count();
            //    //IEnumerable<VM_WipInStoreForTableShow> resultForFirst = query.ToPageList<VM_WipInStoreForTableShow>("ChkDt asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    IEnumerable<VM_WipInStoreForTableShow> resultForFirst = query.ToPageList<VM_WipInStoreForTableShow>("ChkDt asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}
            paging.total = query.Count();
            IEnumerable<VM_WipInStoreForTableShow> result = query.ToPageList<VM_WipInStoreForTableShow>("ChkDt asc", paging);
            return result;
        }

        #endregion

        #region IWipInRecordRepository 成员(在制品库入库登录画面数据表示（yc添加）)


        public IEnumerable GetWipInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {
            //进货检验单表(入库物资来自外协外购时)
            IQueryable<PurChkList> purChkList = null;
            //送货单表(入库物资来自外协外购时)
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList = null;
            //送货单详细表
            IQueryable<MCDeliveryOrderDetail> mCDeliveryOrderDetailList = null;
            //过程检验单表(入库物资来自生产时)
            IQueryable<ProcChkList> procChkList = null;
            //加工送货单表(入库物资来自生产时)
            IQueryable<ProcessDelivery> ProcessDeliveryList = null;
            //加工送货单详细表
            IQueryable<ProcessDeliveryDetail> processDeliveryDetailList = null;
            //在制品库入库履历表
            IQueryable<WipInRecord> wipInRecordList = null;
            //在制品库入库履历详细表
            IQueryable<WipInDetailRecord> wipInDetailRecordList = null;
            //零件信息表
            IQueryable<PartInfo> partInfoList = null;
            //单位信息表
            IQueryable<UnitInfo> unitInfoList = null;
            //工序信息表
            IQueryable<Process> processList = null;
            //让步信息表
            IQueryable<ConcInfo> concInfoList = null;
            //部门表
            IQueryable<Department> departmentList = null;
            //供货商信息表
            IQueryable<CompInfo> compInfoList = null;

            //外协外购（加工单位）取得供货商信息表
            compInfoList = base.GetList<CompInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //自生产（加工单位）取得部门信息表
            departmentList = base.GetList<Department>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得让步信息表
            concInfoList = base.GetList<ConcInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得工序信息表
            processList = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的进货检验单表数据
            purChkList = base.GetList<PurChkList>().Where(p => p.OsSupFlg == "1" && p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的送货单表数据
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(m => m.DelFlag == "0" && m.EffeFlag == "0");
            //取得满足条件的送货单详细表数据
            mCDeliveryOrderDetailList = base.GetList<MCDeliveryOrderDetail>().Where(m => m.DelFlag == "0" && m.WarehouseID == wipWhID && m.EffeFlag == "0");
            //取得满足条件的入库履历表数据
            wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的过程检验单表数据
            procChkList = base.GetList<ProcChkList>().Where(p => p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的加工送货单详细表数据
            processDeliveryDetailList = base.GetList<ProcessDeliveryDetail>().Where(p => p.WarehouseID == wipWhID && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的加工送货单表数据
            ProcessDeliveryList = base.GetList<ProcessDelivery>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            //入库与入库履历中的数据
            IQueryable<VM_WipInLoginStoreForTableShow> wipInRecordListQuery = from a in wipInRecordList
                                                                              join b in wipInDetailRecordList on a.TecnPdtInID equals b.TecnPdtInID
                                                                              join c in partInfoList on b.PdtID equals c.PartId
                                                                              join d in unitInfoList on c.UnitId equals d.UnitId
                                                                              join e in processList on b.TecnProcess equals e.ProcessId
                                                                              //join f in compInfoList on a.ProcUnit equals f.CompId 
                                                                              //join g in departmentList on a.ProcUnit equals g.DeptId
                                                                              where (deliveryOrderID.Contains(a.DlvyListID))
                                                                              select new VM_WipInLoginStoreForTableShow
                                                                              {
                                                                                  //计划单号
                                                                                  PlanID = a.PlanID,
                                                                                  //送货单号
                                                                                  DeliveryOrderID = a.DlvyListID,
                                                                                  //加工单位（来自生产）
                                                                                  ProcUnit = (departmentList.Where(f => f.DeptId == a.ProcUnit)).FirstOrDefault().DeptName,
                                                                                  //加工单位（来自外协外购）
                                                                                  ProcUnits = (compInfoList.Where(f => f.CompId == a.ProcUnit)).FirstOrDefault().CompName,
                                                                                  //物资验收入库单号
                                                                                  McIsetInListID = a.TecnPdtInID,
                                                                                  //批次号
                                                                                  BthID = a.BthID,
                                                                                  //检验报告单号
                                                                                  IsetRepID = b.IsetRepID,
                                                                                  //让步区分
                                                                                  GiCls = b.GiCls,
                                                                                  //物资ID
                                                                                  PdtID = b.PdtID,
                                                                                  //物资名称
                                                                                  PdtName = b.PdtName,
                                                                                  //规格型号
                                                                                  PdtSpec = b.PdtSpec,
                                                                                  //加工工艺
                                                                                  TecnProcess = e.ProcName,
                                                                                  //合格
                                                                                  Qty = b.Qty,
                                                                                  //报废
                                                                                  ProScrapQty = b.ProScrapQty,
                                                                                  //料废
                                                                                  ProMaterscrapQty = b.ProMaterscrapQty,
                                                                                  //余料
                                                                                  ProOverQty = b.ProOverQty,
                                                                                  //缺料
                                                                                  ProLackQty = b.ProLackQty,
                                                                                  //合计
                                                                                  ProTotalQty = b.ProTotalQty,
                                                                                  //单位
                                                                                  Unit = d.UnitName,
                                                                                  //单价
                                                                                  PrchsUp = b.PrchsUp,
                                                                                  //估价
                                                                                  ValuatUp = b.ValuatUp,
                                                                                  //金额
                                                                                  NotaxAmt = b.NotaxAmt,
                                                                                  //入库日期
                                                                                  InDate = b.InDate,
                                                                                  //备注
                                                                                  Rmrs = b.Rmrs,
                                                                                  //标识
                                                                                  WipLoginFlg = "ForLogin",
                                                                                  //单价标识
                                                                                  WipLoginPriceFlg = "1",
                                                                                  //供货商ID
                                                                                  CompID = "",
                                                                                  //外协、外购、自生产区分
                                                                                  OsSupProFlg = a.PlanCls

                                                                              };

            //单价取得方式、加工单位取得
            var wipInRecordListQuerys = wipInRecordListQuery.ToList();
            for (int j = 0; j < wipInRecordListQuerys.Count; j++)
            {
                if (wipInRecordListQuerys[j].ProcUnit == "" || wipInRecordListQuerys[j].ProcUnit == null)
                {
                    wipInRecordListQuerys[j].ProcUnit = wipInRecordListQuerys[j].ProcUnits;
                }
            }
            var gg = wipInRecordListQuerys.ToList();
            //来自外协外购
            IQueryable<VM_WipInLoginStoreForTableShow> purChkListQuery = from p in purChkList
                                                                         join m in mCDeliveryOrderDetailList on p.DlyOdrId equals m.DeliveryOrderID
                                                                         join n in mCDeliveryOrderList on m.DeliveryOrderID equals n.DeliveryOrderID
                                                                         join c in partInfoList on p.PartId equals c.PartId
                                                                         join d in unitInfoList on c.UnitId equals d.UnitId
                                                                         join e in processList on p.ProcessId equals e.ProcessId
                                                                         join f in compInfoList on n.DeliveryCompanyID equals f.CompId
                                                                         //join a incordList on  p.DlyOdrId equals a.DlvyListID
                                                                         where (p.PartId == m.MaterielID && isetRepID.Contains(p.ChkListId))
                                                                         select new VM_WipInLoginStoreForTableShow
                                                                         {
                                                                             //计划单号
                                                                             PlanID = m.DeliveryOrderID,
                                                                             //送货单号
                                                                             DeliveryOrderID = m.DeliveryOrderID,
                                                                             //加工单位
                                                                             ProcUnit = f.CompName,
                                                                             //加工单位
                                                                             ProcUnits = f.CompName,
                                                                             //物资验收入库单号
                                                                             McIsetInListID = m.DeliveryOrderID + wipWhID,
                                                                             //批次号
                                                                             BthID = n.BatchID,
                                                                             //检验报告单号
                                                                             IsetRepID = p.ChkListId,
                                                                             //让步区分
                                                                             GiCls = p.GiCls,
                                                                             //物资ID
                                                                             PdtID = p.PartId,
                                                                             //物资名称
                                                                             PdtName = p.PartName,
                                                                             //规格型号
                                                                             PdtSpec = p.PdtSpec,
                                                                             //加工工艺
                                                                             TecnProcess = e.ProcName,
                                                                             //合格
                                                                             Qty = p.StoQty,
                                                                             //报废
                                                                             ProScrapQty = '0',
                                                                             //料废
                                                                             ProMaterscrapQty = '0',
                                                                             //余料
                                                                             ProOverQty = '0',
                                                                             //缺料
                                                                             ProLackQty = '0',
                                                                             //合计
                                                                             ProTotalQty = '0',
                                                                             //单位
                                                                             Unit = d.UnitName,
                                                                             //加工单价
                                                                             PrchsUp = m.PriceWithTax,
                                                                             //估价
                                                                             ValuatUp = '0',
                                                                             //金额
                                                                             NotaxAmt = p.StoQty * m.PriceWithTax,
                                                                             //入库日期
                                                                             InDate = DateTime.Now,
                                                                             //备注
                                                                             Rmrs = p.Remark,
                                                                             //标识
                                                                             WipLoginFlg = "",
                                                                             //单价标识
                                                                             WipLoginPriceFlg = "1",
                                                                             //供货商ID
                                                                             CompID = p.CompId,
                                                                             //外协、外购、自生产区分
                                                                             OsSupProFlg = "00" + p.OsSupFlg
                                                                         };
            //单价取得方式
            var yy = purChkListQuery.ToList();

            //来自生产
            IQueryable<VM_WipInLoginStoreForTableShow> procChkListQuery = from p in procChkList
                                                                          join q in processDeliveryDetailList on p.DlyOdrId equals q.ProcessDeliveryID
                                                                          join n in ProcessDeliveryList on q.ProcessDeliveryID equals n.ProcDelivID
                                                                          join c in partInfoList on p.PartId equals c.PartId
                                                                          join d in unitInfoList on c.UnitId equals d.UnitId
                                                                          join e in processList on p.ProcessId equals e.ProcessId
                                                                          join f in departmentList on n.DepartID equals f.DeptId
                                                                          //join a incordList on  p.DlyOdrId equals a.DlvyListID
                                                                          where (p.PartId == q.PartID && isetRepID.Contains(p.ChkListId))
                                                                          select new VM_WipInLoginStoreForTableShow
                                                                          {
                                                                              //计划单号
                                                                              PlanID = p.DlyOdrId,
                                                                              //送货单号
                                                                              DeliveryOrderID = p.DlyOdrId,
                                                                              //加工单位
                                                                              ProcUnit = f.DeptName,
                                                                              //加工单位
                                                                              ProcUnits = f.DeptName,
                                                                              //物资验收入库单号
                                                                              McIsetInListID = p.DlyOdrId + wipWhID,
                                                                              //批次号
                                                                              BthID = n.BatchID,
                                                                              //检验报告单号
                                                                              IsetRepID = p.ChkListId,
                                                                              //让步区分
                                                                              GiCls = p.GiCls,
                                                                              //物资ID
                                                                              PdtID = p.PartId,
                                                                              //物资名称
                                                                              PdtName = p.PartName,
                                                                              //规格型号
                                                                              PdtSpec = p.PdtSpec,
                                                                              //加工工艺
                                                                              TecnProcess = e.ProcName,
                                                                              //合格
                                                                              Qty = q.QualifiedQuantity,
                                                                              //报废
                                                                              ProScrapQty = q.ScrapQuantity,
                                                                              //料废
                                                                              ProMaterscrapQty = q.WasteQuantity,
                                                                              //余料
                                                                              ProOverQty = q.ExcessQuantity,
                                                                              //缺料
                                                                              ProLackQty = q.LackQuantity,
                                                                              //合计
                                                                              ProTotalQty = q.PlanTotal,
                                                                              //单位
                                                                              Unit = d.UnitName,
                                                                              //加工单价
                                                                              PrchsUp = '0',
                                                                              //估价
                                                                              ValuatUp = '0',
                                                                              //金额
                                                                              NotaxAmt = p.StoQty,
                                                                              //入库日期
                                                                              InDate = DateTime.Now,
                                                                              //备注
                                                                              Rmrs = p.Remark,
                                                                              //标识
                                                                              WipLoginFlg = "",
                                                                              //单价标识
                                                                              WipLoginPriceFlg = "1",
                                                                              //供货商ID
                                                                              CompID = "",
                                                                              //外协、外购、自生产区分
                                                                              OsSupProFlg = "000"
                                                                          };
            //单价取得方式
            var zz = procChkListQuery.ToList();

            var querys = wipInRecordListQuerys.Union(purChkListQuery);
            var query = querys.Union(procChkListQuery);
            paging.total = query.Count();
            IEnumerable<VM_WipInLoginStoreForTableShow> result = query.AsQueryable().ToPageList<VM_WipInLoginStoreForTableShow>("InDate desc", paging);
            return result;
        }

        #endregion(在制品库入库履历画面数据表示)

        #region IWipInRecordRepository 成员(在制品库入库履历一览初始化页面（yc添加）)


        public IEnumerable GetWipInRecordBySearchByPage(VM_WipInRecordStoreForSearch wipInRecordStoreForSearch, Paging paging)
        {
            IQueryable<WipInRecord> wipInRecordList = null;
            IQueryable<WipInDetailRecord> wipInDetailRecordList = null;
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
            //取得满足条件的入库履历表数据
            wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

           // bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.IsetRepID))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.IsetRepID == wipInRecordStoreForSearch.IsetRepID);
                //isPaging = false;
            }           
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.PlanID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.PlanID.Contains(wipInRecordStoreForSearch.PlanID));
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.DeliveryOrderID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.DlvyListID.Contains(wipInRecordStoreForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.BthID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.BthID.Contains(wipInRecordStoreForSearch.BthID));
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.McIsetInListID))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.TecnPdtInID.Contains(wipInRecordStoreForSearch.McIsetInListID));
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.GiCls))
            {
                if (wipInRecordStoreForSearch.GiCls == norMalGiCls)
                {
                    wipInDetailRecordList = wipInDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (wipInRecordStoreForSearch.GiCls != norMalGiCls)
                {
                    wipInDetailRecordList = wipInDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.PdtName))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.PdtName.Contains(wipInRecordStoreForSearch.PdtName));
            }
            //入库日期
            if (wipInRecordStoreForSearch.StartInDate != null)
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.InDate >= wipInRecordStoreForSearch.StartInDate);
            }
            if (wipInRecordStoreForSearch.StartInDate != wipInRecordStoreForSearch.EndInDate)
            {               
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.InDate <= wipInRecordStoreForSearch.EndInDate);
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.WhID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.WhID.Contains(wipInRecordStoreForSearch.WhID));
            }
            if (!String.IsNullOrEmpty(wipInRecordStoreForSearch.PdtSpec))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.PdtSpec.Contains(wipInRecordStoreForSearch.PdtSpec));
            }


            //入库与入库履历中的数据
            IQueryable<VM_WipInRecordStoreForTableShow> wipInRecordListQuery = from a in wipInRecordList
                                                                               join b in wipInDetailRecordList on a.TecnPdtInID equals b.TecnPdtInID
                                                                               join c in partInfoList on b.PdtID equals c.PartId
                                                                               join d in unitInfoList on c.UnitId equals d.UnitId
                                                                               join e in processList on b.TecnProcess equals e.ProcessId
                                                                               select new VM_WipInRecordStoreForTableShow
                                                                               {
                                                                                   //计划单号
                                                                                   PlanID = a.PlanID,
                                                                                   //送货单号
                                                                                   DeliveryOrderID = a.DlvyListID,
                                                                                   //加工单位（来自生产）
                                                                                   ProcUnit = (departmentList.Where(f => f.DeptId == a.ProcUnit)).FirstOrDefault().DeptName,
                                                                                   //加工单位（来自外协外购）
                                                                                   ProcUnits = (compInfoList.Where(f => f.CompId == a.ProcUnit)).FirstOrDefault().CompName,
                                                                                   //批次号
                                                                                   BthID = a.BthID,
                                                                                   //入库单号
                                                                                   McIsetInListID = a.TecnPdtInID,
                                                                                   //检验报告单号
                                                                                   IsetRepID = b.IsetRepID,
                                                                                   //让步区分
                                                                                   GiCls = b.GiCls,
                                                                                   //物资ID
                                                                                   PdtID = b.PdtID,
                                                                                   //物资名称
                                                                                   PdtName = b.PdtName,
                                                                                   //规格型号
                                                                                   PdtSpec = b.PdtSpec,
                                                                                   //加工工艺
                                                                                   TecnProcess = e.ProcName,
                                                                                   //合格
                                                                                   Qty = b.Qty,
                                                                                   //报废
                                                                                   ProScrapQty = b.ProScrapQty,
                                                                                   //料废
                                                                                   ProMaterscrapQty = b.ProMaterscrapQty,
                                                                                   //余料
                                                                                   ProOverQty = b.ProOverQty,
                                                                                   // 缺料
                                                                                   ProLackQty = b.ProLackQty,
                                                                                   //合计
                                                                                   ProTotalQty = b.ProTotalQty,
                                                                                   //单位
                                                                                   Unit = d.UnitName,
                                                                                   //加工单价
                                                                                   PrchsUp = b.PrchsUp,
                                                                                   //金额
                                                                                   NotaxAmt = b.Qty * b.PrchsUp,
                                                                                   //入库日期
                                                                                   InDate = b.InDate,
                                                                                   //备注
                                                                                   Rmrs = b.Rmrs,
                                                                                   //外协、外购、自生产区分
                                                                                   OsSupProFlg = a.PlanCls

                                                                               };
           

                paging.total = wipInRecordListQuery.Count();
                IEnumerable<VM_WipInRecordStoreForTableShow> results = wipInRecordListQuery.AsQueryable().ToPageList<VM_WipInRecordStoreForTableShow>("InDate desc", paging);
                //加工单位取得
               
                var wipInRecordListQuerys = results.ToList();
                for (int j = 0; j < wipInRecordListQuerys.Count; j++)
                {
                    if (wipInRecordListQuerys[j].ProcUnit == "" || wipInRecordListQuerys[j].ProcUnit == null)
                    {
                        wipInRecordListQuerys[j].ProcUnit = wipInRecordListQuerys[j].ProcUnits;
                    }
                }
                return wipInRecordListQuerys;
        }

        #endregion

        #region IWipInRecordRepository 成员(获取附件库入库履历数据对象（yc添加）)


        public WipInRecord SelectWipInRecord(WipInRecord wipInRecord)
        {
            return base.First(a => a.DlvyListID == wipInRecord.DlvyListID && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion

        #region IWipInRecordRepository 成员（删除在制品入库履历数据（yc添加））


        public bool WipInRecordForDel(WipInRecord wipInRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_WIP_IN_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where DLVY_LIST_ID={2} ", DateTime.Now, wipInRecord.DelUsrID, wipInRecord.DlvyListID);
        }

        #endregion

        #region IAccOutRecordRepository 成员（在制品库入库打印选择画面初始化页面（yc添加））


        public IEnumerable GetWipInPrintBySearchByPage(VM_WipInPrintForSearch wipInPrintForSearch, Paging paging)
        {
            IQueryable<WipInRecord> wipInRecordList = null;
            IQueryable<WipInDetailRecord> wipInDetailRecordList = null;
            IQueryable<PartInfo> partInfoList = null;
            IQueryable<UnitInfo> unitInfoList = null;
            IQueryable<Process> processList = null;
            //部门表
            IQueryable<Department> departmentList = null;
            //供货商信息表
            IQueryable<CompInfo> compInfoList = null;
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList = null;
            IQueryable<ProcessDelivery> ProcessDeliveryList = null;

            //取得满足条件的加工送货单表数据
            ProcessDeliveryList = base.GetList<ProcessDelivery>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的送货单表数据
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(m => m.DelFlag == "0" && m.EffeFlag == "0");
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

            //取得满足条件的入库履历表数据
            wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.WhID == wipWhID && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

           // bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;
            if (!String.IsNullOrEmpty(wipInPrintForSearch.IsetRepID))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.IsetRepID == wipInPrintForSearch.IsetRepID);
                //isPaging = false;
            }
            if (!String.IsNullOrEmpty(wipInPrintForSearch.DeliveryOrderID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.DlvyListID.Contains(wipInPrintForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(wipInPrintForSearch.BthID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.BthID.Contains(wipInPrintForSearch.BthID));
            }
            if (!String.IsNullOrEmpty(wipInPrintForSearch.GiCls))
            {
                if (wipInPrintForSearch.GiCls == norMalGiCls)
                {
                    wipInDetailRecordList = wipInDetailRecordList.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (wipInPrintForSearch.GiCls != norMalGiCls)
                {
                    wipInDetailRecordList = wipInDetailRecordList.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(wipInPrintForSearch.PdtID))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.PdtID.Contains(wipInPrintForSearch.PdtID));
            }
            if (!String.IsNullOrEmpty(wipInPrintForSearch.PrintFlg))
            {
                wipInDetailRecordList = wipInDetailRecordList.Where(a => a.PrintFlg.Contains(wipInPrintForSearch.PrintFlg));
            }
            //送货日期(来自外协外购)
            if (wipInPrintForSearch.StartInDate != null)
            {
                mCDeliveryOrderList = mCDeliveryOrderList.Where(a => a.DeliveryDate >= wipInPrintForSearch.StartInDate);
                ProcessDeliveryList = ProcessDeliveryList.Where(a => a.BillDt >= wipInPrintForSearch.StartInDate);
            }
            if (wipInPrintForSearch.EndInDate != null)
            {
                mCDeliveryOrderList = mCDeliveryOrderList.Where(a => a.DeliveryDate <= wipInPrintForSearch.EndInDate);
                ProcessDeliveryList = ProcessDeliveryList.Where(a => a.BillDt <= wipInPrintForSearch.EndInDate);
            }

            if (!String.IsNullOrEmpty(wipInPrintForSearch.WhID))
            {
                wipInRecordList = wipInRecordList.Where(a => a.WhID.Contains(wipInPrintForSearch.WhID));
            }

            //履历中的数据
            IQueryable<VM_WipInPrintForTableShow> wipInPrintListQuery = from a in wipInRecordList
                                                                        join b in wipInDetailRecordList on a.TecnPdtInID equals b.TecnPdtInID
                                                                        join c in partInfoList on b.PdtID equals c.PartId
                                                                        join d in unitInfoList on c.UnitId equals d.UnitId
                                                                        join e in processList on b.TecnProcess equals e.ProcessId
                                                                        //join m in mCDeliveryOrderList on a.DlvyListID equals m.DeliveryOrderID
                                                                        //join n in ProcessDeliveryList on a.DlvyListID equals n.ProcDelivID  //送货日期取得暂且注释？？待确认
                                                                        select new VM_WipInPrintForTableShow
                                                                        {
                                                                            //打印状态
                                                                            PrintFlg = b.PrintFlg,
                                                                            //计划单号
                                                                            PlanID = a.PlanID,
                                                                            //送货单号
                                                                            DeliveryOrderID = a.DlvyListID,
                                                                            //加工单位（来自生产）
                                                                            ProcUnit = (departmentList.Where(f => f.DeptId == a.ProcUnit)).FirstOrDefault().DeptName,
                                                                            //加工单位（来自外协外购）
                                                                            ProcUnits = (compInfoList.Where(f => f.CompId == a.ProcUnit)).FirstOrDefault().CompName,
                                                                            //批次号
                                                                            BthID = a.BthID,
                                                                            //加工产品入库单据号
                                                                            McIsetInListID = a.TecnPdtInID,
                                                                            //检验报告单
                                                                            IsetRepID = b.IsetRepID,
                                                                            //让步区分
                                                                            GiCls = b.GiCls,
                                                                            //物料ID
                                                                            PdtID = b.PdtID,
                                                                            //物料名称
                                                                            PdtName = b.PdtName,
                                                                            //规格型号
                                                                            PdtSpec = b.PdtSpec,
                                                                            //加工工艺
                                                                            TecnProcess = e.ProcName,
                                                                            //合格
                                                                            Qty = b.Qty,
                                                                            //报废
                                                                            ProScrapQty = b.ProScrapQty,
                                                                            //料废
                                                                            ProMaterscrapQty = b.ProMaterscrapQty,
                                                                            //余料
                                                                            ProOverQty = b.ProOverQty,
                                                                            // 缺料
                                                                            ProLackQty = b.ProLackQty,
                                                                            //合计
                                                                            ProTotalQty = b.ProTotalQty,
                                                                            //单位
                                                                            Unit = d.UnitName,
                                                                            //加工单价
                                                                            PrchsUp = b.PrchsUp,
                                                                            //金额
                                                                            NotaxAmt = b.NotaxAmt,
                                                                            //入库日期
                                                                            InDate = b.InDate,
                                                                            //备注
                                                                            Rmrs = b.Rmrs,

                                                                        };

            
            //if (isPaging)
            //{
            //    paging.total = wipInPrintListQuerys.Count();
            //    IEnumerable<VM_WipInPrintForTableShow> resultForFirst = wipInPrintListQuerys.AsQueryable().ToPageList<VM_WipInPrintForTableShow>("InDate desc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}

            paging.total = wipInPrintListQuery.Count();
            IEnumerable<VM_WipInPrintForTableShow> result = wipInPrintListQuery.AsQueryable().ToPageList<VM_WipInPrintForTableShow>("InDate desc", paging);
            //加工单位取得
            var wipInPrintListQuerys = result.ToList();
            for (int j = 0; j < wipInPrintListQuerys.Count; j++)
            {
                if (wipInPrintListQuerys[j].ProcUnit == "" || wipInPrintListQuerys[j].ProcUnit == null)
                {
                    wipInPrintListQuerys[j].ProcUnit = wipInPrintListQuerys[j].ProcUnits;
                }
            }
            return wipInPrintListQuerys;
        }

        #endregion

        #region IWipInRecordRepository 成员（在制品库入库单打印预览初始化页面（yc添加））


        public IEnumerable SelectForWipInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            IQueryable<WipInRecord> wipInRecordList = null;
            IQueryable<WipInDetailRecord> wipInDetailRecordList = null;
            IQueryable<PurChkList> purChkList = null;
            IQueryable<ProcChkList> procChkList = null;
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
            //取得零件信息表
            partInfoList = base.GetList<PartInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得进货检验单表信息表
            purChkList = base.GetList<PurChkList>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得过程检验单表信息表
            procChkList = base.GetList<ProcChkList>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得单位信息表
            unitInfoList = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的出库履历表数据
            wipInRecordList = base.GetList<WipInRecord>().Where(a => a.DelFlag == "0" && a.WhID == wipWhID && a.EffeFlag == "0");
            //取得满足条件的出库履历详细表数据
            wipInDetailRecordList = base.GetList<WipInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");

            //履历中的数据+外协外购
            IQueryable<VM_WipInPrintIndexForTableShow> wipInPrintListQuery = from a in wipInRecordList
                                                                             join b in wipInDetailRecordList on a.TecnPdtInID equals b.TecnPdtInID
                                                                             join c in partInfoList on b.PdtID equals c.PartId
                                                                             join d in unitInfoList on c.UnitId equals d.UnitId
                                                                             join e in processList on b.TecnProcess equals e.ProcessId
                                                                             //join f in purChkList on b.IsetRepID equals f.ChkListId
                                                                             //join g in purChkList on b.GiCls equals g.GiCls //(假注释)
                                                                             //join h in compInfoList on a.ProcUnit equals h.CompId//(假注释)
                                                                             where (deliveryOrderID.Contains(a.DlvyListID) && pdtID.Contains(b.PdtID) && (deliveryOrderID).Contains(b.TecnPdtInID.Substring(0, b.TecnPdtInID.Length - 4)))
                                                                             select new VM_WipInPrintIndexForTableShow
                                                                             {
                                                                                 //打印状态
                                                                                 PrintFlg = b.PrintFlg,
                                                                                 //开单日期
                                                                                 Date = DateTime.Now,
                                                                                 //加工单位（来自外协外购）
                                                                                 ProcUnit = (compInfoList.Where(g => g.CompId == a.ProcUnit)).FirstOrDefault().CompName,
                                                                                 //加工单位（来自生产）
                                                                                 ProcUnits = (departmentList.Where(g => g.DeptId == a.ProcUnit)).FirstOrDefault().DeptName,
                                                                                 //物料ID
                                                                                 PdtID = b.PdtID,
                                                                                 //物料名称
                                                                                 PdtName = b.PdtName,
                                                                                 //加工工艺
                                                                                 TecnProcess = e.ProcName,
                                                                                 //合格
                                                                                 Qty = b.Qty,
                                                                                 //报废
                                                                                 ProScrapQty = b.ProScrapQty,
                                                                                 //料废
                                                                                 ProMaterscrapQty = b.ProMaterscrapQty,
                                                                                 //余料
                                                                                 ProOverQty = b.ProOverQty,
                                                                                 // 缺料
                                                                                 ProLackQty = b.ProLackQty,
                                                                                 //合计
                                                                                 ProTotalQty = b.ProTotalQty,
                                                                                 //单位
                                                                                 Unit = d.UnitName,
                                                                                 //备注
                                                                                 Rmrs = b.Rmrs,
                                                                                 //质检
                                                                                 UID = (purChkList.Where(g => b.IsetRepID == g.ChkListId)).FirstOrDefault().ChkPsnId,
                                                                                 //质检
                                                                                 UIDs = (procChkList.Where(f => b.IsetRepID == f.ChkListId)).FirstOrDefault().CreUsrID,
                                                                                 //经办人
                                                                                 UID1 = LoginUserID,//当前用户ID
                                                                                 //仓管
                                                                                 WhkpID = b.WhkpID,
                                                                                 //入库日期
                                                                                 InDate = b.InDate

                                                                             };

         
            var xx = wipInPrintListQuery.ToList();
            paging.total = wipInPrintListQuery.Count();
            IEnumerable<VM_WipInPrintIndexForTableShow> result = wipInPrintListQuery.ToPageList<VM_WipInPrintIndexForTableShow>("InDate desc", paging);
            var wipInPrintListQuerys = result.ToList();
            for (int i = 0; i < wipInPrintListQuerys.Count; i++)
            {
                if (wipInPrintListQuerys[i].ProcUnit == "" || wipInPrintListQuerys[i].ProcUnit == null)
                {
                    wipInPrintListQuerys[i].ProcUnit = wipInPrintListQuerys[i].ProcUnits;
                }
                else
                {
                }
                if (wipInPrintListQuerys[i].UID == "" || wipInPrintListQuerys[i].UID == null)
                {
                    wipInPrintListQuerys[i].UID = wipInPrintListQuerys[i].UIDs;
                }
                else
                {
                }
                string WhkpID1 = wipInPrintListQuerys[0].WhkpID;
                string WhkpID = wipInPrintListQuerys[i].WhkpID;
                if (!WhkpID.Equals(WhkpID1))
                {
                    for (int j = 0; j < wipInPrintListQuerys.Count; j++)
                    {
                        wipInPrintListQuerys[j].WhkpID = WhkpID1 + "等";
                    }
                    break;
                }
                else
                {
                    //wipInPrintListQueryList[i].WhkpID = WhkpID1;
                }
            }
            return wipInPrintListQuerys;

        }

        #endregion
    }
}
