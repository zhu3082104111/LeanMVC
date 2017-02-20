/*****************************************************************************
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：FinInRecordController.cs
// 文件功能描述：
//          内部成品库入库履历画面用Controller
//
// 修改履历：2013/11/25 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using Model.Store;
using App_UI.App_Start;
using Model;
using App_UI.Areas.Controllers;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 内部成品库入库履历画面用Controller
    /// </summary>
    public class FinInRecordController : BaseController
    {
        //内部成品库入库画面的Service接口类
        private IFinInStoreService finInStoreService;

        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="finInStoreService">Service接口类</param>
        public FinInRecordController(IFinInStoreService finInStoreService) 
        {
            this.finInStoreService = finInStoreService;
        }

        /// <summary>
        /// 界面初始加载
        /// </summary>
        /// <returns>入库履历一览视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 入库履历详细/登录
        /// </summary>
        /// <param name="flg">详细页面状态</param>
        /// <param name="id">成品交仓单号</param>
        /// <returns>详细页面视图</returns>
        public ActionResult FinInRecordDetls(string flg, string id)
        {
            string uId = GetLoginUserID();  //获取登录人员ID
            ViewBag.uId = uId;
            ViewBag.productWarehouseID = id;
            ViewBag.state = flg;

            //存放返回的值，（成品交仓单号、批次号、仓库编号、入库移动区分、入库日期、仓管员和备注）
            //详细跳转
            if (flg == "1")
            {
                VM_StoreFinInRecordForTableShow Info = finInStoreService.GetDetailInformation(id);
                ViewBag.planId = Info.PlanID;
                ViewBag.batchId = Info.BatchID;
                ViewBag.wareHouseId = Info.WareHouseID;
                ViewBag.inMoveCls = Info.InMoveCls;
                ViewBag.inDate = Info.InDate.ToString("yyyy-MM-dd");
                ViewBag.wareHouseKpId = Info.WareHouseKpID;
                ViewBag.remarks = Info.Remarks;
            }
            //手工登录跳转
            else if (flg == "0")
            {
                ViewBag.planId = "";
                ViewBag.batchId = "";
                ViewBag.wareHouseId = "";
                ViewBag.inMoveCls = "";
                ViewBag.inDate = DateTime.Today.ToString("yyyy-MM-dd");
                ViewBag.wareHouseKpId = uId;
                ViewBag.remarks = "";
            }
            //入库新添加跳转
            else
            {
                VM_StoreFinInRecordForDetailShow Info = finInStoreService.GetDetailInformations(id);
                ViewBag.planId = Info.ProductWarehouseID;
                ViewBag.batchId = Info.BatchID;
                ViewBag.wareHouseId = Info.WareHouseID;
                ViewBag.inMoveCls = Info.InMoveCls;
                ViewBag.inDate = Info.InDate.ToString("yyyy-MM-dd");
                ViewBag.wareHouseKpId = uId;
                ViewBag.remarks = Info.MRemarks;
            }

            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="search">刷选条件</param>
        /// <returns>入库履历一览画面数据</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_StoreFinInRecordForSearch search)
        {
            IEnumerable<VM_StoreFinInRecordForTableShow> query = finInStoreService.GetFinInRecordForSearch(search, paging);
            
            //object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            return query.ToJson(paging.total);

        }

        /// <summary>
        /// 入库详细画面
        /// </summary>
        /// <param name="productWarehouseID">成品交仓单号</param>
        /// <param name="flag">标记页面状态</param>
        /// <param name="paging">分页参数</param>
        /// <returns>入库履历详细画面数据</returns>
        [HttpPost]
        public JsonResult ShowInRecordDetail(string productWarehouseID,string flag,Paging paging)
        {
            //flag标记页面状态
            //0—手动登录状态
            //1—履历一览画面点击详细跳转状态
            //2—待入库一览点击入库跳转状态
            if (flag == "1")
            {
                var users = finInStoreService.GetFinInRecordDetailById(productWarehouseID, paging);
                return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            }
            else
            {
                var users = finInStoreService.GetFinInRecordBySearchById(productWarehouseID, paging);
                return users.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
            }
        }

        /// <summary>
        /// 登录保存
        /// </summary>
        /// <param name="finInRecordList">画面更新数据</param>
        /// <param name="pageFlg">标识页面状态</param>
        /// <param name="editFlg">标识编辑状态</param>
        /// <param name="inRecordList">画面更新数据</param>
        /// <returns>保存数据结果</returns>
        public ActionResult Login(VM_StoreFinInRecordForDetailShow finInRecordList, string pageFlg, string editFlg, Dictionary<string, string>[] inRecordList)
        {
            bool result = true;
            string message = "";
            try
            {
                string uId = GetLoginUserID();  //获取登录人员ID
                message = finInStoreService.FinInRecordForLogin(finInRecordList, pageFlg, editFlg, uId, inRecordList);
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
        /// 删除
        /// </summary>
        /// <param name="planID">成品交仓单号</param>
        /// <returns>删除结果</returns>
        [HttpPost]
        public JsonResult Delete(string planID)
        {
            //返回值初始化
            bool r = true;
            try
            {
                //New一个List存放待操作的交仓单号
                List<string> list = new List<string>();
                //将传递过来的参数以 “，”为分隔存放到delete变量中
                var delete = planID.Split(',');
                //将分隔好的单号放到list中
                foreach (var de in delete)
                {
                    list.Add(de);
                }
                string uId = GetLoginUserID();
                //删除list中交仓单号的数据
                finInStoreService.DeleteFinInStore(list,uId);
            }
            catch (Exception e)
            {
                //异常错误捕捉返回False
                r = false;
            }
            return r.ToJson();

        }

        /// <summary>
        /// 入库详细手工登录状态，根据输入的零件略称自动生成零件名称（暂时不用）
        /// </summary>
        /// <param name="partAbbrevi">零件略称</param>
        /// <returns>零件信息结果</returns>
        [HttpPost]
        public JsonResult GetProductName(string partAbbrevi)
        {
            var productInfo = finInStoreService.GetFinInRecordPdtInfoById(partAbbrevi);
            JsonResult p =new JsonResult();
            if (productInfo == null)
            {
                p.Data = null;
            }
            else
            {
                p.Data = productInfo[0].PartName;
            }
            return p;
        
        }

    }
}
