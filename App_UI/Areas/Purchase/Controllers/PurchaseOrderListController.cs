/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：purchaseOrderListController.cs
// 文件功能描述：
//          产品外购单一览画面的Controller类
//      
// 修改履历：2013/10/28 刘云 新建
/*****************************************************************************/
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using App_UI.Resource.ClientMessage;
using BLL;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App_UI.Areas.Purchase.Controllers
{   
    /// <summary>
    /// 产品外购单一览画面的Controller类
    /// </summary>
    public class PurchaseOrderListController : BaseController
    {
        // 产品外购的Service
        private IPurchaseOrderService purchaseOrderService;

        /// <summary>
        /// 产品外购单一览画面的构造函数
        /// </summary>
        /// <param name="purchaseOrderService">产品外购的Service</param>
        public PurchaseOrderListController(IPurchaseOrderService purchaseOrderService)
        {
            this.purchaseOrderService = purchaseOrderService;
        }

        /// <summary>
        /// 显示产品外购单一览画面
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseOrderList()
        {
            // TODO:权限处理

            // 返回视图
            return View();
        }

        /// <summary>
        /// 取得外购单一览画面的详细信息
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="searchConditon">筛选条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Query(Paging paging, VM_PurchaseOrderListForSearch searchConditon)
        {
            // 得到将要在页面上显示的数据
            var purchaseOrderLists = purchaseOrderService.GetPurchaseOrderListInfoByPage(searchConditon, paging);
            
            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseOrderLists.ToJson(paging.total);
        }
      
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="outOrderID">外购单号(外购单号1,外购单号2,外购单号3)</param>
        /// <returns>删除处理结果</returns>
        public JsonResult Delete(string outOrderID)
        {
            // 删除处理结果
            bool ret = true;
            // 删除处理Message
            string message = "";
            // 取得登录用户ID
            string uId = Session["UserID"].ToString();

            try
            {
                // 将参数的外购单号转换成外购单号List
                List<string> pOrderList = getOutOrderList(outOrderID);

                // 调用产品外购service的删除处理
                ret = purchaseOrderService.Delete(pOrderList, uId);
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
        /// 批准
        /// </summary>
        /// <param name="outOrderId">外购单号(外购单号1,外购单号2,外购单号3)</param>
        /// <returns>批准处理结果</returns>
        [HttpPost]
        public JsonResult Approve(string outOrderId)
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

                // 调用产品外购service的批准处理
                ret = purchaseOrderService.Approve(pOrderList, uId);
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
        /// <param name="outOrderID">外购单号(外购单号1,外购单号2,外购单号3)</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Audit(string outOrderID)
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

                // 调用产品外购service的审核处理
                ret = purchaseOrderService.Audit(pOrderList, uId);
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
