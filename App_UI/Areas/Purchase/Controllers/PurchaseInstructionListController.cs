/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseInstructionListController.cs
// 文件功能描述：
//          外购计划一览画面的Controller
//      
// 修改履历：2013/11/18 陈阵 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using Extensions;
using BLL;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外购计划一览画面的Controller
    /// </summary>
    public class PurchaseInstructionListController : BaseController
    {
        // 外购指示和外协指示的Service
        private IInstructionsService instructionsService;

        /// <summary>
        /// 外购计划一览画面的构造函数
        /// </summary>
        /// <param name="instructionsService">外购指示和外协指示的Service</param>
        public PurchaseInstructionListController(IInstructionsService instructionsService)
        {
            this.instructionsService = instructionsService;
        }

        /// <summary>
        /// 显示外购计划（外购指示）一览画面
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseInstructionList()
        {
            // 返回视图
            return View();
        }

        /// <summary>
        /// 查询并取得外购计划一览画面的详细信息
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Query(Paging paging, VM_PurchaseInstructionList4Search search)
        {
            // 查询外购指示List信息
            var purchaseInstructionList = instructionsService.GetPurchaseInstructionList(search, paging);
            
            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseInstructionList.ToJson(paging.total);
        }
    }
}
