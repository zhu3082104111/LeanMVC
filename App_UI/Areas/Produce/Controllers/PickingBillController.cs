// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IPickingMaterialService.cs
// 文件功能描述：生产领料单Controller
// 
// 创建标识：代东泽 20131127
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using App_UI.App_Start;
using App_UI.Areas.Controllers;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Produce;
using System.Collections;
using Model;
using App_UI.Helper;
using BLL.Common;
using BLL.ServerMessage;
namespace App_UI.Areas.Produce.Controllers
{
    /// <summary>
    /// 代东泽 20131127
    /// </summary>
    public class PickingBillController : BaseController
    {
        //领料单service接口
        private IPickingMaterialService pickingMaterialService;

        private IAutoCreateOddNoService autoCreateOddNoService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pickingMaterialService"></param>
        public PickingBillController(IAutoCreateOddNoService autoCreateOddNoService,IPickingMaterialService pickingMaterialService) 
        { 
            this.pickingMaterialService=pickingMaterialService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        // Index
        // GET: /Produce/PickingBill/

        public ActionResult Index()
        {
            return View(1);
        }

        //
        // GET: /Produce/PickingBill/
        [HttpPost]
        public JsonResult Get(Paging paging,VM_ProduceMaterRequestForSearch search) 
        {
           IEnumerable<VM_ProduceMaterRequestForTableShow> query = pickingMaterialService.GetMaterialsForSearch(search, paging);
           return query.ToJson(paging.total);//object.ToJson方法把数据封装到json中,兼容ie及其他不支持json的浏览器
                  
        }
  
        //
        // GET: /Produce/PickingBill/Details/5

        public ActionResult Details(string id)
        {
            ViewData["model"] = UIConstant.OPERATE_MODEL_DETAIL;

            return View();
        }

        //
        // GET: /Produce/PickingBill/Create

        public ActionResult Create()
        {
            ViewData["model"] = UIConstant.OPERATE_MODEL_CREATE;
            return View();
        }
        /// <summary>
        /// 总装大工票
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult IsCanCreateByBigWorkticket(string id) 
        {
            bool canCreate = pickingMaterialService.IsCanCreatePickingBill(id, Constant.PickingBillComeFrom.ASSEM_DISPATCH);
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = canCreate;
            return js;
        }

        /// <summary>
        /// 代东泽 20131225
        /// 总装大工票 开具领料单
        /// GET: /Produce/PickingBill/CreateByTranslateCard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateByBigWorkticket(string id)
        {
            AssemBill card = new AssemBill();
            card.AssemBillID = id;
            IList<Model.Store.VM_Reserve> list = new List<Model.Store.VM_Reserve>();
            bool canCreate = pickingMaterialService.GetPickingBillDataByAssemBigBill(card, list);
            if (canCreate)
            {
                ViewData["comFrom"] = Constant.PickingBillComeFrom.Txt_ASSEM_DISPATCH;
                ViewData["comFromVlue"] = Constant.PickingBillComeFrom.ASSEM_DISPATCH;
                ViewData["AssemBillID"] = id;
                ViewData["ProcDelivID"] = "";
                ViewData["ExportID"] = card.ProductID;
                ViewData["ProcessID"] = card.AssemProcessID;
                return View("Details", list);
            }
            else
            {

                JsonResult js = new JsonResult();
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                js.Data = canCreate;
                return js;
            }
        }


        /// <summary>
        /// 代东泽 20131225
        /// 加工流转卡 开具领料单
        /// GET: /Produce/PickingBill/CreateByTranslateCard
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CreateByTranslateCard(string id)
        {
            ProcessTranslateCard card = new ProcessTranslateCard();
            card.ProcDelivID = id;
            IList<Model.Store.VM_Reserve> list = new List<Model.Store.VM_Reserve>();
            bool canCreate = pickingMaterialService.GetPickingBillDataByTranslateCard(card, list);
            if (canCreate)
            {
                ViewData["comFrom"] = Constant.PickingBillComeFrom.Txt_CIRCULATE_CARD;
                ViewData["comFromVlue"] = Constant.PickingBillComeFrom.CIRCULATE_CARD;
                ViewData["ProcDelivID"] = id;
                ViewData["AssemBillID"] ="";
                ViewData["ExportID"] = card.ExportID;
                ViewData["ProcessID"] = card.ProcessID;
                return View("Details", list);
            }
            else 
            {
                 
                JsonResult js = new JsonResult();
                js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                js.Data = canCreate;
                return js;
            }
        }

        /// <summary>
        /// 判断该流转卡是否可以继续领料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult IsCanCreateByTranslateCard(string id) 
        {
            bool canCreate = pickingMaterialService.IsCanCreatePickingBill(id, Constant.PickingBillComeFrom.CIRCULATE_CARD);
            JsonResult js = new JsonResult();
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            js.Data = canCreate;
            return js;
        }

        /// <summary>
        /// 代东泽 20131230
        /// 开具领料单页面的保存功能
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveBill(FormCollection form)
        {
            HandleResult hr = new HandleResult();
            try
            {
                ProduceMaterRequest pmq = new ProduceMaterRequest();
                pmq.MaterHandlerID = form["MaterHandlerID"];//领料人
                pmq.MaterReqNo = autoCreateOddNoService.GetPickingMateriaRequestId(this.GetLoginUserID());
                pmq.MaterReqType = form["comFromVlue"];
                //pmq.Purpose=form[""];
                pmq.DeptID = form["PickingUnit"];
                pmq.RequestDate = DateTime.Parse((string)form["RequestDate"]);
                pmq.DeptAuditFlag = "0";
                string ProcDelivID = form["ProcDelivID"];
                string AssemBillID = form["AssemBillID"];
                string ProcessID = form["ProcessID"];

                string ExportID = form["ExportID"];

                string PickingUnit = form["PickingUnit"];

                char[] d = new char[] { ',' };

                string OrdeBthDtailListID = form["OrdeBthDtailListID"];//仓库预约表中的批次号id
                string[] OrdeBthDtailListIDArr = OrdeBthDtailListID.Split(d);

                string CustomerOrderNum = form["CustomerOrderNum"];
                string[] CustomerOrderNumArr = CustomerOrderNum.Split(d);

                string WHID = form["WHID"];
                string[] WHIDArr = WHID.Split(d);

                string CustomerOrderDetails = form["CustomerOrderDetails"];
                string[] CustomerOrderDetailsArr = CustomerOrderDetails.Split(d);

                string PdtSpec = form["PdtSpec"];
                string[] PdtSpecArr = PdtSpec.Split(d);

                string BthID = form["BthID"];
                string[] BthIDArr = BthID.Split(d);

                string AppoQty = form["AppoQty"];
                string[] AppoQtyArr = AppoQty.Split(d);

                string UnitPrice = form["UnitPrice"];
                string[] UnitPriceArr = UnitPrice.Split(d);

                string PriceUnitID = form["PriceUnitID"];
                string[] PriceUnitIDArr = PriceUnitID.Split(d);

                string MaterialID = form["MaterialID"];
                string[] MaterialIDArr = MaterialID.Split(d);

                IList<ProduceMaterDetail> pmdList = new List<ProduceMaterDetail>();
                for (int i = 0; i < MaterialIDArr.Length; i++)
                {
                    ProduceMaterDetail pmd = new ProduceMaterDetail();
                    pmd.MaterReqNo = pmq.MaterReqNo; //物料领用单 主键 
                    pmd.MaterReqDetailNo = i.ToString();//物料领用单详细号
                    pmd.CustomerOrderDetails = CustomerOrderDetailsArr[i];
                    pmd.CustomerOrderNum = CustomerOrderNumArr[i];
                    pmd.ExportID = ExportID;
                    pmd.ProdType = "1";

                    pmd.MaterialID = MaterialIDArr[i]; //零件号
                    pmd.PdtSpec = PdtSpecArr[i];
                    pmd.PriceUnitID = PriceUnitIDArr[i];
                    pmd.WHID = WHIDArr[i];
                    pmd.UnitPrice = decimal.Parse(UnitPriceArr[i]);
                    pmd.AppoQty = decimal.Parse(AppoQtyArr[i]);
                    pmd.BthID = BthIDArr[i];
                    pmd.PriceUnitID = PriceUnitIDArr[i];


                    pmd.ProcDelivID = ProcDelivID;
                    pmd.AssBillID = AssemBillID;
                    pmd.ProcessID = ProcessID;
                    pmd.ProdType = "1";
                    pmd.SpecFlag = "0";
                    pmd.TotalPrice = 0M;
                    pmd.TotalUnitID = "";

                    pmdList.Add(pmd);
                }
                IList<Reserve> reserveList = new List<Reserve>();
                IList<ReserveDetail> reserveDetailList = new List<ReserveDetail>();
                for (int i = 0; i < OrdeBthDtailListIDArr.Length; i++)
                {
                    string temp = OrdeBthDtailListIDArr[i];
                    if ("0".Equals(temp))
                    {
                        //是无批次的
                        Reserve r = new Reserve();
                        r.WhID = WHIDArr[i];
                        r.OrdeBthDtailListID = int.Parse(OrdeBthDtailListIDArr[i]);
                        r.ClnOdrID = CustomerOrderNumArr[i]; ;
                        r.ClnOdrDtl = CustomerOrderDetailsArr[i];
                        r.PdtID = MaterialIDArr[i];
                        r.PickOrdeQty = decimal.Parse(AppoQtyArr[i]);

                        reserveList.Add(r);
                    }
                    else
                    {
                        //是不是有批次的 开具数量要与 有批次的 开具数量 和 相等
                        //有批次的
                        /*Reserve r = new Reserve();
                        r.WhID = WHIDArr[i];
                        r.OrdeBthDtailListID = int.Parse(OrdeBthDtailListIDArr[i]);
                        r.ClnOdrID = CustomerOrderNumArr[i]; ;
                        r.ClnOdrDtl = CustomerOrderDetailsArr[i];
                        r.PdtID = MaterialIDArr[i];
                        reserveList.Add(r);*/
                        ReserveDetail detail = new ReserveDetail();
                        detail.OrdeBthDtailListID = int.Parse(OrdeBthDtailListIDArr[i]);
                        detail.BthID = BthIDArr[i];
                        detail.PickOrdeQty = decimal.Parse(AppoQtyArr[i]);
                        reserveDetailList.Add(detail);
                    }


                }

                pickingMaterialService.SavePickingMaterial(pmq, pmdList, reserveList, reserveDetailList);
                hr.IsSuccess = true;
                
                return hr.ToJson();
            }catch(Exception e)
            {
                hr.IsSuccess = false;
                hr.Message = e.Message;
                return hr.ToJson();
            }
        }

      

        //
        // POST: /Produce/PickingBill/Create

        [HttpPost]
        public JsonResult Create(ProduceMaterRequest pickingList)
        {
            bool result = false;
            try
            {
                result= pickingMaterialService.CreatePickingList(pickingList);
                
            }
            catch
            {
                
            }
            return result.ToJson();
        }

        //
        // GET: /Produce/PickingBill/Edit/5
        /// <summary>
        /// 打开修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            ProduceMaterRequest p = new ProduceMaterRequest();
            p.MaterReqNo = id;
            //VM_ProduceMaterRequestForTableShow model=pickingMaterialService.GetProduceMaterRequest(p);
            IEnumerable<VM_ProduceMaterDetailForDetailShow> list= pickingMaterialService.GetProduceMaterDetailsForEdit(p);
            var query = from a in list
                                                       group a by new { a.PickingNo, a.PickingTime, a.PickingUnit, a.ComeFrom, a.ComeFromNo, a.ComeFromNoW,a.WHManager,a.Auditor,a.Picker,a.Use } into _a
                                                       select new VM_ProduceMaterRequestForTableShow
                                                       {
                                                           PickingNo = _a.Key.PickingNo,
                                                           ComeFrom = _a.Key.ComeFrom,
                                                           ComeFromNo = _a.Key.ComeFromNo,
                                                           ComeFromNoW = _a.Key.ComeFromNoW,
                                                           PickingUnit = _a.Key.PickingUnit,
                                                           PickingTime = _a.Key.PickingTime,
                                                           Auditor=_a.Key.Auditor,
                                                           UsePerson=_a.Key.Picker,
                                                           Use=_a.Key.Use,
                                                           StoreManager=_a.Key.WHManager
                                                       };
            VM_ProduceMaterRequestForTableShow model = query.FirstOrDefault();
            ViewData["StoreManager"]=model.StoreManager;
            ViewData["UsePerson"]=model.UsePerson;
            ViewData["Auditor"]=model.Auditor;
            ViewData["PickingNo"] = model.PickingNo;
            ViewData["comFrom"] = model.ComeFrom;
            ViewData["comFromVlue"] = Constant.PickingBillComeFrom.CIRCULATE_CARD;
            ViewData["ProcDelivID"] = model.ComeFromNo+model.ComeFromNoW;
            ViewData["ExportID"] = "";
            ViewData["ProcessID"] = "";
            ViewData["Purpose"] = model.Use;
            ViewData["PickingUnit"] = model.PickingUnit;
            ViewData["PickingDate"] =model.PickingTime.Value.ToShortDateString();
            ViewData["model"] = UIConstant.OPERATE_MODEL_EDIT;
            return View(list);
        }

