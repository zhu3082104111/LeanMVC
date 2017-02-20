/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：InProcessingPlanRepositoryImp.cs
// 文件功能描述：
//  内部加工计划资源库实现类
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Model;
using Repository.database;

namespace Repository
{
    /// <summary>
    /// 内部加工计划资源库实现类
    /// </summary>
    public class InProcessingPlanRepositoryImp:AbstractRepository<DB,ProduceSchedu>,IInProcessingPlanRepository
    {
        public IEnumerable<VM_InProcessingPlanShow> GetPlanViewByPage(VM_InProcessingPlanSearch search, Paging paging)
        {
            IQueryable<MaterialDecompose> materialIQ = GetAvailableList<MaterialDecompose>();//物料表
            IQueryable<ProduceSchedu> produceScheduIQ = GetAvailableList<ProduceSchedu>();//生成排期计划表
            IQueryable<PartInfo> partInfoIQ = GetAvailableList<PartInfo>();//零件表
            IQueryable<ProdInfo> productInfoIQ = GetAvailableList<ProdInfo>();//成品信息表
            //IQueryable<ProcessDetail> processDetailIQ = GetAvailableList<ProcessDetail>();//工序详细表
            
            produceScheduIQ = produceScheduIQ.Where(sch => sch.ProductType.Equals(Constant.ProduceType.SELFPROD));//筛选生产排期计划中自加工的记录
            materialIQ = materialIQ.FilterBySearch(search);//根据条件筛选物料分解信息
            materialIQ = materialIQ.Where(material => material.ProductsPartsID != material.ProductID && material.ProduceNeedQuantity > 0);//自生产：零件ID！=产品ID and 加工数量>0

            var query=from m in materialIQ 
                       join schedu in produceScheduIQ on new{cID=m.ClientOrderID,cDetail=m.ClientOrderDetail,pID=m.ProductsPartsID} equals 
                            new{cID=schedu.OrderID,cDetail=schedu.OrderDetail,pID=schedu.ExportId} into schedus
                       from schedu in schedus.DefaultIfEmpty()
                       join part in partInfoIQ on m.ProductsPartsID equals part.PartId into parts
                       from part in parts.DefaultIfEmpty()
                       join product in productInfoIQ on m.ProductID equals product.ProductId into products
                       from product in products.DefaultIfEmpty()
                       select new VM_InProcessingPlanShow()
                       {
                           OrderId = m.ClientOrderID,
                           ProductId = m.ProductID,
                           ProductAbbrev = product!=null? product.ProdAbbrev:null,
                           OrderDetail = m.ClientOrderDetail,
                           Specifica = m.Specifica,
                           PartName = part != null ? part.PartName : null,
                           DemondQuantity = m.ProduceNeedQuantity,
                           StartDt = m.StartDate,
                           ProvideDt = m.ProvideDate,
                           ScheduStartDt = schedu != null ? schedu.ScheduStartDt : Constant.CONST_FIELD.INIT_DATETIME,
                           RealEndDt = schedu != null ? schedu.ScheduEndDt : Constant.CONST_FIELD.INIT_DATETIME,
                           Rate = schedu != null ? (schedu.PlanTotalQuanlity>0?(schedu.ActualTotalQuanlity / schedu.PlanTotalQuanlity):0) : 0,
                           PlanTotalTime = schedu!=null?schedu.EndProduceTime:1,
                           RealQuanlity = schedu!=null?schedu.ActualTotalQuanlity:0,
                           State = m.ProduceProductionQuantity==0?Constant.ProcessingPlanState.NOTSTART:
                            (m.ProduceProductionQuantity>0 &&m.ProduceProductionQuantity<m.ProduceNeedQuantity?Constant.ProcessingPlanState.PRODUCING:
                            (m.ProduceProductionQuantity>=m.ProduceNeedQuantity?Constant.ProcessingPlanState.FINISHED:"null"))
                       };
            
            List<VM_InProcessingPlanShow> list = query.ToList();
            List<CompCalInfo> calendarList =GetAvailableList<CompCalInfo>().Where(c => c.ReatFlg == "0").ToList();//获取所有的工作日

            for (int i = 0, len = list.Count(); i < len; i++)
            {
                VM_InProcessingPlanShow plan = list[i];
                DateTime endDt = plan.RealEndDt != Constant.CONST_FIELD.INIT_DATETIME ? (DateTime)plan.RealEndDt : DateTime.Now;//完工日
                
                if (endDt > plan.ProvideDt)//完成日 > 提供日
                {
                    plan.DelayState = Constant.ProcessingPlanState.FAULT;//延误
                    continue;
                }
                if (plan.ScheduStartDt != Constant.CONST_FIELD.INIT_DATETIME)
                {
                    decimal workDays = 0;//= GetWorkDayCount((DateTime) plan.ScheduStartDt, endDt);
                    workDays = calendarList.Where(cal =>
                    {
                        var dt = Convert.ToDateTime(cal.Year + "-" + cal.Month + "-" + cal.Day);
                        return dt >= plan.ScheduStartDt && dt <= endDt;
                    }).Count();
                    decimal planQuanlity = workDays*plan.DemondQuantity/plan.PlanTotalTime;
                    if (planQuanlity > plan.RealQuanlity)//实绩值<计划值
                    {
                        plan.DelayState = Constant.ProcessingPlanState.DELAY;//延迟
                    }
                }
            }

            query = list.AsQueryable();
            if (!String.IsNullOrEmpty(search.State))//进度状态
            {
                query = query.Where(m => m.State.Equals(search.State));
            }

            paging.total = query.Count();
            return query.AsQueryable().ToPageList("OrderId", paging);
        }

