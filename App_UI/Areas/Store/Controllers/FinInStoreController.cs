/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinInStoreController.cs
// 文件功能描述：
//          内部成品库待入库一览画面用Controller
//
// 修改履历：2013/11/23 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Store;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;


namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 内部成品库待入库一览画面用Controller
    /// </summary>
    public class FinInStoreController : BaseController
    {
        //内部成品库入库画面的Service接口类
        private IFinInStoreService finInStoreService;

        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="finInStoreService">Service接口类</param>
        public FinInStoreController(IFinInStoreService finInStoreService) 
        {
            this.finInStoreService = finInStoreService;
        }

        /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>待入库一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="search">刷选条件</param>
        /// <returns>待入库一览画面数据</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_StoreFinInStoreForSearch search)
        {
            IEnumerable<VM_StoreFinInStoreForTableShow> query = finInStoreService.GetFinInStoreForSearch(search, paging);
           
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return query.ToJson(paging.total);

        }

      
    }
}
