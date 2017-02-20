/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProducePlanController.cs
// 文件功能描述：生产计划的Service接口
// 
// 修改履历：2013/12/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class ProducePlanController : BaseController
    {
        private IProducePlanService producePlanService;

        public ProducePlanController(IProducePlanService producePlanService)
        {
            this.producePlanService = producePlanService;
        }

        /// <summary>
        /// 返回生产计划总表视图
        /// </summary>
        /// <returns>页面视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 返回生产计划总表一览数据
        /// </summary>
        /// <param name="paging">分页</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>一览数据</returns>
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_ProducePlanForSearch useForSearch)
        {
            var producePlan = producePlanService.GetProducePlanSearch( paging,useForSearch);
            return producePlan.ToJson(paging.total); //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }
    }
}
