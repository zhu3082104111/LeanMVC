/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInStoreController.cs
// 文件功能描述：
//          在制品库待入库一览、在制品库入库登录Controller
//      
// 修改履历：2013/11/05 杨灿 新建
/*****************************************************************************/
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
    public class WipInStoreController : Controller
    {
        private IWipStoreService wipStoreService;
        private IAccStoreService accStoreService;

        public WipInStoreController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService = wipStoreService;
            this.accStoreService = accStoreService;
        }
        //
        // GET: /Store/WipInStore/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_WipInStoreForSearch wipInStoreForSearch, Paging paging)
        {
           // int total;
            var users = wipStoreService.GetWipInStoreBySearchByPage(wipInStoreForSearch, paging);
            //total = ;
            return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult WipInLogin(string deliveryOrderID, string isetRepID)
        {
            ViewBag.WipInLogin_deliveryOrderID = deliveryOrderID;
            ViewBag.WipInLogin_isetRepID = isetRepID;
            return View();
        }

        [HttpPost]
        public JsonResult GetWipInLogin(Paging paging, string deliveryOrderID, string isetRepID)
        {
            int total;
            var users = wipStoreService.GetWipInStoreForLoginBySearchByPage(deliveryOrderID, isetRepID, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult SaveWipInForLogin(Dictionary<string, string>[] orderList)
        {
            bool result = true;
            try
            {
                List<VM_WipInLoginStoreForTableShow> wipInLoginStoreForTableShowList = new List<VM_WipInLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipInLoginStoreForTableShow wipInLoginStore = new VM_WipInLoginStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    wipInLoginStore.PlanID = orderList[i]["PlanID"];
                    wipInLoginStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    wipInLoginStore.ProcUnit = orderList[i]["ProcUnit"];
                    wipInLoginStore.BthID = orderList[i]["BthID"];
                    wipInLoginStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    wipInLoginStore.IsetRepID = orderList[i]["IsetRepID"];
                    wipInLoginStore.GiCls = orderList[i]["GiCls"];
                    wipInLoginStore.PdtName = orderList[i]["PdtName"];
                    wipInLoginStore.PdtSpec = orderList[i]["PdtSpec"];
                    wipInLoginStore.TecnProcess = orderList[i]["TecnProcess"];
                    wipInLoginStore.Unit = orderList[i]["Unit"];
                    wipInLoginStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    wipInLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    wipInLoginStore.Rmrs = orderList[i]["Rmrs"];
                    wipInLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipInLoginStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    wipInLoginStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    wipInLoginStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    wipInLoginStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    wipInLoginStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    wipInLoginStore.OsSupProFlg = orderList[i]["OsSupProFlg"];

                    // wipInLoginStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;

                    wipInLoginStoreForTableShowList.Add(wipInLoginStore);
                }
                //（暂用修改方法）
                // wipStoreService.UpdateWipStore(wipStore);

                //修改方法
                if (wipInLoginStoreForTableShowList.Count != 0)
                {
                    //wipStoreService.WipInForLogin(wipInLoginStoreForTableShowList);//假注释

                    //***********************************(一期测试)
                    wipStoreService.WipInForLoginTest(wipInLoginStoreForTableShowList);
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                result = false;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = result,
                Message = "保存成功！"
            };
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
