/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProducePlanServiceImp.cs
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
using Repository;

namespace BLL.Produce.ProducePlan
{
    class ProducePlanServiceImp : AbstractService, IProducePlanService
    {
        private IProduceGeneralPlanRepository iProduceGeneralPlanRepository;

        public ProducePlanServiceImp(IProduceGeneralPlanRepository iProduceGeneralPlanRepository)
        {
            this.iProduceGeneralPlanRepository = iProduceGeneralPlanRepository;
        }

        public IEnumerable<VM_ProducePlanShow> GetProducePlanSearch(Paging paging, VM_ProducePlanForSearch useForSearch)
        {
            return  iProduceGeneralPlanRepository.GetProducePlanSearch(paging, useForSearch);
        }
    }
}
