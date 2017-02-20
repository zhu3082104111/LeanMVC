// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：WorkticketController.cs
// 文件功能描述：加工工票控制器
// 
// 创建标识：代东泽 20131126
//
// 修改标识：代东泽 20131120
// 修改描述：此功能后期开发
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using App_UI.Areas.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Extensions;
using Model.Produce;
using BLL;
namespace App_UI.Areas.Produce.Controllers
{
    public class WorkticketController : BaseController
    {
        private IWorkticketService workticketService;

        public WorkticketController(IWorkticketService workticketService) 
        {
            this.workticketService = workticketService;
        }

        //
        // GET: /Produce/Workticket/

        public ActionResult Index()
        {

            return View(1);
        }

        [HttpPost]
        public ActionResult Get(Paging paging, VM_ProduceBillForSrarch search) 
        {
         
           /* int total = 0;

            IEnumerable<VM_ProduceBillForTableShow> query = workticketService.GetWorkticketsForSearch(search, paging);
            total = paging.total;
            //var obj= query.AsQueryable().ToPageList("OrderID asc", paging);


            //var users = pickingMaterialService.GetMaterials(search, paging, out total);
            return query.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器*/
            return View();
        }

        //
        // GET: /Produce/Workticket/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Do()
        {
            return View();
        }


        //
        // GET: /Produce/Workticket/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Produce/Workticket/Create

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
        // GET: /Produce/Workticket/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Produce/Workticket/Edit/5

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
        // GET: /Produce/Workticket/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/Workticket/Delete/5

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
    }
}
