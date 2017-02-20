// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemBillRepositoryImp.cs
// 文件功能描述：工票数据操作实现类
// 
// 创建标识：代东泽 20131126
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using Model.Produce;
using Extensions;
using Util;
namespace Repository
{
    class AssemBillRepositoryImp : AbstractRepository<DB, AssemBill>, IAssemBillRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<VM_AssemBigBillForTableShow> GetAssemBigBillsWithPaging(VM_AssemBigBillForSearch bill, Paging page)
        {
            //IQueryable<MarketOrder> marketOrderList = base.GetAvailableList<MarketOrder>().FilterBySearch(bill);
            // IQueryable<AssemBillDetail> asmBillDetailList = base.GetAvailableList<AssemBillDetail>();
            // IQueryable<Process> processList = base.GetAvailableList<Process>();
            //IQueryable<ProcessDetail> proDetailList = base.GetAvailableList<ProcessDetail>();
            //IQueryable<AssemblyDispatchDetail> asmDetailDistpachList = base.GetAvailableList<AssemblyDispatchDetail>();
            IQueryable<MasterDefiInfo> masterList = base.GetAvailableList<MasterDefiInfo>();
            IQueryable<AssemBill> asmBill = base.GetAvailableList<AssemBill>().FilterBySearch(bill);
            IQueryable<AssemblyDispatch> asmDistpachList = base.GetAvailableList<AssemblyDispatch>().FilterBySearch(bill);
            IQueryable<ProdInfo> prodInfoList = base.GetList<ProdInfo>().FilterBySearch(bill);
            var query = from ab in asmBill
                        join m in masterList on new { c = ab.EndFlag, cd = "00048" } equals new { c = m.AttrCd, cd = m.SectionCd } into _m
                        from _rm in _m.DefaultIfEmpty()
                        join ad in asmDistpachList on ab.AssemBillID equals ad.AssemblyTicketID into _ad
                        from _cc in _ad
                        group _cc by new { ab.AssemBillID, _rm.AttrValue, ab.ProductID} into _acc
                        select new
                        {
                            count=_acc.Sum(n=>n.ActualAssemblyNum),
                            _acc.Key.AssemBillID,
                            _acc.Key.AttrValue,
                            _acc.Key.ProductID
                        };
            IQueryable<VM_AssemBigBillForTableShow> _relation = from ab in query
                        join pd in prodInfoList on ab.ProductID equals pd.ProductId into _pd
                        from _rpd in _pd
                        //group _rpd by new { pn = _rpd.ProdName, pa = _rpd.ProdAbbrev, abid =ab.AssemBillID, abflag = ab.AttrValue} into _bcc 
                        select new VM_AssemBigBillForTableShow
                        {
                           /* LoadCount = _rpd.Sum(n ),
                            AssemBillID = _bcc.Key.abid,
                            ProductName = _bcc.Key.pn,
                            ProductType = _bcc.Key.pa,
                            IsOver = _bcc.Key.abflag,*/
                            LoadCount = (int)ab.count,
                            AssemBillID=ab.AssemBillID,
                            ProductName =_rpd.ProdName,
                            ProductType = _rpd.ProdAbbrev,
                            IsOver = ab.AttrValue
                        };

