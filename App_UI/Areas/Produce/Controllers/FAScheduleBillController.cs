/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FAScheduleBillController.cs
// 文件功能描述：总装调度单画面的Controller
//     
// 修改履历：2013/10/17 朱静波 新建
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using BLL;
using BLL.Common;
using BLL.ServerMessage;
using CrystalDecisions.CrystalReports.Engine;
using Extensions;
using Model;
using Model.Produce;

namespace App_UI.Areas.Produce.Controllers
{
    public class FAScheduleBillController : BaseController
    {
        //总装调度单Service接口
        private IFAScheduleBillService faScheduleBillService;
        //各种单据号自动生成的共通接口
        private IAutoCreateOddNoService autoCreateOddNoService;

        public FAScheduleBillController(IFAScheduleBillService faScheduleBillService,
            IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.faScheduleBillService = faScheduleBillService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        /// <summary>
        /// 总装调度单一览
        /// </summary>
        /// <returns>总装调度单一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 通过筛选条件获取总装调度单一览数据
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>总装调度单一览数据</returns>
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_FAScheduleBillForSearch useForSearch)
        {
            var inProcessingRate = faScheduleBillService.GetFAScheduleBillSearch(useForSearch, paging);
            return inProcessingRate.ToJson(paging.total); //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 更新总装调度单
        /// </summary>
        /// <param name="id">总装调度单ID</param>
        /// <returns>总装调度单详细页面</returns>
        public ActionResult UpdateBill(String id)
        {
            ViewBag.assemblyTicketId = id;
            ViewBag.IsAdd = false;
            return View();
        }

        /// <summary>
        /// 总装调度单打印预览
        /// </summary>
        /// <param name="id">总装调度单ID</param>
        /// <returns>总装调度单打印预览页面</returns>
        public ActionResult PrintPreview(String id)
        {
            ViewBag.assemblyTicketId = id;
            ViewBag.IsAdd = false;
            return View();
        }

        /// <summary>
        /// 新增总装调度单
        /// </summary>
        /// <param name="customerOrderId">客户订单号</param>
        /// <param name="customerOrderDetails">客户订单详细</param>
        /// <param name="productId">产品ID</param>
        /// <param name="prodAbbrev">产品略称</param>
        /// <param name="assemblyPlanNum">计划数量</param>
        /// <returns>总装调度单详细页面</returns>
        public ActionResult AddNewBill(String customerOrderId, String customerOrderDetails, String productId,
            String prodAbbrev, String assemblyPlanNum)
        {
            ViewBag.IsAdd = true;
            ViewBag.customerOrderId = customerOrderId;
            ViewBag.customerOrderDetails = customerOrderDetails;
            ViewBag.prodAbbrev = prodAbbrev;
            ViewBag.assemblyPlanNum = assemblyPlanNum;
            ViewBag.productId = productId;
            return View("UpdateBill");
        }


        /// <summary>
        /// 对选择的总装调度单进行删除
        /// </summary>
        /// <param name="orderNoString">总装调度单号</param>
        /// <returns>json</returns>
        [HttpPost]
        public ActionResult Delete(string orderNoString)
        {
            bool result = true;
            string message = "";
            //获取用户ID
            var userId = Session["UserID"].ToString();
            try
            {
                List<string> deleList = new List<string>();
                var delete = orderNoString.Split(',');
                foreach (var de in delete)
                {
                    deleList.Add(de);
                }

                message = faScheduleBillService.DeleteSchedulingBill(deleList, userId);
            }
            catch (Exception e)
            {
                result = false;
                message = e.InnerException.Message;
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
        /// 生成大工票功能
        /// </summary>
        /// <param name="orderNoString">总装调度单号</param>
        /// <returns>Json</returns>
        public ActionResult NewFAWorkticket(string orderNoString)
        {
            bool result = true;
            string message = "";
            //获取用户ID
            var userId = Session["UserID"].ToString();
            try
            {
                //总装调度单号
                List<string> createList = new List<string>();
                //寄存生成的总装大工票号
                List<string> assembBillIDList = new List<string>();
                var delete = orderNoString.Split(',');
                foreach (var de in delete)
                {
                    createList.Add(de);
                    assembBillIDList.Add(autoCreateOddNoService.GetAssembBillID(userId));
                }

                message = faScheduleBillService.NewFAWorkticket(createList, assembBillIDList, userId);
            }
            catch (Exception e)
            {
                result = false;
                message = e.InnerException.Message;
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
        /// 点击单条记录时为调度单页面加载数据
        /// </summary>
        /// <param name="symbol">添加和编辑标志</param>
        /// <param name="id">总装调度单ID</param>
        /// <param name="customerOrderId">客户订单号</param>
        /// <param name="customerOrderDetails">客户订单详细</param>
        /// <param name="productId">产品ID</param>
        /// <param name="prodAbbrev">产品略称</param>
        /// <param name="assemblyPlanNum">计划数量</param>
        /// <returns>json</returns>
        public JsonResult LoadAllData(Boolean symbol, String id, String customerOrderId, String customerOrderDetails,
            String productId, String prodAbbrev, String assemblyPlanNum)
        {
            IList<VM_NewBillDataGrid> assemblyDDlList = null;
            var assemblyDispatch = new AssemblyDispatch();
            //
            if (symbol == true)
            {
                //添加
                assemblyDispatch.CustomerOrderNum = customerOrderId;
                assemblyDispatch.CustomerOrderDetails = customerOrderDetails;
                //用于寄存产品简称
                assemblyDispatch.ProductID = productId;
                assemblyDispatch.ProdAbbrev = prodAbbrev;
                assemblyDispatch.AssemblyPlanNum = decimal.Parse(assemblyPlanNum);
                assemblyDispatch.TotalNumberOfSets = assemblyDispatch.AssemblyPlanNum;
                assemblyDispatch = faScheduleBillService.GetBasicInformation(assemblyDispatch);

                //调度员默认当前登录用户
                assemblyDispatch.DispatcherName = base.GetLoginUserName();
                assemblyDispatch.Dispatcher = base.GetLoginUserID();

                IEnumerable<VM_NewBillDataGrid> assemblyDispatchDetail =
                    faScheduleBillService.GetNewBillDataGrid(assemblyDispatch.ProductID, customerOrderId, customerOrderDetails);
                if (assemblyDispatchDetail != null)
                {
                    assemblyDDlList = assemblyDispatchDetail.ToList();
                }
            }
            else
            {
                //编辑
                assemblyDispatch = faScheduleBillService.GetFAScheduleBillByID(id);
                //判空 
                if (assemblyDispatch != null)
                {
                    IEnumerable<VM_NewBillDataGrid> assemblyDispatchDetail =
                     faScheduleBillService.GetAssemblyDispatchDetail(assemblyDispatch.AssemblyDispatchID);
                    if (assemblyDispatchDetail != null)
                    {
                        assemblyDDlList = assemblyDispatchDetail.ToList();
                    }
                }
            }

            var oddDispatchDetail = new List<VM_NewBillDataGrid>();
            var evenDispatchDetail = new List<VM_NewBillDataGrid>();

            var jr = new JsonResult();

            if (assemblyDDlList != null)
            {
                for (int i = 1; i <= assemblyDDlList.Count(); i++)
                {
                    // 判断一个整数是奇数还是偶数
                    if ((i & 1) == 0)
                    {
                        //偶数
                        evenDispatchDetail.Add(assemblyDDlList.ElementAt(i - 1));
                    }
                    else
                    {
                        //奇数
                        oddDispatchDetail.Add(assemblyDDlList.ElementAt(i - 1));
                    }
                }

                jr.Data = new
                {
                    isSuccess = true,
                    oddGridData = oddDispatchDetail.ToJson(oddDispatchDetail.Count()),
                    evenGridData = evenDispatchDetail.ToJson(evenDispatchDetail.Count()),
                    HeadfootData = assemblyDispatch
                };
                jr.ContentType = "text/html";
                //jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return jr;
            }

            jr.Data = new
            {
                isSuccess = false
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 保存或新增总装调度单
        /// </summary>
        /// <param name="symbol">新增或编辑标志位</param>
        /// <param name="assemblyTicketId">总装调度单号</param>
        /// <param name="assemblyDispatch">编辑数据集</param>
        /// <returns>json</returns>
        public JsonResult SaveData(Boolean symbol, string assemblyTicketId,
            Dictionary<string, string>[] assemblyDispatch)
        {
            //Dictionary<string,string>[] assemblyDispatch
            //System.Threading.Thread.Sleep(5000);
            //return str[0]["name"];
            string message = "";
            //获取用户ID
            var userId = Session["UserID"].ToString();
            try
            {
                if (symbol)
                {
                    assemblyTicketId = autoCreateOddNoService.GetAssemblyDispatchID(userId);
                }
                message = faScheduleBillService.SaveNewAndUpdate(symbol, assemblyTicketId, assemblyDispatch, userId);
            }

            catch (Exception e)
            {
                message = e.InnerException.Message;
            }

            var jr = new JsonResult();
            jr.Data = new
            {
                message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }

        /// <summary>
        /// 客户订单号的自动填充
        /// </summary>
        /// <param name="name">控件输入值</param>
        /// <returns>json</returns>
        [HttpGet]
        public JsonResult GetAutoComplete(string name)
        {
            var dd = new object[] {new {id = "aaa", name = "ccc"}, new {id = "bbb", name = "ddd"}};

            var useForSearch = new VM_FAScheduleBillForSearch();
            var inProcessingRate = faScheduleBillService.GetAutoCompleteSearch(useForSearch);
            var result = inProcessingRate.Where(a => a.CustomerOrderNum.Contains(name)).Select(a =>
                new
                {
                    id = a.CustomerOrderNum,
                    name = a.CustomerOrderNum
                });

            var ffs = result.ToList();
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// 删除大工票功能
        ///// </summary>
        ///// <param name="orderNoString"></param>
        ///// <returns></returns>
        //public ActionResult DelFAWorkticket(string orderNoString)
        //{
        //    bool result = true;
        //    string sign = "";
        //    try
        //    {
        //        List<string> deleList = new List<string>();
        //        var delete = orderNoString.Split(',');
        //        foreach (var de in delete)
        //        {
        //            deleList.Add(de);
        //        }

        //        sign = faScheduleBillService.DelFAWorkticket(deleList);
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    JsonResult jr = new JsonResult();
        //    jr.Data = new
        //    {
        //        Result = result,
        //    };
        //    jr.ContentType = "text/html";
        //    return jr;

        //}
    }
}
