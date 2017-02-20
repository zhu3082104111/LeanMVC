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
    public class AccPrintPreviewController : Controller
    {
        
         private IWipStoreService wipStoreService;

         public AccPrintPreviewController(IWipStoreService wipStoreService)
         {
            this.wipStoreService=wipStoreService;
         }
        //
        // GET: /Store/AccPrintPreview/

        public ActionResult Index(string id)
        {
            ViewBag.preview_id = id;
            return View();
        }

        [HttpPost]
        public JsonResult previewInfos(string pid)
        
        {
            int total = 0;
            if (string.IsNullOrEmpty(pid))
            {
                //return Content("请选择您要查询的数据");
            }
            //var deleteID = pid.Split(',');
            ////定义数组存放需要查询的ID
            //List<string> list = new List<string>();
            //foreach (var Dsid in deleteID)
            //{
            //    list.Add(Dsid);
            //}
            //然后执行查询的方法查询数据
            var users = wipStoreService.SelectWipStore(pid);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

    }
}