            page.total = _relation.Count();
            IEnumerable<VM_AssemBigBillForTableShow> result = _relation.ToPageList<VM_AssemBigBillForTableShow>("AssemBillID asc", page);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public VM_AssemBigBillForDetailShow GetAssemBigBill(AssemBill bill)
        {
            IQueryable<AssemBill> asmBill = base.GetAvailableList<AssemBill>();
            IQueryable<AssemblyDispatch> asmDistpachList = base.GetAvailableList<AssemblyDispatch>();
            IQueryable<ProdInfo> prodInfoList = base.GetAvailableList<ProdInfo>();
            /* IQueryable<AssemBill> asbList = list.AsQueryable<AssemBill>();
             IQueryable<AssemBillDetail> asbdList = assemDetail.AsQueryable<AssemBillDetail>();
             IQueryable<AssemblyDispatch> asbsList = schebill.AsQueryable<AssemblyDispatch>();*/
            IQueryable<MasterDefiInfo> masterList=base.GetAvailableList<MasterDefiInfo>();
            IQueryable<VM_AssemBigBillForDetailShow> obj = from a in asmBill
                                                           join m in masterList on new {acd=a.CheckResult,scd="00056"} equals new {acd=m.AttrCd,scd=m.SectionCd} into _rs
                                                           from _rrs in _rs.DefaultIfEmpty()
                                                           join d in asmDistpachList on a.AssemBillID equals d.AssemblyTicketID
                                                           where a.AssemBillID == bill.AssemBillID
                                                           group a by new { a.CheckerID, a.TeamLeaderID, _rrs.AttrValue, a.Remark, a.AssemBillID, a.EndFlag, d.AssemblyDispatchID, a.ProductID, d.AssemblyPlanNum, d.ActualAssemblyNum } into _acc
                                                           from _a in _acc
                                                           join p in prodInfoList on _a.ProductID equals p.ProductId
                                                           group p by new { derid = _a.DispatcherID, ckid = _a.CheckerID, tldid = _a.TeamLeaderID, crt = _acc.Key.AttrValue, remark = _a.Remark, pa = p.ProdAbbrev, abid = _a.AssemBillID, abflag = _a.EndFlag, plan = _acc.Key.AssemblyPlanNum, real = _acc.Key.ActualAssemblyNum } into _bcc
                                                           select new VM_AssemBigBillForDetailShow
                                                           {
                                                               AssemBillID = _bcc.Key.abid,
                                                               PlanCount = _bcc.Sum(n => _bcc.Key.plan),
                                                               LoadCount = _bcc.Sum(n => _bcc.Key.real),
                                                               CheckerID = _bcc.Key.ckid,
                                                               DispatcherID = _bcc.Key.derid,
                                                               TeamLeaderID = _bcc.Key.tldid,
                                                               ProductCheckResult = _bcc.Key.crt,
                                                               Remark = _bcc.Key.remark,
                                                               ProductType = _bcc.Key.pa,
                                                               IsOver=_bcc.Key.abflag
                                                           };
            return obj.First<VM_AssemBigBillForDetailShow>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        public IEnumerable<VM_AssemBigBillPartForDetailShow> GetAssemBigBillDetails(AssemBill bill)
        {
            IQueryable<AssemBill> asbList = base.GetAvailableList<AssemBill>();
            IQueryable<AssemBillDetail> asbdList = base.GetAvailableList<AssemBillDetail>();
            IQueryable<Process> pcsList = base.GetAvailableList<Process>();
            IQueryable<ProcessDetail> pcsdList = base.GetAvailableList<ProcessDetail>();

            var objList = from a in asbList join d in asbdList on a.AssemBillID equals d.AssemBillID where a.AssemBillID == bill.AssemBillID select new { a.AssemProcessID, d.ProcessOrderNO, d.ProjectNO, d.OperatorID, d.OperateDate, d.OperatorRealCount };
            IQueryable<VM_AssemBigBillPartForDetailShow> obj2 = from a in objList
                                                                from p in pcsdList
                                                                where a.AssemProcessID == p.ProcessId && a.ProcessOrderNO == p.SeqNo
                                                                select new VM_AssemBigBillPartForDetailShow
                                                                {
                                                                    ProcessName = p.SubProcName,
                                                                    QuotNo = p.QuotNum,
                                                                    ProjectNO = a.ProjectNO,
                                                                    RealOperateCount = a.OperatorRealCount,
                                                                    Operator = a.OperatorID,
                                                                    OperateDate = a.OperateDate,
                                                                    ProcessOrderNO = a.ProcessOrderNO
                                                                };

            return obj2.OrderBy(n => n.ProcessOrderNO);
        }

        /// <summary>
        ///  IAssemBillRepository 方法
        ///  代东泽 20131205
        /// </summary>
        /// <param name="assemBill"></param>
        /// <returns></returns>
        public IEnumerable<VM_AssemblyDispatch> GetCustomOrdersByAssemBigBill(AssemBill assemBill)
        {
            
            IQueryable<AssemBill> asmBill = base.GetAvailableList<AssemBill>().Where(n=>n.AssemBillID.Equals(assemBill.AssemBillID));
            IQueryable<AssemblyDispatch> asmDistpachList = base.GetAvailableList<AssemblyDispatch>();
            IQueryable<MarketOrder> marketOrderList = base.GetAvailableList<MarketOrder>();
            IQueryable<VM_AssemblyDispatch> _relation = from ab in asmBill
                                                                join ad in asmDistpachList on ab.AssemBillID equals ad.AssemblyTicketID  into _acc
                                                                from _a in _acc
                                                                join mo in marketOrderList on _a.CustomerOrderNum equals mo.ClientOrderID into _bcc
                                                                 select new VM_AssemblyDispatch
                                                                {
                                                                    CustomerOrderNum=_a.CustomerOrderNum,
                                                                    CustomerOrderDetails=_a.CustomerOrderDetails,
                                                                    ActualAssemblyNum=_a.ActualAssemblyNum,
                                                                    AssemblyPlanNum=_a.AssemblyPlanNum,
                                                                    AssemblyDispatchID=_a.AssemblyDispatchID
                                                                };
            return _relation.AsEnumerable();
        }

       

        public bool AddAssemBillDetails()
        {
            ProduceMaterDetail pd = new ProduceMaterDetail();
            pd.MaterReqNo = "123456";

            return base.UpdateNotNullColumn<ProduceMaterDetail>(pd);
        }

        public bool AddAssemBigBillDetail(AssemBillDetail entity)
        {
            return Add<AssemBillDetail>(entity);
        }

        public IEnumerable<AssemBillDetail> GetAssemBillDetail(string assemblyTicketID)
        {
            return base.GetAvailableList<AssemBillDetail>().Where(t => t.AssemBillID.Equals(assemblyTicketID));
        }

        public bool DeleteAssemBill(String id, String userId)
        {
            return base.ExecuteStoreCommand("update PD_ASSEM_BILL set DEL_FLG={0},DEL_DT={1},DEL_USR_ID='{2}' where ASS_BILL_ID='{3}' ", Constant.GLOBAL_DELFLAG_OFF, DateTime.Now, userId, id);
        }

        public bool DeleteAssemBillDetail(String id, String userId)
        {
            return base.ExecuteStoreCommand("update PD_ASSEM_BILL_DETAIL set DEL_FLG={0},DEL_DT={1},DEL_USR_ID='{2}' where ASS_BILL_ID='{3}' ", Constant.GLOBAL_DELFLAG_OFF, DateTime.Now, userId, id);
        }


        public void UpdateAssemBillDetail(AssemBillDetail entity)
        {
            base.UpdateNotNullColumn(entity);
        }
    }
}
