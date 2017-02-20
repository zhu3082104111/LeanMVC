using System.Web.Mvc;

namespace App_UI.Areas.Market
{
    public class TechnologyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Market";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Market_default",
                "Market/{controller}/{action}/{id}",
                  new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                  new[] { "App_UI.Areas.Market.Controllers" }
            );
        }
    }
}