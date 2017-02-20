// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessTranslateCardRepositoryImp.cs
// 文件功能描述：加工流转卡数据操作实现类
// 
// 创建标识：代东泽 20131206
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository.database;
namespace Repository
{
    /// <summary>
    /// 代东泽 20131216
    /// </summary>
    class ProcessTranslateCardRepositoryImp : AbstractRepository<DB, ProcessTranslateCard>, IProcessTranslateCardRepository
    {
        /// <summary>
        /// 代东泽 20131217
        /// </summary>
        /// <param name="card"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable<VM_ProcessTranslateCardForTableShow> IProcessTranslateCardRepository.GetTranslateCardsWithPagingBySearch(VM_ProcessTranslateCardForSearch card, Paging paging)
        {
            IQueryable<ProcessTranslateCard> cardList = base.GetAvailableList<ProcessTranslateCard>().FilterBySearch(card);
            IQueryable<ProcessDetail> pcsdList = base.GetAvailableList<ProcessDetail>();
            IQueryable<PartInfo> ptiList = base.GetAvailableList<PartInfo>();
            IQueryable<ProcessTranslateDetail> cardDetailList = base.GetAvailableList<ProcessTranslateDetail>();
            //IQueryable<CustomTranslateInfo>
            
            
            var temp0 =from c in cardList
                             join cd in cardDetailList on c.ProcDelivID equals cd.ProcDelivID
                             group cd by new { c.ProcDelivID, cd.SeqNo,c.NedProcQty } into _a
                             where _a.Sum(n => n.RalOprQty) >= _a.Key.NedProcQty
                             select new
                             {
                                 ProcDelivID = _a.Key.ProcDelivID,
                                 SeqNo=_a.Key.SeqNo,
                                 RalOprQty = _a.Sum(n =>n.RalOprQty)
                             };
       
            var temp1 = from _r in temp0
                        group _r by new { _r.ProcDelivID } into _a
                        select new
                        {
                            ProcDelivID = _a.Key.ProcDelivID,
                            SeqNo = _a.Max(n => n.SeqNo)
                        };
            /*  var temp1_1 = from a in cardDetailList group a by new {a.ProcDelivID,a.SeqNo} into _a select new { _a.Key.ProcDelivID,_a.Key.SeqNo, RalOprQty=_a.Sum(n=>n.RalOprQty) };
              var temp_1_2 = from a in temp1_1 group a by new {a.ProcDelivID} into _a from b in _a select new {b.RalOprQty,_a.Key.ProcDelivID,SeqNo=_a.Max(n => n.SeqNo)};
            
              var temp1_2=from ptd in cardDetailList 
                          where ptd.SeqNo.Equals((from _cd in cardDetailList where _cd.ProcDelivID == ptd.ProcDelivID select _cd.SeqNo).FirstOrDefault())
                              select ptd.ProcDelivID;
              var temp1_4 = from c in cardList//零件型号
                            join pi in ptiList on c.ExportID equals pi.PartId
                            select new
                            {
                                pi.PartAbbrevi,
                                c.ProcDelivID,
                                c.ProcessID,
                                c.NedProcQty
                            };
              var temp1_5 = from c in temp1_4//已完成工序号
                            join q in temp1 on c.ProcDelivID equals q.ProcDelivID
                            select new
                            {
                                c.PartAbbrevi,
                                c.ProcDelivID,
                                c.ProcessID,
                                c.NedProcQty,
                                q.SeqNo
                            };
              var temp1_6 = from c in temp1_5//已完成工序名称
                            join pd1 in pcsdList on new { _p1 = c.ProcessID, _p2 = c.SeqNo } equals new { _p1 = pd1.ProcessId, _p2 = pd1.SeqNo }
                            select new {
                                c.PartAbbrevi,
                                c.ProcDelivID,
                                c.ProcessID,
                                c.NedProcQty,
                                c.SeqNo,
                                pd1.SubProcName
                          
                            };
              //var temp1_7=from a in car
           

              var temp1_3 = from c in temp1_5
                          join pd1 in pcsdList on new { _p1 = c.ProcessID, _p2 = c.SeqNo } equals new { _p1 = pd1.ProcessId, _p2 = pd1.SeqNo } into _a
                          from a in _a.DefaultIfEmpty()
                          join pd2 in pcsdList on new { A = c.ProcessID, B = c.SeqNo + 10 } equals new { A = pd2.ProcessId, B = pd2.SeqNo }
                            select new VM_ProcessTranslateCardForTableShow
                            {
                                ProdModel=c.PartAbbrevi,
                                ProcessInDoing = pd2.SubProcName,
                                FinishProcess = a.SubProcName,
                                ProcDelivID = c.ProcDelivID,
                                ReceQty = c.NedProcQty,
                              
                          };
              var temp2 = from c in cardList
                          join pi in ptiList on c.ExportID equals pi.PartId
                          join q in temp1 on c.ProcDelivID equals q.ProcDelivID
                          join pd1 in pcsdList on new { _p1 = c.ProcessID, _p2 = q.SeqNo } equals new { _p1 = pd1.ProcessId, _p2 = pd1.SeqNo }
                          join pd2 in pcsdList on new { A = c.ProcessID, B = q.SeqNo + 10 } equals new { A = pd2.ProcessId, B = pd2.SeqNo }
                          //where SqlFunctions.IsNumeric(q.SeqNo) + 1 == SqlFunctions.IsNumeric(pd2.SeqNo)
                          join ptd in temp_1_2 on c.ProcDelivID equals ptd.ProcDelivID 
                      
                          select new VM_ProcessTranslateCardForTableShow
                          {
                              FinishProcess = pd2.SubProcName,
                              ProcessInDoing = pd1.SubProcName,
                              ProcDelivID = c.ProcDelivID,
                              ProdModel = pi.PartAbbrevi,
                              ReceQty = c.NedProcQty,
                              LastProcessQty=ptd.RalOprQty
                          }; */


            IQueryable<VM_ProcessTranslateCardForTableShow> query = from c in cardList
                                                                 join pi in ptiList on c.ExportID equals pi.PartId into _pi
                                                                    from _rpi in _pi.DefaultIfEmpty()
                                                                 join q in temp1 on c.ProcDelivID equals q.ProcDelivID into _q
                                                                    from _rq in _q.DefaultIfEmpty()
                                                                    join pd1 in pcsdList on new { _p1 = c.ProcessID, _p2 = _rq.SeqNo } equals new { _p1 = pd1.ProcessId, _p2 = pd1.SeqNo } into _pd1
                                                                    from _rpd1 in _pd1.DefaultIfEmpty()
                                                                    join pd2 in pcsdList on new { A = c.ProcessID, B = _rpd1.SeqNo + 10 } equals new { A = pd2.ProcessId, B = pd2.SeqNo } into _pd2
                                                                    from _rpd2 in _pd2.DefaultIfEmpty()
                                                                 join ptd in cardDetailList on new { c.ProcDelivID, S = (from _cd in cardDetailList group _cd by new { _cd.ProcDelivID } into _dcc where _dcc.Key.ProcDelivID == c.ProcDelivID select _dcc.Max(n => n.SeqNo)).FirstOrDefault() } equals new { ptd.ProcDelivID, S = ptd.SeqNo } 
                                                                  group ptd by new
                                                                  {
                                                                      SubProcName1 = _rpd1.SubProcName,
                                                                      SubProcName2 = _rpd2.SubProcName,
                                                                      ProcDelivID1 = c.ProcDelivID,
                                                                      PartAbbrevi = _rpi.PartAbbrevi,
                                                                      NedProcQty = c.NedProcQty,
                                                                 ProcDelivID2=ptd.ProcDelivID,PlanStartDate=c.PlanStartDate,PlanEndDate=c.PlanEndDate
                                                                 } into _q
                                                                  select new VM_ProcessTranslateCardForTableShow
                                                                 {
                                                                     FinishProcess = _q.Key.SubProcName1,
                                                                     ProcessInDoing = _q.Key.SubProcName2,
                                                                     ProcDelivID = _q.Key.ProcDelivID1,
                                                                     ProdModel = _q.Key.PartAbbrevi,
                                                                     ReceQty = _q.Key.NedProcQty,
                                                                     LastProcessQty = _q.Sum(n=>n.RalOprQty),
                                                                     PlanStartDate = _q.Key.PlanStartDate,
                                                                     PlanEndDate = _q.Key.PlanEndDate
                                                                 };


           
            paging.total = query.Count();
            IEnumerable<VM_ProcessTranslateCardForTableShow> result = query.ToPageList<VM_ProcessTranslateCardForTableShow>("ProcDelivID asc", paging);
            return result;
        }

