/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：SemOutStoreController.cs
// 文件功能描述：
//          半成品出库履历一览Controller
//      
// 修改履历：2013/11/05 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Web.Security;
using Util;
using DoddleReport;
using DoddleReport.Web;
using App_UI.Helper;
using App_UI.App_Start;
using App_UI.Areas;
using App_UI.Areas.Controllers;
using Extensions;

namespace App_UI.Areas.Store.Controllers
{
    public class SemOutRecordController : Controller 
    {
        private ISemStoreService semStoreService;

        public SemOutRecordController(ISemStoreService semStoreService)
        {
            this.semStoreService = semStoreService;
        }
        //
        // GET: /Store/AccOutRecord/

         public ActionResult Index()
         {
             //检查页面权限
             int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限           
             return View(author);
         }

         //[CustomAuthorize]
         [HttpPost]
         public JsonResult Get(VM_SemOutRecordStoreForSearch semOutRecordStoreForSearch, Paging paging)
         {
             int total;
             var users = semStoreService.GetSemOutRecordBySearchByPage(semOutRecordStoreForSearch, paging);
             total = paging.total;
             return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
         }

         //出库履历一览删除
         //[CustomAuthorize]
         [HttpPost]
         public JsonResult SemOutRecordDel(Dictionary<string, string>[] orderList)
         {
             bool result = true;
             try
             {
                 List<VM_SemOutRecordStoreForTableShow> semOutRecordStoreForTableShowList = new List<VM_SemOutRecordStoreForTableShow>();
                 for (int i = 0; i < orderList.Length; i++)
                 {
                     VM_SemOutRecordStoreForTableShow semOutRecordStore = new VM_SemOutRecordStoreForTableShow();
                     //orderList[i]["RowIndex"];
                     semOutRecordStore.PickListID = orderList[i]["PickListID"];
                     semOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     semOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                     semOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                     semOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                     semOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                     semOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                     semOutRecordStore.BthID = orderList[i]["BthID"];
                     semOutRecordStore.GiCls = orderList[i]["GiCls"];
                     semOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                     semOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                     semOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                     semOutRecordStore.Unit = orderList[i]["Unit"];
                     semOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                     semOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                     semOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                     // semOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                     semOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                     semOutRecordStoreForTableShowList.Add(semOutRecordStore);
                 }

                 //入库履历一览删除方法
                 if (semOutRecordStoreForTableShowList.Count != 0)
                 {
                     //semStoreService.SemOutRecordForDel(semOutRecordStoreForTableShowList);
                 }
                 else
                 {

                 }
             }
             catch (Exception e)
             {
                 result = false;
             }
             return result.ToJson();
         }


         //修改保存出库履历数据

         public ActionResult SemOutRecordSave(Dictionary<string, string>[] orderList)
         {

             bool result = true;
             try
             {
                 List<VM_SemOutRecordStoreForTableShow> semOutRecordStoreForTableShowList = new List<VM_SemOutRecordStoreForTableShow>();
                 for (int i = 0; i < orderList.Length; i++)
                 {
                     VM_SemOutRecordStoreForTableShow semOutRecordStore = new VM_SemOutRecordStoreForTableShow();
                     //orderList[i]["RowIndex"];
                     semOutRecordStore.PickListID = orderList[i]["PickListID"];
                     semOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     semOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                     semOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                     semOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                     semOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                     semOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                     semOutRecordStore.BthID = orderList[i]["BthID"];
                     semOutRecordStore.GiCls = orderList[i]["GiCls"];
                     semOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                     semOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                     semOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                     semOutRecordStore.Unit = orderList[i]["Unit"];
                     semOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                     semOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                     semOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                     // semOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                     semOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                     semOutRecordStoreForTableShowList.Add(semOutRecordStore);
                 }
                 //出库履历一览修改方法
                 if (semOutRecordStoreForTableShowList.Count != 0)
                 {
                     // semStoreService.SemOutRecordForUpdate(semOutRecordStoreForTableShowList);
                 }
                 else
                 {

                 }
             }
             catch (Exception e)
             {
                 result = false;
             }
             return result.ToJson();
         }

    }
}
