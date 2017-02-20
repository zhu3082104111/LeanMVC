using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Controllers
{   
    /// <summary>
    /// 代东泽 20131010
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 代东泽 20131212
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUserID() 
        {
            object uid=Session["UserID"];
            if (uid != null)
            {
                return uid.ToString();
            }
            else 
            {
                throw new Exception("用户登录超时，请重新登录！");
            }
        }
        /// <summary>
        /// 代东泽 20131212
        /// </summary>
        /// <returns></returns>
        protected string GetLoginUserName()
        {
            object name = Session["UserName"];
            if (name != null)
            {
                return name.ToString();
            }
            else
            {
                throw new Exception("用户登录超时，请重新登录！");
            }
        }
    }
}