        //
        // POST: /Produce/PickingBill/Edit/5

        [HttpPost]
        public ActionResult EditSave(FormCollection form)
        {
            HandleResult hr = new HandleResult();
            try
            {
                string PickingNo = form["PickingNo"];
                string MaterialID = form["MaterialID"];
                string CustomerOrderNum = form["CustomerOrderNum"];
                string CustomerOrderDetails = form["CustomerOrderDetails"];
                string MaterReqDetailNo = form["MaterReqDetailNo"];
                string PleasePickingCount = form["PleasePickingCount"];
                string LastPleasePickingCount = form["LastPleasePickingCount"];
                string WHID = form["WHID"];
                string BthID = form["BthID"];
                string OrdeBthDtailListID = form["OrdeBthDtailListID"];
                char[] d = new char[] { ',' };
                string[] MaterReqDetailNoArr = MaterReqDetailNo.Split(d);
                string[] MaterialIDArr = MaterialID.Split(d);

                string[] CustomerOrderNumArr = CustomerOrderNum.Split(d);


                string[] WHIDArr = WHID.Split(d);


                string[] CustomerOrderDetailsArr = CustomerOrderDetails.Split(d);


                string[] BthIDArr = BthID.Split(d);

                string[] PleasePickingCountArr = PleasePickingCount.Split(d);
                string[] LastPleasePickingCountArr = LastPleasePickingCount.Split(d);

                string[] OrdeBthDtailListIDArr = OrdeBthDtailListID.Split(d);
                ProduceMaterRequest pmq = new ProduceMaterRequest();
                pmq.MaterReqNo = PickingNo;
                pmq.Purpose = form["Purpose"];
                IList<ProduceMaterDetail> pmdList = new List<ProduceMaterDetail>();

                IList<Reserve> reserveList = new List<Reserve>();
                IList<ReserveDetail> reserveDetailList = new List<ReserveDetail>();

                for (int i = 0; i < MaterReqDetailNoArr.Length; i++)
                {
                    ProduceMaterDetail pmd = new ProduceMaterDetail();
                    pmd.MaterReqNo = PickingNo;
                    pmd.MaterReqDetailNo = MaterReqDetailNoArr[i];
                    //pmd.MaterialID = MaterialIDArr[i];
                    //pmd.BthID = BthIDArr[i];
                    //pmd.CustomerOrderNum = CustomerOrderNumArr[i];
                    // pmd.CustomerOrderDetails = CustomerOrderDetailsArr[i];
                    // pmd.WHID = WHIDArr[i];
                    pmd.AppoQty = decimal.Parse(PleasePickingCountArr[i]);
                    pmdList.Add(pmd);
                    decimal tempCount = decimal.Parse(PleasePickingCountArr[i]) - decimal.Parse(LastPleasePickingCountArr[i]); ;
                    string temp = OrdeBthDtailListIDArr[i];
                    if ("0".Equals(temp))
                    {
                        //是无批次的
                        Reserve r = new Reserve();
                        r.WhID = WHIDArr[i];
                        r.OrdeBthDtailListID = int.Parse(OrdeBthDtailListIDArr[i]);
                        r.ClnOdrID = CustomerOrderNumArr[i]; ;
                        r.ClnOdrDtl = CustomerOrderDetailsArr[i];
                        r.PdtID = MaterialIDArr[i];
                        r.PickOrdeQty = tempCount;
                        reserveList.Add(r);
                    }
                    else
                    {
                        ReserveDetail detail = new ReserveDetail();
                        detail.OrdeBthDtailListID = int.Parse(OrdeBthDtailListIDArr[i]);
                        detail.BthID = BthIDArr[i];
                        detail.PickOrdeQty = tempCount;
                        reserveDetailList.Add(detail);
                    }
                }

                pickingMaterialService.UpdatePickingList(pmq, pmdList, reserveList, reserveDetailList);
                hr.IsSuccess = true;
                
                return hr.ToJson();
            }
            catch (Exception e) 
            {
                hr.IsSuccess = false;
                hr.Message = e.Message;
                return hr.ToJson();
            }
        }

        //
        // GET: /Produce/PickingBill/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/PickingBill/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, ProduceMaterRequest pickingList)
        {
            bool result = false;
            try
            {
                result = pickingMaterialService.DeletePickingList(pickingList);

            }
            catch
            {
                ;
            }
            return result.ToJson();
        }
    }
}
