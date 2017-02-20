/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutStoreController.cs
// 文件功能描述：
//          内部成品库待出库一览画面用Controller
//
// 修改履历：2013/12/02 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using Model.Store;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 内部成品库待出库一览画面用Controller
    /// </summary>
    public class FinOutStoreController : BaseController
    {
        //内部成品库出库画面的Service接口类
        private IFinOutStoreService finOutStoreService;

        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="finOutStoreService">Service接口类</param>
        public FinOutStoreController(IFinOutStoreService finOutStoreService) 
        {
            this.finOutStoreService = finOutStoreService;
        }

        /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>待出库一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }

         /// <summary>
        ///  获取数据
         /// </summary>
         /// <param name="paging">分页参数</param>
         /// <param name="search">筛选条件</param>
         /// <returns>待出库一览画面数据</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_storeFinOutStoreForSearch search)
        {

            IEnumerable<VM_storeFinOutStoreForTableShow> query = finOutStoreService.GetFinOutStoreForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_storeFinOutStoreForTableShow>();
                paging.total = 0;
            }
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return query.ToJson(paging.total);

        }

    }
}
