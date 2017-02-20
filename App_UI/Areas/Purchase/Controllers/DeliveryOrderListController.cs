/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：DeliveryOrderListController.cs
// 文件功能描述：
//          送货单一览画面的Controller
//      
// 修改履历：2013/11/13 姬思楠 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;
using App_UI.Resource.ClientMessage;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 送货单一览画面的Controller类
    /// </summary>
    public class DeliveryOrderListController : BaseController
    {

        // 送货单相关的Service
        private IDeliveryOrderService deliveryOrderService;

        /// <summary>
        /// 送货单一览画面的实例化函数
        /// </summary>
        /// <param name="DeliveryOrderListService">送货单相关service</param>
        public DeliveryOrderListController(IDeliveryOrderService deliveryOrderService)
        {
            this.deliveryOrderService = deliveryOrderService;
        }

        /// <summary>
        /// 送货单一览画面的初始化函数
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryOrderList()
        {
            // TODO:权限处理

            return View();
        }

        /// <summary>
        /// 送货单一览画面的查询函数
        /// </summary>
        /// <param name="searchCondition">筛选条件</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Query(VM_DeliveryOrderListForSearch searchCondition, Paging paging)
        {
            //得到将要在页面上显示的数据
            var deliveryOrderInfoList = deliveryOrderService.GetDeliveryOrderListBySearchByPage(searchCondition, paging);

            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return deliveryOrderInfoList.ToJson(paging.total);
        }


        /// <summary>
        /// 删除送货单
        /// </summary>
        /// <param name="deliveryOrderID">送货单号（形式如下：送货单号1，送货单号2，送货单号3）</param>
        /// <returns>处理结果</returns>
        [HttpPost]
        public JsonResult Delete(string deliveryOrderID)
        {
            // 处理结果
            bool ret = true;
            // 处理结果Message
            string message = "";
            // 从session里获取登录用户ID
            string uId = Session["UserID"].ToString();
            
            try
            {
                // 将参数的送货单号转换成送货单号List
                List<String> dOrderList = new List<string>();
                var delOrdId = deliveryOrderID.Split(',');
                for (int i = 0; i < delOrdId.Length; i++)
                {
                    dOrderList.Add(delOrdId[i]);
                }

                // 调用送货单service的删除送货单方法
                ret = deliveryOrderService.Delete(dOrderList, uId);
                // 删除成功
                if (ret)
                {
                    // 设置删除成功Message
                    message = CM_Purchase.CMSG_PUR_C00001;
                }
            }
            // 出错时
            catch (Exception e)
            {
                // 处理结果
                ret = false;
                // 处理结果Message（service处理返回的Message）
                message = e.InnerException.Message;
            }

            // 封装处理结果
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = ret,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 审核送货单
        /// </summary>
        /// <param name="deliveryOrderID">送货单号（形式如下：送货单号1，送货单号2，送货单号3）</param>
        /// <returns>处理结果</returns>
        [HttpPost]
        public JsonResult Audit(string deliveryOrderID)
        {
            // 处理结果
            bool ret = true;
            // 处理结果Message
            string message = "";
            // 从session里获取登录用户ID
            string uId = Session["UserID"].ToString();

            try
            {
                // 将参数的送货单号转换成送货单号List
                List<String> dOrderList = new List<string>();
                var delOrdId = deliveryOrderID.Split(',');
                for (int i = 0; i < delOrdId.Length; i++)
                {
                    dOrderList.Add(delOrdId[i]);
                }

                // 调用送货单service的审核送货单函数
                ret = deliveryOrderService.Audit(dOrderList, uId);
                // 审核成功
                if (ret)
                {
                    // 设置审核成功Message
                    message = CM_Purchase.CMSG_PUR_C00001;
                }
            }
            // 出错时
            catch (Exception e)
            {
                // 处理结果
                ret = false;
                // 处理结果Message（service处理返回的Message）
                message = e.InnerException.Message;
            }

            // 封装处理结果
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
