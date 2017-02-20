using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL.Common;
using Extensions;

namespace App_UI.Areas.Common.Controllers
{
    public class AutoCompleteController : BaseController
    {
        private IAutoSearchService autoSearchService;

        public AutoCompleteController(IAutoSearchService autoSearchService)
        {
            this.autoSearchService = autoSearchService;
        }

        //
        // GET: /Common/AutoComplete/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Get(Searcher searcher)
        {
            List<Searcher> list = autoSearchService.GetBySearcher(searcher);
            return list.ToJson(0);
        }
        
    }

}
