using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;
using Repository.database;
using Util;

namespace Repository
{
    /// <summary>
    ///此类目前返回的列表全部是有有效和未删除的
    ///C:梁龙飞
    /// </summary>
    class ProduceGeneralPlanRepositoryImp : AbstractRepository<DB, ProduceGeneralPlan>, IProduceGeneralPlanRepository
    {

        #region 基础方法 梁龙飞
        /// <summary>
        /// 根据一个计划的状态和其在分解表中有无信息来确定它的排产状态
        /// 注意：对于订单状态为已接收外的所有状态都返回排产完成的排产状态
        /// </summary>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        private string GenerateSchedulState(string orderState, string clientOrderID, string orderDetail)
        {
            if (orderState == Constant.GeneralPlanState.ACCEPTED)
            {
                IQueryable<MaterialDecompose> data = base.GetAvailableList<MaterialDecompose>().Where(a => a.ClientOrderID == clientOrderID && a.ClientOrderDetail == orderDetail);
                if (data != null && data.Count() > 0)
                {
                    return Constant.GeneralPlanState.SCHEDULING;
                }
                return Constant.GeneralPlanState.ACCEPTED;
            }
            else if (orderState==Constant.GeneralPlanState.SCHEDULING)
            { 
                 return Constant.GeneralPlanState.SCHEDULING;
            }
            return Constant.GeneralPlanState.SCHEDULED;
        }

        // 返回未删除并且有效的计划表
        private IQueryable<ProduceGeneralPlan> GetPlanAvailable()
        {
            return base.GetAvailableList<ProduceGeneralPlan>();
        }
        // 根据客户订单返回计划表IQueryable列表：未删除、有效
        public IQueryable<ProduceGeneralPlan> GetPlanByClientOrderID(string clientOrderID)
        {
            return GetPlanAvailable().Where(a => a.ClientOrderID == clientOrderID);
        }

        //  返回计划状态为目标数组的所有订单
        public IQueryable<ProduceGeneralPlan> GetPlanByStatus(string[] status)
        {
            return GetPlanAvailable().Where(a => status.Contains(a.Status));
        }
        // delete an Obj by SQL
        public bool DeletePlanBySQL(ProduceGeneralPlan target)
        {
            return base.ExecuteStoreCommand("delete from PD_GENERAL_PLAN  where Id={0}", target.ClientOrderID);
        }

        #endregion

        #region OrderAccept C:梁龙飞
        /// <summary>
        /// 订单接收搜索
        /// </summary>
        /// <param name="searchConditon"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        public IEnumerable<VM_OrderAcceptShow> GetPlanNotAccept(VM_OrderAcceptSearch searchConditon, Paging pagex)
        {
            //origin data:general plan
            string[] status = { Constant.GeneralPlanState.UN_ACC };
            IQueryable<ProduceGeneralPlan> planNotAcc = GetPlanByStatus(status);
            //filter:like %订单号%
            if (!string.IsNullOrEmpty(searchConditon.ClientOrderID))
            {
                planNotAcc = planNotAcc.Where(a => a.ClientOrderID.Contains(searchConditon.ClientOrderID));
            }

            //origin data:product information
            IQueryable<ProdInfo> productInfo = base.GetAvailableList<ProdInfo>();

            //like %产品型号%(产品型号=产品略称)
            if (!string.IsNullOrEmpty(searchConditon.ProductType))
            {
                //Get ClientOrderIDs by productAbbrev
                IEnumerable<string> list = from plan in planNotAcc
                                           join proInf in productInfo
                                           on plan.ProductID equals proInf.ProductId
                                           where proInf.ProdAbbrev.Contains(searchConditon.ProductType)
                                           select plan.ClientOrderID;
                planNotAcc = planNotAcc.Where(a => list.Contains(a.ClientOrderID));
            }

            //source:取出订单明细
            IQueryable<MarketOrderDetail> orderDetailAvailable = base.GetList<MarketOrderDetail>().Where(a => a.DelFlag == Constant.CONST_FIELD.UN_DELETED && a.EffeFlag == Constant.CONST_FIELD.EFFECTIVE);

            //时间段
            if (searchConditon.DeliveryDateBegin != Constant.CONST_FIELD.INIT_DATETIME)
            {
                orderDetailAvailable = orderDetailAvailable.Where(a => a.DeliveryDate == null || (DateTime)a.DeliveryDate >= searchConditon.DeliveryDateBegin);
            }
            if (searchConditon.DeliveryDateEnd != Constant.CONST_FIELD.INIT_DATETIME)
            {
                //确保界面回传的时间是000000
                DateTime timeFloor = searchConditon.DeliveryDateEnd.AddDays(1);
                orderDetailAvailable = orderDetailAvailable.Where(a => a.DeliveryDate == null || (DateTime)a.DeliveryDate < timeFloor);
            }

            //查询
            IQueryable<VM_OrderAcceptShow> vmPlan = from plan in planNotAcc
                                                    join detail in orderDetailAvailable
                                                    on new { plan.ClientOrderID, plan.ClientOrderDetail } equals new { detail.ClientOrderID, detail.ClientOrderDetail }
                                                    join prodInf in productInfo
                                                    on plan.ProductID equals prodInf.ProductId
                                                    select new VM_OrderAcceptShow
                                                    {
                                                        ClientOrderID = plan.ClientOrderID,
                                                        DeliveryDate = (DateTime)detail.DeliveryDate,//界面显示是strDeliveryDate
                                                        ClientOrderDetail = detail.ClientOrderDetail,
                                                        ProductID = detail.ProductID,//隐藏数据，界面并不接收
                                                        ProductType = prodInf.ProdAbbrev,//产品型号
                                                        Quantity = detail.Quantity,
                                                        PackageQuantity = detail.PackageQuantity,
                                                        PackageSize = detail.PackageSize
                                                    };
            pagex.total = vmPlan.Count();
            IEnumerable<VM_OrderAcceptShow> finalData = vmPlan.ToPageList<VM_OrderAcceptShow>("ClientOrderID asc", pagex);
            return finalData;
        }
        #endregion

