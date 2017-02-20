/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemInRecordController.cs
// 文件功能描述：
//          半成品库入库履历一览画面用Controller
//
// 修改履历： 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;


namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 半成品库入库履历一览画面用Controller
    /// </summary>
    public class SemInRecordController : Controller
    {
        // 半成品库入库履历一览画面的调用的Service
        private ISemStoreService semStoreService;
        /// <summary>
        /// Service的实现
        /// </summary>
        /// <param name="semStoreService"></param>
        public SemInRecordController(ISemStoreService semStoreService)
        {
            this.semStoreService = semStoreService;
        }

        // 画面初始化
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限           
            return View(author);
        }

        //[CustomAuthorize]
        /// <summary>
        /// 半成品库入库履历一览画面数据的获取
        /// </summary>
        /// <param name="semInRecordStoreForSearch">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns></returns>
        /// //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_SemInRecordStoreForSearch semInRecordStoreForSearch, Paging paging)
        {
            int total;
            var users = semStoreService.GetSemInRecordBySearchByPage(semInRecordStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        //入库履历一览删除
        //[CustomAuthorize]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SemInRecordDel(Dictionary<string, string>[] orderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_SemInRecordStoreForTableShow> semInRecordStoreForTableShowList = new List<VM_SemInRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_SemInRecordStoreForTableShow semInRecordStore = new VM_SemInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    semInRecordStore.PlanID = orderList[i]["PlanID"];
                    semInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    semInRecordStore.BthID = orderList[i]["BthID"];
                    semInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    semInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    semInRecordStore.GiCls = orderList[i]["GiCls"];
                    semInRecordStore.PdtID = orderList[i]["PdtID"];
                    semInRecordStore.PdtName = orderList[i]["PdtName"];
                    semInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    semInRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                    semInRecordStore.Unit = orderList[i]["Unit"];
                    semInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    semInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    semInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    semInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    semInRecordStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    semInRecordStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    semInRecordStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    semInRecordStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    semInRecordStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    semInRecordStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    semInRecordStoreForTableShowList.Add(semInRecordStore);
                }

                if (semInRecordStoreForTableShowList.Count != 0)
                {
                    //wipStoreService.WipInRecordForDel(wipInRecordStoreForTableShowList);
                    //暂用方法************************
                    message = semStoreService.SemInRecordForDelTest(semInRecordStoreForTableShowList);

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public ActionResult SemInRecordSave(Dictionary<string, string>[] orderList)
        {
            bool result = true;
            try
            {
                List<VM_SemInRecordStoreForTableShow> semInRecordStoreForTableShowList = new List<VM_SemInRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_SemInRecordStoreForTableShow semInRecordStore = new VM_SemInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    semInRecordStore.PlanID = orderList[i]["PlanID"];
                    semInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    semInRecordStore.BthID = orderList[i]["BthID"];
                    semInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    semInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    semInRecordStore.GiCls = orderList[i]["GiCls"];
                    semInRecordStore.PdtID = orderList[i]["PdtID"];
                    semInRecordStore.PdtName = orderList[i]["PdtName"];
                    semInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    semInRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                    semInRecordStore.Unit = orderList[i]["Unit"];
                    semInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    semInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    semInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    semInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    semInRecordStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    semInRecordStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    semInRecordStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    semInRecordStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    semInRecordStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    semInRecordStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    semInRecordStoreForTableShowList.Add(semInRecordStore);
                }

                if (semInRecordStoreForTableShowList.Count != 0)
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

