/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名： AccOutRecordController.cs
// 文件功能描述：
//          附件库出库履历一览Controller
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
    public class AccOutRecordController : Controller
    {
         private IWipStoreService wipStoreService;
         private IAccStoreService accStoreService;

         public AccOutRecordController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService=wipStoreService;
            this.accStoreService = accStoreService;
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
         public JsonResult Get(VM_AccOutRecordStoreForSearch accOutRecordStoreForSearch, Paging paging)
         {
             int total;
             var users = accStoreService.GetAccOutRecordBySearchByPage(accOutRecordStoreForSearch, paging);
             total = paging.total;
             return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
         }

         //入库履历一览删除
         //[CustomAuthorize]
         [HttpPost]
         public JsonResult AccOutRecordDel(Dictionary<string, string>[] orderList)
         {
             string message = "";
             bool result = true;
             try
             {
                 List<VM_AccOutRecordStoreForTableShow> accOutRecordStoreForTableShowList = new List<VM_AccOutRecordStoreForTableShow>();
                 for (int i = 0; i < orderList.Length; i++)
                 {
                     VM_AccOutRecordStoreForTableShow accOutRecordStore = new VM_AccOutRecordStoreForTableShow();
                     //orderList[i]["RowIndex"];
                     accOutRecordStore.PickListID = orderList[i]["PickListID"];
                     accOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     accOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                     accOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     accOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                     accOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                     accOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                     accOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                     accOutRecordStore.BthID = orderList[i]["BthID"];                   
                     accOutRecordStore.GiCls = orderList[i]["GiCls"];
                     accOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                     accOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];                     
                     accOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                     accOutRecordStore.Unit = orderList[i]["Unit"];
                     accOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                     accOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                     accOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                     // accOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                     accOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                     accOutRecordStoreForTableShowList.Add(accOutRecordStore);
                 }

                 //入库履历一览删除方法
                 if (accOutRecordStoreForTableShowList.Count != 0)
                 {
                     //accStoreService.AccOutRecordForDel(accOutRecordStoreForTableShowList);
                     //暂用方法************************
                     message = accStoreService.AccOutRecordForDelTest(accOutRecordStoreForTableShowList);
                 }
                 else
                 {

                 }
             }
             catch (Exception e)
             {
                 //message = e.InnerException.Message;
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


         //修改保存入库履历数据
         public ActionResult AccOutRecordSave(Dictionary<string, string>[] orderList)
         {

             bool result = true;
             try
             {
                 List<VM_AccOutRecordStoreForTableShow> accOutRecordStoreForTableShowList = new List<VM_AccOutRecordStoreForTableShow>();
                 for (int i = 0; i < orderList.Length; i++)
                 {
                     VM_AccOutRecordStoreForTableShow accOutRecordStore = new VM_AccOutRecordStoreForTableShow();
                     //orderList[i]["RowIndex"];
                     accOutRecordStore.PickListID = orderList[i]["PickListID"];
                     accOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     accOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                     accOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                     accOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                     accOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                     accOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                     accOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                     accOutRecordStore.BthID = orderList[i]["BthID"];
                     accOutRecordStore.GiCls = orderList[i]["GiCls"];
                     accOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                     accOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                     accOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                     accOutRecordStore.Unit = orderList[i]["Unit"];
                     accOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                     accOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                     accOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                     // accOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                     accOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                     accOutRecordStoreForTableShowList.Add(accOutRecordStore);
                 }
                 //入库履历一览修改方法
                 if (accOutRecordStoreForTableShowList.Count != 0)
                 {
                     //accStoreService.AccOutRecordForUpdate(accOutRecordStoreForTableShowList);
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
