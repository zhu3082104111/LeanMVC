/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemblyRepositoryImp.cs
// 文件功能描述：
//  总装计划资源库实现类
// 
// 创建标识：2013/12/18  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Model;
using Repository.database;
using Extensions;

namespace Repository
{
    /// <summary>
    /// 总装计划实现
    /// </summary>
    public class AssemblyPlanRepositoryImp:AbstractRepository<DB,AssemblyDispatch>,IAssemblyPalnRepository
    {
        /// <summary>
        /// 获取总装计划一览数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable<VM_AssemblePlanShow> GetAssemblePlan(VM_AssemblePlanSearch search, Paging paging)
        {
            IQueryable<MaterialDecompose> materialIQ = GetAvailableList<MaterialDecompose>();//物料表
            IQueryable<ProduceSchedu> produceScheduIQ = GetAvailableList<ProduceSchedu>();//生成排期计划表
            //IQueryable<PartInfo> partInfoIQ = GetAvailableList<PartInfo>();//零件表
            IQueryable<ProduceRealDetail> produceRealDetailIQ = GetAvailableList<ProduceRealDetail>();//生产排期实绩详细表
            IQueryable<ProdInfo> productInfoIQ = GetAvailableList<ProdInfo>();//成品信息表
            IQueryable<ProcessDetail> processDetailIQ = GetAvailableList<ProcessDetail>();//工序详细表
            string notStart = Constant.ProcessingPlanState.NOTSTART;//进度-未开始
            string producing = Constant.ProcessingPlanState.PRODUCING;//进度-生产中
            string finished = Constant.ProcessingPlanState.FINISHED;//进度-已完成

            produceScheduIQ = produceScheduIQ.Where(sch => sch.ProductType.Equals(Constant.ProduceType.ASSEM));//筛选生产排期计划中自加工的记录
            materialIQ = materialIQ.Where(material => material.ProductsPartsID.Equals(material.ProductID));//总装：筛选零件ID == 产品ID
            materialIQ = materialIQ.FilterBySearch(search);//根据条件筛选物料分解信息

            var query = from m in materialIQ
                        join realDetail in produceRealDetailIQ on new { cID = m.ClientOrderID, cDetail = m.ClientOrderDetail, pID = m.ProductsPartsID } equals
                             new { cID = realDetail.ClientOrderID, cDetail = realDetail.ClientOrderDetails, pID = realDetail.ExportID } into realDetails
                        from realDetail in realDetails.DefaultIfEmpty()
                        join schedu in produceScheduIQ on new { cID = m.ClientOrderID, cDetail = m.ClientOrderDetail, pID = m.ProductsPartsID } equals
                             new { cID = schedu.OrderID, cDetail = schedu.OrderDetail, pID = schedu.ExportId } into schedus
                        from schedu in schedus.DefaultIfEmpty()
                        //join part in partInfoIQ on m.ProductsPartsID equals part.PartId into parts
                        //from part in parts.DefaultIfEmpty()
                        join product in productInfoIQ on m.ProductID equals product.ProductId into products
                        from product in products.DefaultIfEmpty()
                        join processDetail in processDetailIQ on m.ProcessID equals processDetail.ProcessId into processDetails
                        select new VM_AssemblePlanShow()
                        {
                            OrderId = m.ClientOrderID,
                            ProductId = m.ProductID,
                            ProductAbbrev = product != null ? product.ProdAbbrev : null,
                            OrderDetail = m.ClientOrderDetail,
                            Specifica = m.Specifica,
                            //PartName = part != null ? part.PartName : null,
                            DemondQuantity = m.ProduceNeedQuantity,
                            StartDt = m.StartDate,
                            ProvideDt = m.ProvideDate,
                            ScheduStartDt = schedu != null ? schedu.ScheduStartDt : Constant.CONST_FIELD.INIT_DATETIME,
                            RealEndDt = schedu != null ? schedu.ScheduEndDt : Constant.CONST_FIELD.INIT_DATETIME,
                            Rate = schedu != null ? (schedu.PlanTotalQuanlity > 0 ? (schedu.ActualTotalQuanlity / schedu.PlanTotalQuanlity) : 0) : 0,
                            PlanTotalTime = schedu != null ? schedu.EndProduceTime : 1,
                            RealQuanlity = schedu != null ? schedu.ActualTotalQuanlity : 0,
                            State=m.ProduceProductionQuantity==0?notStart:
                              (m.ProduceProductionQuantity>0&&m.ProduceProductionQuantity<m.ProduceNeedQuantity?producing:
                              (m.ProduceProductionQuantity>=m.ProduceNeedQuantity?finished:"null"))
                        };

            List<VM_AssemblePlanShow> list = query.ToList();
            List<CompCalInfo> calendarList = GetAvailableList<CompCalInfo>().Where(c => c.ReatFlg == "0").ToList();//获取所有的工作日
            for (int i = 0, len = list.Count(); i < len; i++)
            {
                VM_AssemblePlanShow plan = list[i];
                DateTime endDt = plan.RealEndDt != Constant.CONST_FIELD.INIT_DATETIME ? (DateTime)plan.RealEndDt : DateTime.Now;//完工日
                
                if (endDt > plan.ProvideDt)//完成日 > 提供日
                {
                    plan.DelayState = Constant.ProcessingPlanState.FAULT;//延误
                    continue;
                }
                if (plan.ScheduStartDt != Constant.CONST_FIELD.INIT_DATETIME)
                {
                    //decimal workDays = (from cal in calendarList
                    //                    let dt = DateTime.Parse(cal.Year + "-" + cal.Month + "-" + cal.Day)
                    //                    where dt >= plan.ScheduStartDt && dt <= endDt
                    //                    select cal).Count();
                    decimal workDays = calendarList.Count(cal =>
                    {
                        var dt = DateTime.Parse(cal.Year + "-" + cal.Month + "-" + cal.Day);
                        return dt >= plan.ScheduStartDt && dt <= endDt;
                    });
                    decimal planQuanlity = workDays * plan.DemondQuantity / plan.PlanTotalTime;
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

        /// <summary>
        /// 获取总装中计划数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable<VM_AssembleMiddlePlanShow> GetAssembleMiddlePlan(VM_AssembleMiddlePlanSearch search, Paging paging)
        {
            IQueryable<MaterialDecompose> materialIQ = GetAvailableList<MaterialDecompose>();//物料信息表
            //IQueryable<PartInfo> partInfoIQ = GetAvailableList<PartInfo>();//零件信息表
            IQueryable<ProduceSchedu> scheduIQ = GetAvailableList<ProduceSchedu>();//排产计划表
            IQueryable<AssemblyDispatch> assemblyDispatchIQ = GetAvailableList<AssemblyDispatch>();//总装调度单表
            IQueryable<AssemBill> assemBillIQ = GetAvailableList<AssemBill>();//总装工票表
            IQueryable<ProdComposition> prodCompositionIQ = GetAvailableList<ProdComposition>();//成品构成信息表
            IQueryable<ProdInfo> productInfoIQ = GetAvailableList<ProdInfo>();//成品信息表

            materialIQ = materialIQ.Where(material => material.ProductsPartsID.Equals(material.ProductID));//总装：筛选零件ID == 产品ID
            materialIQ = materialIQ.FilterBySearch(search);//数据筛选
            scheduIQ = scheduIQ.Where(sch => sch.ProductType.Equals(Constant.ProduceType.ASSEM));//筛选生产排期的总装记录

            var query = from material in materialIQ
                        join schedu in scheduIQ on new { orderId = material.ClientOrderID, orderDetail = material.ClientOrderDetail, partId = material.ProductsPartsID }
                          equals new { orderId = schedu.OrderID, orderDetail = schedu.OrderDetail, partId = schedu.ExportId } into schedus//生产排期表
                        from schedu in schedus.DefaultIfEmpty()
                        join productInfo in productInfoIQ on material.ProductID equals productInfo.ProductId into productInfos
                        //join part in partInfoIQ on material.ProductsPartsID equals part.PartId into parts//零件
                        //from part in parts.DefaultIfEmpty()
                        join prodComp in prodCompositionIQ on new{pId=material.ProductID,sId=material.ProductID} //成品构成表，获取对应子工序ID
                            equals new {pId=prodComp.ParItemId,sId=prodComp.SubItemId} into prodComps
                        join assTemp in (from ass in assemblyDispatchIQ 
                                                      join assemBill in assemBillIQ on ass.AssemblyTicketID equals assemBill.AssemBillID
                                                      select new{ass,assemBill})
                            on new { orderId = material.ClientOrderID, orderDetail = material.ClientOrderDetail,exportId=material.ProductsPartsID } //总装调度单
                            equals new {orderId=assTemp.ass.CustomerOrderNum,orderDetail=assTemp.ass.CustomerOrderDetails,exportId=assTemp.ass.ProductID} into assTemps
                        select new VM_AssembleMiddlePlanShow()
                        {
                            OrderId = material.ClientOrderID,
                            ProductId = material.ProductID,
                            ProductAbbrev = productInfos.FirstOrDefault().ProdAbbrev,
                            OrderDetail = material.ClientOrderDetail,
                            //PartName = part != null ? part.PartName : null,//零件名称
                            ExportId = material.ProductsPartsID,
                            ProcessId = prodComps.Select(pc=>pc.SubProcId).DefaultIfEmpty("").FirstOrDefault(),
                            //ProcessId = material.ProcessID,
                            Specifica = material.Specifica,
                            ScheduQuanlity = material.ProduceNeedQuantity,
                            AssembleQuantity = material.ProduceNeedQuantity-
                                assTemps.Select(temp=>temp.assemBill.EndFlag.Equals(Constant.TranslateCardState.OVER)?temp.ass.ActualAssemblyNum:temp.ass.AssemblyPlanNum)
                                .DefaultIfEmpty(0).Sum(),
                            StartDt = material.StartDate,
                            ProvideDt = material.ProvideDate,
                            ScheduStartDt = schedu != null ? schedu.ScheduStartDt : material.StartDate,//生产排期表，开始日期
                            ScheduEndDt = schedu != null ? schedu.ScheduEndDt : material.ProvideDate,//生产排期表，完成日
                            DispatchIds = assTemps.Select(temp=>temp.ass.AssemblyDispatchID),
                            IsPlaned = schedu != null ? true : false
                        };
            paging.total = query.Count();
            return query.ToPageList("OrderId asc", paging);//分页
        }

        /// <summary>
        /// 获取原料数量
        /// </summary>
        /// <param name="search">匹配条件</param>
        /// <returns></returns>
        public IEnumerable<VM_MaterialQuanlityShow> GetMaterialQuanlity(VM_MaterialQuanlitySearch search)
        {
            IQueryable<ProdComposition> productCompositionIQ = GetAvailableList<ProdComposition>();//成品构成表
            IQueryable<PartInfo> partInfoIQ = GetAvailableList<PartInfo>();//零部件表
            IQueryable<GiReserve> giReserveIQ = GetAvailableList<GiReserve>();//让步仓库预约表
            IQueryable<Reserve> reserveIQ = GetAvailableList<Reserve>();//仓库预约表

            productCompositionIQ = productCompositionIQ.Where(prodComp => prodComp.ParItemId.Equals(search.ExportId));//筛选某一零件的子零件

            var query = from prodComp in productCompositionIQ //成品构成表
                join reserve in reserveIQ on //仓库预约表,获取实际在库数量
                    new {orderId = search.OrderId, orderDetail = search.OrderDetail, exportId = prodComp.SubItemId}
                    equals new {orderId = reserve.ClnOdrID, orderDetail = reserve.ClnOdrDtl, exportId = reserve.PdtID} into reserves
                join giReseve in giReserveIQ on 
                    new{orderId=search.OrderId,orderDetail=search.OrderDetail,exportId=prodComp.SubItemId}
                    equals new{orderId=giReseve.PrhaOrderID,orderDetail=giReseve.ClientOrderDetail,exportId=giReseve.ProductID} into giReserves
                let realInQty=(reserves.Select(r=>r.RecvQty).DefaultIfEmpty(0).Sum()-reserves.Select(r=>r.PickOrdeQty).DefaultIfEmpty(0).Sum() //获取在库数量
                    + giReserves.Select(gr=>gr.OrderQuantity).DefaultIfEmpty(0).Sum()-giReserves.Select(gr=>gr.PickOrderQuantity).DefaultIfEmpty(0).Sum())
                let realQtyMax=realInQty/prodComp.ConstQty
                join part in partInfoIQ on prodComp.SubItemId equals part.PartId into parts //零部件表
                select new VM_MaterialQuanlityShow()
                {
                    ExportId = prodComp.SubItemId,
                    ExportName = parts.Select(p=>p.PartName).DefaultIfEmpty("").FirstOrDefault(),
                    ExportAbbrev = parts.Select(p => p.PartAbbrevi).DefaultIfEmpty("").FirstOrDefault(),
                    RealInQuanlity = realInQty,
                    MaxQuanlity = (int)realQtyMax
                    //RealInQuanlity = reserves.Select(r => r.RecvQty).DefaultIfEmpty(0).Sum(),
                    //MaxQuanlity = (int)(reserves.Select(r => r.RecvQty).DefaultIfEmpty(0).Sum() / (prodComp.ConstQty > 0 ? prodComp.ConstQty : 1))
                };
            return query.ToList();
        } 
         
    }
}
