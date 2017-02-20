// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：FAWorkticketController.cs
// 文件功能描述：总装小工票控制器
// 
// 创建标识：代东泽 20131120
//
// 修改标识：代东泽 20131126
// 修改描述：此功能二期开发
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using BLL;
using Extensions;
using Model.Produce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Produce.Controllers
{
    public class FAWorkticketController : Controller
    {

        private IWorkticketService workticketService;

        public FAWorkticketController(IWorkticketService workticketService) 
        {
            this.workticketService = workticketService;
        }
        //
        // GET: /Produce/FAWorkticket/

        public ActionResult Index()
        {
            return View(1);
        }

        [HttpPost]
        public JsonResult Get(Paging paging, VM_AssemSmallBillForSearch search) 
        {
            
            /*IEnumerable<VM_AssemSmallBillForTableShow> query = workticketService.GetAssemSmallBillsForSearch(search, paging);
            if (query == null) {
                query = new List<VM_AssemSmallBillForTableShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);*/
            return null;
        }
        //
        // GET: /Produce/FAWorkticket/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Produce/FAWorkticket/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Produce/FAWorkticket/Create

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
        // GET: /Produce/FAWorkticket/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Produce/FAWorkticket/Edit/5

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
        // GET: /Produce/FAWorkticket/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/FAWorkticket/Delete/5

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
