using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Configuration;
using Util;
using Extensions;
using App_UI.Areas.Controllers;
using App_UI.App_Start;

namespace App_UI.Areas.Admin.Controllers
{
    public class WebConfigAppSettingController : BaseController
    {
        //
        // GET: /Admin/WebConfigAppSetting/
        [CustomAuthorize]
        public ActionResult Index()
        {
            NameValueCollection model = ConfigurationManager.AppSettings;
            return View(model);
        }
        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
            ViewBag.id = id;
            ViewBag.value = ConfigurationManager.AppSettings[id];
            return View();
        }
        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(string id, string value)
        {
            try
            {
                var webConfig = new WebAppSetting();
                webConfig.Modify(id, value);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            Edit(id);
            return View();
        }

    }
}
