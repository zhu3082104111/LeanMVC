using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.App_Start;

namespace App_UI.Areas.Admin.Controllers
{
    public class SysStatisticController : BaseController
    {
        //
        // GET: /Admin/SysStatistic/
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
