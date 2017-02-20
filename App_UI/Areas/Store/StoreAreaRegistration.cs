using System.Web.Mvc;

namespace App_UI.Areas.Store
{
    public class StoreAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Store";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Store_default",
                "Store/{controller}/{action}/{id}",
                  new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                  new[] { "App_UI.Areas.Store.Controllers" }
            );
        }
    }
}
