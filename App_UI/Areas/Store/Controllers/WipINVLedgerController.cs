using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Store.Controllers
{
    public class WipINVLedgerController : Controller
    {
        private IWipStoreService wipStoreService;
        public WipINVLedgerController(IWipStoreService wipStoreService)
        {
            this.wipStoreService=wipStoreService;
        }
        //
        // GET: /Store/WipINVLedger/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(Paging paging, WipStore wipStore)
        {
            int total;
            var users = wipStoreService.GetWipStoreBySearchByPage(wipStore, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

    }
}
