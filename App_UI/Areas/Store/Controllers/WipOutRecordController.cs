/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名： WipOutRecordController.cs
// 文件功能描述：
//          在制品出库履历一览Controller
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
    public class WipOutRecordController : Controller
    {
        private IWipStoreService wipStoreService;

        public WipOutRecordController(IWipStoreService wipStoreService)
        {
            this.wipStoreService=wipStoreService;
        }
        //
        // GET: /Store/WipOutRecord/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限           
            return View(author);
        }

        //[CustomAuthorize]
        [HttpPost]
        public JsonResult Get(VM_WipOutRecordStoreForSearch wipOutRecordStoreForSearch, Paging paging)
        {
            int total;
            var users = wipStoreService.GetWipOutRecordBySearchByPage(wipOutRecordStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        //出库履历一览删除
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult WipOutRecordDel(Dictionary<string, string>[] orderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_WipOutRecordStoreForTableShow> wipOutRecordStoreForTableShowList = new List<VM_WipOutRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipOutRecordStoreForTableShow wipOutRecordStore = new VM_WipOutRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    wipOutRecordStore.PickListID = orderList[i]["PickListID"];
                    wipOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                    wipOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                    wipOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                    wipOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                    wipOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                    wipOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                    wipOutRecordStore.BthID = orderList[i]["BthID"];
                    wipOutRecordStore.GiCls = orderList[i]["GiCls"];
                    wipOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    wipOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                    wipOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipOutRecordStore.Unit = orderList[i]["Unit"];
                    wipOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    wipOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                    wipOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                    wipOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                    wipOutRecordStoreForTableShowList.Add(wipOutRecordStore);
                }

                //入库履历一览删除方法
                if (wipOutRecordStoreForTableShowList.Count != 0)
                {
                    //wipStoreService.WipOutRecordForDel(wipOutRecordStoreForTableShowList);
                    //暂用方法************************
                    message = wipStoreService.WipOutRecordForDelTest(wipOutRecordStoreForTableShowList);
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


        //修改保存出库履历数据

        public ActionResult WipOutRecordSave(Dictionary<string, string>[] orderList)
        {

            bool result = true;
            try
            {
                List<VM_WipOutRecordStoreForTableShow> wipOutRecordStoreForTableShowList = new List<VM_WipOutRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_WipOutRecordStoreForTableShow wipOutRecordStore = new VM_WipOutRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    wipOutRecordStore.PickListID = orderList[i]["PickListID"];
                    wipOutRecordStore.PickListDetNo = orderList[i]["PickListDetNo"];
                    wipOutRecordStore.PickListTypeID = orderList[i]["PickListTypeID"];
                    wipOutRecordStore.SaeetID = orderList[i]["SaeetID"];
                    wipOutRecordStore.CallinUnitID = orderList[i]["CallinUnitID"];
                    wipOutRecordStore.MaterielID = orderList[i]["MaterielID"];
                    wipOutRecordStore.MaterielName = orderList[i]["MaterielName"];
                    wipOutRecordStore.BthID = orderList[i]["BthID"];
                    wipOutRecordStore.GiCls = orderList[i]["GiCls"];
                    wipOutRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    wipOutRecordStore.TecnProcess = orderList[i]["TecnProcess"];
                    wipOutRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    wipOutRecordStore.Unit = orderList[i]["Unit"];
                    wipOutRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    wipOutRecordStore.SellPrc = Convert.ToDecimal(orderList[i]["SellPrc"]);
                    wipOutRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // wipOutRecordStore.OutDate = Convert.ToDateTime(orderList[i]["OutDate"]) ;
                    wipOutRecordStore.Rmrs = orderList[i]["Rmrs"];
                    wipOutRecordStoreForTableShowList.Add(wipOutRecordStore);
                }
                //出库履历一览修改方法
                if (wipOutRecordStoreForTableShowList.Count != 0)
                {
                   // wipStoreService.WipOutRecordForUpdate(wipOutRecordStoreForTableShowList);
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
