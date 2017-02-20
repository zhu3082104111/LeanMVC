// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessTranslateServiceImp.cs
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
using Model;
using Model.Produce;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace BLL
{
    /// <summary>
    /// 代东泽 20131220
    /// </summary>
    class ProcessTranslateServiceImp:AbstractService,IProcessTranslateService
    {
        /// <summary>
        /// 代东泽 20131220
        /// </summary>
        private IProcessTranslateCardRepository translateCardRepository;

        /// <summary>
        /// 代东泽 20131220
        /// </summary>
        private ICompanyCalendarRepository companyCalendarRepository;
        /// <summary>
        /// 代东泽 20131220
        /// </summary>
        private ICustomTranslateInfoRepository customTranslateInfoRepository;

        /// <summary>
        /// 代东泽 20131220
        /// </summary>
        /// <param name="translateCardRepository"></param>
        public ProcessTranslateServiceImp(ICustomTranslateInfoRepository customTranslateInfoRepository,IProcessTranslateCardRepository translateCardRepository,ICompanyCalendarRepository companyCalendarRepository) 
        {
            this.translateCardRepository = translateCardRepository;
            this.companyCalendarRepository = companyCalendarRepository;
            this.customTranslateInfoRepository=customTranslateInfoRepository;
        }

        /// <summary>
        /// 代东泽 20131220
        /// </summary>
        /// <param name="page"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Model.Produce.VM_ProcessTranslateCardForTableShow> GetProcessTranslateCardsForSearch(Extensions.Paging page, Model.Produce.VM_ProcessTranslateCardForSearch search)
        {
            IList < Model.Produce.VM_ProcessTranslateCardForTableShow > translateCards= translateCardRepository.GetTranslateCardsWithPagingBySearch(search, page).ToList();
            if (translateCards.Count <= 0)
            {

            }
            else 
            {
                DateTime startD = translateCards.Min(n => n.PlanStartDate);
                DateTime endD = translateCards.Max(n => n.PlanEndDate);
                IList<CompCalInfo> ccList = companyCalendarRepository.GetWorkDayRangeList(startD, endD).ToList();
                DateTime now = DateTime.Now;
                foreach (var obj in translateCards)
                {
                    decimal needCount = obj.ReceQty;
                    decimal nowCount = obj.LastProcessQty;

                    if (needCount == 0M) {
                        needCount = 1;
                    }
                    decimal acc = nowCount * 100 / needCount;
                    DateTime start = obj.PlanStartDate;
                    DateTime end = obj.PlanEndDate;
                    int countDays = GetDayCount(ccList, start, end);
                    int useDays = GetDayCount(ccList, start, now);

                    if (countDays == 0) {
                        countDays = 1;
                    }
                    decimal bcc = useDays * 100 / countDays;

                    if ((int)acc > (int)bcc)
                    {
                        obj.ScheduleState = "提前";
                        //超前
                    }
                    else if ((int)acc == (int)bcc)
                    {
                        //正常
                        obj.ScheduleState = "正常";
                    }
                    else if ((int)acc < (int)bcc)
                    {
                        //延迟
                        if (now.CompareTo(end) < 0)
                        {
                            //延期
                            obj.ScheduleState = "延期";
                        }
                        else
                        {
                            //延误ProcessingPlanState
                            obj.ScheduleState = "延误";
                        }
                    }

                }
            }
            return translateCards;
        }
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="query"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetDayCount(IEnumerable<CompCalInfo> query, DateTime begin, DateTime end) 
        {
            string yearBegin =begin.Year.ToString();
            string monthBegin=begin.Month.ToString();
            string dayBegin=begin.Day.ToString();
            string yearEnd = end.Year.ToString();
            string monthEnd = end.Month.ToString();
            string dayEnd = end.Day.ToString();
            var result=from a in query 
                  where (a.Year.CompareTo(yearBegin)>=0 && a.Month.CompareTo(monthBegin)>=0 && a.Day.CompareTo(dayBegin)>=0)&&(a.Year.CompareTo(yearEnd)<=0 && a.Month.CompareTo(monthEnd)<=0 && a.Day.CompareTo(dayEnd)<=0)
                  select a;
            if (result != null)
            {
                return result.Count();
            }
            else {
                return 0;
            }
            
        
        }


        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public IEnumerable<VM_CustomTranslateInfoForDetaiShow> GetCustomOrdersForTranslateCard(ProcessTranslateCard card)
        {
           return  translateCardRepository.GetCustomOrdersForTranslateCard(card);
        }
        /// <summary>
        /// 代东泽 20131223
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Model.Produce.VM_ProcessTranslateCardForDetailShow GetTranslateCard(ProcessTranslateCard card)
        {
            return translateCardRepository.GetTranslateCard(card);
        }
        /// <summary>
        /// 代东泽 20131223
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public IEnumerable<Model.Produce.VM_ProcessTranslateCardPartForDetailShow> GetTranslateDetailInfos(ProcessTranslateCard card)
        {
            return translateCardRepository.GetTranslateDetailInfos(card);
        }

        /// <summary>
        /// 代东泽 20131224
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        /// <param name="ctiList"></param>
        public void SaveTranslateCardPlan(ProcessTranslateCard bill, IList<ProcessTranslateDetail> list, IList<CustomTranslateInfo> ctiList, IList<string> flags)
        {
            translateCardRepository.UpdateNotNullColumn(bill);
            int k = 0;
            foreach (var a in list)
            {
                if (CommonConstant.OLD.Equals(flags.ElementAt(k)))//update
                {
                    translateCardRepository.UpdateTranslateDetail(a);
                }
                else //add
                {
                    translateCardRepository.AddTranslateDetail(a);
                }
                k++;
            }
            foreach (var a in ctiList)
            {
                customTranslateInfoRepository.UpdateNotNullColumn(a);
            }
        }
        /// <summary>
        /// 代东泽 20131224
        /// </summary>
        /// <param name="bill"></param>
        /// <param name="list"></param>
        public void SaveTranslateCard(ProcessTranslateCard bill, IList<ProcessTranslateDetail> list, IList<string> flags)
        {
            translateCardRepository.UpdateNotNullColumn(bill);
            int k = 0;
            foreach (var a in list)
            {
                if (CommonConstant.OLD.Equals(flags.ElementAt(k)))//update
                {
                    translateCardRepository.UpdateTranslateDetail(a);
                }
                else //add
                {
                    translateCardRepository.AddTranslateDetail(a);
                }
                k++;
            }
        }
    }
}
