/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：OrderRateController.cs
// 文件功能描述：产品和订单进度查询的Controller
//     
// 修改履历：2013/10/22 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class OrderRateController : BaseController
    {

        // 产品和订单进度查询Service接口
        private IOrderRateService orderRateService;

        public OrderRateController(IOrderRateService orderRateService)
        {
            this.orderRateService = orderRateService;
        }

        /// <summary>
        /// 产品和订单进度查询一览页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string clientOrderID, string clientOrderDetail, string productID)
        {
            ViewBag.clientOrderID = clientOrderID;
            ViewBag.clientOrderDetail = clientOrderDetail;
            ViewBag.productID = productID;
            return View();
        }

        /// <summary>
        /// 通过筛选条件获取数据
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>进度一览数据集</returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_OrderRateForSrarch useForSearch)
        {
            //用于取得标题头数据
            var headerData = new VM_HeaderData();
            var inProcessingRate = orderRateService.GetOrderRateSearch(useForSearch, paging, headerData);
            ViewBag.ClientOrderID = headerData.ClientOrderID;
            ViewBag.DeliveryDate = headerData.DeliveryDate;
            ViewBag.PlanQuantity = headerData.PlanQuantity;

            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                gridData = inProcessingRate.ToJson(paging.total),
                headerData = new
                {
                    ClientOrderID = headerData.ClientOrderID,
                    DeliveryDate = headerData.DeliveryDate,
                    PlanQuantity = headerData.PlanQuantity,
                    AchieveRate = headerData.AchieveRate
                }
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 根据客户订单号取得产品型号信息
        /// </summary>
        /// <param name="clientOrderID">客户订单号</param>
        /// <returns>产品型号信息</returns>
        public JsonResult GetProduceType(string clientOrderID)
        {
            //新建一个id和name相关的下拉列表
            IList<VM_ProduceType> ls = new List<VM_ProduceType>() ;
            //取得显示信息
            ls = orderRateService.GetProduceType(clientOrderID).ToList();
            if (ls.Count > 0)
            {
                ls[0].selected = true;
            }
            //返回值初始化
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //生成结果赋给返回值
            js.Data = ls;
            return js;
        }
    }
}
