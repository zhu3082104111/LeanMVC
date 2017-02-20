/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierAccoutingListController.cs
// 文件功能描述：
//          外协计划台帐一览画面的Controller类
//      
// 创建标识：2013/12/12 吴飚 新建
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

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外协计划台帐一览画面的Controller类
    /// </summary>
    public class SupplierAccoutingListController : BaseController
    {
        // 计划台帐的Service
        private IPlanAccoutingService planAccoutingService;

        /// <summary>
        /// 外协计划台帐一览画面的Controller的构造函数
        /// </summary>
        /// <param name="IPlanAccoutingService">计划台帐Service接口对象</param>
        public SupplierAccoutingListController(IPlanAccoutingService planAccoutingService)
        {
            this.planAccoutingService = planAccoutingService;
        }

        /// <summary>
        /// 外购计划台帐一览画面的初始化函数
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierAccoutingList()
        {
            // 返回视图
            return View();
        }

        /// <summary>
        /// 取得外协计划台帐一览信息
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="searchCondition">筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(VM_SupplierAccoutingList4Search searchCondition, Paging paging)
        {
            // 得到将要在页面上显示的数据
            var SupplierAccoutingList = planAccoutingService.GetSupplierAccoutingListBySearchByPage(searchCondition, paging);

            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return SupplierAccoutingList.ToJson(paging.total);
        }
    }
}
