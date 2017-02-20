/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInPrintController.cs
// 文件功能描述：
//          在制品库入库单打印选择Controller
//      
// 修改履历：2013/11/05 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Store.Controllers
{
    public class WipInPrintController : Controller
    {
         private IWipStoreService wipStoreService;

         public WipInPrintController(IWipStoreService wipStoreService)
         {
            this.wipStoreService=wipStoreService;
         }
        //
        // GET: /Store/WipInPrint/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_WipInPrintForSearch wipInPrintForSearch, Paging paging)
        {
            int total;
            var users = wipStoreService.GetWipInPrintBySearchByPage(wipInPrintForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult WipInPrintIndex(string pdtID, string deliveryOrderID)
        {
            ViewBag.preview_pdtID = pdtID;
            ViewBag.preview_deliveryOrderID = deliveryOrderID;
            return View();
        }

        //加工产品出库单显示
        public ActionResult WipInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            int total = 0;
            if (string.IsNullOrEmpty(pdtID))
            {
                //return Content("请选择您要查询的数据");
            }
            var users = wipStoreService.SelectForWipInPrintPreview(pdtID, deliveryOrderID, paging);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

    }
}
