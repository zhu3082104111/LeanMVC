using System.Web.Mvc;

namespace App_UI.Areas.Purchase
{
    public class PurchaseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Purchase";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Purchase_default",
                "Purchase/{controller}/{action}/{id}",
                  new { controller = "Index", action = "Index", id = UrlParameter.Optional },
                  new [] { "App_UI.Areas.Purchase.Controllers" }
            );
        }
    }
}
