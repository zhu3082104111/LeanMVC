/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurchaseSchedulingController.cs
// 文件功能描述：
//          外购排产画面的Controller类
//      
// 修改履历：2013/11/27 陈阵 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using Extensions;
using BLL;
using System.Collections;
using BLL.Common;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外购排产画面的Controller类
    /// </summary>
    public class PurchaseSchedulingController : BaseController
    {
        // 外购排产的Service
        private ISchedulingService schedulingService;

        // 自动生成单据号的Service
        private IAutoCreateOddNoService autoCreateOddNoService;
        
        /// <summary>
        /// 外购排产画面的构造函数
        /// </summary>
        /// <param name="schedulingService">外购排产的Service</param>
        /// <param name="autoCreateOddNoService">自动生成单据号的Service</param>
        public PurchaseSchedulingController(ISchedulingService schedulingService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.schedulingService = schedulingService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }
        
        /// <summary>
        /// 显示外购排产画面
        /// </summary>
        /// <param name="schedulingObject">排产对象</param>
        /// <param name="deptName">生产部门名称</param>
        /// <returns></returns>
        public ActionResult PurchaseScheduling(String schedulingObject, String deptName)
        {
            // 生产部门名称传到前台
            ViewBag.DeptName = deptName;

            // 筛选条件传到前台
            ViewBag.schedulingObject = schedulingObject; 

            // 得到当前时间，传到前台
            ViewBag.NowTime = DateTime.Now.ToLongDateString();
            
            // 返回视图
            return View();
        }
        
        /// <summary>
        /// 取得外购排产对象信息
        /// </summary>
        /// <param name="schedulingObject">排产对象</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult GetPurSchedulingInfo(String schedulingObject)
        {
            // 去掉最后一个分隔符({)
            schedulingObject = schedulingObject.Substring(0, schedulingObject.Length - 1);
            // 将排产对象数组化
            string[] obj = schedulingObject.Split(';');            
            
            // 得到将要在页面上显示的数据
            var purchaseScheduling = schedulingService.GetPurchaseSchedulingInfo(obj);

            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return purchaseScheduling.ToJson(0);
        }

        /// <summary>
        /// 订单生成
        /// </summary>
        /// <param name="orderInfoList">外购排产画面的输入信息</param>
        /// <returns></returns>
        public JsonResult MakeOrder(Dictionary<String, String>[] orderInfoList)
        {
            // 返回结果
            bool result = true;

            // 取得登录用户ID
            string userID = Session["UserID"].ToString();
            
            // 传递给页面的信息
            string message = "";

            // 存放供货商ID的List
            List<String> outCompanyID = new List<String>();
            
            // 存放外购单号的List
            List<String> outOrderID = new List<String>();
            
            // 存放产品零件ID的List
            List<String> ProductPartID = new List<String>();
            
            // 存放材料规格与要求的List
            List<String> Specifica = new List<String>();

            // 遍历外购排产画面的输入信息
            for (int i = 0; i < orderInfoList.Length; i++)
            {
                // 是否生产新订单号
                bool isNewOrder = true;
                // 如果是第一条数据
                if (i == 0)
                {
                    // 生成新外购单号
                    orderInfoList[i]["OutOrderID"] = autoCreateOddNoService.getNextOutOrderNo(userID);
                    // 把该行的以下数据添加List里，用于合并排产时判断用
                    // 供货商
                    outCompanyID.Add(orderInfoList[i]["OutCompanyID"]);
                    // 外购单号
                    outOrderID.Add(orderInfoList[i]["OutOrderID"]);
                    // 产品零件ID
                    ProductPartID.Add(orderInfoList[i]["ProductPartID"]);
                    // 材料规格及要求
                    Specifica.Add(orderInfoList[i]["Specifica"]);
                }
                else
                {
                    // 遍历供货商List
                    for (int j = 0; j < outCompanyID.Count; j++)
                    {
                        // 合并排产条件：在供货商ID相同的前提下：1.产品零件ID不同 2.产品零件ID相同，材料规格要求相同
                        // 1.检索供货商相同的记录
                        if (orderInfoList[i]["OutCompanyID"] == outCompanyID[j])
                        {
                            // 2.在1的基础上，检索产品零件ID不同或产品零件ID与材料规格要求均相同的记录
                            if ((orderInfoList[i]["ProductPartID"] != ProductPartID[j]) 
                                || (orderInfoList[i]["ProductPartID"] == ProductPartID[j] && orderInfoList[i]["Specifica"] == Specifica[j]))
                            {
                                // 符合合并排产的条件，不需要生产新订单号
                                orderInfoList[i]["OutOrderID"] = outOrderID[j];
                                isNewOrder = false;
                            }
                        }
                    }
                    // 需要生产新订单号（不能合并排产原因：1.供货商ID不同 2.供货商ID相同，产品零件ID相同，材料规格要求不同）
                    if (isNewOrder)
                    {
                        // 生成新外购单号
                        orderInfoList[i]["OutOrderID"] = autoCreateOddNoService.getNextOutOrderNo(Session["UserID"].ToString());
                        // 把该行的以下数据添加List里，用于合并排产时判断用
                        // 供货商
                        outCompanyID.Add(orderInfoList[i]["OutCompanyID"]);
                        // 外购单号
                        outOrderID.Add(orderInfoList[i]["OutOrderID"]);
                        // 产品零件ID
                        ProductPartID.Add(orderInfoList[i]["ProductPartID"]);
                        // 材料规格及要求
                        Specifica.Add(orderInfoList[i]["Specifica"]);
                    }
                }
            }

            try
            {
                // 调用外购排产Service的排产函数
                result = schedulingService.MakeOrder4Purchase(orderInfoList, userID, DateTime.Now);
                // 判断成功与否，设置返回Message
                if (result == false)
                {
                    message = "排产失败!";
                }
                if (result)
                {
                    message = "排产成功!";
                }
            }
            catch (Exception e)
            {
                message = e.InnerException.Message;
                result = false;
            }
           
            // 结果传给页面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                Result = result,
                Message = message
            };
            jr.ContentType = "text/html";
            return jr;
        }
    }
}