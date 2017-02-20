using Extensions;
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;

namespace App_UI.App_Start
{
    /// <summary>
    /// 对请求进行权限验证
    /// </summary>
    public class CustomAuthorizeAttribute:ActionFilterAttribute
    {
        private IRoleInfoService roleInfoService;      
        private UserInfo userInfo;

        
         public CustomAuthorizeAttribute()
         {
             //
         } 
         public override void OnActionExecuting(ActionExecutingContext filterContext) 
         {
             roleInfoService = DependencyResolver.Current.GetService<IRoleInfoService>();
             var area = (string)filterContext.HttpContext.Request.RequestContext.RouteData.DataTokens["area"];
             var action = (string)filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"];
             var controller = (string)filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"];

                userInfo = roleInfoService.getUserInfoById((string)filterContext.HttpContext.Session["UserID"]);
                if (!roleInfoService.CheckSysUserSysRoleSysControllerSysActions(userInfo.Enabled, userInfo.UId, area, action, controller))
                {
                    throw new Exception("没有权限！请联系系统管理员进行权限分配！");
                }
                         
      

         } 

   
   
    }
}