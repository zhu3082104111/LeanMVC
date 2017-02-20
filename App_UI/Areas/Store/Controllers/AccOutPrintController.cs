/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccOutPrintController.cs
// 文件功能描述：
//          附件库出库单打印选择Controller
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
    public class AccOutPrintController : Controller
    {
        private IWipStoreService wipStoreService;
        private IAccStoreService accStoreService;
        
        
        public AccOutPrintController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
         {
            this.wipStoreService=wipStoreService;
            this.accStoreService = accStoreService;
         }
        
        #region 出库单打印选择(附件库)(fyy修改)

         /// <summary>
         /// (附件库)出库单打印选择
         /// </summary>
         /// <returns></returns>
         public ActionResult Index()
         {
             //检查页面权限
             int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
             return View(author);
         } //end Index

         //[CustomAuthorize]
         [HttpPost]
         public JsonResult Get(VM_AccOutPrintForSearch accOutPrintForSearch, Paging paging)
         {
             int total;
             var users = accStoreService.GetAccOutPrintBySearchByPage(accOutPrintForSearch, paging);
             total = paging.total;
             return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
         } //end Get
         
         #endregion

        #region 材料领用出库单(附件库)(fyy修改)
         
        /// <summary>
         /// (附件库)出库单打印预览
         /// </summary>
         /// <param name="pickListID">领料单</param>
         /// <param name="saeetID">加工产品出库单号</param>
         /// <param name="pickListDetNoArr">领料单详细号结果集</param>
         /// <param name="bthIDArr">批次号结果集</param>
         /// <returns></returns>
         /// 修改者：冯吟夷
         public ActionResult AccOutPrintIndex(string pickListID, string tecnProductOutID, string batchIDArr, string pickListDetailNOArr)
         {
             ViewBag.PickListID = pickListID;
             ViewBag.TecnProductOutID = tecnProductOutID;
             ViewBag.BatchIDArr = batchIDArr;
             ViewBag.PickListDetailNOArr = pickListDetailNOArr;

             VM_AccOutPrintIndexForInfoShow accOutPrintIndexForInfoShow = accStoreService.GetAccOutPrintForInfoShow(pickListID);
             accOutPrintIndexForInfoShow.WareHouseKpName = Convert.ToString(Session["UserID"]);

             return View(accOutPrintIndexForInfoShow);
         
         } //end AccOutPrintIndex

        /// <summary>
        /// 获取(附件库)出库单打印预览结果集
        /// </summary>
        /// <param name="saeetID">加工产品出库单号</param>
        /// <param name="pickListDetNoArr">领料单详细号结果集</param>
        /// <param name="bthIDArr">批次号结果集</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        [HttpPost]
        public JsonResult GetPreview(string saeetID, string pickListDetNoArr, string bthIDArr)
        {
            if (string.IsNullOrEmpty(saeetID) == true) //判断是否有值
            {
                return null;
            }


            List<AccOutDetailRecord> accOutDetailRecordList = new List<AccOutDetailRecord>(); //定义 AccOutDetailRecord 泛型
            string[] bArr = bthIDArr.Split(',');
            string[] pldNOArr = pickListDetNoArr.Split(',');

            for (int index = 0; index < bArr.Length; index++) //循环分别为每个 AccOutDetailRecord 对象赋值
            {
                AccOutDetailRecord accOutDetailRecord = new AccOutDetailRecord(); //新增 AccOutDetailRecord 对象
                accOutDetailRecord.SaeetID = saeetID;
                accOutDetailRecord.PickListDetNo = pldNOArr[index];
                accOutDetailRecord.BthID = bArr[index];

                accOutDetailRecordList.Add(accOutDetailRecord); //添加 AccOutDetailRecord 对象
            }
            List<VM_AccOutPrintIndexForTableShow> accOutPrintIndexForTableShowList = accStoreService.GetAccOutPrintForTableShow(accOutDetailRecordList);

            return accOutPrintIndexForTableShowList.ToJson(0);
        } //end GetPreview

        #endregion

        public ActionResult AccOutPrintPreview(string Pid)//参数需要加工产品出库单（送货单号）、批次号、送货单详细号
         {
             int total = 0;
             if (string.IsNullOrEmpty(Pid))
             {
                 //return Content("请选择您要查询的数据");
             }
             var users = wipStoreService.SelectWipStore(Pid);
             return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
         }

    } //end AccOutPrintController
}
