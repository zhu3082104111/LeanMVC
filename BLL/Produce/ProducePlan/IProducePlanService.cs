/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IProducePlanService.cs
// 文件功能描述：生产计划的Service接口
//     
// 修改履历：2013/12/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Model.Produce;

namespace BLL
{
    public interface IProducePlanService
    {
        /// <summary>
        /// 根据条件返回生成计划总表数据
        /// </summary>
        /// <param name="paging">分页</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>生产计划总表视图数据集</returns>
        IEnumerable<VM_ProducePlanShow> GetProducePlanSearch(Paging paging, VM_ProducePlanForSearch useForSearch);
    }
}