        #region OrderScheduling C：梁龙飞
        /// <summary>
        /// 订单排产搜索结果
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        public IEnumerable<VM_OrderSchedulingShow> GetPlanAccepted(VM_OrderSchedulingSearch searchCondition, Paging pagex)
        {
            //(1)ProduceGeneralPlan X|X.status∉｛NotAccepted｝
            IQueryable<ProduceGeneralPlan> planSchedul = base.GetAvailableList<ProduceGeneralPlan>().Where(a => a.Status != Constant.GeneralPlanState.UN_ACC);
            //(2)Product Information
            IQueryable<ProdInfo> productInfo = base.GetAvailableList<ProdInfo>();
            //(3)Get available OrderDetail 
            IQueryable<MarketOrderDetail> orderDetailAvailable = base.GetAvailableList<MarketOrderDetail>();

            //Like "%ClientOrderID%"
            if (!string.IsNullOrEmpty(searchCondition.ClientOrderID))
            {
                planSchedul = planSchedul.Where(a => a.ClientOrderID.Contains(searchCondition.ClientOrderID));
            }


            //Like "%ProductType%"
            if (!string.IsNullOrEmpty(searchCondition.ProductType))
            {
                //Get ClientOrderIDs by productAbbrev
                IEnumerable<string> list = from plan in planSchedul
                                           join proInf in productInfo
                                           on plan.ProductID equals proInf.ProductId
                                           where proInf.ProdAbbrev.Contains(searchCondition.ProductType)
                                           select plan.ProductID;
                planSchedul = planSchedul.Where(a => list.Contains(a.ProductID));
            }

            //between Time
            if (searchCondition.DeliveryDateFrom != Constant.CONST_FIELD.INIT_DATETIME)
            {
                orderDetailAvailable = orderDetailAvailable.Where(a => a.DeliveryDate == null || (DateTime)a.DeliveryDate >= searchCondition.DeliveryDateFrom);
            }
            if (searchCondition.DeliveryDateTo != Constant.CONST_FIELD.INIT_DATETIME)
            {
                //The datetime from view must be formatted like this:yyyy/MM/dd 00:00:00
                DateTime floor = searchCondition.DeliveryDateTo.AddDays(1);
                orderDetailAvailable = orderDetailAvailable.Where(a => a.DeliveryDate == null || (DateTime)a.DeliveryDate < floor);
            }

            IQueryable<VM_OrderSchedulingShow> tempResultData = from plan in planSchedul
                                                                join detail in orderDetailAvailable
                                                                on new { plan.ClientOrderID, plan.ClientOrderDetail } equals new { detail.ClientOrderID, detail.ClientOrderDetail }
                                                                join prodInf in productInfo
                                                                on plan.ProductID equals prodInf.ProductId
                                                                select new VM_OrderSchedulingShow
                                                                {
                                                                    ClientOrderID = plan.ClientOrderID,
                                                                    ClientOrderDetail = plan.ClientOrderDetail,
                                                                    SalePersonName = "没有来源",//数据库中没有此字段的来源
                                                                    DeliveryDateOfPlan = detail.DeliveryDate,
                                                                    ProductType = prodInf.ProdAbbrev,
                                                                    ProductID = detail.ProductID,
                                                                    QuantityInPlan = detail.Quantity,
                                                                    SchedulingState = plan.Status
                                                                };
            //change to valuetype for changing the schedulstate
            List<VM_OrderSchedulingShow> tempList = tempResultData.ToList();
            //generate SchedulingState
            foreach (VM_OrderSchedulingShow item in tempList)
            {
                item.SchedulingState = GenerateSchedulState(item.SchedulingState, item.ClientOrderID, item.ClientOrderDetail);
            }

            //if SchedulingState != "ALL"
            if (searchCondition.SchedulingState != Constant.GeneralPlanState.ALL_STATE)
            {
                tempResultData = tempList.Where(a => a.SchedulingState == searchCondition.SchedulingState).AsQueryable<VM_OrderSchedulingShow>();
            }
            else
            {
                tempResultData = tempList.AsQueryable<VM_OrderSchedulingShow>();
            }
            pagex.total = tempResultData.Count();
            IEnumerable<VM_OrderSchedulingShow> finalData = tempResultData.ToPageList<VM_OrderSchedulingShow>("ClientOrderID asc", pagex);
            return finalData;

        }
        #endregion


