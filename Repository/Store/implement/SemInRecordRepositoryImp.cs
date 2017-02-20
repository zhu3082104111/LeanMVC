/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemInRecordRepositoryImp.cs
// 文件功能描述：
//          半成品库入库履历表的Repository的实现
//      
// 修改履历：2013/12/07 汪腾飞 新建
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
    /// <summary>
    /// 半成品库入库履历表的Repository的实现
    /// </summary>
    public class SemInRecordRepositoryImp : AbstractRepository<DB, SemInRecord>, ISemInRecordRepository
    {
        //定义常量
        /// <summary>
        /// 仓库编号 
        /// </summary>
        string semstoreid = "0202";
        //用户登录ID
        string LoginId = "201228";
        //让步区分正常品
        public string norMalGiCls = "999";

        #region ISemInRecordRepository (半成品库待入库一览画面)

        /// <summary>
        /// 半成品库入库履历信息获取
        /// </summary>
        /// <param name="searchCondition">搜索条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        public IEnumerable GetSemStoreBySearchByPage(VM_SemInStoreForSearch searchCondition, Extensions.Paging paging)
        {

            #region 进货检验单
            // 获取进货检验单数据
            IQueryable<PurChkList> purchaseChkList = base.GetAvailableList<PurChkList>().FilterBySearch(searchCondition);
            // 获取进货检验单 入库状态为未入库数据
            purchaseChkList = purchaseChkList.Where(a => a.StoStat == "0"); 
            #endregion

            #region 送货单详细表数据
            // 获取送货单详细表数据
            IQueryable<MCDeliveryOrderDetail> deliveryOrder = base.GetAvailableList<MCDeliveryOrderDetail>();
            // 根据半成品仓库编码 筛选信息
            deliveryOrder = deliveryOrder.Where(d=>d.WarehouseID==semstoreid);
            #endregion

            #region 过程检验单
            //取得满足条件的过程检验单表数据（入库状态为未入库的）
            IQueryable<ProcChkList> processChkList = base.GetAvailableList<ProcChkList>().FilterBySearch(searchCondition);
            processChkList = processChkList.Where(a=>a.StoStat=="0");
            #endregion

            #region 加工送货详细表
            // 取得加工送货详细表数据
            IQueryable<ProcessDeliveryDetail> processDelivery = base.GetAvailableList<ProcessDeliveryDetail>();
            // 根据仓库编码 获得加工送货单详细数据
            processDelivery=processDelivery.Where(p=>p.WarehouseID==semstoreid);
            #endregion

            bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;

            #region 条件检索
            //--------------------------------------搜索条件---------------------------------------
            // 检验报告单号
            if (!String.IsNullOrEmpty(searchCondition.IsetRepID))
            {
                // 进货检验单
                purchaseChkList = purchaseChkList.Where(w => w.ChkListId.Contains(searchCondition.IsetRepID));
                // 过程检验单
                processChkList = processChkList.Where(w => w.ChkListId.Contains(searchCondition.IsetRepID));
                // 打开分页
                isPaging = false;
            }

            // 送货单号
            if (!String.IsNullOrEmpty(searchCondition.DeliveryOrderID))
            {
                // 进货检验单
                purchaseChkList = purchaseChkList.Where(w => w.DlyOdrId.Contains(searchCondition.DeliveryOrderID));
                // 过程检验单
                processChkList = processChkList.Where(w => w.DlyOdrId.Contains(searchCondition.DeliveryOrderID));
            }

            // 物料
            if (!String.IsNullOrEmpty(searchCondition.PName))
            {
                // 进货检验单
                purchaseChkList = purchaseChkList.Where(w => w.PartName.Contains(searchCondition.PName));
                // 过程检验单
                processChkList = processChkList.Where(w => w.PartName.Contains(searchCondition.PName));
            }
            
            //------------------------------------搜索判断结束----------------------------------------------- 
            #endregion

            // 进货检验单数据
            IQueryable<VM_SemInStoreForTableShow> purchaseCheckData = from u in purchaseChkList
                                                                      join d in deliveryOrder
                                                                      on new { id = u.DlyOdrId, PartId = u.PartId }
                                                                      equals new { id = d.DeliveryOrderID, PartId = d.MaterielID }
                                                                      select new VM_SemInStoreForTableShow
                                                                      {
                                                                          IsetRepID = u.ChkListId,
                                                                          DeliveryOrderID = u.DlyOdrId,
                                                                          PName = u.PartName,
                                                                          QCDate = u.ChkDt,
                                                                      };
            // 过程检验单数据
            IQueryable<VM_SemInStoreForTableShow> processCheckData = from o in processChkList
                                                                     join s in processDelivery
                                                                     on new { id = o.DlyOdrId, PartId = o.PartId } 
                                                                     equals new { id = s.ProcessDeliveryID, PartId = s.PartID }
                                                                     select new VM_SemInStoreForTableShow
                                                                     {
                                                                         IsetRepID = o.ChkListId,
                                                                         DeliveryOrderID = o.DlyOdrId,
                                                                         PName = o.PartName,
                                                                         QCDate = o.ChkDt,
                                                                     };
            // 进货检验单数据和过程检验单数据整合
            var query = purchaseCheckData.Union(processCheckData);

            if (isPaging)
            {
                paging.total = query.Count();
                IEnumerable<VM_SemInStoreForTableShow> resultForFirst = query.ToPageList<VM_SemInStoreForTableShow>("QCDate asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
                return resultForFirst;
            }  

           paging.total = query.Count();
           IEnumerable<VM_SemInStoreForTableShow> result = query.ToPageList<VM_SemInStoreForTableShow>("QCDate asc", paging);
           return result;

        }
        #endregion

        #region ISemInRecordRepository (半成品库入库单打印选择画面)
        public IEnumerable GetSemInPrintBySearchByPage(VM_SemInPrintForSearch seminprint, Extensions.Paging paging) 
        {
            //取得满足条件的入库履历表数据
            IQueryable<SemInRecord> SemInPrint = base.GetList<SemInRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的入库履历详细表数据
            IQueryable<SemInDetailRecord> SemInDetailPrint = base.GetList<SemInDetailRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的零件信息表数据
            IQueryable<PartInfo> PartInfo = base.GetList<PartInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的单位信息表数据
            IQueryable<UnitInfo> UnitInfo = base.GetList<UnitInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的让步信息表数据
            IQueryable<ConcInfo> concInfo = base.GetList<ConcInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的工序信息表数据
            IQueryable<Process> process = base.GetList<Process>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");


            //查询
            if (!String.IsNullOrEmpty(seminprint.DeliveryOrderID))
            {
                SemInPrint = SemInPrint.Where(w => w.DlvyListId == seminprint.DeliveryOrderID);
            }
            if (!String.IsNullOrEmpty(seminprint.BthID))
            {
                SemInPrint = SemInPrint.Where(w => w.BthId == seminprint.BthID);
            }
            if (!String.IsNullOrEmpty(seminprint.IsetRepID))
            {
                SemInDetailPrint = SemInDetailPrint.Where(w => w.IsetRepId == seminprint.IsetRepID);
            }
            if (!String.IsNullOrEmpty(seminprint.PdtName))
            {
                SemInDetailPrint = SemInDetailPrint.Where(w => w.PdtName == seminprint.PdtName);
            }

            if (!String.IsNullOrEmpty(seminprint.GiCls))
            {
                if (seminprint.GiCls == norMalGiCls)
                {
                    SemInDetailPrint = SemInDetailPrint.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (seminprint.GiCls != norMalGiCls)
                {
                    SemInDetailPrint = SemInDetailPrint.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (seminprint.StartInDate != null)
            {
                SemInDetailPrint = SemInDetailPrint.Where(c => c.InDate >= seminprint.StartInDate);
            }
            if (seminprint.StartInDate != seminprint.EndInDate)
            {
                if (seminprint.EndInDate != null)
                {
                    SemInDetailPrint = SemInDetailPrint.Where(d => d.InDate <= seminprint.EndInDate);
                }
            }
            if (!String.IsNullOrEmpty(seminprint.PrintFlg))
            {
                SemInDetailPrint = SemInDetailPrint.Where(a => a.PrintFlag.Contains(seminprint.PrintFlg));
            }

            if (!String.IsNullOrEmpty(seminprint.WhID))
            {
                SemInPrint = SemInPrint.Where(w => w.WhId == seminprint.WhID);
            }



            IQueryable<VM_SemInPrintForTableShow> query = from i in SemInDetailPrint
                                                          join u in SemInPrint
                                                          on i.TecnPdtInId equals u.TecnPdtInId
                                                          join s in PartInfo
                                                          on i.PdtId equals s.PartId
                                                          join o in UnitInfo
                                                          on s.UnitId equals o.UnitId
                                                          //join w in concInfo
                                                          join w in process 
                                                          on i.Tecnrocess equals w.ProcessId
                                                          select new VM_SemInPrintForTableShow
                                                          {
                                                              PrintFlg = i.PrintFlag,//打印状态
                                                              WhID = u.WhId,//仓库编码
                                                              PlanID = u.PlanId,//计划单号
                                                              DeliveryOrderID = u.DlvyListId,//送货单号
                                                              BthID = u.BthId,//批次号
                                                              McIsetInListID = u.TecnPdtInId,//入库单号
                                                              IsetRepID = i.IsetRepId,//检验报告单号
                                                              GiCls = i.GiCls,//让步区分
                                                              PdtName = i.PdtName,//物资名称
                                                              PdtSpec = i.PdtSpec,//规格型号
                                                              TecnProcess = w.ProcName,//加工工艺
                                                              Qty = i.Qty,//合格
                                                              ProScrapQty = i.ProScrapQty,//报废
                                                              ProMaterscrapQty = i.ProMaterscrapQty,//料废
                                                              ProOverQty = i.ProOverQty,//余料
                                                              ProLackQty = i.ProLackQty,//缺料
                                                              ProTotalQty = i.Qty + i.ProScrapQty + i.ProMaterscrapQty + i.ProOverQty,//合计
                                                              Unit = o.UnitName, //单位
                                                              PrchsUp = i.PrchsUp,//加工单价
                                                              NotaxAmt = i.Qty * i.PrchsUp,//金额
                                                              Rmrs = i.Rmrs,//备注
                                                              InDate = i.InDate,//入库日期
                                                              PdtID = i.PdtId//物料ID

                                                          };

         

            //if (isPaging)
            //{
            //    paging.total = query.Count();
            //    IEnumerable<VM_SemInStoreForTableShow> resultForFirst = query.ToPageList<VM_SemInStoreForTableShow>("zjjfDate asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
            //    return resultForFirst;
            //}

            paging.total = query.Count();
            IEnumerable<VM_SemInPrintForTableShow> result = query.ToPageList<VM_SemInPrintForTableShow>("InDate asc", paging);
            return result;


        }
        #endregion

        #region ISemInRecordRepository (半成品库入库单打印预览画面)
        public IEnumerable SelectSemStore(string pdtID, string deliveryOrderID, Paging paging)
        {
            //取得满足条件的入库履历表数据
            IQueryable<SemInRecord> SemInPrint = base.GetList<SemInRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的入库履历详细表数据
            IQueryable<SemInDetailRecord> SemInDetailPrint = base.GetList<SemInDetailRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的零件信息表数据
            IQueryable<PartInfo> PartInfo = base.GetList<PartInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的单位信息表数据
            IQueryable<UnitInfo> UnitInfo = base.GetList<UnitInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //////取得满足条件的过程检验记录单表数据
            //IQueryable<ProcChkReco> ProcChkReco = base.GetList<ProcChkReco>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的进货检验单表数据
            //IQueryable<>
            //取得满足条件的工序信息表数据
            IQueryable<Process> process = base.GetList<Process>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");

            IQueryable<VM_SemInPrintIndexForTableShow> query = from i in SemInDetailPrint
                                                               join u in SemInPrint 
                                                               on i.TecnPdtInId equals u.TecnPdtInId
                                                               join s in PartInfo
                                                               on i.PdtId equals s.PartId
                                                               join o in UnitInfo
                                                               on s.UnitId equals o.UnitId
                                                               join f in process on i.Tecnrocess equals f.ProcessId
                                                               where (deliveryOrderID.Contains(u.DlvyListId) && pdtID.Contains(i.PdtId))
                                                               //join c in ProcChkReco 
                                                               //on i.PdtId equals c.PartId
                                                               //join p in
                                                               select new VM_SemInPrintIndexForTableShow
                                                               {
                                                                   PrintFlg = i.PrintFlag,//打印状态
                                                                   Date = DateTime.Now,//开单日期
                                                                   DeliveryCompanyID = u.ProcUnit,//加工单位
                                                                   PdtName = i.PdtName,//产品名称
                                                                   TecnProcess = f.ProcName,//加工工艺
                                                                   Unit = o.UnitName,//单位
                                                                   Qty = i.Qty,//合格
                                                                   ProScrapQty = i.ProScrapQty,//报废
                                                                   ProMaterscrapQty = i.ProMaterscrapQty,//料废
                                                                   ProOverQty = i.ProOverQty,//余料
                                                                   ProLackQty = i.ProLackQty,//缺料
                                                                   ProTotalQty = i.Qty + i.ProScrapQty + i.ProMaterscrapQty + i.ProOverQty,//合计
                                                                   Rmrs = i.Rmrs,//备注
                                                                   UID = LoginId,//质检员
                                                                   UID1 = i.WhkpId,//经办人
                                                                   WhkpID = i.WhkpId,//仓管
                                                                   InDate = i.InDate//入库日期                                                     
                                                             
                                                               };

            var semInPrintListQueryList = query.ToList();
            for (int i = 0; i < semInPrintListQueryList.Count; i++)
            {
                string WhkpID1 = semInPrintListQueryList[0].WhkpID;
                string WhkpID = semInPrintListQueryList[i].WhkpID;
                if (!WhkpID.Equals(WhkpID1))
                {
                    for (int j = 0; j < semInPrintListQueryList.Count; j++)
                    {
                        semInPrintListQueryList[j].WhkpID = WhkpID1 + "等";
                    }
                    break;
                }
                else
                {
                    //semInPrintListQueryList[i].WhkpID = WhkpID1;
                }
            }
            var wipInPrintListQuery11 = semInPrintListQueryList;
            paging.total = semInPrintListQueryList.Count();
            IEnumerable<VM_SemInPrintIndexForTableShow> result = semInPrintListQueryList.AsQueryable().ToPageList<VM_SemInPrintIndexForTableShow>("InDate desc", paging);
            return result;

        }

#endregion

        #region ISemInRecordRepository (半成品库入库履历一览画面)

        public IEnumerable GetSemInRecordBySearchByPage(VM_SemInRecordStoreForSearch semInRecordStoreForSearch, Paging paging)
        {
            //取得满足条件的入库履历表数据
            IQueryable<SemInRecord> semInRecord = base.GetList<SemInRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的入库履历详细表数据
            IQueryable<SemInDetailRecord> semInDetailRecord = base.GetList<SemInDetailRecord>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的零件信息表数据
            IQueryable<PartInfo> partInfo = base.GetList<PartInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的单位信息表数据
            IQueryable<UnitInfo> unitInfo = base.GetList<UnitInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的让步信息表数据
            IQueryable<ConcInfo> concInfo = base.GetList<ConcInfo>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");
            //取得满足条件的工序信息表数据
            IQueryable<Process> process = base.GetList<Process>().Where(a => a.EffeFlag == "0" && a.DelFlag == "0");


            bool isPaging = true;//按主键查询时(单条记录)，不分页
            paging.total = 1;

            //搜索1
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.PlanID))
            {
                semInRecord = semInRecord.Where(w => w.PlanId.Equals(semInRecordStoreForSearch.PlanID));
                isPaging = false;
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.DeliveryOrderID))
            {
                semInRecord = semInRecord.Where(w => w.DlvyListId.Equals(semInRecordStoreForSearch.DeliveryOrderID));
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.BthID))
            {
                semInRecord = semInRecord.Where(w => w.BthId.Equals(semInRecordStoreForSearch.BthID));
            }
            if (semInRecordStoreForSearch.StartInDate != null)
            {
                semInDetailRecord = semInDetailRecord.Where(c => c.InDate >= semInRecordStoreForSearch.StartInDate);
            }
            if (semInRecordStoreForSearch.StartInDate != semInRecordStoreForSearch.EndInDate)
            {
                if (semInRecordStoreForSearch.EndInDate != null)
                {
                    semInDetailRecord = semInDetailRecord.Where(d => d.InDate <= semInRecordStoreForSearch.EndInDate);
                }
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.PdtName))
            {
                semInDetailRecord = semInDetailRecord.Where(w => w.PdtName.Equals(semInRecordStoreForSearch.PdtName));
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.IsetRepID))
            {
                semInDetailRecord = semInDetailRecord.Where(w => w.IsetRepId.Equals(semInRecordStoreForSearch.IsetRepID));
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.McIsetInListID))
            {
                semInDetailRecord = semInDetailRecord.Where(w => w.TecnPdtInId.Equals(semInRecordStoreForSearch.McIsetInListID));
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.WhID))
            {
                semInRecord = semInRecord.Where(w => w.WhId.Equals(semInRecordStoreForSearch.WhID));
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.GiCls))
            {
                if (semInRecordStoreForSearch.GiCls == norMalGiCls)
                {
                    semInDetailRecord = semInDetailRecord.Where(a => a.GiCls.Equals(norMalGiCls));
                }
                else if (semInRecordStoreForSearch.GiCls != norMalGiCls)
                {
                    semInDetailRecord = semInDetailRecord.Where(a => a.GiCls != norMalGiCls);
                }
            }
            if (!String.IsNullOrEmpty(semInRecordStoreForSearch.PdtSpec))
            {
                semInDetailRecord = semInDetailRecord.Where(w => w.PdtSpec.Equals(semInRecordStoreForSearch.PdtSpec));
            }




            IQueryable<VM_SemInRecordStoreForTableShow> semInRecordListQuery = from a in semInDetailRecord
                                                                               join b in semInRecord on a.TecnPdtInId equals b.TecnPdtInId
                                                                               join c in partInfo on a.PdtId equals c.PartId
                                                                               join d in unitInfo on c.UnitId equals d.UnitId
                                                                               //join e in concInfo
                                                                               join f in process on a.Tecnrocess equals f.ProcessId
                                                                               select new VM_SemInRecordStoreForTableShow
                                                                               {
                                                                                   //仓库编号
                                                                                   WhID = b.WhId,
                                                                                   //计划单号
                                                                                   PlanID = b.PlanId,
                                                                                   //送货单号
                                                                                   DeliveryOrderID = b.DlvyListId,
                                                                                   //批次号
                                                                                   BthID = b.BthId,
                                                                                   //入库单号
                                                                                   McIsetInListID = a.TecnPdtInId,
                                                                                   //检验报告单号
                                                                                   IsetRepID = a.IsetRepId,
                                                                                   //让步区分
                                                                                   GiCls = a.GiCls,
                                                                                   //物资ID
                                                                                   PdtID = a.PdtId,
                                                                                   //物资名称
                                                                                   PdtName = a.PdtName,
                                                                                   //规格型号
                                                                                   PdtSpec = a.PdtSpec,
                                                                                   //加工工艺
                                                                                   TecnProcess = f.ProcName,
                                                                                   //合格
                                                                                   Qty = a.Qty,
                                                                                   //报废
                                                                                   ProScrapQty = a.ProScrapQty,
                                                                                   //料废
                                                                                   ProMaterscrapQty = a.ProMaterscrapQty,
                                                                                   //余料
                                                                                   ProOverQty = a.ProOverQty,
                                                                                   //缺料
                                                                                   ProLackQty = a.ProLackQty,
                                                                                   //合计
                                                                                   ProTotalQty = a.Qty + a.ProScrapQty + a.ProMaterscrapQty + a.ProOverQty,
                                                                                   //单位
                                                                                   Unit = d.UnitName,
                                                                                   //加工单价
                                                                                   PrchsUp = a.PrchsUp,
                                                                                   //金额
                                                                                   NotaxAmt =a.Qty * a.PrchsUp,
                                                                                   //入库日期
                                                                                   InDate = a.InDate,
                                                                                   //备注
                                                                                   Rmrs = a.Rmrs,
                                                                                   //外协、外购、自生产区分
                                                                                   OsSupProFlg = b.PlanCls

                                                                               };

            if (isPaging)
            {
                paging.total = semInRecordListQuery.Count();
                IEnumerable<VM_SemInRecordStoreForTableShow> resultForFirst = semInRecordListQuery.ToPageList<VM_SemInRecordStoreForTableShow>("InDate asc", paging).Skip((paging.page - 1) * paging.rows).Take(paging.rows);
                return resultForFirst;
            }  

            paging.total = semInRecordListQuery.Count();
            IEnumerable<VM_SemInRecordStoreForTableShow> result = semInRecordListQuery.ToPageList<VM_SemInRecordStoreForTableShow>("InDate asc", paging);
            return result;

        }
        #endregion


        #region ISemInRecordRepository (半成品库入库登录数据显示)
        public IEnumerable GetSemInStoreForLoginBySearchByPage(string deliveryOrderID, string isetRepID, Paging paging)
        {

            IQueryable<PurChkList> purChkList = null;
            IQueryable<MCDeliveryOrder> mCDeliveryOrderList = null;
            IQueryable<MCDeliveryOrderDetail> mCDeliveryOrderDetailList = null;
            IQueryable<SemInRecord> semInRecordList = null;
            IQueryable<SemInDetailRecord> semInDetailRecordList = null;
            IQueryable<UnitInfo> unitInfo = null;
            //过程检验单
            IQueryable<ProcChkList> procChkList = null;
            //加工送货单详细表
            IQueryable<ProcessDeliveryDetail> processDeliveryDetailList = null;
            //加工送货单表
            IQueryable<ProcessDelivery> processDeliveryList = null;
            //工序信息表
            IQueryable<Process> processList = null;

            //取得满足条件的进货检验单表数据
            purChkList = base.GetList<PurChkList>().Where(p => p.OsSupFlg == "1" && p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的送货单表数据
            mCDeliveryOrderList = base.GetList<MCDeliveryOrder>().Where(m => m.DelFlag == "0" && m.EffeFlag == "0");
            //取得满足条件的送货单详细表数据
            mCDeliveryOrderDetailList = base.GetList<MCDeliveryOrderDetail>().Where(m => m.DelFlag == "0" && m.WarehouseID == semstoreid && m.EffeFlag == "0");
            //取得满足条件的入库履历表数据
            semInRecordList = base.GetList<SemInRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取得满足条件的入库履历详细表数据
            semInDetailRecordList = base.GetList<SemInDetailRecord>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");
            //取的单位信息表
            unitInfo = base.GetList<UnitInfo>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
             //取得工序信息表
            processList = base.GetList<Process>().Where(u => u.DelFlag == "0" && u.EffeFlag == "0");
            //取得满足条件的过程检验单表数据
            procChkList = base.GetList<ProcChkList>().Where(p => p.StoStat == "0" && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的加工送货单详细表数据
            processDeliveryDetailList = base.GetList<ProcessDeliveryDetail>().Where(p => p.WarehouseID == semstoreid && p.DelFlag == "0" && p.EffeFlag == "0");
            //取得满足条件的加工送货单表数据
            processDeliveryList = base.GetList<ProcessDelivery>().Where(a => a.DelFlag == "0" && a.EffeFlag == "0");


            //入库与入库履历中的数据
            IQueryable<VM_SemInLoginStoreForTableShow> semInRecordListQuery = from a in semInRecordList
                                                                              join b in semInDetailRecordList on a.TecnPdtInId equals b.TecnPdtInId
                                                                              join c in unitInfo on b.PdtId equals c.UnitId
                                                                              join d in processList on b.Tecnrocess equals d.ProcessId
                                                                              where (deliveryOrderID.Contains(a.DlvyListId))
                                                                              select new VM_SemInLoginStoreForTableShow
                                                                              {
                                                                                  //计划单号
                                                                                  PlanID = a.PlanId,
                                                                                  //送货单号
                                                                                  DeliveryOrderID = a.DlvyListId,
                                                                                  //批次号
                                                                                  BthID = a.BthId,
                                                                                  //入库单号
                                                                                  McIsetInListID = a.TecnPdtInId,
                                                                                  //检验报告单号
                                                                                  IsetRepID = b.IsetRepId,
                                                                                  //让步区分
                                                                                  GiCls = b.GiCls,
                                                                                  //物资名称
                                                                                  PdtName = b.PdtName,
                                                                                  //规格型号
                                                                                  PdtSpec = b.PdtSpec,
                                                                                  //加工工艺
                                                                                  TecnProcess = d.ProcName,
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
                                                                                  ProTotalQty =b.Qty + b.ProMaterscrapQty + b.ProScrapQty + b.ProOverQty,
                                                                                  //单位
                                                                                  Unit = c.UnitName,
                                                                                  //加工单价
                                                                                  PrchsUp = b.PrchsUp,
                                                                                  //估价
                                                                                  ValuatUp = b.ValuatUp,
                                                                                  //金额
                                                                                  NotaxAmt = b.Qty * b.PrchsUp,
                                                                                  //备注
                                                                                  Rmrs = b.Rmrs,
                                                                                  ////入库日期
                                                                                  //InDate = b.InDate,
                                                                                  //标识
                                                                                  SemLoginFlg = "ForLogin",
                                                                                  //单价标识
                                                                                  AccLoginPriceFlg = "1",
                                                                                  //供货商ID
                                                                                  CompID = "",
                                                                                  //外协、自生产区分
                                                                                  OsSupProFlg = a.PlanCls
                                                                              };

            var xx = semInRecordListQuery.ToList();

            //来自外协

            IQueryable<VM_SemInLoginStoreForTableShow> purChkListQuery = from p in purChkList
                                                                         join m in mCDeliveryOrderDetailList on p.DlyOdrId equals m.DeliveryOrderID
                                                                         join n in mCDeliveryOrderList on m.DeliveryOrderID equals n.DeliveryOrderID
                                                                         join c in unitInfo on p.PartId equals c.UnitId
                                                                         join d in processList on p.ProcessId equals d.ProcessId
                                                                         where (p.PartId == m.MaterielID && isetRepID.Contains(p.ChkListId))
                                                                         select new VM_SemInLoginStoreForTableShow
                                                                         {
                                                                             //计划单号
                                                                             PlanID = m.DeliveryOrderID,
                                                                             //送货单号
                                                                             DeliveryOrderID = m.DeliveryOrderID,
                                                                             //批次号
                                                                             BthID = n.BatchID,
                                                                             //入库单号
                                                                             McIsetInListID = m.DeliveryOrderID + semstoreid,
                                                                             //检验报告单号
                                                                             IsetRepID = p.ChkListId,
                                                                             //让步区分
                                                                             GiCls = p.GiCls,
                                                                             //物资名称
                                                                             PdtName = p.PartName,
                                                                             //规格型号
                                                                             PdtSpec = p.PdtSpec,
                                                                             //加工工艺
                                                                             TecnProcess = d.ProcName,
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
                                                                             Unit = c.UnitName,
                                                                             //加工单价
                                                                             PrchsUp = m.PriceWithTax,
                                                                             //估价
                                                                             ValuatUp = '0',
                                                                             //金额
                                                                             NotaxAmt = p.StoQty * m.PriceWithTax,
                                                                             //备注
                                                                             Rmrs = p.Remark,
                                                                             //标识
                                                                             SemLoginFlg = "",
                                                                             //单价标识
                                                                             AccLoginPriceFlg = "1",
                                                                             //供货商ID
                                                                             CompID = p.CompId,
                                                                             //外协、外购、自生产区分
                                                                             OsSupProFlg = "000"
                                                                         };

            var yy = purChkListQuery.ToList();

            //来自自生产
            IQueryable<VM_SemInLoginStoreForTableShow> procChkListQuery = from p in procChkList
                                                                          join q in processDeliveryDetailList on p.DlyOdrId equals q.ProcessDeliveryID
                                                                          join n in processDeliveryList on q.ProcessDeliveryID equals n.ProcDelivID
                                                                          join c in unitInfo on p.PartId equals c.UnitId
                                                                          join d in processList on p.ProcessId equals d.ProcessId
                                                                          //join a incordList on  p.DlyOdrId equals a.DlvyListID
                                                                          where (p.PartId == q.PartID && isetRepID.Contains(p.ChkListId))
                                                                          select new VM_SemInLoginStoreForTableShow
                                                                          {
                                                                              //计划单号
                                                                              PlanID = p.DlyOdrId,
                                                                              //送货单号
                                                                              DeliveryOrderID = p.DlyOdrId,
                                                                              //批次号
                                                                              BthID = n.BatchID,
                                                                              //物资验收入库单号
                                                                              McIsetInListID = p.DlyOdrId + semstoreid,
                                                                              //检验报告单号
                                                                              IsetRepID = p.ChkListId,
                                                                              //让步区分
                                                                              GiCls = p.GiCls,
                                                                              //物资名称
                                                                              PdtName = p.PartName,
                                                                              //规格型号
                                                                              PdtSpec = p.PdtSpec,
                                                                              //加工工艺
                                                                              TecnProcess = d.ProcName,
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
                                                                              Unit = c.UnitName,
                                                                              //加工单价
                                                                              PrchsUp = '0',
                                                                              //估价
                                                                              ValuatUp = '0',
                                                                              //金额
                                                                              NotaxAmt = p.StoQty,
                                                                              //入库日期
                                                                              //InDate = DateTime.Now,
                                                                              //备注
                                                                              Rmrs = p.Remark,
                                                                              //标识
                                                                              SemLoginFlg = "",
                                                                              //单价标识
                                                                              AccLoginPriceFlg = "1",
                                                                              //供货商ID
                                                                              CompID = "",
                                                                              //外协、外购、自生产区分
                                                                              OsSupProFlg = "000"
                                                                          };

            var zz = procChkListQuery.ToList();

            var querys = semInRecordListQuery.Union(purChkListQuery);
            var query = querys.Union(procChkListQuery);
            paging.total = query.Count();
            IEnumerable<VM_SemInLoginStoreForTableShow> result = query.ToPageList<VM_SemInLoginStoreForTableShow>("PlanID desc", paging);
            return result;
        }


        #endregion

        #region ISemInRecordRepository 成员(获取履历数据对象(yc添加))


        public SemInRecord SelectSemInRecord(SemInRecord semInRecord)
        {
            return base.First(a => a.DlvyListId == semInRecord.DlvyListId && a.DelFlag == "0" && a.EffeFlag == "0");
        }

        #endregion


        #region ISemInRecordRepository （入库履历删除功能）


        public bool SemInRecordForDel(SemInRecord semInRecord)
        {
            return base.ExecuteStoreCommand("update MC_WH_SEM_IN_RECORD set DEL_FLG='1',DEL_DT={0},DEL_USR_ID={1} where DLVY_LIST_ID={2} ", DateTime.Now, semInRecord.DelUsrID, semInRecord.DlvyListId);
        }

        #endregion


        #region ISemInRecordRepository (入库登陆保存功能，一期测试)

        public bool SemInStoreForDel(string IsetRepID,string StoStat)
        {
            return base.ExecuteStoreCommand("update QU_PUR_CHK_LIST set STO_STAT={0} where CHK_LIST_ID={1}", StoStat, IsetRepID);
        }

       
        public bool SemInStoreForDelProc(string IsetRepID, string StoStat)
        {
            return base.ExecuteStoreCommand("update QU_PROC_CHK_LIST set STO_STAT={0} where CHK_LIST_ID={1}", StoStat, IsetRepID);
        }
        
        
        #endregion







    }
}
