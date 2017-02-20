using System.Web.Mvc;

namespace App_UI.Areas.Platform
{
    public class PlatformAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Platform";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Platform_default",
                "Platform/{controller}/{action}/{id}",
                new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                new[] { "App_UI.Areas.Platform.Controllers" }
            );
        }
    }
}

