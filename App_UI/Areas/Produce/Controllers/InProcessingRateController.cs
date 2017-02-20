/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：OrderRateController.cs
// 文件功能描述：内部加工进度统计的Controller
//     
// 修改履历：2013/10/21 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;


namespace App_UI.Areas.Produce.Controllers
{
    public class InProcessingRateController : BaseController
    {
        private IInProcessingRateService inProcessingRateService;
        
        public InProcessingRateController(IInProcessingRateService inProcessingRateService)
        {
            this.inProcessingRateService = inProcessingRateService;
        }

        /// <summary>
        /// 跳转到显示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 通过筛选条件获取数据
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="userForSearch">筛选条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_InProcessingRateSearch userForSearch)
        {
            var inProcessingRate = inProcessingRateService.GetInProcessingRateSearch(userForSearch, paging);
            return inProcessingRate.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }
    }
}
