using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace App_UI.Areas.Store.Controllers
{
    public class SemOutPrintController : BaseController
    {
        private IWipStoreService wipStoreService;
        private ISemStoreService semStoreService;


        public SemOutPrintController(IWipStoreService wipStoreService, ISemStoreService semStoreService)
         {
            this.wipStoreService=wipStoreService;
            this.semStoreService = semStoreService;
         } //end SemOutPrintController

        #region 出库单打印选择(半成品库)(fyy修改)

        /// <summary>
        /// (半成品库)出库单打印选择
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
         {
             //检查页面权限
             int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限
             return View(author);
         } //end Index
        
        //[CustomAuthorize]
        /// <summary>
        /// 获取(半成品库)出库单打印选择结果集
        /// </summary>
        /// <param name="semOutPrintForSearch">VM_SemOutPrintForSearch 表单查询类</param>
        /// <param name="paging">Paging 分页属性类</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        [HttpPost]
        public JsonResult Get(VM_SemOutPrintForSearch semOutPrintForSearch, Paging paging)
        {
            int total;
            var users = semStoreService.GetSemOutPrintBySearchByPage(semOutPrintForSearch, paging);           
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        } //end Get

        #endregion

        #region 材料领用出库单(半成品库)(fyy修改)

        /// <summary>
        /// (半成品库)材料领用出库单
        /// </summary>
        /// <param name="pickListID">领料单</param>
        /// <param name="tecnProductOutID">加工产品出库单号</param>
        /// <param name="batchIDArr">批次号结果集</param>
        /// <param name="pickListDetailNOArr">领料单详细号结果集</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        public ActionResult SemOutPrintIndex(string pickListID, string tecnProductOutID, string batchIDArr, string pickListDetailNOArr)
        {
            ViewBag.PickListID = pickListID;
            ViewBag.TecnProductOutID = tecnProductOutID;
            ViewBag.BatchIDArr = batchIDArr;
            ViewBag.PickListDetailNOArr = pickListDetailNOArr;

            VM_SemOutPrintIndexForInfoShow semOutPrintIndexForInfoShow = semStoreService.GetSemOutPrintForInfoShow(pickListID);
            semOutPrintIndexForInfoShow.WareHouseKpName = Convert.ToString(Session["UserID"]);
            
            return View(semOutPrintIndexForInfoShow);

        } //end SemOutPrintIndex

        /// <summary>
        /// 获取(半成品库)材料领用出库单结果集
        /// </summary>
        /// <param name="tecnProductOutID">加工产品出库单号</param>
        /// <param name="pickListDetailNOArr">领料单详细号结果集</param>
        /// <param name="batchIDArr">批次号结果集</param>
        /// <returns></returns>
        /// 修改者：冯吟夷
        [HttpPost]
        public JsonResult GetPreview(string pickListID, string tecnProductOutID, string batchIDArr, string pickListDetailNOArr)
        {
            if (string.IsNullOrEmpty(batchIDArr) == true) //判断是否有值
            {
                return null;
            }


            List<SemOutDetailRecord> semOutDetailRecordList = new List<SemOutDetailRecord>(); //定义 SemOutDetailRecord 泛型
            string[] bArr = batchIDArr.Split(',');
            string[] pldNOArr = pickListDetailNOArr.Split(',');

            for (int index = 0; index < bArr.Length; index++) //循环分别为每个 SemOutDetailRecord 对象赋值
            {
                SemOutDetailRecord semOutDetailRecord = new SemOutDetailRecord(); //新增 SemOutDetailRecord 对象
                semOutDetailRecord.TecnProductOutID = tecnProductOutID;
                semOutDetailRecord.PickListDetNo = pldNOArr[index];
                semOutDetailRecord.BatchID = bArr[index];

                semOutDetailRecordList.Add(semOutDetailRecord); //添加 SemOutDetailRecord 对象
            }
            List<VM_SemOutPrintIndexForTableShow> semOutPrintIndexForTableShowList = semStoreService.GetSemOutPrintForTableShow(pickListID, semOutDetailRecordList);

            return semOutPrintIndexForTableShowList.ToJson(0);
        } //end SemOutPrintPreview

        #endregion


    } //end SemOutPrintController
}
