/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：InProcessingPlanServiceImp.cs
// 文件功能描述：
//  公司日历资源接口类
// 
// 创建标识：2013/12/17  杜兴军
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Model;

namespace Repository
{
    public interface ICompanyCalendarRepository:IRepository<CompCalInfo>
    {
        /// <summary>
        /// 获取有效的记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompCalInfo> GetList();

        /// <summary>
        /// 获取有效且为工作日的记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompCalInfo> GetWorkDaysList();

        /// <summary>
        /// 获取工作日区间日期列表
        /// 代东泽
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompCalInfo> GetWorkDayRangeList(DateTime begin, DateTime end);

        /// <summary>
        /// 获取区间工作日数
        /// 20131226 杜兴军
        /// </summary>
        /// <param name="begin">开始日期</param>
        /// <param name="end">结束日期</param>
        /// <returns></returns>
        decimal GetWorkDayRangeCount(DateTime begin, DateTime end);
    }
}
