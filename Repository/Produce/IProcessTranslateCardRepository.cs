// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProcessTranslateCardRepository.cs
// 文件功能描述：加工流转卡数据操作实接口
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Produce;
using Extensions;
namespace Repository
{
    public interface IProcessTranslateCardRepository:IRepository<ProcessTranslateCard>
    {

        /// <summary>
        /// 代东泽 20131216
        /// </summary>
        /// <param name="card"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        IEnumerable<VM_ProcessTranslateCardForTableShow> GetTranslateCardsWithPagingBySearch(VM_ProcessTranslateCardForSearch card, Paging paging);

        /// <summary>
        /// 根据条件取得可交仓数量大于0的数据
        /// </summary>
        /// <param name="entity">筛选条件</param>
        /// <param name="paging">分页</param>
        /// <returns>加工待交仓一览数据集</returns>
        /// 朱静波 20131206
        IEnumerable<VM_IWaitingWarehouseView> SearchTranslateCard(VM_IWaitingWarehouseForSearch entity, Paging paging);

        /// <summary>
        /// 增加交仓数合计
        /// </summary>
        /// <param name="entity">修改条件</param>
        /// <param name="user">用户ID</param>
        /// <returns></returns>
        /// 朱静波 20131206
        bool AddWarehTalQty(VM_IWaitingWarehouseView entity, string user);

        /// <summary>
        /// 扣除交仓数合计
        /// </summary>
        /// <param name="ProcTranID">流转卡号</param>
        /// <param name="PlanTotal">预计交仓合计</param>
        /// <param name="user">用户ID</param>
        /// <returns>执行结果</returns>
        /// 朱静波 20131206
        bool ReduceWarehTalQty(string ProcTranID, decimal PlanTotal, string user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        IEnumerable<VM_CustomTranslateInfoForDetaiShow> GetCustomOrdersForTranslateCard(ProcessTranslateCard card);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        VM_ProcessTranslateCardForDetailShow GetTranslateCard(ProcessTranslateCard card);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        IEnumerable<VM_ProcessTranslateCardPartForDetailShow> GetTranslateDetailInfos(ProcessTranslateCard card);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        void UpdateTranslateDetail(ProcessTranslateDetail a);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        void AddTranslateDetail(ProcessTranslateDetail a);
    }
}
