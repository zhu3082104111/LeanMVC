using System.Web.Mvc;

namespace App_UI.Areas.Produce
{
    public class ProduceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Produce";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Produce_default",
                "Produce/{controller}/{action}/{id}",
                  new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                  new[] { "App_UI.Areas.Produce.Controllers" }
            );
        }
    }
}
