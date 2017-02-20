/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinOutRecordController.cs
// 文件功能描述：
//          内部成品库出库履历画面用Controller
//
// 修改履历：2013/12/02 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App_UI.App_Start;
using Extensions;
using Model.Store;
using BLL;
using BLL.Common;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 内部成品库出库履历画面用Controller
    /// </summary>
    public class FinOutRecordController : BaseController
    {
        //内部成品库出库画面的Service接口类
        private IFinOutStoreService finOutStoreService;
        private IAutoCreateOddNoService autoCreateOddNoService;
        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="finOutStoreService">Service接口类</param>
        /// <param name="autoCreateOddNoService">Service接口类</param>
        public FinOutRecordController(IFinOutStoreService finOutStoreService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.finOutStoreService = finOutStoreService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>出库履历一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 出库履历详细
        /// </summary>
        /// <param name="flg">页面标识</param>
        /// <param name="inerFinOutID">成品送货单号</param>
        /// <param name="ladiID">提货单号</param>
        /// <returns>详细页面视图</returns>
        public ActionResult FinOutRecordDetls(string inerFinOutID, string ladiID,string whkpID,string outDate,string remarks)
        {
            //ViewBag.state = flg;
            ////详细
            //if (flg == "1")
            //{
                ViewBag.inerFinOutID = inerFinOutID;
                ViewBag.ladiID = ladiID;
                ViewBag.whkpID = whkpID;
                ViewBag.outDate = outDate;
                ViewBag.remarks = remarks;
            //}
            ////新增
            //else
            //{
            //    string uId = GetLoginUserID();  //获取登录人员ID
            //    ViewBag.whkpID = uId;
            //    ViewBag.inerFinOutID = autoCreateOddNoService.getNextInerFinOutOrderNo(uId, "02");
            //    ViewBag.ladiID = "";
            //    ViewBag.outDate = DateTime.Today.ToString("yyyy-MM-dd");
            //    ViewBag.remarks = "";
            //}

            return View();
        }

        public ActionResult FinOutRecordDetlsEdit(string flg, string inerFinOutID, string ladiID, string whkpID, string outDate, string remarks)
        {
            if (flg == "1")
            {
                string uId = GetLoginUserID();  //获取登录人员ID
                ViewBag.whkpID = uId;
                ViewBag.inerFinOutID = autoCreateOddNoService.getNextInerFinOutOrderNo(uId, "02");
                ViewBag.ladiID = "";
                ViewBag.outDate = DateTime.Today.ToString("yyyy-MM-dd");
                ViewBag.remarks = "";
            }
            else
            {
                ViewBag.inerFinOutID = inerFinOutID;
                ViewBag.ladiID = ladiID;
                ViewBag.whkpID = whkpID;
                ViewBag.outDate = outDate;
                ViewBag.remarks = remarks;
            }
            
            ViewBag.state = flg;
            return View();
        }

        /// <summary>
        /// 内部成品送货单
        /// </summary>
        /// <returns>送货单视图</returns>
        public ActionResult FinOutPrintIndex(string inerFinOutID, string clientId,string OPdtId,string batchId)
        {
            ViewBag.inerFinOutID = inerFinOutID;
            ViewBag.clientId = clientId;
            ViewBag.OPdtId = OPdtId;
            ViewBag.batchId = batchId;
            return View();
        }

        /// <summary>
        /// 显示产品零件信息（子查询画面）
        /// </summary>
        /// <param name="abbrev">物料编号（产品零件略称）</param>
        /// <param name="name">物料名称（产品零件名称）</param>
        /// <returns></returns>
        public ActionResult ShowProdAndPartInfo(string abbrev, string name)
        {
            ViewBag.abbrev = abbrev;
            ViewBag.name = name;

            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="search">筛选条件</param>
        /// <returns>出库履历一览画面数据</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_storeFinOutRecordForSearch search)
        {

            IEnumerable<VM_storeFinOutRecordForTableShow> query = finOutStoreService.GetFinOutRecordForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_storeFinOutRecordForTableShow>();
                paging.total = 0;
            }
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return query.ToJson(paging.total);

        }

        /// <summary>
        /// 出库履历一览删除操作
        /// </summary>
        /// <param name="inerFinOutID">成品送货单号</param>
        /// <returns>删除结果</returns>
        [HttpPost]
        public JsonResult Delete(string inerFinOutID)
        {
            //返回值初始化
            bool r = true;
            try
            {
                //New一个List存放待操作的送货单号
                List<string> list = new List<string>();
                //将传递过来的参数以 “，”为分隔存放到delete变量中
                var delete = inerFinOutID.Split(',');
                //将分隔好的单号放到list中
                foreach (var de in delete)
                {
                    list.Add(de);
                }
                string uId = GetLoginUserID();
                //删除list中交仓单号的数据
                finOutStoreService.DeleteFinOutStore(list, uId);
            }
            catch (Exception e)
            {
                //异常错误捕捉返回False
                r = false;
            }
            return r.ToJson();

        }

        /// <summary>
        /// 出库履历详细
        /// </summary>
        /// <param name="inerFinOutID">成品送货单号</param>
        /// <param name="flag">页面标识</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库履历详细画面数据</returns>
        [HttpPost]
        public JsonResult ShowOutRecordDetail(string inerFinOutID, Paging paging)
        {

            string uId = GetLoginUserID();  //获取登录者的Id
            var users = finOutStoreService.GetFinOutRecordDetailById(inerFinOutID, uId, paging);
            return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
          
        }

        /// <summary>
        /// 登录保存
        /// </summary>
        /// <param name="finOutRecordList">画面数据</param>
        /// <param name="pageFlg">标识页面状态</param>
        /// <param name="outRecordList">画面更新数据</param>
        /// <returns>保存数据结果</returns>
        public ActionResult Login(VM_storeFinOutRecordDetailForTableShow finOutRecordList, string pageFlg, Dictionary<string, string>[] outRecordList)
        {
            bool result = true;
            string message = "";
            try
            {
                string uId = GetLoginUserID();  //获取登录人员ID
                message = finOutStoreService.FinOutRecordForLogin(finOutRecordList, pageFlg, uId, outRecordList);
                if (message != "更新成功")
                {
                    result = false;
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
        /// 获得内部成品送货单画面数据
        /// </summary>
        /// <param name="inerFinOutID">内部成品送货单号</param>
        /// <param name="clientId">客户订单号</param>
        /// <param name="OPdtId">产品ID</param>
        /// <param name="batchId">批次号</param>
        /// <param name="page">分页参数</param>
        /// <returns>画面数据结果</returns>
        [HttpPost]
        public JsonResult ShowOutPrintIndex(string inerFinOutID,string clientId,string OPdtId,string batchId, Paging paging)
        {

            var users = finOutStoreService.GetFinOutPrintIndexById(inerFinOutID,clientId,OPdtId,batchId,paging);
            return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器

        }

        /// <summary>
        /// 出库履历详细新增页面 根据输入的零件略称自动生成方法
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        [HttpPost]
        public JsonResult GetProductInfo(string partAbbrevi)
        {
            var productInfo = finOutStoreService.GetFinOutRecordPdtInfoById(partAbbrevi);

            JsonResult jr = new JsonResult();
            if (productInfo != null)
            {
                jr.Data = new
                {
                    ProductName = productInfo[0].PartName,
                    PrchsUp = productInfo[0].Pricee
                };
            }
            else
            {
                jr.Data = null;
            }
            
            return jr;
           
        }

        /// <summary>
        /// 获取产品零件信息（子查询画面）
        /// </summary>
        /// <param name="search"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProdAndPartInfo(VM_ProdAndPartInfo search, Paging paging)
        {
            var proInfoList = finOutStoreService.GetProdAndPartInfo(search, paging);
            return proInfoList.ToJson(paging.total);
        }
    }
}
