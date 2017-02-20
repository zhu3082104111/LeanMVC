using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{   
    /// <summary>
    ///成品待交仓一览画面的Controller类 
    /// </summary>
    public class PWaitingWarehouseListController : Controller
    {
        //成品待交仓一览画面的Service——潘军
        private IProductWarehouseService iproductwarehouseservice;
        /// <summary>
        /// 成品待交仓一览——潘军
        /// </summary>
        /// <param name="iproductwarehouseservice"></param>
        public PWaitingWarehouseListController(IProductWarehouseService iproductwarehouseservice) 
        {
            this.iproductwarehouseservice = iproductwarehouseservice;
        }
        
        // GET: /Produce/PWaitingWarehouseList/
        /// <summary>
        /// 跳转成品待交仓一览画面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取列表——潘军
        /// </summary>
        /// <param name="searchCondition">分页参数类</param>
        /// <param name="paging">筛选条件</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(VM_PWaitingWarehouseListForSearch searchCondition, Paging paging)
        {
           //得到将要在页面上显示的数据0
            var PWaitingWarehouseLists = iproductwarehouseservice.GetPWaitingWarehouseListByPage(searchCondition, paging);
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return PWaitingWarehouseLists.ToJson(paging.total);
        }
        
        // GET: /Produce/PWaitingWarehouseList/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Produce/PWaitingWarehouseList/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Produce/PWaitingWarehouseList/Create

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
        // GET: /Produce/PWaitingWarehouseList/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Produce/PWaitingWarehouseList/Edit/5

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
        // GET: /Produce/PWaitingWarehouseList/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/PWaitingWarehouseList/Delete/5

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
