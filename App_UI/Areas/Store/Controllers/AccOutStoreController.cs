/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccOutStoreController.cs
// 文件功能描述：
//          附件库待出库一览Controller
//      
// 修改履历：2013/11/05 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 附件库待出库一览Controller
    /// </summary>
    public class AccOutStoreController : Controller
    {
        //附件库出库画面的Service接口类
        private IWipStoreService wipStoreService;
        private IAccStoreService accStoreService;


        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="wipStoreService">Service接口类</param>
        /// <param name="accStoreService">Service接口类</param>
        public AccOutStoreController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService=wipStoreService;
            this.accStoreService = accStoreService;
        }

        #region 待出库一览(附件库)(fyy修改)

        /// <summary>
        /// 待出库一览(附件库)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        } //end Index

        /// <summary>
        /// 获取待出库一览结果集(附件库)
        /// </summary>
        /// <param name="accOutStoreForSearch">VM_AccOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        [HttpPost]
        public JsonResult Get(VM_AccOutStoreForSearch accOutStoreForSearch, Paging paging)
        {
            int total;
            var users = accStoreService.GetAccOutStoreBySearchByPage(accOutStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end Get

        #endregion

        public ActionResult AccOutLogin(string materReqNOArr)
        {
            ViewBag.MaterReqNOArr = materReqNOArr;

            return View();
        } //end AccOutLogin

        /// <summary>
        /// 出库登录画面 陈健
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <returns>出库登录画面数据</returns>
        [HttpPost]
        public JsonResult GetAccOutLogin(Paging paging, string pickListID, string materielID, string pickListDetNo)
        {
            int total;
            var users = accStoreService.GetAccOutStoreForLoginBySearchByPage(pickListID, materielID,pickListDetNo, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 出库登录保存 陈健
        /// </summary>
        /// <param name="orderList">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public ActionResult SaveAccOutForLogin(Dictionary<string, string>[] orderList, Dictionary<string, string>[] selectOrderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_AccOutLoginStoreForTableShow> accOutLoginStoreForTableShowList = new List<VM_AccOutLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_AccOutLoginStoreForTableShow accOutLoginStore = new VM_AccOutLoginStoreForTableShow();
                    accOutLoginStore.PickListID = orderList[i]["PickListID"];
                    accOutLoginStore.SaeetID = orderList[i]["SaeetID"];
                    accOutLoginStore.CallinUnitID = orderList[i]["CallinUnitID"];
                    accOutLoginStore.MaterielID = orderList[i]["MaterielID"];
                    accOutLoginStore.TecnProcess = orderList[i]["TecnProcess"];
                    accOutLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    accOutLoginStore.Unit = orderList[i]["Unit"];
                    accOutLoginStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                    accOutLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    accOutLoginStore.Rmrs = orderList[i]["Rmrs"];
                    accOutLoginStore.RowIndex = orderList[i]["RowIndex"];
                    accOutLoginStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    accOutLoginStore.PickListDetNo = orderList[i]["PickListDetNo"];
                    accOutLoginStoreForTableShowList.Add(accOutLoginStore);
                }              
                //修改方法
                if (accOutLoginStoreForTableShowList.Count != 0)
                {
                    accStoreService.AccOutForLogin(accOutLoginStoreForTableShowList, selectOrderList);
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                result = false;
                message = "请检查输入数据！";
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
        /// 出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="pickListDetNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        public ActionResult AccOutBthSelect(decimal qty, string pdtID, string pickListID, string pickListDetNo, string osSupProFlg, Paging paging)
        {
            int total = 0;
            var users = accStoreService.SelectAccOutStoreForBthSelect(qty, pdtID, pickListID, pickListDetNo, osSupProFlg, paging);
            total = paging.total;
            return users.ToJson(total);

        }

    }
}
