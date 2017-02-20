using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.App_Start
{
    public class CustomRazorViewEngine : RazorViewEngine
    {
        public CustomRazorViewEngine()
            : base()
        {
            

            //Area视图路径其中{2},{1},{0}分别代表Area名，Controller名，Action名

            base.AreaViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };

            //Area模版路径

            base.AreaMasterLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };

            //Area的分部视图路径

            base.AreaPartialViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.cshtml", "~/Areas/{2}/Views/{1}/{0}.vbhtml", "~/Areas/{2}/Views/Shared/{0}.cshtml", "~/Areas/{2}/Views/Shared/{0}.vbhtml" };

            //主视图路径

            base.ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //主模版路径

            base.MasterLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            //主分部视图路径

            base.PartialViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" };

            base.FileExtensions = new string[] { "cshtml", "vbhtml" };

        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {

            return base.CreatePartialView(controllerContext, partialPath);

        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {

            return base.CreateView(controllerContext, viewPath, masterPath);

        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {

            return base.FileExists(controllerContext, virtualPath);

        }

    }
}