        public IEnumerable<VM_IWaitingWarehouseView> SearchTranslateCard(VM_IWaitingWarehouseForSearch entity, Paging paging)
        {
            //加工流转卡详细表，取流转卡号对应最大工序顺序号
            IEnumerable<VM_ProcessTranslateDetailData> MaxSeqNo = from proTraDetail in base.GetAvailableList<ProcessTranslateDetail>().AsEnumerable()
                                                               group proTraDetail by proTraDetail.ProcDelivID
                                                                   into g
                                                                   select new VM_ProcessTranslateDetailData
                                                                   {
                                                                       ProcDelivID = g.Key,
                                                                       SeqNo = g.Max(t => t.SeqNo) 
                                                                   };

            //取得相应加工流转详细表，对应加工卡号和顺序号的实际操作数量
            IEnumerable<VM_ProcessTranslateDetailData> proTraCarDet = from proTraDet in base.GetAvailableList<ProcessTranslateDetail>()
                                                                   group proTraDet by new { proTraDet.ProcDelivID, proTraDet.SeqNo }
                                                                       into g
                                                                       select new VM_ProcessTranslateDetailData
                                                                       {
                                                                           ProcDelivID = g.Key.ProcDelivID,
                                                                           SeqNo = g.Key.SeqNo,
                                                                           SumRalOprQty = g.Sum(t => t.RalOprQty)
                                                                       };
            //加工流转卡详细表中对应流转卡的最大工序顺序号的实际数量之和
            IEnumerable<VM_ProcessTranslateDetailData> proTraCarDetMaxSeqNo = from ptcd in proTraCarDet
                                                                           join msn in MaxSeqNo on
                                                                           new { ProcDelivID = ptcd.ProcDelivID, SeqNo = ptcd.SeqNo }
                                                                           equals new { ProcDelivID = msn.ProcDelivID, SeqNo = msn.SeqNo }
                                                                           select new VM_ProcessTranslateDetailData
                                                                              {
                                                                                  ProcDelivID = ptcd.ProcDelivID,
                                                                                  SeqNo = ptcd.SeqNo,
                                                                                  SumRalOprQty = ptcd.SumRalOprQty
                                                                              };
            //加工流转卡
            IQueryable<ProcessTranslateCard> proTraCar = base.GetAvailableList<ProcessTranslateCard>();
            //零件信息表
            IEnumerable<PartInfo> partInfos = base.GetList<PartInfo>();
            //取得视图的基本信息
            IQueryable<VM_IWaitingWarehouseView> iWaiWar = from adl in proTraCar.ToList().AsQueryable()
                                                           join ptdmsn in proTraCarDetMaxSeqNo.ToList().AsQueryable() on adl.ProcDelivID equals ptdmsn.ProcDelivID into gPtdmsn
                                                           from sptdmsn in gPtdmsn.DefaultIfEmpty()
                                                           join partInfo in partInfos.ToList().AsQueryable() on adl.ExportID equals partInfo.PartId into gpartInfos
                                                           from spartInfo in gpartInfos.DefaultIfEmpty()
                                                           select new VM_IWaitingWarehouseView
                                                            {
                                                                ProcDelivID = adl.ProcDelivID,
                                                                ExportID = adl.ExportID,
                                                                ProdAbbrev = (spartInfo != null ? spartInfo.PartName : ""),
                                                                ProcessID=adl.ProcessID,
                                                                UnitId=(spartInfo != null ? spartInfo.UnitId : ""),
                                                                MaterReqQty = adl.NedProcQty,//领料数量删除了修改为需加工总件数
                                                                WarehQtyAvailable = (sptdmsn != null ? sptdmsn.SumRalOprQty - adl.WarehTalQty : 0),
                                                                WarehQtySubmitted = adl.WarehTalQty,
                                                                WarehouseNo = "",//未交仓时都为空
                                                                ConcessionPart = Constant.Warehouse.CONCESSIONPART_OFF//默认是不让步接收状态
                                                            };
            //根据条件过滤可交仓数量>0的数据
            IQueryable<VM_IWaitingWarehouseView> resultData = iWaiWar.FilterBySearch(entity);
            paging.total = resultData.Count();

            IEnumerable<VM_IWaitingWarehouseView> result = resultData.ToPageList<VM_IWaitingWarehouseView>("ProcDelivID asc", paging);
            return result;
        }

