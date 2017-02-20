/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInPrintController.cs
// 文件功能描述：
//          附件库入库单打印选择Controller
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
    public class AccInPrintController : Controller
    {
        
         private IWipStoreService wipStoreService;
         private IAccStoreService accStoreService;

         public AccInPrintController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
         {
             this.wipStoreService = wipStoreService;
             this.accStoreService = accStoreService;
         }
        //
        // GET: /Store/AccInPrint/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_AccInPrintForSearch accInPrintForSearch, Paging paging)
        {
            int total;
            var users = accStoreService.GetAccInPrintBySearchByPage(accInPrintForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult AccInPrintIndex(string pdtID, string deliveryOrderID)
        {
            ViewBag.preview_pdtID = pdtID;
            ViewBag.preview_deliveryOrderID = deliveryOrderID;
            return View();
        }

        public ActionResult AccInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            int total = 0;
            if (string.IsNullOrEmpty(pdtID))
            {
                //return Content("请选择您要查询的数据");
            }
            //var pdtIDs = pdtID.Split(',');
            //var deliveryOrderIDs = deliveryOrderID.Split(',');
            var users = accStoreService.SelectForAccInPrintPreview(pdtID, deliveryOrderID, paging);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }


        
    }
}
