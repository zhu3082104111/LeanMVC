/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutPrintController.cs
// 文件功能描述：
//          内部成品库送货单打印选择画面用Controller
//
// 修改履历：2013/12/05 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions;
using Model.Store;
using BLL;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 内部成品库送货单打印选择画面用Controller
    /// </summary>
    public class FinOutPrintController : BaseController
    {
        //内部成品库出库画面的Service接口类
        private IFinOutStoreService finOutStoreService;

        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="finOutStoreService">Service接口类</param>
        public FinOutPrintController(IFinOutStoreService finOutStoreService)
        {
            this.finOutStoreService = finOutStoreService;
        }

         /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>送货单打印选择视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="search">筛选条件</param>
        /// <returns>送货单打印选择画面数据</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_storeFinOutPrintForSearch search)
        {

            IEnumerable<VM_storeFinOutPrintForTableShow> query = finOutStoreService.GetFinOutPrintForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_storeFinOutPrintForTableShow>();
                paging.total = 0;
            }
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return query.ToJson(paging.total);

        }

    }
}
