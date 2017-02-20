/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：InProcessingPlanServiceImp.cs
// 文件功能描述：
//  公司日历
// 
// 创建标识：2013/12/17  杜兴军 
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.database;
using Model;

namespace Repository
{
    public class CompanyCalendarRepositoryImp:AbstractRepository<DB,CompCalInfo>,ICompanyCalendarRepository
    {
        public IEnumerable<CompCalInfo> GetList()
        {
            return GetAvailableList<CompCalInfo>().ToList().AsEnumerable();
        }

        public IEnumerable<CompCalInfo> GetWorkDaysList()
        {
            return GetAvailableList<CompCalInfo>().Where(cal=>cal.ReatFlg.Equals(Constant.CalendarRetFlg.WORK)).AsEnumerable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<CompCalInfo> GetWorkDayRangeList(DateTime begin,DateTime end)
        {
            string yearBegin =begin.Year.ToString();
            string monthBegin=begin.Month.ToString();
            if (monthBegin.Length <= 1)
            {
                monthBegin = "0" + monthBegin;
            }

            string dayBegin=begin.Day.ToString();
            if (dayBegin.Length <= 1)
            {
                dayBegin = "0" + dayBegin;
            }
            string yearEnd = end.Year.ToString();
            string monthEnd = end.Month.ToString();
            if (monthEnd.Length <= 1)
            {
                monthEnd = "0" + monthEnd;
            }
            string dayEnd = end.Day.ToString();
            if (dayEnd.Length <= 1)
            {
                dayEnd = "0" + dayEnd;
            }
            IQueryable<CompCalInfo> query=base.GetAvailableList<CompCalInfo>();
            query=from a in query 
                  where (a.Year.CompareTo(yearBegin)>=0 && a.Month.CompareTo(monthBegin)>=0 && a.Day.CompareTo(dayBegin)>=0)&&(a.Year.CompareTo(yearEnd)<=0 && a.Month.CompareTo(monthEnd)<=0 && a.Day.CompareTo(dayEnd)<=0)
                  select a;
            return query;
        }

        /// <summary>
        /// 获取区间工作日数
        /// 20131226 杜兴军
        /// </summary>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns></returns>
        public decimal GetWorkDayRangeCount(DateTime begin, DateTime end)
        {
            return GetWorkDayRangeList(begin, end).AsQueryable().Count(cal=>cal.ReatFlg.Equals(Constant.CalendarRetFlg.WORK));
        }
    }
}
