/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierAccoutingDetailController.cs
// 文件功能描述：
//          外协计划台帐详细画面的Controller
//      
// 创建标识：2013/12/19 廖齐玉 新建
/*****************************************************************************/
using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外协计划台帐详细画面的Controller类
    /// </summary>
    public class SupplierAccoutingDetailController : BaseController
    {
        // 外购外协计划台帐的Service
        private IPlanAccoutingService planAccoutingService;

        /// <summary>
        /// 外协计划台账详细画面的构造器
        /// </summary>
        /// <param name="SupplierAccoutingListService">外购外协计划台帐的Service</param>
        public SupplierAccoutingDetailController(IPlanAccoutingService planAccoutingService)
        {
            this.planAccoutingService = planAccoutingService;
        }

        /// <summary>
        /// 显示外协计划台帐详细画面
        /// </summary>
        /// <param name="supOrderNo">外协单号</param>
        /// <returns></returns>
        public ActionResult ShowDetail(string supOrderNo)
        {
            // 外协单号
            ViewBag.SupOrderNo = supOrderNo;
            // 外协单相关数据数据获得
            VM_SupplierAccoutingDetailInfo supplierAcc= planAccoutingService.SelectSupplierOrderInfo(supOrderNo);
            // 紧急状态
            ViewBag.UrgentStatus = supplierAcc.UrgentStatus;
            // 供货商
            ViewBag.InCompName = supplierAcc.SupCompName;
            // 生产部门
            ViewBag.Department = supplierAcc.Department;
            // 下单日期
            DateTime date = supplierAcc.MarkDate.Value;
            ViewBag.MarkDate = date.ToString("yyyy年MM月dd日");

            // 返回视图
            return View();
        }

        /// <summary>
        /// 外协台账详细画面-详细数据的加载
        /// </summary>
        /// <param name="supOrderNo"></param>
        /// <returns></returns>
        public JsonResult ShowSupplierAccoutingDetail(string supOrderNo)
        {
            // 得到将要在页面上显示的数据
            var supplierAccouting = planAccoutingService.GetSupplierAccoutingDetailByNo(supOrderNo);

            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return supplierAccouting.ToJson(0);
        }
    }
}
