/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IAssemblePlanService.cs
// 文件功能描述：
//  总装计划业务接口类
// 
// 创建标识：2013/12/18  杜兴军 创建
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
using Extensions;

namespace BLL
{
    /// <summary>
    /// 总装计划Service接口
    /// </summary>
    public interface IAssemblePlanService
    {
        /// <summary>
        /// 获取总装计划一览数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_AssemblePlanShow> GetAssemblePlan(VM_AssemblePlanSearch search, Paging paging);
        
        /// <summary>
        /// 获取总装中计划数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        IEnumerable<VM_AssembleMiddlePlanShow> GetAssembleMiddlePlan(VM_AssembleMiddlePlanSearch search, Paging paging);

        /// <summary>
        /// 获取原料数量
        /// </summary>
        /// <param name="search">匹配条件</param>
        /// <returns></returns>
        IEnumerable<VM_MaterialQuanlityShow> GetMaterialQuanlity(VM_MaterialQuanlitySearch search);

    }
}
