// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：LogErrorAttribute.cs
// 文件功能描述：日志异常处理过滤器
// 
// 创建标识：代东泽 20131220
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.App_Start
{
    public class LogErrorAttribute:FilterAttribute, IExceptionFilter
    {

        /// <summary>
        ///代东泽 20131220
        ///过滤方法
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            string innerMsg = "无";
            if(filterContext.Exception.InnerException!=null){
                innerMsg=filterContext.Exception.InnerException.Message;
            }
            string stack=filterContext.Exception.StackTrace;
            filterContext.ExceptionHandled = true;
            ViewResult rs= new ViewResult();rs.ViewName = "ErrorException";
            rs.ViewData["Msg"] = filterContext.Exception.Message;
            rs.ViewData["InnerMsg"] = innerMsg;
            rs.ViewData["Stack"] = stack.Substring(0,stack.IndexOf("行号")+5);
            rs.ViewData["Path"] = filterContext.HttpContext.Request.Path;
            filterContext.Result =rs;
        }

    }
}