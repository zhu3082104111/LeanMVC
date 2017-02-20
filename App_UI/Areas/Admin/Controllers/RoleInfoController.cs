using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Util;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Admin.Controllers
{
    public class RoleInfoController : BaseController
    {
       
        private IRoleInfoService roleInfoService;
        public RoleInfoController( IRoleInfoService roleInfoService)
        {
            this.roleInfoService = roleInfoService;

        }

        //
        // GET: /Admin/RoleInfo/
        [CustomAuthorize]
        public ActionResult Index(int pageIndex = 1)
        {
            var model = roleInfoService.findAllRoleInfoOrderBy().Select(
                                   a =>
                                   new
                                   {
                                       a.RName,
                                       a.RDesc,
                                       a.RId
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

        //
        // GET: /Admin/RoleInfo/Details/5
        [CustomAuthorize]
        public ActionResult Details(string id )
        {
            RoleInfo roleinfo = roleInfoService.getRoleInfoById(id);
            if (roleinfo == null)
            {
                return HttpNotFound();
            }
            return View(roleinfo);
        }

        //
        // GET: /Admin/RoleInfo/Create
        [CustomAuthorize]
        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        //
        // POST: /Admin/RoleInfo/Create
        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleInfo roleinfo)
        {
            if (ModelState.IsValid)
            {
                roleInfoService.addRoleInfo(roleinfo);
                return RedirectToAction("Index");
            }

            return View(roleinfo);
        }

        //
        // GET: /Admin/RoleInfo/Edit/5
        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
           var item=new RoleInfo();
            if (id!=null)
            {
                item.RId = id;
                item = roleInfoService.getRoleInfoById(id);
            }
            ViewBag.SysControllers = roleInfoService.getAllMenuInfo();
            return View(item);
  
        }

        //
        // POST: /Admin/RoleInfo/Edit/5
        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Transaction]
        public ActionResult Edit(string id, RoleInfo roleinfo, IEnumerable<String> ChinInfoId)
        {
            if (ModelState.IsValid)
            {
                if (id == null) {
                   id = Guid.NewGuid().ToString();
                   roleinfo.RId = id;
                   roleInfoService.addRoleInfo(roleinfo);
                   roleChainInfo(id, ChinInfoId);
                }
                roleinfo.RId = id ; 
                roleInfoService.updateRoleInfo(roleinfo);
                roleChainInfo(id, ChinInfoId);
                return RedirectToAction("Index");
            }

            return View(roleinfo);
        }

        public void roleChainInfo(string id, IEnumerable<String> ChinInfoId)
        {
            //清除原有数据
            RoleChainInfo roleChainInfo = new RoleChainInfo();
            roleChainInfo.RId = id;
            roleInfoService.deleteRoleChainInfo(roleChainInfo);
            
            //添加数据
            if (ChinInfoId != null)
            {
                foreach (var ChinInfosId in ChinInfoId)
                {
                    roleInfoService.addRoleChainInfo(new RoleChainInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        RId = id,
                        CId = ChinInfosId,
                        State=false


                    });
                }
            }
        
        }

        //
        // GET: /Admin/RoleInfo/Delete/5
        [CustomAuthorize]
        public ActionResult Delete(string id)
        {
            RoleInfo roleInfo = new RoleInfo();
            roleInfo.RId = id;
            roleInfoService.deleteRoleInfo(roleInfo);
            return RedirectToAction("Index");
        }

    }
}