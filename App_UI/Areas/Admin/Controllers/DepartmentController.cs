using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using Util;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using Extensions;
namespace App_UI.Areas.Admin.Controllers
{
    public class DepartmentController : BaseController
    {
        private IRoleInfoService roleInfoService;
        private IBaseInfoService departmentService;
        public DepartmentController(IRoleInfoService roleInfoService, IBaseInfoService departmentService)
        {
            this.roleInfoService = roleInfoService;
            this.departmentService = departmentService;

        }
        //
        // GET: /Admin/Department/
        [CustomAuthorize]
        public ActionResult Index(int pageIndex = 1)
        {
            var model = departmentService.findAllDepartmentOrderBy().Select(
                                 a =>
                                 new 
                                 {    
                                     UserInfo=a.UserInfo.UName,
                                     a.DeptName,
                                     a.DeptParentId,
                                     a.Tel,
                                     a.DeptId
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
        // GET: /Admin/Department/Details/5
        [CustomAuthorize]
        public ActionResult Details(string id)
        {
            Department department = new Department();
            department = departmentService.getDepartmentById(id);
            ViewBag.ManagerIds = department.UserInfo.UName;
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        //
        // GET: /Admin/Department/Create
        [CustomAuthorize]
        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }
        //
        // POST: /Admin/Department/Create
        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentService.addDepartment(department);
                return RedirectToAction("Index");
            }

            return View(department);
        }
        //
        // GET: 
        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
            var item = new Department();
            if (id != null)
            {
                item =departmentService.getDepartmentById(id);
            }
            ViewBag.ManagerIds = new SelectList(roleInfoService.findAllUserInfoOrderBy(), "UId", "UName", item.ManagerIds);
            ViewBag.DeptIds = new MultiSelectList(departmentService.findAllDepartmentOrderBy(), "DeptId", "DeptName", departmentService.findAllDepartmentOrderById(item.DeptId).Select(a => a.DeptParentId));
            return View(item);
        }


        [HttpPost]
        public JsonResult Get()
        {
            JsonResult jr = new JsonResult();
            IEnumerable<Department> list = departmentService.getAllDepartment();
            var data = from d in list
                       select new
                       {
                           DeptName = d.DeptName,
                           DeptId = d.DeptId
                       };
            jr.Data = data;
            return jr;
        }

        //
        // POST: 
        [CustomAuthorize]
        [HttpPost]
        [Transaction]
        public ActionResult Edit(string id, Department department)
        {
            if (!ModelState.IsValid)
            {
                Edit(id);
                return View(department);
            }
            if (id == null)
            {
                departmentService.addDepartment(department);
            }
            else
            {
                if (department.DeptIds!= null)
                {
                    foreach (var DeptParentId in department.DeptIds)
                    {
                        department.DeptParentId = DeptParentId;
                    }
                }
                departmentService.updateDepartment(department);
            }
            return RedirectToAction("Index");
        }
        //
        // GET: /Admin/Department/Delete/5
        [Transaction]
        [CustomAuthorize]
        public ActionResult Delete(string id)
        {
            Department department = new Department();
            department.DeptId = id;
            departmentService.deleteDepartment(department);
            UDepartment UDepartment = new UDepartment();
            UDepartment.DeptId = id;
            departmentService.deleteUDepartment(UDepartment);
            return RedirectToAction("Index");
        }


    }
}