        #region 波波

        /// <summary>
        /// 内部加工进度查询--现已经不在使用
        /// </summary>
        /// <param name="inProcessingRate"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public IEnumerable GetInProcessingRateSearch(VM_InProcessingRateSearch inProcessingRate, Paging paging)
        {
            IQueryable<MaterialDecompose> material = base.GetList<MaterialDecompose>();
            IQueryable<ProduceRealDetail> scheduleRealDetail = base.GetList<ProduceRealDetail>();
            IQueryable<MarketOrderDetail> orderDetail = base.GetList<MarketOrderDetail>();

            inProcessingRate.TxtClientOrderID = "B130010SBA";
            inProcessingRate.TxtProductID = "RAT2000";

            //还有条件未加上尚待补全
            if (!String.IsNullOrEmpty(inProcessingRate.TxtClientOrderID))
            {
                material = material.Where(t => t.ClientOrderID == inProcessingRate.TxtClientOrderID);
                orderDetail = orderDetail.Where(t => t.ClientOrderID == inProcessingRate.TxtClientOrderID);
            }
            if (!String.IsNullOrEmpty(inProcessingRate.TxtProductID))
            {
                material = material.Where(t => t.ProcessSequenceNo.ToString() == inProcessingRate.TxtProductID);
                orderDetail = orderDetail.Where(t => t.ProductID == inProcessingRate.TxtProductID);
            }

            //取得计划交期和计划数量
            IQueryable<VM_HeaderData> query = from mod in orderDetail
                                              select new VM_HeaderData
                                              {
                                                  ClientOrderID = mod.ClientOrderID,
                                                  PlanQuantity = mod.Quantity,
                                                  DeliveryDate = (DateTime)mod.DeliveryDate
                                              };
            IEnumerable<VM_HeaderData> dataList = query.ToList();

            Decimal planQuantity = 0;
            DateTime deliveryDate = Constant.CONST_FIELD.INIT_DATETIME;

            if (dataList.Any())
            {
                deliveryDate = dataList.ElementAt(0).DeliveryDate;
                planQuantity = dataList.ElementAt(0).PlanQuantity;
            }

            IQueryable<VM_InProcessingRateForTableShow> querys = from mat in material
                                                                 join sch in scheduleRealDetail on
                                                                     new
                                                                     {
                                                                         ClientOrderID = mat.ClientOrderID,
                                                                         ClientOrderDetails = mat.ClientOrderDetail,
                                                                         ExportID = mat.ProductsPartsID
                                                                     }
                                                                 equals new { sch.ClientOrderID, sch.ClientOrderDetails, sch.ExportID } into prodGroup
                                                                 select new VM_InProcessingRateForTableShow
                                                                 {
                                                                     //STP_Num = prodGroup.Sum(a => a.ActualProcessNum),
                                                                     ClientOrderID = mat.ClientOrderID,
                                                                     ProduceID = mat.ProcessSequenceNo.ToString(),
                                                                     PlanNumber = planQuantity,
                                                                     PlannedDelivery = deliveryDate,
                                                                     MaterialName = mat.ProductsPartsID,
                                                                     ProcessName = "",
                                                                     StartTime = mat.StartDate,
                                                                     EndTime = mat.ProvideDate,
                                                                     AssemblyNum = mat.DemondQuantity,
                                                                     ActualNum = prodGroup.Sum(a => a.ActualProcessNum),
                                                                     AchievementRate = (mat.DemondQuantity > 0) ? Decimal.Round(prodGroup.Sum(a => a.ActualProcessNum) / mat.DemondQuantity, 3) : 0
                                                                 };
            querys = querys.OrderBy(c => c.ClientOrderID).ThenBy(c => c.ProduceID).ThenBy(c => c.PlanNumber).ThenBy(c => c.PlannedDelivery);

            paging.total = querys.Count();
            IEnumerable<VM_InProcessingRateForTableShow> result = querys.ToPageList<VM_InProcessingRateForTableShow>(null, paging);
            return result;
        }

