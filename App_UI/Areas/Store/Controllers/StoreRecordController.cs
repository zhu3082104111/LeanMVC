/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：StoreRecordController.cs
// 文件功能描述：在库品一览画面的Controller实现
//      
// 修改履历：2014/1/8 刘云 新建
/*****************************************************************************/
using BLL;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 在库品一览画面的Controller实现
    /// </summary>
    public class StoreRecordController : Controller
    {
        //在库品一览画面的Service
        private IStoreRecordService storeRecordService;

        /// <summary>
        /// 在库品一览画面的构造函数
        /// </summary>
        /// <param name="storeRecordService"></param>
        public StoreRecordController(IStoreRecordService storeRecordService)
        {
            this.storeRecordService = storeRecordService;
        }

        /// <summary>
        /// 跳转到视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="searchConditon">筛选条件</param>
        /// <returns></returns>
        public JsonResult Query(Paging paging, VM_StoreRecordForSearch searchConditon)
        {
            //得到将要在页面上显示的数据
            var purchaseOrderLists = storeRecordService.GetStoreRecordBySearchByPage(searchConditon, paging);
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseOrderLists.ToJson(paging.total);
        }


    }
}