        public IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlanViewByPage(VM_InProcessingMiddlePlanSearch search, Paging paging)
        {
            IQueryable<MaterialDecompose> materialIQ = GetAvailableList<MaterialDecompose>();//物料信息表
            IQueryable<PartInfo> partInfoIQ = GetAvailableList<PartInfo>();//零件信息表
            IQueryable<ProduceSchedu> scheduIQ = GetAvailableList<ProduceSchedu>();//排产计划表
            IQueryable<CustomTranslateInfo> customTranslateInfoIQ = GetAvailableList<CustomTranslateInfo>();//客户订单加工流转卡关系表
            IQueryable<ProcessTranslateCard> translateCardIQ = GetAvailableList<ProcessTranslateCard>();//加工流转卡表
            IQueryable<Reserve> reserveIQ = GetAvailableList<Reserve>();//仓库预约表
            IQueryable<GiReserve> giReserveIQ = GetAvailableList<GiReserve>();//让步仓库预约表
            IQueryable<ProdComposition> prodCompositionIQ = GetAvailableList<ProdComposition>();//成品构成表
            IQueryable<ProdInfo> productInfoIQ = GetAvailableList<ProdInfo>();//成品信息表

            materialIQ =materialIQ.Where(material => material.ProductsPartsID != material.ProductID && material.ProduceNeedQuantity > 0);//自生产：零件ID！=产品ID and 加工数量>0
            materialIQ = materialIQ.FilterBySearch(search);//数据筛选
            scheduIQ = scheduIQ.Where(sch => sch.ProductType.Equals(Constant.ProduceType.SELFPROD));//筛选生产排期的自生产记录

            var query=from material in materialIQ
                      join schedu in scheduIQ on new { orderId = material.ClientOrderID, orderDetail = material.ClientOrderDetail, partId = material.ProductsPartsID } 
                        equals new { orderId = schedu.OrderID, orderDetail = schedu.OrderDetail, partId = schedu.ExportId } into schedus//生产排期表
                      from schedu in schedus.DefaultIfEmpty()
                      join productInfo in productInfoIQ on material.ProductID equals productInfo.ProductId into productInfos//成品表，成品略称
                      join part in partInfoIQ on material.ProductsPartsID equals part.PartId into parts//零件
                      from part in parts.DefaultIfEmpty()
                      join translate in (from translate in translateCardIQ //流转卡信息,此部门主要获取加工数量
                                             join cusT in customTranslateInfoIQ on translate.ProcDelivID equals cusT.ProcDelivID
                                         select new { cardId = translate.ProcDelivID,EndFlag=translate.EndFlag, cus = new { orderId = cusT.CustomerOrderNum, 
                                             orderDetail = cusT.CustomerOrderDetails, exportId = translate.ExportID, planProcQty = cusT.PlnQty, warehQty =cusT.WarehQty} })
                        on new {orderId=material.ClientOrderID,orderDetail=material.ClientOrderDetail,exportId=material.ProductsPartsID} 
                        equals new{orderId=translate.cus.orderId,orderDetail=translate.cus.orderDetail,exportId=translate.cus.exportId} into translates
                      let reserveQ=reserveIQ.Where(r=>r.ClnOdrID==material.ClientOrderID && r.ClnOdrDtl==material.ClientOrderDetail)//筛选仓库预约表
                      let giReserveQ=giReserveIQ.Where(gr=>gr.PrhaOrderID==material.ClientOrderID && gr.ClientOrderDetail==material.ClientOrderDetail)//筛选让步仓库预约表
                      let materialQty=(from prodComp in prodCompositionIQ //成品构成表,此部分获取原料数量
                                      where prodComp.ParItemId==material.ProductsPartsID
                                      join reserve in reserveQ on prodComp.SubItemId equals reserve.PdtID into reserves//仓库预约表
                                      join giReserve in giReserveQ on prodComp.SubItemId equals giReserve.ProductID into giReserves//让步仓库预约表
                                       select ((reserves.Select(r => r.RecvQty).DefaultIfEmpty(0).Sum() - reserves.Select(r => r.PickOrdeQty).DefaultIfEmpty(0).Sum()
                                        + giReserves.Select(gr => gr.OrderQuantity).DefaultIfEmpty(0).Sum()
                                        - giReserves.Select(gr => gr.PickOrderQuantity).DefaultIfEmpty(0).Sum()) / prodComp.ConstQty)).DefaultIfEmpty(0).Min()
                      select new VM_InProcessingMiddlePlanShow()
                      {
                          OrderId = material.ClientOrderID,
                          ProductId = material.ProductID,
                          ProductAbbrev = productInfos.FirstOrDefault().ProdAbbrev,
                          OrderDetail = material.ClientOrderDetail,
                          PartName = part != null ? part.PartName : null,//零件名称
                          ExportId = material.ProductsPartsID,
                          ProcessId = material.ProcessID,
                          Specifica = material.Specifica,
                          ScheduQuanlity = material.ProduceNeedQuantity,
                          ProduceQuanlity = material.ProduceNeedQuantity-
                            translates.Select(t=>t.EndFlag.Equals(Constant.TranslateCardState.NOTOVER)?t.cus.planProcQty:t.cus.warehQty).DefaultIfEmpty(0).Sum(),
                          MaterialQuanlity = materialQty,
                          StartDt = material.StartDate,
                          ProvideDate = material.ProvideDate,
                          ScheduStartDt = schedu != null ? schedu.ScheduStartDt : material.StartDate,//生产排期表，开始日期
                          ScheduEndDt = schedu != null ? schedu.ScheduEndDt : material.ProvideDate,//生产排期表，完成日
                          IsPlaned = schedu!=null?true:false,
                          IsTranslateCarded = translates.Any()?true:false,
                          IsTranslateAllEnd = translates.All(trans=>trans.EndFlag.Equals(Constant.TranslateCardState.OVER))
                      }; 
            
            if (!String.IsNullOrEmpty(search.ScheduState))//排产状态(已排产,未排产,已投料,已完成)
            {
                switch (search.ScheduState)
                {
                    case Constant.ProcessingMiddlePlanState.HAVESCHEDUED://已排产:排期表中有的记录 and 无流转卡 and 原料数量==0
                        query = query.Where(q => q.IsPlaned && !q.IsTranslateCarded && q.MaterialQuanlity==0);
                        break;
                    case Constant.ProcessingMiddlePlanState.NOTSCHEDU://未排产:排期表中不存在的记录
                        query = query.Where(q => !q.IsPlaned);
                        break;
                    case Constant.ProcessingMiddlePlanState.HAVEPRODUCE://已投料:排期表中已有的记录 and 已有流转卡的记录 
                        query = query.Where(q => q.IsPlaned && q.IsTranslateCarded);
                        break;
                    case Constant.ProcessingMiddlePlanState.HAVEFINISHED://已完成:加工数量为0 and 所有流转卡已完结
                        query = query.Where(q => q.ProduceQuanlity <= 0 && q.IsTranslateAllEnd);
                        break;
                    default:break;     
                }
            }

            paging.total = query.Count();
            return query.AsQueryable().ToPageList("OrderId asc", paging);//分页
        }