        public IEnumerable<VM_OrderRateForTableShow> GetOrderRateForSrarch(VM_OrderRateForSrarch OrderRate, Paging paging, VM_HeaderData headerData)
        {
            IQueryable<MarketOrderDetail> orderDetail = base.GetAvailableList<MarketOrderDetail>();//订单详细表
            IQueryable<MaterialDecompose> material = base.GetAvailableList<MaterialDecompose>();//物料分解表
            IQueryable<ProdInfo> prodinfs = GetAvailableList<ProdInfo>();//产品信息
            IQueryable<PartInfo> partifs = GetAvailableList<PartInfo>();//零件信息
            IQueryable<AssemblyDispatch> assemblyDispatches = base.GetAvailableList<AssemblyDispatch>();//总装调度单用于计算第一行

            if (!String.IsNullOrEmpty(OrderRate.TxtClientOrderID))
            {
                material = material.Where(t => t.ClientOrderID.Equals(OrderRate.TxtClientOrderID));
                orderDetail = orderDetail.Where(t => t.ClientOrderID.Equals(OrderRate.TxtClientOrderID));
                assemblyDispatches = assemblyDispatches.Where(t => t.CustomerOrderNum.Equals(OrderRate.TxtClientOrderID));
            }

            if (!String.IsNullOrEmpty(OrderRate.ClientOrderDetail))
            {
                material = material.Where(t => t.ClientOrderDetail.Equals(OrderRate.ClientOrderDetail));
                orderDetail = orderDetail.Where(t => t.ClientOrderDetail.Equals(OrderRate.ClientOrderDetail));
                assemblyDispatches = assemblyDispatches.Where(t => t.CustomerOrderDetails.Equals(OrderRate.ClientOrderDetail));
            }

            if (!String.IsNullOrEmpty(OrderRate.ProductID))
            {
                material = material.Where(t => t.ProductID.Equals(OrderRate.TxtProductID));
                orderDetail = orderDetail.Where(t => t.ProductID.Equals(OrderRate.TxtProductID));
                assemblyDispatches = assemblyDispatches.Where(t => t.ProductID.Equals(OrderRate.ProductID));
            }


            //取得计划交期和计划数量
            IQueryable<VM_HeaderData> query = from mod in orderDetail
                                              select new VM_HeaderData
                                              {
                                                  ClientOrderID = mod.ClientOrderID,
                                                  PlanQuantity = mod.Quantity,
                                                  DeliveryDate = (DateTime)mod.DeliveryDate
                                              };
            VM_HeaderData dataList = query.FirstOrDefault();
            //外协外购的进度由对方提供接口统计进度（参数：客户订单号，客户订单明细，零件ID）
            if (dataList != null)
            {
                headerData.ClientOrderID = dataList.ClientOrderID;
                headerData.DeliveryDate = dataList.DeliveryDate;
                headerData.PlanQuantity = dataList.PlanQuantity;
            }

            IEnumerable<VM_OrderRateForTableShow> querys = from mat in material.ToList().AsQueryable()
                                                           join prdi in prodinfs.ToList().AsQueryable() //产品名称
                                                           on mat.ProductID equals prdi.ProductId into gprdi
                                                           from prodinf in gprdi.DefaultIfEmpty()
                                                           join pati in partifs.ToList().AsQueryable()//left join 零件信息
                                                           on mat.ProductsPartsID equals pati.PartId into gpartifs
                                                           from partif in gpartifs.DefaultIfEmpty()

                                                           select new VM_OrderRateForTableShow
                                                                 {
                                                                     strID = mat.MatSequenceNo.ToString(),
                                                                     strParentID = mat.ProcessSequenceNo.ToString(),
                                                                     ProductID = mat.ProductID,
                                                                     ProductType = prodinf != null ? prodinf.ProdAbbrev : "",
                                                                     ProductsPartsID = mat.ProductsPartsID,
                                                                     MaterialName = partif != null ? partif.PartName : "",
                                                                     PlanNeedNumber = mat.DemondQuantity,
                                                                     TotalLockNumber = mat.NormalLockQuantity + mat.AbnormalLockQuantity,
                                                                     PurchaseQuantity = mat.ProduceNeedQuantity + mat.PurchNeedQuantity + mat.AssistNeedQuantity,
                                                                     StartDate = mat.StartDate,
                                                                     EndDate = mat.ProvideDate,
                                                                     //STP_Num = prodGroup.Sum(a => a.ActualProcessNum),
                                                                     STP_Num = mat.ProduceNeedQuantity,
                                                                     STP_AchieveRate = Decimal.Round((mat.ProduceNeedQuantity > 0 ? (mat.ProduceProductionQuantity / mat.ProduceNeedQuantity) : 0), 3),
                                                                     OutSource_Num = mat.PurchNeedQuantity,
                                                                     OutSource_AchieveRate = Decimal.Round(mat.PurchNeedQuantity > 0 ? (mat.PurchProductionQuantity / mat.PurchNeedQuantity) : 0, 3),
                                                                     Association_Num = mat.AssistNeedQuantity,
                                                                     Association_AchieveRate = Decimal.Round(mat.AssistNeedQuantity > 0 ? (mat.AssistProductionQuantity / mat.AssistNeedQuantity) : 0, 3),
                                                                     MaterialAchieveRate = 0,
                                                                     PreparationPeriod = mat.PreparationPeriod,
                                                                     ProduceProductionQuantity = mat.ProduceProductionQuantity,
                                                                     PurchProductionQuantity = mat.PurchProductionQuantity,
                                                                     AssistProductionQuantity = mat.AssistProductionQuantity
                                                                 };

            querys = querys.ToList().OrderBy(t => t.id).ThenBy(t => t._parentId);
            paging.total = querys.Count();

            //第一行的物料是产品本身,通过总装调度单计算其自加工比率
            if (querys.FirstOrDefault() != null)
            {
                
                var assDis =
                assemblyDispatches.FirstOrDefault();
                if (assDis != null)
                {
                    querys.First(a => a.MaterialName == "").STP_AchieveRate = Decimal.Round(assDis.AssemblyPlanNum > 0 ? assDis.ActualAssemblyNum /
                        assDis.AssemblyPlanNum : 0, 3);
                }

                querys.First(a => a.MaterialName == "").MaterialName = querys.First().ProductType;
                //计算自加工比率
            }
            IEnumerable<VM_OrderRateForTableShow> result = querys;
            return result;
        }

