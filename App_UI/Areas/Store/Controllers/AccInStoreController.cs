/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInStoreController.cs
// 文件功能描述：
//          附件库待入库一览、附件库入库登录Controller
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
    public class AccInStoreController : Controller
    {
         private IWipStoreService wipStoreService;
         private IAccStoreService accStoreService;

         public AccInStoreController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService=wipStoreService;
            this.accStoreService = accStoreService;
        }
        //
        // GET: /Store/AccInStore/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(Paging paging, VM_AccInStoreForSearch accInStoreForSearch)
        {
            int total;
            var users = accStoreService.GetAccInStoreBySearchByPage(accInStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult AccInLogin(string deliveryOrderID, string isetRepID)
        {
            ViewBag.AccInLogin_deliveryOrderID = deliveryOrderID;
            ViewBag.AccInLogin_isetRepID = isetRepID;
            return View();
        }

        [HttpPost]
        public JsonResult GetAccInLogin(Paging paging, string deliveryOrderID, string isetRepID)
        {
            int total;
            var users = accStoreService.GetAccInStoreForLoginBySearchByPage(deliveryOrderID, isetRepID, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult SaveAccInForLogin(Dictionary<string, string>[] orderList)
        {
            string message = "";
            bool result = true;          
            try
            {
                List<VM_AccInLoginStoreForTableShow> accInLoginStoreForTableShowList = new List<VM_AccInLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_AccInLoginStoreForTableShow accInLoginStore = new VM_AccInLoginStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    accInLoginStore.PrhaOdrID = orderList[i]["PrhaOdrID"];
                    accInLoginStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    accInLoginStore.BthID = orderList[i]["BthID"];
                    accInLoginStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    accInLoginStore.IsetRepID = orderList[i]["IsetRepID"];
                    accInLoginStore.GiCls = orderList[i]["GiCls"];
                    accInLoginStore.PdtName = orderList[i]["PdtName"];
                    accInLoginStore.PdtSpec = orderList[i]["PdtSpec"];
                    accInLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    accInLoginStore.Unit = orderList[i]["Unit"];
                    accInLoginStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    accInLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // accInLoginStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    accInLoginStore.Rmrs = orderList[i]["Rmrs"];
                    accInLoginStore.AccLoginPriceFlg = orderList[i]["AccLoginPriceFlg"];
                    accInLoginStoreForTableShowList.Add(accInLoginStore);
                }

                //修改方法
                if (accInLoginStoreForTableShowList.Count!=0)
                {
                    //accStoreService.AccInForLogin(accInLoginStoreForTableShowList);

                    //***********************************(一期测试)
                    accStoreService.AccInForLoginTest(accInLoginStoreForTableShowList);
                }
                else { 
                
                }
            }
            catch (Exception e)
            {
                result = false;
                message = "请检查输入数据！";
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


    }
}
