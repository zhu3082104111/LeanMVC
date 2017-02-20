using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using App_UI.Areas.Login.Models;
using System.Web.Security;
using Extensions;
using System.Web.Script.Serialization;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Login.Controllers
{
    public class IndexController : BaseController
    {
        //
        // GET: /Login/Index/

        private IRoleInfoService roleInfoService;
        public IndexController(IRoleInfoService roleInfoService)
        {
            this.roleInfoService = roleInfoService;

        }

        public ActionResult Index()
        {
            if (Request.Url != null)
            { }
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                var User = roleInfoService.GetByUserNamePassword(user.UId, user.UPwd);
                //取得用户的权限相关信息，并放入session中
                if (User != null)
                {
                        //FormsAuthentication.RedirectFromLoginPage(User.UId, user.Remember);
                        Session["UserID"] = User.UId;
                        Session["UserName"] = User.UName;
                        Session.Timeout =6000;
                        return RedirectToRoute("Admin_default");
                   /* else
                    {
                        ModelState.AddModelError("", lang.UserDisabled);
                    }*/
                }
                else
                {
                    ModelState.AddModelError("", lang.UserNamePasswordError);
                }
            }
            //Session["UserID"] = user.UId;
            //Index();
            //return RedirectToAction("Index", "Index");
            return View(user);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        //AJAX实现文本框提示
        public void GetAjax(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World");

            string keyword = context.Request.QueryString["keyword"];
            if (keyword != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer(); // 通过JavaScriptSerializer对象的Serialize序列化为["value1","value2",...]的字符串 
                string jsonString = serializer.Serialize(GetFilteredList(keyword));
                context.Response.Write(jsonString); // 返回客户端json格式数据
            }
        }
        private string[] GetFilteredList(string keyword)
        {
            List<UserInfo> userinfo= roleInfoService.findAllUserInfoOrderById(keyword).ToList();
            List<string> nameList = new List<string>();
            for(int i=0;i<userinfo.Count();i++){
            //string uid=userinfo.uid
           
            }
            return nameList.ToArray();
        }

    }
}
