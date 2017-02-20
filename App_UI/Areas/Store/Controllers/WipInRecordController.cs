/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInRecordController.cs
// 文件功能描述：
//          在制品库入库履历一览Controller
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
    public class WipInRecordController : Controller
    {
         private IWipStoreService wipStoreService;

         public WipInRecordController(IWipStoreService wipStoreService)
        {
            this.wipStoreService=wipStoreService;
        }
        //
        // GET: /Store/WipInRecord/
       
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限           
            return View(author);
        }

       // [CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_WipInRecordStoreForSearch wipInRecordStoreForSearch,Paging paging)
        {
           // int total;
            var users = wipStoreService.GetWipInRecordBySearchByPage(wipInRecordStoreForSearch, paging);
            //total = ;
            return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        //入库履历一览删除
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult WipInRecordDel(Dictionary<string, string>[] orderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_WipInRecordStoreForTableShow> wipInRecordStoreForTableShowList = new List<VM_WipInRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipInRecordStoreForTableShow wipInRecordStore = new VM_WipInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    wipInRecordStore.PlanID = orderList[i]["PlanID"];
                    wipInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    wipInRecordStore.ProcUnit = orderList[i]["ProcUnit"];
                    wipInRecordStore.BthID = orderList[i]["BthID"];
                    wipInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    wipInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    wipInRecordStore.GiCls = orderList[i]["GiCls"];
                    wipInRecordStore.PdtID = orderList[i]["PdtID"];
                    wipInRecordStore.PdtName = orderList[i]["PdtName"];
                    wipInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    wipInRecordStore.TecnProcess = orderList[i]["TecnProcess"];                   
                    wipInRecordStore.Unit = orderList[i]["Unit"];
                    wipInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    wipInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    wipInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    wipInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipInRecordStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    wipInRecordStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    wipInRecordStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    wipInRecordStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    wipInRecordStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    wipInRecordStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    wipInRecordStoreForTableShowList.Add(wipInRecordStore);
                }

                if (wipInRecordStoreForTableShowList.Count != 0)
                {
                    //wipStoreService.WipInRecordForDel(wipInRecordStoreForTableShowList);
                    //暂用方法************************
                    message = wipStoreService.WipInRecordForDelTest(wipInRecordStoreForTableShowList);

                }
                else
                {

                }
            }
            catch (Exception e)
            {
               //message = e.InnerException.Message;
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
       
        //修改保存入库履历数据
        //[CustomAuthorize]
        public ActionResult WipInRecordSave(Dictionary<string, string>[] orderList)
        {
            bool result = true;
            try
            {
                List<VM_WipInRecordStoreForTableShow> wipInRecordStoreForTableShowList = new List<VM_WipInRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipInRecordStoreForTableShow wipInRecordStore = new VM_WipInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    wipInRecordStore.PlanID = orderList[i]["PlanID"];
                    wipInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    wipInRecordStore.ProcUnit = orderList[i]["ProcUnit"];
                    wipInRecordStore.BthID = orderList[i]["BthID"];
                    wipInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    wipInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    wipInRecordStore.GiCls = orderList[i]["GiCls"];
                    wipInRecordStore.PdtID = orderList[i]["PdtID"];
                    wipInRecordStore.PdtName = orderList[i]["PdtName"];
                    wipInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    wipInRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                    wipInRecordStore.Unit = orderList[i]["Unit"];
                    wipInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    wipInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    wipInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    wipInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipInRecordStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    wipInRecordStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    wipInRecordStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    wipInRecordStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    wipInRecordStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    wipInRecordStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    wipInRecordStoreForTableShowList.Add(wipInRecordStore);
                }

                if (wipInRecordStoreForTableShowList.Count != 0)
                {
                    //wipStoreService.WipInRecordForUpdate(wipInRecordStoreForTableShowList);
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                result = false;
            }
            return result.ToJson();
        }

    }
}
