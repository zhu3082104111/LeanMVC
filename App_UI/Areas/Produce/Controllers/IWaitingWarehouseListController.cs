/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：IWaitingWarehouseListController.cs
// 文件功能描述：产品待交仓一览Controller
//     
// 修改履历：2013/11/30 朱静波 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.Resource.ViewText.Common;
using App_UI.Resource.ViewText.Produce;
using BLL;
using BLL.Common;
using Extensions;
using Model;

namespace App_UI.Areas.Produce.Controllers
{
    public class IWaitingWarehouseListController : Controller
    {
        //交仓Service接口
        private IIWaitingWarehouseListService iWaitingWarehouseListService;
        //各种单据号自动生成的共通接口
        private IAutoCreateOddNoService autoCreateOddNoService;

        public IWaitingWarehouseListController(IIWaitingWarehouseListService iWaitingWarehouseListService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.iWaitingWarehouseListService = iWaitingWarehouseListService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        /// <summary>
        /// 交仓单一览页面
        /// </summary>
        /// <returns>交仓单一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 通过筛选条件获取数据
        /// </summary>
        /// <param name="paging">分页参数类</param>
        /// <param name="useForSearch">筛选条件</param>
        /// <returns></returns>
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult GetData(Paging paging, VM_IWaitingWarehouseForSearch useForSearch)
        {
            int total;
            var warehouseForSearch = iWaitingWarehouseListService.SearchTranslateCard(useForSearch, paging);
            return warehouseForSearch.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 交仓
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PostWarehouse(Dictionary<string, string> postData)
        {
            bool result = true;
            string message = ResourceHelper.ConvertMessage(VT_Common.MAddSuccess, new string[] { VT_IWaitingWarehouseList.PostPositions });
            try
            {
                //Dictionary<string, string> postData
                var entity = new VM_IWaitingWarehouseView();
                entity.ProcDelivID = postData["ProcDelivID"];
                entity.ExportID = postData["ExportID"];
                entity.ProcessID = postData["ProcessID"];
                entity.ProdAbbrev = postData["ProdAbbrev"];
                entity.UnitId = postData["UnitId"];
                entity.MaterReqQty = Decimal.Parse(postData["MaterReqQty"]);
                entity.WarehQtyAvailable = Decimal.Parse(postData["WarehQtyAvailable"]);
                entity.WarehQtySubmitted = Decimal.Parse(postData["WarehQtySubmitted"]);
                entity.WarehouseNo = postData["WarehouseNo"];
                entity.ConcessionPart = postData["ConcessionPart"];

                //获取用户ID
                var userId = Session["UserID"].ToString();
                //生成新的送货单号
                var procDelivID = autoCreateOddNoService.GetProcDelivID(userId);
                result = iWaitingWarehouseListService.DeliveryWarehouse(entity, userId, procDelivID);
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
                Message = message,
                ProcDelivID=postData["ProcDelivID"]
            };
            jr.ContentType = "text/html";
            return jr;
        }

    }
}
