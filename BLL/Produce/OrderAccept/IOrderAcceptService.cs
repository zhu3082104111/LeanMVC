using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    /// <summary>
    /// 20131113 梁龙飞
    /// 订单接受服务接口
    /// </summary>
    public interface IOrderAcceptService
    {
       /// <summary>
       /// 获取未接收的计划列表
       /// </summary>
       /// <param name="searchConditon">搜索条件</param>
       /// <param name="pagex">分页设置</param>
       /// <returns></returns>
       IEnumerable<VM_OrderAcceptShow> GetPlanNotAccept(VM_OrderAcceptSearch searchConditon, Paging pagex);

        /// <summary>
        /// 接收列表订单号下的所有产品计划
        /// </summary>
        /// <param name="clientOrderIDs"></param>
        /// <returns></returns>
       [TransactionAop]
       bool AcceptPlan(List<string> clientOrderIDs);
        /// <summary>
        /// 接受一个计划
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
       bool PlanAccept(ProduceGeneralPlan target);
        /// <summary>
        /// 接受多个计划
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
       bool PlanAccept(List<ProduceGeneralPlan> target);


       bool TempInitDate();



       /// <summary>
       /// 根据客户订单号，删除表 PD_GENERAL_PLAN 相关记录
       /// </summary>
       /// <param name="paraClientOrderID">客户订单号</param>
       /// <returns></returns>
       /// 创建者：冯吟夷
       bool DeleteProduceGeneralPlanListService(string paraClientOrderID);


    } //end IOrderAcceptService
}
