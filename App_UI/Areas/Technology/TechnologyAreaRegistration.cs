using System.Web.Mvc;

namespace App_UI.Areas.Technology
{
    public class TechnologyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Technology";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Technology_default",
                "Technology/{controller}/{action}/{id}",
                  new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                  new[] { "App_UI.Areas.Technology.Controllers" }
            );
        }
    }
}