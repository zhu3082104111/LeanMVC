/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProduceScheduRepository.cs
// 文件功能描述：
//  
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
using Model;

namespace Repository
{
    public interface IProduceScheduRepository:IRepository<ProduceSchedu>
    {

        /// <summary>
        /// 获取月计划
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlan(VM_InProcessingMiddlePlanSearch search);


    }
}
