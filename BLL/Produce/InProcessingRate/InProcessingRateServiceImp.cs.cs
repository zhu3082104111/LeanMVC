/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IInProcessingRateService.cs
// 文件功能描述：内部进度查询画面的Service接口实现
//     
// 修改履历：2013/10/31 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Model;
using Repository;

namespace BLL
{
    class InProcessingRateServiceImp:AbstractService,IInProcessingRateService 
    {
        private IProduceGeneralPlanRepository produceGeneralPlanRepository;


        public InProcessingRateServiceImp(IProduceGeneralPlanRepository produceGeneralPlanRepository)
        {
            this.produceGeneralPlanRepository = produceGeneralPlanRepository;
        }

        public IEnumerable GetInProcessingRateSearch(VM_InProcessingRateSearch inProcessingRate, Paging paging)
        {
            return produceGeneralPlanRepository.GetInProcessingRateSearch( inProcessingRate, paging);
        }
    }
}
