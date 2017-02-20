using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;
using System.Web.Mvc;
using System.Collections.Generic;


namespace App_UI.Areas.Market.Controllers
{
    public class MarketClientInformationController : BaseController
    {
        private IMarketClientInformationService iMCIS;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paraIMarketClientInformationService"></param>
        public MarketClientInformationController(IMarketClientInformationService paraIMarketClientInformationService) 
        {
            this.iMCIS = paraIMarketClientInformationService;
        }

        //
        // GET: /Market/MarketClientInformation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowMarketClientInfoDialog()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetMarketClientInfoList(MarketClientInformation paraMCI, Paging paraPage)
        {
            int total = 0;
            IEnumerable<MarketClientInformation> MarketClientInformationIE = iMCIS.GetMarketClientInfoListService(paraMCI, paraPage);
            total = paraPage.total;

            //
            return MarketClientInformationIE.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

    }// end class
}
