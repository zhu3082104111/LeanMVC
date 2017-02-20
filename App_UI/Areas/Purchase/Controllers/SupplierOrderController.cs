/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierOrderController.cs
// 文件功能描述：
//          外协加工调度单画面用Controller
//
// 修改履历：2013/11/18 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Areas.Controllers;
using BLL;
using Model;
using Extensions;
using BLL.Common;

namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外协加工(注塑)调度单画面用Controller
    /// </summary>
    public class SupplierOrderController : BaseController
    {
        //需要调用的Service类
        /// <summary>
        /// 调度单Service类
        /// </summary>
        private ISupplierOrderService supplierOrderService;
        /// <summary>
        /// 单号自动生成service类
        /// </summary>
        private IAutoCreateOddNoService autoCreateOddNoService;

        //被调用类的声明
        /// <summary>
        /// 被调用Service类的实现
        /// </summary>
        /// <param name="supplierOrderService">调度单service类</param>
        /// <param name="autoCreateOddNoService">单号生成service</param>
        public SupplierOrderController(ISupplierOrderService supplierOrderService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.supplierOrderService = supplierOrderService;
            this.autoCreateOddNoService= autoCreateOddNoService;
        }

        /// <summary>
        /// 新建画面
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierOrderForCreat()
        {
            return View();
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="supplierOrderId">调度单号</param>
        /// <returns></returns>
        public ActionResult PrintPreview(string supplierOrderId)
        {
            // 需打印的单号
            ViewBag.supplierOrderId = supplierOrderId;
            // 界面上表外信息的加载
            showInfomation(supplierOrderId);
            return View();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierOrderId"></param>
        /// <returns></returns>
        public JsonResult Print(string supplierOrderId)
        {
            // 执行的返回结果
            bool result = false;
            // 执行的返回信息
            string message = "";
            // 获取ID
            string uId = Session["UserID"].ToString();
            try
            {
                // 调用打印方法
                result = supplierOrderService.PrintSupplierOrder(uId, supplierOrderId);
                if (result == true) { message = "打印成功！"; };
            }
            catch (Exception e)
            {
                result = false;//异常执行结果
                message = e.InnerException.Message;//异常信息
            }
            

            //提示信息返回到界面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                //返回状态
                Result = result,
                //返回提示信息
                Message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 加载界面的显示的表外信息 代码共通
        /// </summary>
        /// <param name="supplierOrderId"></param>
        /// <returns></returns>
        private ActionResult showInfomation(string supplierOrderId)
        {
            //存放返回的值，（调度单的紧急状态、客户单号、客户单明细号、所属部门、调度单种类和调入单位）
            VM_SupplierOrderInfor abInfo = supplierOrderService.GetDetailInformation(supplierOrderId);
            ViewBag.supplierOrderStatus = abInfo.OrderStatus;//当前状态
            ViewBag.UrgentStatus = abInfo.UrgentStatus;//紧急状态
            ViewBag.departmentId = abInfo.Department;//部门
            ViewBag.OrderType = abInfo.OrderType;//种类
            ViewBag.InCompanyId = abInfo.IncompId;//调入单位
            ViewBag.ProduceManager = abInfo.PrdMngrName;//生产主管
            ViewBag.MarkUser = abInfo.MarkName;//制单人
            ViewBag.Operator = abInfo.OptrName;//经办人
            if (abInfo.PrdMngrSignDate == null) //生产主管签字时间
            {
                ViewBag.ProduceManagerSignDate = "";
            }
            else 
            {
                ViewBag.ProduceManagerSignDate = abInfo.PrdMngrSignDate.Value.Date.ToLongDateString();
            }
            if (abInfo.MarkSignDate == null) //制单人签字时间
            {
                ViewBag.MarkUserSignDate = "";
            }
            else
            {
                ViewBag.MarkUserSignDate = abInfo.MarkSignDate.Value.Date.ToLongDateString();
            }
            if (abInfo.OptrSignDate == null) //经办人签字时间
            {
                ViewBag.OperatorSignDate = "";
            }
            else
            {
                ViewBag.OperatorSignDate = abInfo.OptrSignDate.Value.Date.ToLongDateString();
            }

            return View();

        }

        /// <summary>
        /// 编辑画面
        /// </summary>
        /// <param name="supplierOrderId"></param>
        /// <returns></returns>
        public ActionResult SupplierOrderForEdit(string supplierOrderId)
        {
            //int author = 1;
            ViewBag.supplierOrderId = supplierOrderId;
            //加载界面表外显示需要的信息
            showInfomation(supplierOrderId);

            return View();
        }

      /// <summary>
      /// 显示数据
      /// </summary>
      /// <param name="supplierOrderId">外协单号</param>
      /// <returns></returns>
        [HttpPost]
        public JsonResult ShowSupplierOrderDetail(string supplierOrderId,Paging paging)//
        {
            //int total = 0;
            //得到将要在页面上显示的数据
            VM_SupplierOrder supplierOrderDetail = new VM_SupplierOrder() { SupOrderID = supplierOrderId };

            var users = supplierOrderService.GetSupplierOrderByIdForSearch(supplierOrderDetail,paging);
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return users.ToJson(paging .total);
        }
          
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="s">ViewModel</param>
        /// <param name="orderList">Table数据集合</param>
        /// <returns></returns>
        public ActionResult Save(VM_SupplierOrderInfor s, Dictionary<string, string>[] orderList)
        {
            //返回结果
            bool result = true;

            //提示信息
            string message="操作失败！";
            //获取ID
            string uId = Session["UserID"].ToString();
            try
            {
                //通过界面中s.OutcompId的值判断是否是修改还是新建，修改时该项为null
                if (string.IsNullOrEmpty(s.SupOdrId))
                //数据添加
                {
                    //外协单号生成
                    string supOdrId = autoCreateOddNoService.getNextSuppOrderNo(uId);

                    if (supplierOrderService.Add(supOdrId, uId, s, orderList) == true)
                    {
                        message = "添加数据成功!";//成功信息
                    }; 
                }
                else
                //数据修改
                {
                    //判断修改操作返回的结果
                    if (supplierOrderService.UpdateSupplierOrderDetail(uId,s,orderList) == true)
                    {
                        message = "更新数据成功!";//成功信息
                    };
                }
            }
            catch (Exception e)
            {                
                //获取异常信息
                if (e.InnerException == null)
                {
                    message = "数据未填写！";
                }
                else
                {
                    message = e.InnerException.Message;
                }
                
                result = false;
            }

            //提示信息返回到界面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                //返回状态
                Result = result,
                //返回提示信息
                Message=message
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="supplierOrder"></param>
        /// <returns></returns>
        public ActionResult Audit(string supplierOrder)
        {
            //返回结果
            bool result = true;
            string message = "";
            //获取Id
            string uId = Session["UserID"].ToString();
            //审核需要的调度单
            try
            {
                //New一个List存放待操作的调度单号
                List<string> list = new List<string>() { supplierOrder };           
                supplierOrderService.AuditSupplierOrder(list,uId);
                message = "审核成功！";
        
            }
            catch (Exception e)
            {
                //异常返回结果
                result = false;
                message = e.InnerException.Message;
            }

            //提示信息返回到界面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                //返回状态
                Result = result,
                //返回提示信息
                Message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }
    }
}
