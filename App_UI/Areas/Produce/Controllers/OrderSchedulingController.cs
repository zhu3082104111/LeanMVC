/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：OrderSchedlingController.cs
// 文件功能描述：订单排产
// 
// 创建标识：201310 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using Extensions;
using Model;
using BLL;

namespace App_UI.Areas.Produce.Controllers
{
    public class OrderSchedulingController : BaseController
    {

        private IOrderSchedulingService OrderSchedulingService;

        public OrderSchedulingController(IOrderSchedulingService OrderSchedulingService)
        {
            this.OrderSchedulingService=OrderSchedulingService;
        }

        //
        // GET: /Produce/OrderScheduling/

        public ActionResult Index()
        {
            return View(1);
        }


        [HttpPost]
        public JsonResult GetData(VM_OrderSchedulingSearch searchCondition, Paging pagex)
        {
            IEnumerable<VM_OrderSchedulingShow> finalData = OrderSchedulingService.GetSchedulOrder(searchCondition, pagex);
            if (finalData == null)
            {
                finalData = new List<VM_OrderSchedulingShow>();
            }
            return finalData.ToJson(pagex.total);
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
                // TODO: Add insert logic here

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

        //
        // POST: /Produce/InProcessingPlan/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
