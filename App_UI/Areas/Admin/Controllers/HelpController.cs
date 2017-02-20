using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using BLL;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Admin.Controllers
{
    public class HelpController : BaseController
    {
        private INoticeService noticeService;
        private IRoleInfoService roleInfoService;
        public HelpController(INoticeService noticeService, IRoleInfoService roleInfoService)
        {
            
            this.noticeService=noticeService;
            this.roleInfoService = roleInfoService;

        }
        //
        // GET: /Admin/Help/
        public ActionResult Index(string keyword)
        {

            var model = noticeService.getAllNotice();
            if (!string.IsNullOrEmpty(keyword))
            {
             
            }

            return View(model);
        }

    }
}
