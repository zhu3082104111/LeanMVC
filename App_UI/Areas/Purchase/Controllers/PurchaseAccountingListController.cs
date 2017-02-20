/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseAccountingListController.cs
// 文件功能描述：
//          外购计划台账一览画面的Controller类
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外购计划台帐一览画面的Controller类
    /// </summary>
    public class PurchaseAccountingListController : BaseController
    {

        // 计划台帐的Service
        private IPlanAccoutingService planAccoutingService;

        /// <summary>
        /// 外购计划台账一览画面的构造函数
        /// </summary>
        /// <param name="planAccoutingService">计划台帐Service接口对象</param>
        public PurchaseAccountingListController(IPlanAccoutingService planAccoutingService)
        {
            this.planAccoutingService = planAccoutingService;
        }

        /// <summary>
        /// 外购计划台帐一览画面的初始化函数
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseAccountingList()
        {
            // 返回视图
            return View();
        }

        /// <summary>
        /// 取得外购计划台帐一览信息
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="searchCondition">筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(VM_PurchaseAccoutingListForSearch searchCondition, Paging paging)
        {
            // 总件数
            int total;

            // 得到将要在页面上显示的数据
            var PurchaseAccoutingList = planAccoutingService.GetPurchaseAccoutingListBySearchByPage(searchCondition, paging);

            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return PurchaseAccoutingList.ToJson(paging.total);
        }
    }
}
