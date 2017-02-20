using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model.Technology;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App_UI.Areas.Technology.Controllers
{
    public class ProdInfoController : BaseController
    {
        private IProdInfoService iProdInfoService;

        public ProdInfoController(IProdInfoService paraIProdInfoService) 
        {
            this.iProdInfoService = paraIProdInfoService;
        }

        //
        // GET: /Technology/ProductInfomation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowProdInfoDialog(string singleSelect)
        {
            if (string.IsNullOrEmpty(singleSelect) == false)
            {
                ViewBag.singleSelect = singleSelect.ToLower();
            }
            else
            {
                ViewBag.singleSelect = false;
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetProdInfoList(VM_ProdInfoForSearchTableProdInfo paraPIFSTPI, Paging paraPage)
        {
            int total = 0;
            IEnumerable<VM_ProdInfoForTableProdInfo> iEnumerablePIFTPI = iProdInfoService.GetProdInfoListService(paraPIFSTPI, paraPage);
            total = paraPage.total;
            //
            return iEnumerablePIFTPI.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }


    }
}
