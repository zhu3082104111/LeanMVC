
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using Model;
using System;
namespace App_UI.Areas.Store.Controllers
{
    public class DiscardApplicationController : Controller
    {


        private IDiscardService discardService;
        //
        //构造函数
        public DiscardApplicationController(IDiscardService discardService) 
        {
            this.discardService = discardService;
        }

        //
        // GET: /Store/AProductScrapped/

        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 报废和取消报废处理
        /// </summary>
        /// <param name="discardid">物料单号</param>
        /// <param name="state">让步区分</param>
        /// <param name="whId">仓库编号</param>
        /// <param name="partDtID">零件号</param>
        /// <param name="bthID">批次别号</param>
        /// <param name="flg">标识字符（0是报废，1是报废取消）</param>
        /// <returns></returns>
        public ActionResult Updata(VM_StoreDiscardDetailForSearch discardList, string flg, string discardid, string state, string whId, string partDtID, string bthID)
        {
            discardList.DiscardID = discardid;
            discardList.State = state;
            discardList.WhID = whId;
            discardList.PdtID = partDtID;
            discardList.BthID = bthID;
            bool result = true;
            //提示信息
            string message="";
            try
            {
                message = discardService.DiscardForScrappedOrCancel(discardList, flg);
                

            }
            catch (Exception e)
            {
                result = false;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = result,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;
            
            
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="search">查询数据</param>
        /// <returns>查询页面</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_storeDiscardForSearch search)
        {

            IEnumerable<VM_StoreDiscardForTableShow> query = discardService.GetdiscardStoreForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_StoreDiscardForTableShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);
            
        }




    }
}
