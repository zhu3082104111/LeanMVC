using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using App_UI.Areas.Login.Models;
using Extensions;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;
using BLL.Common;


namespace App_UI.Areas.Store.Controllers
{
    public class DiscardController : BaseController
    {
        //实例化
        private IDiscardService  discardService;
            
        private  IAutoCreateOddNoService iautoCreatNo;
        public DiscardController(IDiscardService discardService, IAutoCreateOddNoService iautoCreatNo) 
            {
                this.discardService = discardService;
            this.iautoCreatNo=iautoCreatNo;
            }
        //
        //在库品报废申请详细
        public ActionResult Index()
        {
            return View();
        }

        //
        //在库品报废申请详细
        public ActionResult Creat()
        {
            string uId = Session["UserID"].ToString();//取得UserID
            ViewBag.autoNo = iautoCreatNo.getNextDiscardOrderNo(uId, "01");//自动生成报废单号
            return View();
        }

        //
        //弹出页面
        public ActionResult CreatBthList( string rowindex)
        {
            ViewBag.rowindex = rowindex;
            return View();
        }



        /// <summary>
        /// 在库品报废申请详细
        /// </summary>
        /// <param name="flg">标识字符</param>
        /// <param name="pdtID">物料编号</param>
        /// <param name="whId">仓库ID</param>
        /// <param name="BthID">批次号</param>
        /// <param name="discardId">报废单号</param>
        /// <returns>跳转页面</returns>
        public ActionResult ApplicationDetls(string flg, string pdtID, string whId, string discardId, string bthID)
        {
            
            string uId = Session["UserID"].ToString();
            ViewBag.autoNo = iautoCreatNo.getNextDiscardOrderNo(uId,"01");
            //flg={1:参照画面；其它：非参照画面}
            ViewBag.state = flg;
            ViewBag.id = pdtID;
            ViewBag.iid = whId;
            ViewBag.iiid = discardId;
            ViewBag.BthID = bthID;
            return View();
        }

        /// <summary>
        /// 参照画面
        /// </summary>
        /// <param name="flg">标识字符</param>
        /// <param name="pdtID">物料编号</param>
        /// <param name="whId">仓库ID</param>
        /// <param name="BthID">批次号</param>
        /// <param name="discardId">报废单号</param>
        /// <returns>跳转页面</returns>
        public ActionResult ReferringDetls(string flg, string pdtID, string whId, string discardId, string bthID)
        {

            
            ViewBag.state = flg;
            ViewBag.id = pdtID;
            ViewBag.iid = whId;
            ViewBag.iiid = discardId;
            ViewBag.BthID = bthID;
            return View();
        }

        /// <summary>
        /// 在库品报废申请详细（打印）
        /// </summary>
        /// <param name="id">报废单号</param>
        /// <returns>跳转页面</returns>
        public ActionResult PrintPreview( string DiscardId)
        {
            ViewBag.DiscardId = DiscardId;
            return View();
        }

        #region 待报废品一览
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="search">仓库ID</param>
        /// <returns>查询结果</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_StoreDiscardForSearch search)
        {
            
            IEnumerable<VM_StoreDiscardForShow> query = discardService.GetStoreDiscardForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_StoreDiscardForShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);
        }
        #endregion

        #region  弹出页面
        /// <summary>
        /// 弹出页面查询
        /// </summary>
        /// <param name="search">一组数据</param>
        /// <returns>查询结果</returns>
        [HttpPost]
        public JsonResult GetBthStockList(Paging paging, VM_StoreDiscardForSearch search)
        {

            IEnumerable<VM_StoreDiscardForShow> query = discardService.GetStoreBthStockListForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_StoreDiscardForShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);
        }

        #endregion

        #region 在库待报废品申请详细
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="iid">标识字符</param>
        /// <param name="discardId">报废单号</param>
        /// <param name="orderList">要保存数据</param>
        /// <returns>保存结果</returns> 
        public ActionResult SaveUpdata(VM_StoreDiscardDetailForTableShow discardRecordList, string discardId, Dictionary<string, string>[] orderList, string iid)
        {
            bool result = true;
            discardRecordList.DiscardID = discardId;
            string message = "";
           
                try
                {
                    message = discardService.DiscardSaveUpdata(discardRecordList, orderList, iid);
                    
                }
                catch (Exception e)
                {
                    message = e.InnerException.Message; ;
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
        /// 查询
        /// </summary>
        /// <param name="flg">标识字符</param>
        /// <param name="pdtID">物料编号</param>
        /// <param name="whId">仓库编号</param>
        /// <param name="bthID">批次号</param>
        /// <param name="discardId">报废单号</param>
        /// <returns>查询结果</returns>
        [HttpPost]
        public JsonResult GetApplication(Paging paging, VM_StoreDiscardDetailForSearch search, string flg, string pdtID, string whId, string bthID, string discardId)
        {
            search.DiscardID = discardId;
            search.PdtID = pdtID;
            search.WhID = whId;
            search.BthID = bthID;
            ViewBag.ii = search.PdtID;
            ViewBag.flg = flg;
            
            
                if (flg == null)
                {
                    //从在库品一览跳转的查询
                    IEnumerable<VM_StoreDiscardDetailForTableShow> query = discardService.GetStoreDiscardWHForSearchList(pdtID, whId, bthID,discardId, paging);
                    if (query == null)
                    {
                        query = new List<VM_StoreDiscardDetailForTableShow>();
                        paging.total = 0;
                    }
                    return query.ToJson(paging.total);
                }
                else 
                {   //参照画面跳转
                    IEnumerable<VM_StoreDiscardDetailForTableShow> query = discardService.GetDiscarorSearchList(search, paging);
                    if (query == null)
                    {
                        query = new List<VM_StoreDiscardDetailForTableShow>();
                        paging.total = 0;
                    }
                    return query.ToJson(paging.total);
                
            }
             
        }

#endregion

        #region 在库品申请打印预览
        [HttpPost]
        public JsonResult GetPrintPreview(Paging paging, VM_StoreDiscardDetailForSearch search, String DiscardId)
        {
            search.DiscardID=DiscardId;
            string flg = null;

            IEnumerable<VM_StoreDiscardDetailForTableShow> query = discardService.GetStoreDiscardWHForSearch(search, paging, flg);
            if (query == null)
            {
                query = new List<VM_StoreDiscardDetailForTableShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);
        }
        #endregion


    }
}
