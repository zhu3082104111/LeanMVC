/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IInProcessingPlanRepository.cs
// 文件功能描述：
//  内部加工计划资源库接口类
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;

namespace Repository
{
    /// <summary>
    /// 内部加工计划资源库接口类
    /// </summary>
    public interface IInProcessingPlanRepository:IRepository<ProduceSchedu>
    {
        /// <summary>
        /// 获取"加工计划一览"分页数据
        /// </summary>
        /// <param name="search">检索条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_InProcessingPlanShow> GetPlanViewByPage(VM_InProcessingPlanSearch search, Paging paging);

        /// <summary>
        /// 获取"中计划"分页数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlanViewByPage(VM_InProcessingMiddlePlanSearch search, Paging paging);

        /// <summary>
        /// 获取工序顺序号
        /// </summary>
        /// <param name="processId">工序ID</param>
        /// <returns></returns>
        IEnumerable<int> GetProcessSequence(string processId);

        /// <summary>
        /// 添加加工流转卡信息
        /// </summary>
        /// <param name="translateCard">加工流转卡信息</param>
        /// <param name="customTranslateInfoList">客户订单流转卡关系</param>
        /// <param name="translateDetailList">加工流转卡详细</param>
        /// <returns></returns>
        bool AddProcessTranslateCard(ProcessTranslateCard translateCard, List<CustomTranslateInfo> customTranslateInfoList,
            List<ProcessTranslateDetail> translateDetailList);

        /// <summary>
        /// 根据零件ID获取工序ID
        /// </summary>
        /// <param name="exportId"></param>
        /// <returns></returns>
        string GetProcessIdByExport(string exportId);
    }
}
