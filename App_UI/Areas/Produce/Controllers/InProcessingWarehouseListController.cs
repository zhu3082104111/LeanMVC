/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：InProcessingWarehouseListController.cs
// 文件功能描述：加工产品交仓单一览Controller
//     
// 修改履历：2013/11/30 杜兴军 新建
// 修改履历：2013/12/10 朱静波 修改
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
using Model.Produce;

namespace App_UI.Areas.Produce.Controllers
{
    /// <summary>
    /// 请自行修改细节(控制器目录，名称等)
    /// 杜兴军 20131130
    /// </summary>
    public class InProcessingWarehouseListController : BaseController
    {
        //加工交仓的Service接口
        private IIWaitingWarehouseListService iWaitingWarehouseListService;

        public InProcessingWarehouseListController(IIWaitingWarehouseListService iWaitingWarehouseListService)
        {
            this.iWaitingWarehouseListService = iWaitingWarehouseListService;
        }

        /// <summary>
        /// 加工交仓单一览视图
        /// </summary>
        /// <returns>视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 跳转到加工产品交仓单详细页面
        /// </summary>
        /// <param name="procDelivID">加工送货单号</param>
        /// <returns>加工送货单详细视图</returns>
        public ActionResult Detail(string procDelivID)
        {
            ViewBag.procDelivID = procDelivID;
            //取得加工送货单信息并显示在界面上
            VM_ProcessDelivery proDel = iWaitingWarehouseListService.GetVMProcessDelivery(procDelivID);
            if (proDel == null)
            {
                return View(new VM_ProcessDelivery());
            }
            return View(proDel);

        }

        /// <summary>
        /// 加工交仓单打印预览
        /// </summary>
        /// <param name="id">加工送货单号ID</param>
        /// <returns>加工交仓单打印预览页面</returns>
        public ActionResult PrintPreview(String procDelivID)
        {
            ViewBag.procDelivID = procDelivID;
            //取得加工送货单信息并显示在界面上
            VM_ProcessDelivery proDel = iWaitingWarehouseListService.GetVMProcessDelivery(procDelivID);
            if (proDel == null)
            {
                return View(new VM_ProcessDelivery());
            }
            return View(proDel);
        }


        /// <summary>
        /// 取得加工产品交仓单一览
        /// </summary>
        /// <param name="paging">分页</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns>交仓一览数据集</returns>
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_ProProductWarehouseForSearch useForSearch)
        {
            var waiWarList = iWaitingWarehouseListService.SearchProProductWarehouse(useForSearch, paging);
            return waiWarList.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }



        /// <summary>
        /// 对选择的交仓单进行删除
        /// </summary>
        /// <param name="procDelivId">加工交仓单号</param>
        /// <returns>json</returns>
        [HttpPost]
        public ActionResult Delete(string procDelivId)
        {
            bool result = true;
            string message = "";
            try
            {
                List<string> deleList = new List<string>();
                var delete = procDelivId.Split(',');
                foreach (var de in delete)
                {
                    deleList.Add(de);
                }
                message = iWaitingWarehouseListService.DeleteWarehouse(deleList, Session["UserID"].ToString());
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
        /// 取得加工产品交仓单详细
        /// </summary>
        /// <param name="procDelivID">加工送货单号</param>
        /// <returns>取得加工送货单数据库</returns>
        public JsonResult GetProDelDet(string procDelivID)
        {
            var proDelDet = iWaitingWarehouseListService.GetProDelDetViewByID(procDelivID);
            return proDelDet.ToJson(0);
        }


        /// <summary>
        /// 保存待交仓一览详细对让步标志的修改
        /// </summary>
        /// <param name="procDelivID">加工送货单号</param>
        /// <param name="concessionPart">是否让步接收标志</param>
        /// <returns>json</returns>
        public JsonResult SaveDetail(string procDelivID, string concessionPart)
        {
            string message = "";
            try
            {
                message = iWaitingWarehouseListService.SaveConcession(procDelivID, concessionPart);
            }

            catch (Exception e)
            {
                message = e.InnerException.Message; ;
            }

            var jr = new JsonResult();
            jr.Data = new
            {
                Message = message
            };
            jr.ContentType = "text/html";

            return jr;
        }
    }
}
