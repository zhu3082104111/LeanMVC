/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseAccountingDetailController.cs
// 文件功能描述：
//          外购计划台帐详细画面的Controller类
//      
// 创建标识：2013/12/9 吴飚 新建
/*****************************************************************************/
using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions;
using Model;
using BLL;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外购计划台帐详细画面的Controller类
    /// </summary>
    public class PurchaseAccountingDetailController : BaseController
    {
        // 外购外协计划台帐的Service
        private IPlanAccoutingService planAccoutingService;

        /// <summary>
        /// 外购计划台账详细画面的构造器
        /// </summary>
        /// <param name="planAccoutingService">外购外协计划台帐的Service接口对象</param>
        public PurchaseAccountingDetailController(IPlanAccoutingService planAccoutingService)
        {
            this.planAccoutingService = planAccoutingService;
        }

        /// <summary>
        /// 显示外购计划台帐详细画面
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        public ActionResult DetailShow(string OutOrderNo)
        {
           
            // 获取外购计划台帐画面的显示信息
            VM_PurchaseAccoutingDetail order = planAccoutingService.SelectOrderInfo(OutOrderNo);
            // 紧急状态
            ViewBag.UrgentStatus = order.UrgentStatus;
            // 供货商
            ViewBag.CompName = order.CompName;
            // 外购单号
            ViewBag.OutOrderNo = order.OutOrderId;
            // 生产部门
            ViewBag.DeptName = order.DeptName;
            // 下单日期
            DateTime date = order.EstablishDate.Value;
            ViewBag.EstablishDate = date.ToString("yyyy年MM月dd日");
            
            // 返回视图
            return View();
        }

        /// <summary>
        /// 获取外购计划台帐详细信息
        /// </summary>
        /// <param name="OutOrderNo">外购单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(string OutOrderNo)
        {
            // 得到将要在页面上显示的数据
            var purchaseAccoutingListDetail = planAccoutingService.GetPurchaseAccoutingDetailByNo(OutOrderNo);

            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseAccoutingListDetail.ToJson(0);
        }
    }
}