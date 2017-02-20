/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemblePlanServiceImp.cs
// 文件功能描述：
//  总装计划业务实现类
// 
// 创建标识：2013/12/18  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System.Collections.Generic;
using Repository;
using Model;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 总装计划Service实现
    /// </summary>
    public class AssemblePlanServiceImp:IAssemblePlanService
    {
        private IAssemblyPalnRepository assemblyPalnRepository;//总装计划资源库

        public AssemblePlanServiceImp(IAssemblyPalnRepository assemblyPalnRepository)
        {
            this.assemblyPalnRepository = assemblyPalnRepository;
        }
        
        /// <summary>
        /// 获取总装计划一览数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable<VM_AssemblePlanShow> GetAssemblePlan(VM_AssemblePlanSearch search, Paging paging)
        {
            return assemblyPalnRepository.GetAssemblePlan(search, paging);
        }

        /// <summary>
        /// 获取总装中计划数据
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public IEnumerable<VM_AssembleMiddlePlanShow> GetAssembleMiddlePlan(VM_AssembleMiddlePlanSearch search, Paging paging)
        {
            return assemblyPalnRepository.GetAssembleMiddlePlan(search, paging);
        }

        /// <summary>
        /// 获取原料数量
        /// </summary>
        /// <param name="search">匹配条件</param>
        /// <returns></returns>
        public IEnumerable<VM_MaterialQuanlityShow> GetMaterialQuanlity(VM_MaterialQuanlitySearch search)
        {
            return assemblyPalnRepository.GetMaterialQuanlity(search);
        }
    }
}
