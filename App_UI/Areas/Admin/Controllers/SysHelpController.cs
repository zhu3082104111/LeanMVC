using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using Util;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.Areas.Controllers;
using App_UI.App_Start;
using Extensions;
namespace App_UI.Areas.Admin.Controllers
{
    public class SysHelpController : BaseController
    {
        private INoticeService noticeService;
        private IRoleInfoService roleInfoService;
        public SysHelpController(INoticeService noticeService, IRoleInfoService roleInfoService)
        {
            
            this.noticeService=noticeService;
            this.roleInfoService = roleInfoService;

        }
        //
        // GET: /Admin/Help/
        [CustomAuthorize]
        public ActionResult Index(int pageIndex=1)
        {
            var model =noticeService.findAllNoticeOrderBy().Select(
                                            a =>
                                            new
                                            {
                                               a.Title,
                                               a.NoticeID
                                            });
            ViewBag.PropertyInfo = model.ElementType.GetProperties();
            model = model.Processing(Request.QueryString);

            if (!string.IsNullOrEmpty(Request["report"]))
            {
                //导出
                return new ReportResult(new Report(model.ToReportSource()));
            }
            return View(model.ToPagedList(pageIndex));
        }

        [CustomAuthorize]
        public ActionResult Details(string id)
        {
            Notice nitice = noticeService.getNoticeById(id);
            if (nitice == null)
            {
                return HttpNotFound();
            }
            return View(nitice);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Notice notice)
        {
            if (ModelState.IsValid)
            {
                noticeService.addNotice(notice);
                return RedirectToAction("Index");
            }

            return View(notice);
        }
        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
            var item = new Notice();
            if (id != null)
            {
                item = noticeService.getNoticeById(id);
            } 

            return View(item);
        }
        [CustomAuthorize]
        [HttpPost]
         public ActionResult Edit(string id, Notice notice)
         {
             if (!ModelState.IsValid)
             {
                 Edit(id);
                 return View(notice);
             }
             if (id == null)
             {
                 noticeService.addNotice(notice);
             }
             else
             {
                 noticeService.updateNotice(notice);
             }
             return RedirectToAction("Index");
         }

        [CustomAuthorize]
        public ActionResult Delete(string id)
        {
            Notice notice = new Notice();
            notice.NoticeID = id;
            noticeService.deleteNotice(notice);
            return RedirectToAction("Index");
        }

    }
}
