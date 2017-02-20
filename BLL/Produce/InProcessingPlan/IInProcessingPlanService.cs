/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IInProcessingPlanService.cs
// 文件功能描述：
//  内部加工计划业务逻辑接口
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
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
    /// 内部加工计划Service接口
    /// </summary>
    public interface IInProcessingPlanService
    {
        /// <summary>
        /// 获取内部加工计划分页数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_InProcessingPlanShow> GetPlanViewByPage(VM_InProcessingPlanSearch search,Paging paging);

        /// <summary>
        /// 获取月计划分页数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlanByPage(VM_InProcessingMiddlePlanSearch search,Paging paging);

        /// <summary>
        /// 添加或更新中计划
        /// </summary>
        /// <param name="middlePlanList">中计划集</param>
        /// <returns></returns>
        [TransactionAop]
        bool AddOrUpdateMiddlePlan(List<ProduceSchedu> middlePlanList);

        /// <summary>
        /// 获取工序顺序号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        IEnumerable<int> GetProcessSequence(string processId);

        /// <summary>
        /// 添加加工流转卡信息
        /// </summary>
        /// <param name="translateCard">加工流转卡信息</param>
        /// <param name="customTranslateInfoList">客户订单流转卡关系集</param>
        /// <param name="translateDetailList">加工流转卡详细集</param>
        /// <returns></returns>
        [TransactionAop]
        bool AddProcessTranslateCard(ProcessTranslateCard translateCard,List<CustomTranslateInfo> customTranslateInfoList,List<ProcessTranslateDetail> translateDetailList);

        /// <summary>
        /// 根据零件获取工序
        /// </summary>
        /// <param name="exportId">零件ID</param>
        /// <returns></returns>
        string GetProcessIdByExport(string exportId);
    }
}
