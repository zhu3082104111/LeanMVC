/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：OrderAcceptController.cs
// 文件功能描述：订单指示接收
// 
// 创建标识：201310 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class OrderAcceptController : BaseController
    {
      
        private IOrderAcceptService orderAcceptService;

        public OrderAcceptController(IOrderAcceptService orderAcceptService)
        {
            this.orderAcceptService = orderAcceptService;
        }

        //
        // GET: /Produce/OrderAccept/
        public ActionResult Index()
        {
            return View(1);
        }

        [HttpPost]
        public JsonResult Get(VM_OrderAcceptSearch searchConditon,Paging pagex)
        {

            IEnumerable<VM_OrderAcceptShow> query = orderAcceptService.GetPlanNotAccept(searchConditon, pagex);
            if (query == null)
            {
                query = new List<VM_OrderAcceptShow>();
                pagex.total = 0;
            }
           //JsonResult js= query.ToJson(pagex.total);
           //js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return query.ToJson(pagex.total);
        }

        #region 数据操作

        //
        // GET: /Produce/InProcessingPlan/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Produce/InProcessingPlan/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                orderAcceptService.TempInitDate();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Produce/InProcessingPlan/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        /// <summary>
        /// 接受所有客户订单
        /// </summary>
        /// <param name="clientOrderIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AcceptOrders(string clientOrderIDs)
        {
            bool isAllSuccess = true;//哨兵
            List<string> orderList = new List<string>();
            try
            {
                string[] clientOrderID = clientOrderIDs.Split(',');
                foreach (var item in clientOrderID)
                {
                    orderList.Add(item);
                }
                orderAcceptService.AcceptPlan(orderList);
            }
            catch
            {
                isAllSuccess = false;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new { Result = isAllSuccess };
            jr.ContentType = "text/html";
            return jr;
        }

        //
        // GET: /Produce/InProcessingPlan/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/InProcessingPlan/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
