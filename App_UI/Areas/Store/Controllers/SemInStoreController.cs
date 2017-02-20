/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemInStoreController.cs
// 文件功能描述：
//          待入库一览画面用Controller
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
    /// 待入库一览画面
    /// </summary>
    public class SemInStoreController : Controller
    {
        // 待入库一览的Service
        private ISemStoreService semStoreService;
        // 调用的Service的声明
        public SemInStoreController(ISemStoreService semStoreService)
        {
            this.semStoreService = semStoreService;
        }
        
        //  页面初始化
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        
        
        /// <summary>
        /// 页面显示数据的获取
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="searchCondition">查询条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(Paging paging, VM_SemInStoreForSearch searchCondition)
        {
            int total;
            var users = semStoreService.GetSemStoreBySearchByPage(searchCondition, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }


        ////[CustomAuthorize]
        //[HttpPost]
        //public JsonResult Edit(string id)
        //{
        //    bool result = true;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            //return Content("请选择您要删除的数据");
        //        }
        //        var deleteID = id.Split(',');
        //        //定义数组存放需要删除的ID
        //        List<string> list = new List<string>();
        //        foreach (var Dsid in deleteID)
        //        {
        //            list.Add(Dsid);
        //        }
        //        //然后执行删除的方法删除数据
        //        //wipStoreService.UpdateWipStore(list);
        //    }
        //    catch (Exception e)
        //    {
        //        result = false;
        //    }

        //    return result.ToJson();
        //}

        /// <summary>
        /// 待入库登陆画面初始化
        /// </summary>
        /// <param name="deliveryOrderID"></param>
        /// <param name="isetRepID"></param>
        /// <returns></returns>
        public ActionResult SemInLogin(string deliveryOrderID, string isetRepID)
        {
            ViewBag.SemInLogin_deliveryOrderID = deliveryOrderID;
            ViewBag.SemInLogin_isetRepID = isetRepID;
            return View();
        }

        /// <summary>
        /// 待入库数据获取
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="deliveryOrderID"></param>
        /// <param name="isetRepID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSemInLogin(Paging paging, string deliveryOrderID, string isetRepID)
        {
            int total;
            var users = semStoreService.GetSemInStoreForLoginBySearchByPage(deliveryOrderID, isetRepID, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 待入库数据保存
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public ActionResult SaveSemInForLogin(Dictionary<string, string>[] orderList)
        {
            bool result = true;
            try
            {
                List<VM_SemInLoginStoreForTableShow> semInLoginStoreForTableShowList = new List<VM_SemInLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_SemInLoginStoreForTableShow semInLoginStore = new VM_SemInLoginStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    semInLoginStore.PlanID = orderList[i]["PlanID"];
                    semInLoginStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    semInLoginStore.BthID = orderList[i]["BthID"];
                    semInLoginStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    semInLoginStore.IsetRepID = orderList[i]["IsetRepID"];
                    semInLoginStore.GiCls = orderList[i]["GiCls"];
                    semInLoginStore.PdtName = orderList[i]["PdtName"];
                    semInLoginStore.PdtSpec = orderList[i]["PdtSpec"];
                    semInLoginStore.TecnProcess = orderList[i]["TecnProcess"];
                    semInLoginStore.Unit = orderList[i]["Unit"];
                    semInLoginStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    semInLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    semInLoginStore.Rmrs = orderList[i]["Rmrs"];
                    semInLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    semInLoginStore.ProScrapQty = Convert.ToDecimal(orderList[i]["ProScrapQty"]);
                    semInLoginStore.ProMaterscrapQty = Convert.ToDecimal(orderList[i]["ProMaterscrapQty"]);
                    semInLoginStore.ProOverQty = Convert.ToDecimal(orderList[i]["ProOverQty"]);
                    semInLoginStore.ProLackQty = Convert.ToDecimal(orderList[i]["ProLackQty"]);
                    semInLoginStore.ProTotalQty = Convert.ToDecimal(orderList[i]["ProTotalQty"]);
                    semInLoginStore.OsSupProFlg = orderList[i]["OsSupProFlg"];

                    semInLoginStoreForTableShowList.Add(semInLoginStore);
                }

                //修改方法
                if (semInLoginStoreForTableShowList.Count != 0)
                {
                    semStoreService.SemInStoreForDelTest(semInLoginStoreForTableShowList);
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
