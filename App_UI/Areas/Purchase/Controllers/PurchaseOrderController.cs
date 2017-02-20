/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：purchaseOrderController.cs
// 文件功能描述：
//          外购产品计划单画面的Controller类
//      
// 修改履历：2013/11/1 刘云 新建
/*****************************************************************************/
using BLL;
using Model;
using System.Web.Mvc;
using Extensions;
using System;
using System.Collections.Generic;
using BLL.Common;
using App_UI.Resource.ClientMessage;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外购产品计划单画面的Controller类
    /// </summary>
    public class PurchaseOrderController : Controller
    {
        // 外购产品计划的Service
        private IPurchaseOrderService purchaseOrderService;

        // 各种单据号自动生成的共通接口
        private IAutoCreateOddNoService autoCreateOddNoService;

        /// <summary>
        /// 外购产品计划单画面的构造函数
        /// </summary>
        /// <param name="purchaseOrderService">外购产品计划的Service</param>
        /// <param name="autoCreateOddNoService">各种单据号自动生成的共通接口</param>
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService,IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        /// <summary>
        /// 跳转到新建视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // 得到当前时间
            System.DateTime time = System.DateTime.Now;
            ViewBag.NowTime = time.ToLongDateString();
            return View();
        }

        /// <summary>
        /// 跳转到编辑视图
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        public ActionResult Edit(string outOrderID)
        {
            ViewBag.OutOrderID = outOrderID;
            // 得到当前时间
            System.DateTime time = System.DateTime.Now;
            ViewBag.NowTime = time.ToLongDateString();
            // 得到在页面上显示的外购单表数据
            MCOutSourceOrder order = purchaseOrderService.GetOrderInfoByID(outOrderID);
            ViewBag.ApproveUID = order.ApproveUID;
            ViewBag.VerifyUID = order.VerifyUID;
            ViewBag.EstablishUID = order.EstablishUID;
            ViewBag.SignUID = order.SignUID;
            ViewBag.DeptName = order.DepartmentID;
            ViewBag.CompName = order.OutCompanyID;
            ViewBag.EstablishDate = order.EstablishDate.Value.Date.ToLongDateString();
            ViewBag.UrgentStatus = order.UrgentStatus;
            ViewBag.OutOrderStatus = order.OutOrderStatus;
            ViewBag.Remarks = order.Remarks;
            ViewBag.FaxNo = order.FaxNo;
            return View();
        }

        /// <summary>
        /// 跳转到打印预览视图
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        public ActionResult PrintPreview(string outOrderID)
        {
            //得到当前页面的外购单号
            ViewBag.OutOrderID = outOrderID;
            //得到当前时间
            System.DateTime time = System.DateTime.Now;
            ViewBag.NowTime = time.ToLongDateString();
            //得到在页面上显示的外购单表数据
            MCOutSourceOrder order = purchaseOrderService.GetOrderInfoByID(outOrderID);
            ViewBag.ApproveUID = order.ApproveUID;
            ViewBag.VerifyUID = order.VerifyUID;
            ViewBag.EstablishUID = order.EstablishUID;
            ViewBag.SignUID = order.SignUID;
            ViewBag.DeptName = order.DepartmentID;
            ViewBag.CompName = order.OutCompanyID;
            ViewBag.EstablishDate = order.EstablishDate.Value.Date.ToLongDateString();
            ViewBag.UrgentStatus = order.UrgentStatus;
            ViewBag.OutOrderStatus = order.OutOrderStatus;
            ViewBag.Remarks = order.Remarks;
            ViewBag.FaxNo = order.FaxNo;
            return View();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="OutOrderID">外购单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOutOrderInfoByID(string outOrderID)
        {
            // 得到将要在页面上显示的数据
            var purchaseOrderLists = purchaseOrderService.GetPurchaseOrderDetailInfoByID(outOrderID);
            
            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseOrderLists.ToJson(0);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="purchaseOrderList"></param>
        /// <param name="orderList"></param>
        /// <returns></returns>
        public ActionResult Save(VM_PurchaseOrderForShow purchaseOrderList, Dictionary<string, string>[] orderList)
        {
            // 保存处理处理结果
            bool ret = true;
            // 保存处理Message
            string message = "";
            // 取得登录用户ID
            string uId = Session["UserID"].ToString();
            try
            {
                // 新建模式
                if (string.IsNullOrEmpty(purchaseOrderList.OutOrderID))
                {
                    // 取得新的外购单号
                    string outOrderID = autoCreateOddNoService.getNextOutOrderNo(Session["UserID"].ToString());
                    purchaseOrderList.OutOrderID = outOrderID;

                    // 生成新的外购单
                    ret = purchaseOrderService.Add(purchaseOrderList, orderList, uId);
                    if (ret)
                    {
                        message = CM_Purchase.CMSG_PUR_C00001;
                    }
                }
                // 编辑模式
                else
                {
                    // 更新外购单信息
                    ret = purchaseOrderService.Update(purchaseOrderList, orderList, uId);
                    if (ret)
                    {
                        message = CM_Purchase.CMSG_PUR_C00001;
                    }
                }
            }
            catch (Exception e)
            {
                ret = false;
                message = e.InnerException.Message;
            }

            // 封装返回结果
            return packageProcessResult(ret, message);
        }

        /// <summary>
        /// 批准
        /// </summary>
        /// <param name="outOrderId">外购单号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Approve(string outOrderId, string approveUID)
        {
            // 批准处理结果
            bool ret = true;
            // 批准处理Message
            string message = "";
            // 取得登录用户ID
            string uId = Session["UserID"].ToString();

            try
            {
                // 将参数的外购单号转换成外购单号List
                List<string> pOrderList = getOutOrderList(outOrderId);

                // 如果画面上的批准人为空时，默认登录用户
                if (String.IsNullOrEmpty(approveUID))
                {
                    approveUID = uId;
                }

                // 调用产品外购service的批准处理
                ret = purchaseOrderService.Approve(pOrderList, approveUID);
                if (ret)
                {
                    message = CM_Purchase.CMSG_PUR_C00001; 
                }
            }
            catch (Exception e)
            {
                ret = false;
                message = e.InnerException.Message;
            }

            // 封装返回结果
            return packageProcessResult(ret, message);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Audit(string outOrderID, string verifyUID)
        {
            // 审核处理结果
            bool ret = true;
            // 审核处理Message
            string message = "";
            // 取得登录用户ID
            string uId = Session["UserID"].ToString();
            try
            {
                // 将参数的外购单号转换成外购单号List
                List<string> pOrderList = getOutOrderList(outOrderID);

                // 如果画面上的审核人为空时，默认登录用户
                if (String.IsNullOrEmpty(verifyUID))
                {
                    verifyUID = uId;
                }

                ret = purchaseOrderService.Audit(pOrderList, verifyUID);
                if (ret)
                {
                    message = CM_Purchase.CMSG_PUR_C00001;
                }
            }
            catch (Exception e)
            {
                ret = false;
                message = e.InnerException.Message;
            }

            // 封装返回结果
            return packageProcessResult(ret, message);
        }

        /// <summary>
        /// 打印模式下的【打印】按钮
        /// </summary>
        /// <param name="outOrderID">外购单号</param>
        /// <returns>处理结果</returns>
        public JsonResult PrintInfo(string outOrderID)
        {
            // 打印处理结果
            bool ret = true;
            // 打印处理Message
            string message = "";
            // 取得登录用户ID
            string uId = Session["UserID"].ToString();
            try
            {
                ret = purchaseOrderService.PrintInfo(outOrderID, uId);
                if (ret)
                {
                    message = CM_Purchase.CMSG_PUR_C00001;
                }
            }
            catch (Exception e)
            {
                ret = false;
                message = e.InnerException.Message;
            }

            // 封装返回结果
            return packageProcessResult(ret, message);
        }

        /// <summary>
        /// 取得外购单号List
        /// </summary>
        /// <param name="outOrderID">外购单号(外购单号1,外购单号2,外购单号3)</param>
        /// <returns></returns>
        private List<String> getOutOrderList(string outOrderID)
        {
            List<String> pOrderList = new List<string>();
            string[] outOrdArray = outOrderID.Split(',');
            for (int i = 0; i < outOrdArray.Length; i++)
            {
                pOrderList.Add(outOrdArray[i]);
            }

            return pOrderList;
        }

        /// <summary>
        /// 封装处理结果并返回
        /// </summary>
        /// <param name="ret">处理结果</param>
        /// <param name="message">处理结果Message</param>
        /// <returns></returns>
        private JsonResult packageProcessResult(bool ret, string message)
        {
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = ret,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;
        }
    }
}
