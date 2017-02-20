/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipOutStoreController.cs
// 文件功能描述：
//          在制品库待出库一览Controller
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
    /// 在制品库待出库一览Controller
    /// </summary>
    public class WipOutStoreController : Controller
    {
        //在制品库出库画面的Service接口类
        private IWipStoreService wipStoreService;
        private IAccStoreService accStoreService;


        /// <summary>
        /// 方法实现，引入调用的Service
        /// </summary>
        /// <param name="wipStoreService">Service接口类</param>
        /// <param name="accStoreService">Service接口类</param>
        public WipOutStoreController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService=wipStoreService;
            this.accStoreService = accStoreService;
        }

        #region 待出库一览(在制品库)(fyy修改)

        /// <summary>
        /// 待出库一览(在制品库)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        } //end Index

        /// <summary>
        /// 获取待出库一览结果集(在制品库)
        /// </summary>
        /// <param name="wipOutStoreForSearch">VM_WipOutStoreForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public JsonResult Get(VM_WipOutStoreForSearch wipOutStoreForSearch, Paging paging)
        {
            int total;
            var users = wipStoreService.GetWipOutStoreBySearchByPage(wipOutStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end Get

        #endregion

        //跳转到出库登录界面
        //[CustomAuthorize]
        public ActionResult WipOutLogin(string materReqNOArr)
        {
            ViewBag.MaterReqNOArr = materReqNOArr;

            return View();
        } //end WipOutLogin

        /// <summary>
        /// 出库登录画面 陈健
        /// </summary>
        /// <param name="paging">分页参数</param>
        /// <param name="pickListID">领料单号</param>
        /// <param name="materielID">零件ID</param>
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <returns>出库登录画面数据</returns>
        [HttpPost]
        public JsonResult GetWipOutLogin(Paging paging, string pickListID, string materielID, string materReqDetailNo)
        {
            int total;
            var users = wipStoreService.GetWipOutStoreForLoginBySearchByPage(pickListID, materielID,materReqDetailNo, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        /// <summary>
        /// 出库登录保存
        /// </summary>
        /// <param name="orderList">出库登录画面数据</param>
        /// <param name="selectOrderList">批次别选择画面数据</param>
        /// <returns>保存数据结果</returns>
        public ActionResult SaveWipOutForLogin(Dictionary<string, string>[] orderList, Dictionary<string, string>[] selectOrderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_WipOutLoginStoreForTableShow> wipOutLoginStoreForTableShowList = new List<VM_WipOutLoginStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipOutLoginStoreForTableShow wipOutLoginStore = new VM_WipOutLoginStoreForTableShow();
                    wipOutLoginStore.PickListID = orderList[i]["PickListID"];
                    wipOutLoginStore.SaeetID = orderList[i]["SaeetID"];
                    wipOutLoginStore.CallinUnitID = orderList[i]["CallinUnitID"];
                    wipOutLoginStore.MaterielID = orderList[i]["MaterielID"];
                    wipOutLoginStore.TecnProcess = orderList[i]["TecnProcess"];
                    wipOutLoginStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipOutLoginStore.Unit = orderList[i]["Unit"];
                    wipOutLoginStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                    wipOutLoginStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    wipOutLoginStore.Rmrs = orderList[i]["Rmrs"];
                    wipOutLoginStore.RowIndex = orderList[i]["RowIndex"];
                    wipOutLoginStore.OsSupProFlg = orderList[i]["OsSupProFlg"];
                    wipOutLoginStore.MaterReqDetailNo = orderList[i]["MaterReqDetailNo"];
                    wipOutLoginStoreForTableShowList.Add(wipOutLoginStore);
                }
                //修改方法
                if (wipOutLoginStoreForTableShowList.Count != 0)
                {
                    wipStoreService.WipOutForLogin(wipOutLoginStoreForTableShowList, selectOrderList);
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
        /// <param name="materReqDetailNo">领料单详细号</param>
        /// <param name="osSupProFlg">外协、自生产区分标志</param>
        /// <param name="paging">分页参数</param>
        /// <returns>出库登录批次别选择画面集合</returns>
        public ActionResult WipOutBthSelect(decimal qty, string pdtID,string pickListID,string materReqDetailNo,string osSupProFlg,Paging paging) 
        {

            if (Session["xxx-data"] == null)
            {
                Session["xxx-data"] = new List<VM_WipOutBthForDTableShow>();
            }
            else
            {
                IList<VM_WipOutBthForDTableShow> data = null;
                data = (IList<VM_WipOutBthForDTableShow>)(Session["xxx-data"]);
                for (int i = 0; i < data.Count; i++)
                {
                    if (data.ElementAt(i).OsSupProFlg == osSupProFlg && data.ElementAt(i).MaterielID == pdtID)
                    { 
                    
                    }
                }


            }

                //IList<Object> data=null;
                //if (Session["xxx-data"] != null) {
                //    data= (IList<Object>)(Session["xxx-data"]);
                //}

                //object  obj=data.ElementAt(1);
                //var a = data.

            int total = 0;
            var users = wipStoreService.SelectWipStoreForBthSelect(qty, pdtID,pickListID,materReqDetailNo,osSupProFlg, paging);
            total = paging.total;
            return users.ToJson(total);
        
        }
    }
}
