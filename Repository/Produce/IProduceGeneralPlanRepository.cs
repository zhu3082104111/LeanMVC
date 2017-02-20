using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;

namespace Repository
{
    /// <summary>
    /// 生产计划总表资源库接口
    /// 20131120 梁龙飞C
    /// </summary>
    public interface IProduceGeneralPlanRepository: IRepository<ProduceGeneralPlan>
    {

        #region 基础方法 C：梁龙飞
        IQueryable<ProduceGeneralPlan> GetPlanByClientOrderID(string clientOrderID);
        /// <summary>
        /// 获取目标状态的所有生产计划(target.状态∈status)
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        IQueryable<ProduceGeneralPlan> GetPlanByStatus(string[] status);      

        bool DeletePlanBySQL(ProduceGeneralPlan target);
        #endregion

        #region OrderAccept C：梁龙飞
        /// <summary>
        /// 获取没有接受的生产计划(target.状态 ∈ {未接收})
        /// </summary>
        /// <param name="searchConditon"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        IEnumerable<VM_OrderAcceptShow> GetPlanNotAccept(VM_OrderAcceptSearch searchConditon, Paging pagex);        

        #endregion

        #region OrderScheduling C：梁龙飞
        /// <summary>
        /// 获取已接收的计划（target.状态 ∉ {未接收,生产完成}）
        /// </summary>
        /// <param name="searchCondition"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        IEnumerable<VM_OrderSchedulingShow> GetPlanAccepted(VM_OrderSchedulingSearch searchCondition, Paging pagex);
        #endregion



        #region 朱静波
        /// <summary>
        /// 通过用户输入的查询条件统计订单产品进度
        /// </summary>
        /// <param name="inProcessingRate">用户查询条件</param>
        /// <param name="paging">分页</param>
        /// <returns>进度一览数据集</returns>
        /// 创建：朱静波
        IEnumerable<VM_OrderRateForTableShow> GetOrderRateForSrarch(VM_OrderRateForSrarch inProcessingRate, Paging paging,VM_HeaderData headerData);

        /// <summary>
        /// 通过用户输入的查询条件统计内部加工进度
        /// </summary>
        /// <param name="inProcessingRate">搜索条件</param>
        /// <param name="paging">分页</param>
        /// <returns>进度一览数据集</returns>
        IEnumerable GetInProcessingRateSearch(VM_InProcessingRateSearch inProcessingRate, Paging paging);

        /// <summary>
        /// 根据条件返回生成计划总表数据
        /// </summary>
        /// <param name="paging">分页</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>生产计划总表视图数据集</returns>
        IEnumerable<VM_ProducePlanShow> GetProducePlanSearch(Paging paging, VM_ProducePlanForSearch useForSearch);
        #endregion



        /// <summary>
        /// 根据客户订单号，删除表 PD_GENERAL_PLAN 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        bool DeleteProduceGeneralPlanListRepository(string paraClientOrderID);

    } //end IProduceGeneralPlanRepository
}