        public IEnumerable<int> GetProcessSequence(string processId)
        {
            IQueryable<ProcessDetail> processDetailIQ = GetAvailableList<ProcessDetail>();//工序详细表
            var query = processDetailIQ.Where(pd => pd.ProcessId.Equals(processId)).Select(pd=>pd.SeqNo);
            return query.AsEnumerable();
        }

        public bool AddProcessTranslateCard(ProcessTranslateCard translateCard, List<CustomTranslateInfo> customTranslateInfoList, List<ProcessTranslateDetail> translateDetailList)
        {
            base.Add(translateCard);//流转卡表插入流转卡信息
            foreach (CustomTranslateInfo customTranslateInfo in customTranslateInfoList)
            {
                base.Add(customTranslateInfo);//插入 客户订单流转卡关系
            }
            foreach (ProcessTranslateDetail processTranslateDetail in translateDetailList)
            {
                base.Add(processTranslateDetail);//插入 加工流转卡详细
            }
            return true;
        }

        /// <summary>
        /// 获取时间段内的有效工作日数,      有误，数据不正确
        /// </summary>
        /// <param name="startDt">开始日期</param>
        /// <param name="endDt">结束日期</param>
        /// <returns></returns>
        private decimal GetWorkDayCount(DateTime startDt, DateTime endDt)
        {
            string yearBegin = startDt.Year + "";
            string monthBegin = startDt.Month < 10 ? ("0" + startDt.Month) : (startDt.Month + "");
            string dayBegin = startDt.Day < 10 ? ("0" + startDt.Day) : (startDt.Day + "");
            string yearEnd = endDt.Year + "";
            string monthEnd = endDt.Month < 10 ? ("0" + endDt.Month) : (endDt.Month + "");
            string dayEnd = endDt.Day < 10 ? ("0" + endDt.Day) : (endDt.Day + "");
            IQueryable<CompCalInfo> query = base.GetAvailableList<CompCalInfo>();
            query = query.Where(cal => cal.ReatFlg.Equals(Constant.CalendarRetFlg.WORK));
            query = query.Where(cal => cal.Year.CompareTo(yearBegin) >= 0 && cal.Month.CompareTo(monthBegin) >= 0 &&
                    cal.Day.CompareTo(dayBegin) >= 0 && cal.Year.CompareTo(yearEnd) <= 0 && cal.Month.CompareTo(monthEnd) <= 0 && cal.Day.CompareTo(dayEnd) <= 0);
            return query.Count();


        }



        public string GetProcessIdByExport(string exportId)
        {
            IQueryable<ProdComposition> prodCompositionIQ = GetAvailableList<ProdComposition>();//成品构成表
            string processId = prodCompositionIQ.Where(pc => pc.SubItemId.Equals(exportId)).Select(pc => pc.SubProcId)
                    .DefaultIfEmpty("").First();
            return processId;
        }
    }

}