        public IEnumerable<VM_ProducePlanShow> GetProducePlanSearch(Paging paging, VM_ProducePlanForSearch useForSearch)
        {
            //生成计划总表数据
            IQueryable<ProduceGeneralPlan> produceGeneralPlans = base.GetAvailableList<ProduceGeneralPlan>();
            //订单详细表
            IQueryable<MarketOrderDetail> marketOrderDetails = base.GetAvailableList<MarketOrderDetail>();
            //订单表
            //取得最新版本的订单表中得数据
            //IQueryable<MarketOrder> marketOrders = from mo in base.GetAvailableList<MarketOrder>()
            //    group mo by mo.ClientOrderID
            //    into gmo
            //    select new MarketOrder
            //    {
            //        ClientOrderID = gmo.Key,
            //        EditUserID1 = (from mo2 in gmo where mo2.ClientVersion == gmo.Max(t => t.ClientVersion) select mo2.EditUserID1).FirstOrDefault()
            //    };

            //订单表
            //取得最新版本的订单表中得数据
            IQueryable<MarketOrder> marketOrders = base.GetAvailableList<MarketOrder>();
                                                    
            //成品信息表
            IQueryable<ProdInfo> prodInfos = base.GetAvailableList<ProdInfo>();

            //物料分解信息表
            IQueryable<MaterialDecompose> materials = base.GetAvailableList<MaterialDecompose>();

            //Master数据管理表
            IQueryable<MasterDefiInfo> masterDefiInfos = base.GetAvailableList<MasterDefiInfo>().Where(t => t.SectionCd.Equals(Constant.MasterSection.PRODUCE_PLAN_STATE));

            //取得销售员的名字
            IQueryable<UserInfo> userInfos = base.GetAvailableList<UserInfo>();

            IEnumerable<VM_ProducePlanShow> result =( from pgp in produceGeneralPlans
                                join mod in marketOrderDetails
                                on new { pgp.ClientOrderID, pgp.ClientOrderDetail, pgp.ProductID } equals new
                                {
                                    mod.ClientOrderID,
                                    mod.ClientOrderDetail,
                                    mod.ProductID
                                } into gmod
                                from martOrdDet in gmod.DefaultIfEmpty()

                                join mo in marketOrders on pgp.ClientOrderID equals mo.ClientOrderID into gmo
                                from marketOrder in gmo.DefaultIfEmpty()

                                join pi in prodInfos on pgp.ProductID equals pi.ProductId into  gpi
                                from prodInfo in gpi.DefaultIfEmpty()

                                join mat in materials on new { pgp.ClientOrderID, pgp.ClientOrderDetail, pgp.ProductID } equals new
                                {
                                    mat.ClientOrderID,
                                    mat.ClientOrderDetail,
                                    ProductID=mat.ProductsPartsID
                                } into gmat
                                from material in gmat.DefaultIfEmpty()

                                join mdi in masterDefiInfos on pgp.Status equals mdi.AttrCd into gmdi  //生产状态
                                from masDefInf in gmdi.DefaultIfEmpty()

                                join ui in userInfos on marketOrder.EditUserID1 equals ui.UId into gui
                                from userInfo in gui.DefaultIfEmpty()

                                select new VM_ProducePlanShow
                                {
                                    ClientOrderID=pgp.ClientOrderID,
                                    ClientOrderDetail = pgp.ClientOrderDetail,
                                    SaleID= marketOrder != null ? marketOrder.EditUserID1 : "",
                                    SaleName=userInfo!=null?userInfo.RealName:"",
                                    DeliveryDate= martOrdDet != null ? martOrdDet.DeliveryDate :null,
                                    ProdAbbrev = prodInfo != null ? prodInfo.ProdAbbrev : "",
                                    ProductID=pgp.ProductID,
                                    PlanQTY = martOrdDet != null ? martOrdDet.Quantity : 0,
                                    QTYCompletion = material != null ? (material.ProduceProductionQuantity + material.PurchProductionQuantity + material.AssistProductionQuantity) : 0,
                                    QTYUncompletion=0,
                                    AchievementRate=0,
                                    StateValue=pgp.Status,
                                    ProduceState = masDefInf.AttrValue
                                }).ToList().OrderBy(t => t.ClientOrderID);

            foreach (var vmProducePlanShow in result)
            {
                vmProducePlanShow.QTYUncompletion = vmProducePlanShow.PlanQTY - vmProducePlanShow.QTYCompletion;
                vmProducePlanShow.AchievementRate = vmProducePlanShow.PlanQTY > 0
                    ? Decimal.Round(vmProducePlanShow.QTYCompletion*100 / vmProducePlanShow.PlanQTY, 3)
                    : 0;
            }
            
            //按搜索条件进行过滤
            IEnumerable<VM_ProducePlanShow>  proPlaShow = result.AsQueryable().FilterBySearch(useForSearch);
            paging.total = proPlaShow.Count();
            return proPlaShow.ToList().AsQueryable().ToPageList<VM_ProducePlanShow>("ClientOrderID asc", paging);
        }

        #endregion


        /// <summary>
        /// 根据客户订单号，删除表 PD_GENERAL_PLAN 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public bool DeleteProduceGeneralPlanListRepository(string paraClientOrderID)
        {
            StringBuilder pgpDeleteCommandSB = new StringBuilder();
            pgpDeleteCommandSB.AppendFormat("delete from PD_GENERAL_PLAN where CLN_ODR_ID='{0}' and STATUS='1'", paraClientOrderID);

            return base.ExecuteStoreCommand(pgpDeleteCommandSB.ToString());
        }



    } //end ProduceGeneralPlanRepositoryImp
}
