/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipOutPrintController.cs
// 文件功能描述：
//          在制品库出库单打印选择Controller
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
    public class WipOutPrintController : Controller
    {
        private IWipStoreService wipStoreService;
        

        public WipOutPrintController(IWipStoreService wipStoreService)
         {
            this.wipStoreService=wipStoreService;
         } //end WipOutPrintController
        
        /// <summary>
         /// (在制品库)出库单打印选择
         /// </summary>
         /// <returns></returns>
        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
            return View(author);
        } //end Index

        /// <summary>
        /// 获取(在制品库)出库单打印选择结果集
        /// </summary>
        /// <param name="wipOutPrintForSearch">VM_WipOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public JsonResult Get(VM_WipOutPrintForSearch wipOutPrintForSearch, Paging paging)
        {
            int total;
            var users = wipStoreService.GetWipOutPrintBySearchByPage(wipOutPrintForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end Get

        /// <summary>
        /// (在制品库)出库单打印预览
        /// </summary>
        /// <param name="pickListID">领料单</param>
        /// <param name="tecnProductOutID">加工产品出库单号</param>
        /// <param name="pickListDetailNOArr">领料单详细号结果集</param>
        /// <param name="batchIDArr">批次号结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public ActionResult WipOutPrintIndex(string pickListID, string tecnProductOutID, string pickListDetailNOArr, string batchIDArr)
        {
            ViewBag.TecnProductOutID = tecnProductOutID;
            ViewBag.BatchIDArr = batchIDArr;
            ViewBag.PickListDetailNOArr = pickListDetailNOArr;

            VM_WipOutPrintIndexForInfoShow wipOutPrintIndexForInfoShow = wipStoreService.GetWipOutPrintForInfoShow(pickListID);
            wipOutPrintIndexForInfoShow.WareHouseKpName = Convert.ToString(Session["UserID"]);

            return View(wipOutPrintIndexForInfoShow);

        } //end WipOutPrintIndex

        /// <summary>
        /// 获取(在制品库)出库单打印预览结果集
        /// </summary>
        /// <param name="tecnProductOutID">加工产品出库单号</param>
        /// <param name="pickListDetailNOArr">领料单详细号结果集</param>
        /// <param name="batchIDArr">批次号结果集</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        [HttpPost]
        public JsonResult GetPreview(string tecnProductOutID, string batchIDArr, string pickListDetailNOArr)
        {
            if (string.IsNullOrEmpty(batchIDArr) == true) //判断是否有值
            {
                return null;
            }


            List<WipOutDetailRecord> wipOutDetailRecordList = new List<WipOutDetailRecord>(); //定义 WipOutDetailRecord 泛型
            string[] bArr = batchIDArr.Split(',');
            string[] pldNOArr = pickListDetailNOArr.Split(',');

            for (int index = 0; index < bArr.Length; index++) //循环分别为每个 WipOutDetailRecord 对象赋值
            {
                WipOutDetailRecord wipOutDetailRecord = new WipOutDetailRecord(); //新增 WipOutDetailRecord 对象
                wipOutDetailRecord.TecnPdtOutID = tecnProductOutID;
                wipOutDetailRecord.PickListDetNo = pldNOArr[index];
                wipOutDetailRecord.BthID = bArr[index];

                wipOutDetailRecordList.Add(wipOutDetailRecord); //添加 WipOutDetailRecord 对象
            }
            List<VM_WipOutPrintIndexForTableShow> wipOutPrintIndexForTableShowList = wipStoreService.GetWipOutPrintForTableShow(wipOutDetailRecordList);

            return wipOutPrintIndexForTableShowList.ToJson(0);
        } //end GetPreview

        public ActionResult WipOutPrintPreview(string Pid)
        {
            int total = 0;
            if (string.IsNullOrEmpty(Pid))
            {
                //return Content("请选择您要查询的数据");
            }
            var users = wipStoreService.SelectWipStore(Pid);
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }


    } //end WipOutPrintController
}
