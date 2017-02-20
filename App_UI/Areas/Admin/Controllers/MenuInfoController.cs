using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Util;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.Areas.Controllers;
using App_UI.App_Start;
using Extensions;
namespace App_UI.Areas.Admin.Controllers
{
    public class MenuInfoController : BaseController
    {
        private IRoleInfoService roleInfoService;
        private IBaseInfoService departmentService;
        public MenuInfoController(IRoleInfoService roleInfoService, IBaseInfoService departmentService)
        {
            this.roleInfoService = roleInfoService;
            this.departmentService = departmentService;

        }
        //
        // GET: /Admin/MenuInfo/
        [CustomAuthorize]
        public ActionResult Index(int pageIndex=1)
        {
               var model = roleInfoService.findAllMenuInfoOrderBy();
               var returnModel = model.Select(
                                                 a =>
                                                 new
                                                 {                                    
                                                     a.MName,                                                    
                                                     a.ControllerName,
                                                     a.ActionName,
                                                     a.SystemId,
                                                     a.Display,
                                                     a.Enabled,
                                                     a.MId
                                                 });
                           ViewBag.PropertyInfo = returnModel.ElementType.GetProperties();
                           returnModel = returnModel.Processing(Request.QueryString);

                            if (!string.IsNullOrEmpty(Request["report"]))
                            {
                                //导出
                                //导出数据自定义字段

                                var reportModel = model.Select("new ( MName,ControllerName,ActionName,SystemId,Display,Enabled,Addate )");
                                return new ReportResult(new Report(reportModel.ToReportSource()));
                            }
                            return View(model.ToPagedList(pageIndex));
        }

    }
}
