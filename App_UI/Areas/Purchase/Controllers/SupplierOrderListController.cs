/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SupplierOrderListController.cs
// 文件功能描述：
//          外协加工调度单一览画面用Controller
//
// 修改履历：2013/10/31 廖齐玉 新建
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


namespace App_UI.Areas.Purchase.Controllers
{
    /// <summary>
    /// 外协加工调度单一览画面用Controler类
    /// </summary>
    public class SupplierOrderListController : BaseController
    {
        /// <summary>
        /// 外协调度单画面的Service接口类
        /// </summary>
        private ISupplierOrderService supplierOrderService;

        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="supplierOrderService"></param>
        public SupplierOrderListController(ISupplierOrderService supplierOrderService)
        {
            this.supplierOrderService = supplierOrderService;
        }        
   
        /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult SupplierOrderList()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="searchCondition">刷选条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Query(Paging paging, VM_SupplierOrderListForSearch searchCondition)
        {
            // 返回searchCondition条件执行结果 由supplierOrder接收 
            var supplierOrder = supplierOrderService.GetSupplierOrderListBySearchByPage(searchCondition, paging);
            // object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器 
            return supplierOrder.ToJson(paging.total);  
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="supOrderID">外协加工调度单号</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Delete(string supOrderID)
        {
            // 返回值初始化
            bool r = true;
            // 显示信息的初始化
            string message = "";
            // 获取登录者的Id
            string uId = Session["UserID"].ToString();
            try
            {
                // New一个List存放待操作的调度单号
                List<string> list = new List<string>();
                // 将传递过来的参数以 “，”为分隔存放到delete变量中
                var delete = supOrderID.Split(',');
                // 将分隔好的单号放到list中
                foreach (var de in delete)
                {
                    list.Add(de);
                }
                // 删除list中调度单号的数据
                if (supplierOrderService.DeleteSupplierOrder(list, uId) == true)
                {
                    message = "删除成功！";
                }
                else
                {
                    message = "删除失败！";
                    r = false;
                };
            }
            catch (Exception e)
            {
                //异常错误捕捉返回False
                message = e.InnerException.Message;
                r = false;
            }

            //提示信息返回到界面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                //返回状态
                Result = r,
                //返回提示信息
                Message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="supOrderID">外协加工调度单号</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Audit(string supOrderID)
        {
            //返回值初始化
            bool r = true;
            string message = "";
            //获取登录者的Id
            string UId = Session["UserID"].ToString();
            try
            {
                //New一个List存放待操作的调度单号
                List<string> list = new List<string>();
                //将传递过来的参数以 “，”为分隔存放到delete变量中
                var audit = supOrderID.Split(',');
                //将分隔好的单号放到list中
                foreach (var a in audit)
                {
                    list.Add(a);
                }
                //审核list中调度单号的数据
                if (supplierOrderService.AuditSupplierOrder(list, UId) == true)
                {
                    message = "审核成功！";
                }
                else
                {
                    message = "审核失败！";
                    r = false;
                };
            }
            catch (Exception e)
            {
                //异常错误捕捉返回False
                message = e.InnerException.Message;
                r = false;
            }

            //提示信息返回到界面
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                //返回状态
                Result = r,
                //返回提示信息
                Message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }
    }
}
