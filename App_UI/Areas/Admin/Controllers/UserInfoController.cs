using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;
using Util;
using Model.Base;


namespace App_UI.Areas.Admin.Controllers
{
    public class UserInfoController : BaseController
    {

        private IRoleInfoService roleInfoService;
        private IBaseInfoService departmentService;
        public UserInfoController(IRoleInfoService roleInfoService, IBaseInfoService departmentService)
        {
            this.roleInfoService = roleInfoService;
            this.departmentService = departmentService;

        }
        //
        // GET: /Admin/UserInfo/
        /// <summary>
        /// 跳转到视图
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        #region 列表方法

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="userForSearch">筛选条件</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost]
        public JsonResult Get(Paging paging,VM_UserInfoForSearch userForSearch)
        {
            int total;
            var users = roleInfoService.GetUsersBySearchByPage(userForSearch, paging, out total);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize]
        public ActionResult Details(string id)
        {
            UserInfo userinfo = roleInfoService.getUserInfoById(id);
            if (userinfo == null)
            {
                return HttpNotFound();
            }
            return View(userinfo);
        }
        #endregion


        #region 数据操作
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="uDept">部门信息</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost]
        public JsonResult Create(UserInfo user, UDepartment uDept)
        {
            user.UId = Guid.NewGuid().ToString();
            uDept.UId = user.UId;
            uDept.Id = Guid.NewGuid().ToString();
            bool result = true;
            try
            {
                roleInfoService.AddUserInfo(user, uDept);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result.ToJson();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="uDept">关联部门</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost]
        public JsonResult Delete(UserInfo user, UDepartment uDept)
        {
            bool r = true;
            try
            {
                roleInfoService.DeleteUserInfo(user, uDept);
            }
            catch (Exception e)
            {
                r = false;
            }
            return r.ToJson();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="uDept">关联部门</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost]
        public JsonResult Edit(UserInfo user, UDepartment uDept)
        {
            bool result = true;
            try
            {
                roleInfoService.UpdateUserInfo(user, uDept);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result.ToJson();
        }

        /// <summary>
        /// 返回报表请求的数据
        /// </summary>
        [HttpPost]
        public void TransDateToReport()
        {
            //报表路径
            this.HttpContext.Session["ReportPath"] = System.Web.HttpContext.Current.Server.MapPath("~/") + "Rpts//CrUserList.rpt";
            //报表数据源获取
            List<UserInfo> UserList = roleInfoService.findAllUserInfo().ToList();
            List<UserInfo> userTemp=new List<UserInfo>();
            foreach (UserInfo element in UserList)
            {
                userTemp.Add(element.DeepClone<UserInfo>());
            }
            this.HttpContext.Session["ReportSource"] = userTemp;
        }

        #endregion


       

        #region 下面的可以不看，无联系
        

        //
        // GET: /Admin/UserInfo/Create
        [CustomAuthorize]
        /*public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }
        //
        // POST: /Admin/UserInfo/Create
       [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                roleInfoService.addUserInfo(userInfo);
                return RedirectToAction("Index");
            }

            return View(userInfo);
        }*/
        //
        // GET: /Platform/SysRole/Edit/5
        /*[CustomAuthorize]
         public ActionResult Edit(string id)
         {
             var item = new UserInfo();
             if (id!=null)
             {
                 item = roleInfoService.getUserInfoById(id);
             }
             List<SelectListItem> items = new List<SelectListItem>();
             items.Add(new SelectListItem() { Text = "男", Value = "1", Selected = false });
             items.Add(new SelectListItem() { Text = "女", Value = "0", Selected = false });
             //this.ViewData["TLimitList"] = items;
             //this.ViewData["TLimit"] = item.Sex;//设置选择项  
             ViewBag.RIds = new MultiSelectList(roleInfoService.findAllRoleInfoOrderBy(), "RId", "RName", item.URoleInfo.Select(a => a.RId));     
       
             ViewBag.Sexs = new SelectList(items, item.UserInfos.Select(a => a.Sex));         
             ViewBag.DeptIds = new MultiSelectList(departmentService.findAllDepartmentOrderBy(), "DeptId", "DeptName", item.UDepartment.Select(a => a.DeptId));
          
             return View(item);
         }

         //
         // POST: /Platform/SysUser/Edit/5
         [CustomAuthorize]
         [HttpPost]
         [Transaction]
         public ActionResult Edit(string id, UserInfo collection)
         {
             if (!ModelState.IsValid)
             {
                 Edit(id);
                 return View(collection);
             }
             if (id == null)
             {
                 roleInfoService.addUserInfo(collection);
                 uroleInfo(collection);
             }
             else
             {
                 roleInfoService.updateUserInfo(collection);
                 uroleInfo(collection);
             }
             return RedirectToAction("Index");
         }*/

        public void uroleInfo(UserInfo userInfo)
        {
            //清除原有数据
            URoleInfo uRoleInfo = new URoleInfo();
            uRoleInfo.UId = userInfo.UId;
            roleInfoService.deleteURoleInfo(uRoleInfo);

            UDepartment uDepartment = new UDepartment();
            uDepartment.UId = userInfo.UId;
            roleInfoService.deleteUDepartment(uDepartment);


            //添加数据

            if (userInfo.RIds != null)
            {
                foreach (var RId in userInfo.RIds)
                {
                    roleInfoService.addURoleInfo(new URoleInfo
                    {
                        Id = Guid.NewGuid().ToString(),
                        UId = userInfo.UId,
                        RId = RId,
                        Enabled = true
                    });
                }
            }

            //添加数据
            if (userInfo.DeptIds != null)
            {
                foreach (var DeptId in userInfo.DeptIds)
                {
                    roleInfoService.addUDepartment(new UDepartment
                    {
                        Id = Guid.NewGuid().ToString(),
                        UId = userInfo.UId,
                        DeptId = DeptId
                    });
                }
            }

        }
        //
        // GET: /Admin/UserInfo/Delete/5
        [CustomAuthorize]
        public ActionResult Delete(string id)
        {
            UserInfo userinfo = new UserInfo();
            userinfo.UId = id;
            roleInfoService.deleteUserInfo(userinfo);

            return RedirectToAction("Index");
        }

    }
        #endregion
}
