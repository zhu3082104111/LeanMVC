/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemOutStoreController.cs
// 文件功能描述：
//          半成品库待出库一览Controller
//      
// 修改履历：
/*****************************************************************************/
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace App_UI.Areas.Store.Controllers
{
    /// <summary>
    /// 半成品库待出库一览Controller
    /// </summary>
    public class SemOutStoreController : BaseController
    {
        //半成品库出库画面的Service接口类
        private IWipStoreService wipStoreService;
        private ISemStoreService semStoreService;


        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="wipStoreService">Service接口类</param>
        /// <param name="semStoreService">Service接口类</param>
        public SemOutStoreController(IWipStoreService wipStoreService, ISemStoreService semStoreService)
        {
            this.wipStoreService=wipStoreService;
            this.semStoreService = semStoreService;
        } //end SemOutStoreController

        #region 待出库一览(半成品库)(fyy修改)

        /// <summary>
        /// 待出库一览(半成品库)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        } //end Index

        /// <summary>
        /// 获取待出库一览结果集(半成品库)
        /// </summary>
        /// <param name="semOutStoreForSearch">VM_SemOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        [HttpPost]
        public JsonResult Get(VM_SemOutStoreForSearch semOutStoreForSearch, Paging paging)
        {
            int total;
            var users = semStoreService.GetSemOutStoreBySearchByPage(semOutStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end Get

        #endregion

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Edit(string id)
        {
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    //return Content("请选择您要删除的数据");
                }
                var deleteID = id.Split(',');
                //定义数组存放需要删除的ID
                List<string> list = new List<string>();
                foreach (var Dsid in deleteID)
                {
                    list.Add(Dsid);
                }
                //然后执行删除的方法删除数据
                wipStoreService.UpdateWipStore(list);
            }
            catch (Exception e)
            {
                result = false;
            }

            return result.ToJson();
        } //end Edit

        public ActionResult Create()
        {
            ViewBag.OutStore_id = 0;
            return View();
        } //end Create

        [HttpPost]
        public JsonResult GetSoure(string OutStore_id)
        {
            int total = 0;
            var users = wipStoreService.GetWipStoreBySearchById(OutStore_id);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end GetSoure

        public ActionResult SemOutLogin(string materReqNOArr)
        {
            ViewBag.MaterReqNOArr = materReqNOArr;

            return View();
        } //end SemOutLogin

        /// <summary>
        /// 半成品库出库登录画面数据 陈健
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="materReqNO">领料单号</param>
        /// <returns>出库登录画面数据</returns>
        [HttpPost]
        public JsonResult GetSemOutLogin(Paging paging, string materReqNO)
        {
            int total;
            var users = semStoreService.GetSemOutStoreForLoginBySearchByPage(materReqNO, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        public ActionResult Save(WipStore WipStore)
        {

            bool result = true;
            try
            {
                wipStoreService.UpdateWipStore(WipStore);
            }
            catch (Exception e)
            {
                result = false;
            }
            return result.ToJson();
        } //end Save

        /// <summary>
        /// 出库登录批次别选择画面初始化 陈健
        /// </summary>
        /// <param name="qty">请领数量</param>
        /// <param name="pdtID">产品ID</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        public ActionResult SemOutBthSelect(decimal qty, string pdtID, string pickListID, string materReqDetailNo, string osSupProFlg, Paging paging)
        {
            int total = 0;
            var users = semStoreService.SelectSemStoreForBthSelect(qty, pdtID, pickListID, materReqDetailNo, osSupProFlg, paging);
            total = paging.total;
            return users.ToJson(total);

        }

        /// <summary>
        /// 出库登录保存 陈健
        /// </summary>
        /// <param name="orderList">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public ActionResult SaveSemOutForLogin(Dictionary<string, string>[] orderList, Dictionary<string, string>[] selectOrderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_SemOutLoginStoreForTableShow> semOutLoginStoreForTableShowList = new List<VM_SemOutLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_SemOutLoginStoreForTableShow semOutLoginStore = new VM_SemOutLoginStoreForTableShow();
                    semOutLoginStore.PickListID = orderList[i]["PickListID"];
                    semOutLoginStore.SaeetID = orderList[i]["SaeetID"];
                    semOutLoginStore.CallinUnitID = orderList[i]["CallinUnitID"];
                    semOutLoginStore.MaterielID = orderList[i]["MaterielID"];
                    semOutLoginStore.TecnProcess = orderList[i]["TecnProcess"];
                    semOutLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    semOutLoginStore.Unit = orderList[i]["Unit"];
                    semOutLoginStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                    semOutLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    semOutLoginStore.Rmrs = orderList[i]["Rmrs"];
                    semOutLoginStore.RowIndex = orderList[i]["RowIndex"];
                    semOutLoginStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    semOutLoginStore.MaterReqDetailNo = orderList[i]["MaterReqDetailNo"];
                    semOutLoginStoreForTableShowList.Add(semOutLoginStore);
                }
                //修改方法
                if (semOutLoginStoreForTableShowList.Count != 0)
                {
                    semStoreService.SemOutForLogin(semOutLoginStoreForTableShowList, selectOrderList);
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

    } //end SemOutStoreController
}
