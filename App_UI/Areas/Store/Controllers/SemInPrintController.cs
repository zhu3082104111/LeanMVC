using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Util;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;
using Extensions;
namespace App_UI.Areas.Store.Controllers
{
    public class SemInPrintController : Controller
    {
        private ISemStoreService semStoreService;

        public SemInPrintController( ISemStoreService semStoreService)
         {
            this.semStoreService = semStoreService;
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
        public JsonResult Get(VM_SemInPrintForSearch seminprint, Paging paging)
        {
            int total;
            var users = semStoreService.GetSemInPrintBySearchByPage(seminprint, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult SemInPrintIndex(string pdtID, string deliveryOrderID)
        {
            ViewBag.preview_pdtID = pdtID;
            ViewBag.preview_deliveryOrderID = deliveryOrderID;
            return View();

        }


        public ActionResult SemInPrintPreview(string pdtID, string deliveryOrderID, Paging paging)
        {
            int total = 0;
            if (string.IsNullOrEmpty(pdtID))
            {
                //return Content("请选择您要查询的数据");
            }
            var users = semStoreService.SelectSemStore(pdtID,deliveryOrderID , paging);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

    }
}
