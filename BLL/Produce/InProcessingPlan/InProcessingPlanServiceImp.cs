/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：InProcessingPlanServiceImp.cs
// 文件功能描述：
//  内部加工计划业务逻辑实现类
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Model;
using Repository;
using Repository.Base;

namespace BLL
{
    /// <summary>
    /// 内部加工计划Service实现
    /// </summary>
    public class InProcessingPlanServiceImp :AbstractService, IInProcessingPlanService
    {
        private IInProcessingPlanRepository inProcessingPlanRepository;
        private IProduceScheduRepository produceScheduRepository;
        private ICompanyCalendarRepository companyCalendarRepository;

        /// <summary>
        /// 初始化资源库
        /// </summary>
        /// <param name="inProcessingPlanRepository">内部加工计划</param>
        /// <param name="produceScheduRepository">生产排期</param>
        /// <param name="companyCalendarRepository">公司日历</param>
        public InProcessingPlanServiceImp(IInProcessingPlanRepository inProcessingPlanRepository,
            IProduceScheduRepository produceScheduRepository, ICompanyCalendarRepository companyCalendarRepository)
        {
            this.inProcessingPlanRepository = inProcessingPlanRepository;
            this.produceScheduRepository = produceScheduRepository;
            this.companyCalendarRepository = companyCalendarRepository;
        }

        public IEnumerable<VM_InProcessingPlanShow> GetPlanViewByPage(VM_InProcessingPlanSearch search, Paging paging)
        {
            return inProcessingPlanRepository.GetPlanViewByPage(search, paging);
        }

        public IEnumerable<VM_InProcessingMiddlePlanShow> GetMiddlePlanByPage(VM_InProcessingMiddlePlanSearch search, Paging paging)
        {
            return inProcessingPlanRepository.GetMiddlePlanViewByPage(search, paging);
        }

        public bool AddOrUpdateMiddlePlan(List<ProduceSchedu> schedus)
        {
            IEnumerable<CompCalInfo> calendarList = companyCalendarRepository.GetWorkDaysList();
            for (int i = 0, len = schedus.Count(); i < len; i++)
            {
                ProduceSchedu schedu = schedus[i];
                //schedu.EndProduceTime = companyCalendarRepository.GetWorkDayRangeCount((DateTime)schedu.ScheduStartDt,
                //    (DateTime)schedu.ScheduEndDt);
                schedu.EndProduceTime = (from cal in calendarList
                                         let dt = DateTime.Parse(cal.Year + "-" + cal.Month + "-" + cal.Day)
                                         where dt >= schedu.ScheduStartDt && dt <= schedu.ScheduEndDt
                                         select cal).Count();
                if (schedu.EndProduceTime == 0)
                {
                    throw new Exception("计划所要日数不能为0");
                }
                if (schedu.UpdUsrID!=null)//修改人员不为空，更新；否则，添加
                {
                    produceScheduRepository.Update(schedus[i]);
                }
                else
                {
                    produceScheduRepository.Add(schedu);
                }
            }
            return true;
        }

        public IEnumerable<int> GetProcessSequence(string processId)
        {
            return inProcessingPlanRepository.GetProcessSequence(processId);
        }

        public bool AddProcessTranslateCard(ProcessTranslateCard translateCard, List<CustomTranslateInfo> customTranslateInfoList, List<ProcessTranslateDetail> translateDetailList)
        {
            return inProcessingPlanRepository.AddProcessTranslateCard(translateCard, customTranslateInfoList,
                translateDetailList);
        }


        public string GetProcessIdByExport(string exportId)
        {
            return inProcessingPlanRepository.GetProcessIdByExport(exportId);
        }
    }
}
