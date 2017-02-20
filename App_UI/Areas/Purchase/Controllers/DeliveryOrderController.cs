/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：DeliveryOrderController.cs
// 文件功能描述：送货单画面的Controller
//      
// 修改履历：2013/12/10 刘云 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using Model;
using BLL;
using Extensions;
using BLL.Common;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 送货单画面的Controller
    /// </summary>
    public class DeliveryOrderController : Controller
    {
        //送货单画面的Service
        private IDeliveryOrderService deliveryOrderService;

        //各种单据号自动生成的共通接口
        private IAutoCreateOddNoService autoCreateOddNoService;

        /// <summary>
        /// 送货单画面的构造函数
        /// </summary>
        /// <param name="deliveryOrderService"></param>
        public DeliveryOrderController(IDeliveryOrderService deliveryOrderService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.deliveryOrderService = deliveryOrderService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        /// <summary>
        /// 送货单一览点击新建按钮进入送货单页面
        /// </summary>
        /// <param name="deliceryOrderID"></param>
        /// <returns></returns>
        public ActionResult Init(string deliceryOrderID)
        {
            ViewBag.NowTime = System.DateTime.Now.ToLongDateString();
            return View();
        }

        /// <summary>
        /// 送货单一览点击送货单号码进入送货单页面
        /// </summary>
        /// <param name="deliceryOrderID"></param>
        /// <returns></returns>
        public ActionResult Edit(string deliveryOrderID)
        {
            ViewBag.DeliveryOrderID = deliveryOrderID;
            MCDeliveryOrder order = new MCDeliveryOrder();
            order = deliveryOrderService.getDeliveryOrderById(deliveryOrderID);
            ViewBag.DeliveryCompanyID = order.DeliveryCompanyID;
            ViewBag.DeliveryDate = order.DeliveryDate.ToLongDateString();
            ViewBag.DeliveryUID = order.DeliveryUID;
            ViewBag.TelNo = order.TelNo;
            return View();
        }

        /// <summary>
        /// 跳转到打印预览视图
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        public ActionResult PrintPreview(string deliveryOrderID)
        {
            ViewBag.DeliveryOrderID = deliveryOrderID;
            MCDeliveryOrder order = new MCDeliveryOrder();
            order = deliveryOrderService.getDeliveryOrderById(deliveryOrderID);
            ViewBag.DeliveryCompanyID = order.DeliveryCompanyID;
            ViewBag.DeliveryDate = order.DeliveryDate.ToLongDateString();
            ViewBag.DeliveryUID = order.DeliveryUID;
            ViewBag.TelNo = order.TelNo;
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="OutOrderID">外购单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(string deliveryOrderID)
        {
            int total = 0;
            //得到将要在页面上显示的数据
            VM_DeliveryOrderForShow searchConditon = new VM_DeliveryOrderForShow();
            searchConditon.DeliveryOrderID = deliveryOrderID;
            var deliceryOrderLists = deliveryOrderService.GetDeliveryOrderDetailBySearchById(searchConditon);
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return deliceryOrderLists.ToJson(total);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="deliveryOrderList"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public ActionResult Save(VM_DeliveryOrderForShow deliveryOrderList, Dictionary<string, string>[] orderList)
        {
            bool result = true;
            string message = "";
            string uID = Session["UserID"].ToString();
            try
            {
                if (deliveryOrderList.DeliveryOrderID == null)//新建
                {
                    string deliveryOrderID = autoCreateOddNoService.getNextDeliveryOrderNo(Session["UserID"].ToString());
                    deliveryOrderList.DeliveryOrderID = deliveryOrderID;
                    message = deliveryOrderService.Add(deliveryOrderList, orderList,uID);
                    if (message != "操作成功")
                    {
                        result = false;
                    }
                }
                //修改
                else
                {
                    message = deliveryOrderService.Update(deliveryOrderList, orderList,uID);
                    if (message != "操作成功")
                    {
                        result = false;
                    }
                }
            }
            catch (Exception e)
            {
                message = e.InnerException.Message;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryOrderID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string deliveryOrderID)
        {
            bool r = true;
            string message = "";
            string uId = Session["UserID"].ToString();
            try
            {
                deliveryOrderService.Delete(deliveryOrderID, uId);
            }
            catch (Exception e)
            {
                r = false;
                message = e.InnerException.Message;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = r,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;

        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="orderNo">采购计划单号</param>
        /// <returns></returns>
        public ActionResult ImportInfo(string orderNo)
        {
            bool result = true;
            string message = "";
            List<VM_DeliveryOrderForTableShow> list = new List<VM_DeliveryOrderForTableShow>();
            try
            {
                list = deliveryOrderService.GetImportInfo(orderNo);
            }
            catch (Exception e)
            {
                message = e.InnerException.Message;
                result = false;
            }
            JsonResult jr = new JsonResult();
            if (message != "")
            {
                jr.Data = new
                {
                    Result = result,
                    Message = message
                };
            }
            else
            {
                jr.Data = new
                {
                    Result = result,
                    Message = list
                };
            }
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="deliveryOrderID">送货单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Audit(string deliveryOrderID)
        {
            bool r = true;
            string message = "";
            string uId = Session["UserID"].ToString();
            try
            {
                r = deliveryOrderService.Audit(deliveryOrderID, uId);
                if (r)
                {
                    message = "操作成功";
                }
            }
            catch (Exception e)
            {
                r = false;
                message = e.InnerException.Message;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = r,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;

        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="deliveryOrderID">外购单号</param>
        /// <returns></returns>
        public JsonResult PrintInfo(string deliveryOrderID)
        {
            bool r = true;
            string message = "";
            string uId = Session["UserID"].ToString();
            try
            {
                message = deliveryOrderService.printInfo(deliveryOrderID, uId);
                if (message != "操作成功")
                {
                    r = false;
                }
            }
            catch (Exception e)
            {
                r = false;
                message = e.InnerException.Message;
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = r,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;
        }


    }
}
