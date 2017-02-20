// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProcessTranslateService.cs
// 文件功能描述：加工流转卡service接口
// 
// 创建标识：代东泽 20131216
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
namespace BLL
{
    /// <summary>
    /// 代东泽 20131220
    /// </summary>
    public interface IProcessTranslateService
    {
        /// <summary>
        /// 取得加工流转卡一览数据
        /// 代东泽 20131220
        /// </summary>
        /// <param name="page"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<VM_ProcessTranslateCardForTableShow> GetProcessTranslateCardsForSearch(Paging page, VM_ProcessTranslateCardForSearch search);
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        IEnumerable<VM_CustomTranslateInfoForDetaiShow> GetCustomOrdersForTranslateCard(ProcessTranslateCard card);
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        VM_ProcessTranslateCardForDetailShow GetTranslateCard(ProcessTranslateCard card);
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        IEnumerable<VM_ProcessTranslateCardPartForDetailShow> GetTranslateDetailInfos(ProcessTranslateCard card);
        /// <summary>
        /// 代东泽 20131224
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        /// <param name="ctiList"></param>
        void SaveTranslateCardPlan(ProcessTranslateCard bill, IList<ProcessTranslateDetail> list, IList<CustomTranslateInfo> ctiList, IList<string> flags);
        /// <summary>
        /// 代东泽 20131224
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        void SaveTranslateCard(ProcessTranslateCard bill, IList<ProcessTranslateDetail> list, IList<string> flags);
    }
}
