using System;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;

namespace App_UI.Helper
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {

        private  IRoleInfoService roleInfoService;
        private  UserInfo userInfo;

        public UserAuthorizeAttribute()
        {
             //userInfo = DependencyResolver.Current.GetService<UserInfo>();
             roleInfoService = DependencyResolver.Current.GetService<IRoleInfoService>();

            //_userInfo = ObjectFactory.GetInstance<IUserInfo>();
            //_sysRoleService = ObjectFactory.GetInstance<ISysRoleService>();
            //_sysControllerSysActionService = ObjectFactory.GetInstance<ISysControllerSysActionService>();
            //_sysUserLogService = ObjectFactory.GetInstance<ISysUserLogService>();
        }

        public IList<string> Areas { private get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
         {
          
            var area = (string)httpContext.Request.RequestContext.RouteData.DataTokens["area"];
            var action = (string)httpContext.Request.RequestContext.RouteData.Values["action"];
            var controller = (string)httpContext.Request.RequestContext.RouteData.Values["controller"];

            //是否对该Area区域进行身份验证
            if (Areas.Contains(area))
            {
                //判断是否用户已登录
                //if (httpContext.User.Identity.IsAuthenticated)
                //{
                    //判断用户是否有该区域访问的权限
                    //如果权限中有该区域的任何一个操作既可以进行访问
                    //默认Index控制下的全部内容仅验证是否登录
                    string userId = (string)httpContext.Session["UserID"];
                    if (userId != null)
                    {
                       
                        //获取当前登录用户并根据session中储存的该用户权限信息 验证用户对URL的权限,就是用户对menu的权限

                        /* userInfo = roleInfoService.getUserInfoById(userId);
                         //同步记录用户访问记录
                         //var recordId = (string)httpContext.Request.RequestContext.RouteData.Values["id"];
                         //var chainInfo = roleInfoService.findAllChainInfoOrderBy().FirstOrDefault(a => a.MenuInfo.ControllerName.Equals(controller) && a.EditInfo.ActionName.Equals(action));

                         if (chainInfo != null && httpContext.Request.Url != null)
                         {
                             /*UserInfoLog UserInfoLog = new UserInfoLog();

                             UserInfoLog.Id = Guid.NewGuid().ToString();
                             UserInfoLog.Url = httpContext.Request.Url.AbsolutePath;
                             UserInfoLog.Ip = httpContext.Request.ServerVariables["Remote_Addr"];
                             UserInfoLog.ChainInfoId = chainInfo.Id;
                             UserInfoLog.RecordId = recordId;
                             UserInfoLog.SysUserId = userInfo.UId;
                             UserInfoLog.EnterpriseId = userInfo.Enabled.ToString();
                             roleInfoService.addUserInfoLog(UserInfoLog);*/

                        //}*/
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                    /*if (controller == "Index")
                    {
                        return true;
                    }*/
                //}
                //return false;
            }
            return true;
         }
    }
}