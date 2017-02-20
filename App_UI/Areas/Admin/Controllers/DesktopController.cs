using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Admin.Controllers
{
    public class DesktopController : BaseController
    {
        //
        // GET: /Admin/Desktop/

        public ActionResult Index()
        {
            return View();
        }

    }
}
