using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Login.Controllers
{
    public class ForgotPasswordController : BaseController
    {
        //
        // GET: /Login/ForgotPassword/

        public ActionResult Index()
        {
            return View();
        }

    }
}
