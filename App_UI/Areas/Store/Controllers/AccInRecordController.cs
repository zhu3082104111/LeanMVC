/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInRecordController.cs
// 文件功能描述：
//          附件库入库履历一览Controller
//      
// 修改履历：2013/11/05 杨灿 新建
/*****************************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Extensions;
using Model;
using System;

namespace App_UI.Areas.Store.Controllers
{
    public class AccInRecordController : Controller
    {
        private IWipStoreService wipStoreService;
        private IAccStoreService accStoreService;

        public AccInRecordController(IWipStoreService wipStoreService, IAccStoreService accStoreService)
        {
            this.wipStoreService = wipStoreService;
            this.accStoreService = accStoreService;
        }
        //
        // GET: /Store/AccInRecord/

        public ActionResult Index()
        {
            //检查页面权限
            int author = 1;//按钮权限接口,在此假设1有创建权限，2无此权限           
            return View(author);
        }
 //==================1
//入库履历一览画面相关
//==================
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult GetAccInRecord(Paging paging, VM_AccInRecordStoreForSearch accInRecordStoreForSearch)
        {
            int total;
            var users = accStoreService.GetAccInRecordBySearchByPage(accInRecordStoreForSearch, paging);
            total = paging.total;
            return users.ToJson(total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
        }

        //入库履历一览删除
        //[CustomAuthorize]
        [HttpPost]
        public JsonResult AccInRecordDel(Dictionary<string, string>[] orderList)
        {
            string message = "";
            bool result = true;
            try
            {
                List<VM_AccInRecordStoreForTableShow> accInRecordStoreForTableShowList = new List<VM_AccInRecordStoreForTableShow>();
                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_AccInRecordStoreForTableShow accInRecordStore= new VM_AccInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    //采购订单
                    accInRecordStore.PrhaOdrID = orderList[i]["PrhaOdrID"];
                    //送货单号
                    accInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    //批次号
                    accInRecordStore.BthID = orderList[i]["BthID"];
                    //物资验收入库单号
                    accInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    //检验报告单号
                    accInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    //让步区分
                    accInRecordStore.GiCls = orderList[i]["GiCls"];
                    //零件ID
                    accInRecordStore.PdtID = orderList[i]["PdtID"];
                    //零件名称
                    accInRecordStore.PdtName = orderList[i]["PdtName"];
                    //规格型号
                    accInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    //数量
                    accInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    //单位
                    accInRecordStore.Unit = orderList[i]["Unit"];
                    //单价
                    accInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    //金额
                    accInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // accInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    //备注
                    accInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    //单价标识
                    accInRecordStore.AccInRecordPriceFlg = orderList[i]["AccInRecordPriceFlg"];
                    accInRecordStoreForTableShowList.Add(accInRecordStore);
                }

                //入库履历一览删除方法
                if (accInRecordStoreForTableShowList.Count != 0)
                {    
                    //假注释
                    //message = accStoreService.AccInRecordForDel(accInRecordStoreForTableShowList);
                    //暂用方法************************
                    message = accStoreService.AccInRecordForDelTest(accInRecordStoreForTableShowList);



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
        public ActionResult AccInRecordSave(Dictionary<string, string>[] orderList)
        {

            bool result = true;
            try
            {
                List<VM_AccInRecordStoreForTableShow> accInRecordStoreForTableShowList = new List<VM_AccInRecordStoreForTableShow>();

                for (int i = 0; i < orderList.Length; i++)
                {
                    VM_AccInRecordStoreForTableShow accInRecordStore = new VM_AccInRecordStoreForTableShow();
                    //orderList[i]["RowIndex"];
                    accInRecordStore.PrhaOdrID = orderList[i]["PrhaOdrID"];
                    accInRecordStore.DeliveryOrderID = orderList[i]["DeliveryOrderID"];
                    accInRecordStore.BthID = orderList[i]["BthID"];
                    accInRecordStore.McIsetInListID = orderList[i]["McIsetInListID"];
                    accInRecordStore.IsetRepID = orderList[i]["IsetRepID"];
                    accInRecordStore.GiCls = orderList[i]["GiCls"];
                    accInRecordStore.PdtID = orderList[i]["PdtID"];
                    accInRecordStore.PdtName = orderList[i]["PdtName"];
                    accInRecordStore.PdtSpec = orderList[i]["PdtSpec"];
                    accInRecordStore.Qty = Convert.ToDecimal(orderList[i]["Qty"]);
                    accInRecordStore.Unit = orderList[i]["Unit"];
                    accInRecordStore.PrchsUp = Convert.ToDecimal(orderList[i]["PrchsUp"]);
                    accInRecordStore.NotaxAmt = Convert.ToDecimal(orderList[i]["NotaxAmt"]);
                    // accInRecordStore.InDate = Convert.ToDateTime(orderList[i]["InDate"]) ;
                    accInRecordStore.Rmrs = orderList[i]["Rmrs"];
                    accInRecordStore.AccInRecordPriceFlg = orderList[i]["AccInRecordPriceFlg"];
                    accInRecordStoreForTableShowList.Add(accInRecordStore);
                }

                    //修改方法
                    if (accInRecordStoreForTableShowList.Count != 0)
                    {
                        //accStoreService.AccInRecordForUpdate(accInRecordStoreForTableShowList);
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