        public bool AddWarehTalQty(VM_IWaitingWarehouseView entity,string user)
        {
            ProcessTranslateCard proTraCar= base.GetAvailableList<ProcessTranslateCard>().FirstOrDefault(t=>t.ProcDelivID.Equals(entity.ProcDelivID));
            proTraCar.WarehTalQty = proTraCar.WarehTalQty + entity.WarehQtyAvailable;
            proTraCar.UpdDt = DateTime.Now;
            proTraCar.UpdUsrID = user;
            return base.Update(proTraCar);
        }

        public bool ReduceWarehTalQty(string ProcTranID ,decimal PlanTotal,string user)
        {
            ProcessTranslateCard proTraCar =
            base.GetAvailableList<ProcessTranslateCard>().FirstOrDefault(t => t.ProcDelivID.Equals(ProcTranID));
            proTraCar.WarehTalQty = proTraCar.WarehTalQty - PlanTotal;
            proTraCar.UpdDt = DateTime.Now;
            proTraCar.UpdUsrID = user;

            return base.Update(proTraCar);
        }

        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public IEnumerable<VM_CustomTranslateInfoForDetaiShow> GetCustomOrdersForTranslateCard(ProcessTranslateCard card)
        {
            IQueryable<ProcessTranslateCard> cardList = base.GetAvailableList<ProcessTranslateCard>();
            IQueryable<CustomTranslateInfo> ctinfoList = base.GetAvailableList<CustomTranslateInfo>();
            IQueryable<VM_CustomTranslateInfoForDetaiShow> relation = from a in ctinfoList
                                                                       where a.ProcDelivID.Equals(card.ProcDelivID)
                                                                       select new VM_CustomTranslateInfoForDetaiShow
                                                                       {
                                                                           ProcDelivID = a.ProcDelivID,
                                                                           CustomerOrderNum = a.CustomerOrderNum,
                                                                           CustomerOrderDetails = a.CustomerOrderDetails,
                                                                           PlnQty = a.PlnQty
                                                                       };
            return relation.AsEnumerable();
        }
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public VM_ProcessTranslateCardForDetailShow GetTranslateCard(ProcessTranslateCard card)
        {
            IQueryable<ProcessTranslateCard> cardList=base.GetAvailableList<ProcessTranslateCard>();
            IQueryable<PartInfo> partList = base.GetAvailableList<PartInfo>();
            IQueryable<VM_ProcessTranslateCardForDetailShow> query = from a in cardList
                        where a.ProcDelivID.Equals(card.ProcDelivID)
                        join b in partList on a.ExportID equals b.PartId
                        select new VM_ProcessTranslateCardForDetailShow
                        {
                            ProcDelivID = a.ProcDelivID,
                            ProductType = b.PartAbbrevi,
                            ProcessBeginDate = a.PlanStartDate,
                            IsOver = a.EndFlag,
                            GiveStoreCount=a.PlnTotal,
                            NewBeginCount=a.NedProcQty
                        };
            //此处如果没有找到数据，则应该抛出异常
            if (query.Count() <= 0)
            {
                return new VM_ProcessTranslateCardForDetailShow();
            }
            else 
            {
                return query.First<VM_ProcessTranslateCardForDetailShow>();
            }
            
        }
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public IEnumerable<VM_ProcessTranslateCardPartForDetailShow> GetTranslateDetailInfos(ProcessTranslateCard card)
        {
            IQueryable<ProcessTranslateCard> cardList = base.GetAvailableList<ProcessTranslateCard>();
            IQueryable<ProcessTranslateDetail> cardDetailList = base.GetAvailableList<ProcessTranslateDetail>();
            IQueryable<Process> pcsList = base.GetAvailableList<Process>();
            IQueryable<ProcessDetail> pcsdList = base.GetAvailableList<ProcessDetail>();
            var query = from a in cardList
                        join ad in cardDetailList on a.ProcDelivID equals ad.ProcDelivID
                        where a.ProcDelivID.Equals(card.ProcDelivID)
                        join p in pcsdList on new { A = a.ProcessID, B = ad.SeqNo } equals new { A = p.ProcessId, B = p.SeqNo }
                        select new VM_ProcessTranslateCardPartForDetailShow
                        {
                            ProjectNO = ad.ItemNo,
                            ProcessName = p.SubProcName,
                            ProcessOrderNO = p.SeqNo,
                            Operator = ad.OptorID,
                            OperateDate = ad.RalOprDt,
                            OperateCount = ad.RalOprQty,
                            PlanOperateCount=ad.PlnOprQty,
                            PlanOperateDate=ad.PlnOprDt
                        };
            return query.OrderBy(n=>n.ProcessOrderNO);
        }

        /// <summary>
        /// 代东泽 20131224
        /// </summary>
        /// <param name="a"></param>
        public void UpdateTranslateDetail(ProcessTranslateDetail a)
        {
            base.UpdateNotNullColumn<ProcessTranslateDetail>(a);
        }


        public void AddTranslateDetail(ProcessTranslateDetail a)
        {
            base.Add(a);
        }
    }
}
