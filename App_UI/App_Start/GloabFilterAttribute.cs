using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.App_Start
{
    public class GloabFilterAttribute:ActionFilterAttribute
    {
        public GloabFilterAttribute() 
        {
        
        }

          // 摘要:
        //     在执行操作方法后由 ASP.NET MVC 框架调用。
        //
        // 参数:
        //   filterContext:
        //     筛选器上下文。
        public override void OnActionExecuted(ActionExecutedContext filterContext) 
        {


        
        }
        // 摘要:
        //     在执行操作方法之前由 ASP.NET MVC 框架调用。
        //
        // 参数:
        //   filterContext:
        //     筛选器上下文。
        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {

            string userId = (string)filterContext.HttpContext.Session["UserID"];
            if (userId == null)
            {
                //filterContext.RouteData 

            }

            Uri uri = filterContext.HttpContext.Request.UrlReferrer;
            if (uri == null)
            {

            }
        }
        //
        // 摘要:
        //     在执行操作结果后由 ASP.NET MVC 框架调用。
        //
        // 参数:
        //   filterContext:
        //     筛选器上下文。
        public override void OnResultExecuted(ResultExecutedContext filterContext) 
        {
        
        }
        //
        // 摘要:
        //     在执行操作结果之前由 ASP.NET MVC 框架调用。
        //
        // 参数:
        //   filterContext:
        //     筛选器上下文。
        public override void OnResultExecuting(ResultExecutingContext filterContext) 
        {
        
        }

       
    }
}