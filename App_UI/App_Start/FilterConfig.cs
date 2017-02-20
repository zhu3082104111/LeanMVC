using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using App_UI.Helper;
using App_UI.App_Start;

namespace App_UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GloabFilterAttribute());
            filters.Add(new LogErrorAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UserAuthorizeAttribute { Areas = new List<string> { "Platform", "Admin", "Base", "Market", "Produce", "Purchase", "Finance", "Quality", "Technology", "Store", "Human", "Common" } });
          
        }
    }
}