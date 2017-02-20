/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductSchedulingController.cs
// 文件功能描述：产品计划排产
// 
// 创建标识：201310 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Extensions;

namespace App_UI.Areas.Produce.Controllers
{
    public class ProductSchedulingController : Controller
    {
        #region 构造函数
        private IProductSchedulingService productSchedulingService;
        private IMasterInfoService masterInfoService;
        private IStoreLockService storeLockService;
        public ProductSchedulingController(IProductSchedulingService productSchedulingService,
            IMasterInfoService masterInfoService,
            IStoreLockService storeLockService)
        {
            this.productSchedulingService = productSchedulingService;
            this.masterInfoService = masterInfoService;
            this.storeLockService = storeLockService;
        }
        #endregion
       
        #region 订单产品排产主要功能
        //
        // GET: /Produce/ProductScheduling/
        public ActionResult Index(string clientOrderID, string orderDetail)
        {
            ViewBag.clientOrderID = clientOrderID;
            ViewBag.orderDetail = orderDetail;
            return View();
        }

        /// <summary>
        /// 获取产品排产的数据:｛有？返回：生成排产数据后返回｝
        /// </summary>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDecompInf(string clientOrderID, string orderDetail)
        {
            IEnumerable<VM_ProductSchedulingShow> query = null;
            string excpMsg = "";
            bool isSucceed = true;
            try
            {
                query = productSchedulingService.SchedulingOnePlan(clientOrderID, orderDetail, "");
                if (query == null)
                {
                    query = new List<VM_ProductSchedulingShow>();

                }
            }
            catch (Exception e)
            {
                excpMsg = e.InnerException.Message;
                isSucceed = false;
            }
           

            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                rows = query,
                cOrderID = clientOrderID,//客户订单号
                oDetail = orderDetail,//订单详细
                opResult = isSucceed,
                exMsg=excpMsg
            };
            jr.ContentType = "text/html";
            return jr;

            //JsonResult js= query.ToJson(pagex.total);
            //js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        /// <summary>
        /// 返回指定的格式化之后的排产信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSpeciaDcmInf(string ids, string clientOrderID, string orderDetail)
        {
            string exMsg = "";
            bool isSucceed = false;
            string[] subIds = ids.Split(',');
            List<int> intIDs=new List<int>();
            //int[] intIDs;
            for (int i = 0; i < subIds.Length; i++)
            {
                intIDs.Add(int.Parse(subIds[i]));
                //intIDs[i] = int.Parse(subIds[i]);
            }
            IEnumerable<VM_ProductSchedulingShow> query = null;
            try
            {
                query = productSchedulingService.GetFormatedSchedulingInf(intIDs, clientOrderID, orderDetail, "");
                isSucceed = true;
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
            }
            
            if (query == null)
            {
                query = new List<VM_ProductSchedulingShow>();

            }

            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                rows = query,
                exMsg=exMsg,
                oprResult = isSucceed
            };
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 更新整个产品下所有物料以下排产信息：自产、外协、外购，提供日期、计划天数、启动日期
        /// 【在界面的下一步按钮和保存按钮按后触发】
        /// 但是并不下发投料任务
        /// </summary>
        /// <param name="matDecomList"></param>
        /// <returns></returns>
        public JsonResult UpdateCommission(Dictionary<string, string>[] matDecomList)
        {
            bool isSuccess = true;
            string exMsg = "";
            try
            {                
                List<MaterialDecompose> matList=new List<MaterialDecompose>();
                foreach (var item in matDecomList)
                {                    
                    MaterialDecompose target = new MaterialDecompose()
                    {
                        ClientOrderID = item["ClientOrderID"],
                        ClientOrderDetail = item["ClientOrderDetail"],
                        ProductID = item["ProductID"],
                        ProductsPartsID = item["MaterialID"],
                        ProduceNeedQuantity = Convert.ToDecimal(item["ProduceQuantity"]),//自产数量
                        PurchNeedQuantity = Convert.ToDecimal(item["PurchQuantity"]),//外购数量
                        AssistNeedQuantity = Convert.ToDecimal(item["AssistQuantity"]),//外协数量
                        ProvideDate = Convert.ToDateTime(item["strProvideDate"]),
                        StartDate = Convert.ToDateTime(item["strStartDate"]),
                        PreparationPeriod=Convert.ToDecimal(item["PreparationPeriod"])
                    };
                    matList.Add(target);                  
                }
                productSchedulingService.UpdateCommission(matList);
            }
            catch (Exception e)
            {
                exMsg = e.Message;
                isSuccess = false;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new { result = isSuccess, exMsg = exMsg };
            jr.ContentType = "text/html";
            return jr;

        }

        /// <summary>
        /// 确认排产：排产指定的订单产品
        /// 出于安全考虑：从数据库中取数据
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="clientOrderID"></param>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public JsonResult ConfirmSchedule(string clientOrderID, string clientOrderDetail, string version) 
        {
            string exMsg = "";
            bool isSucceed = false;
            try
            {
                isSucceed = productSchedulingService.ConfirmSchedule(clientOrderID,clientOrderDetail,version);
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                exMsg = exMsg,//客户订单号
                oprResult = isSucceed
            };
            jr.ContentType = "text/html";
            return jr;

        }
        #endregion

        #region 物料规格指定修改

        /// <summary>
        /// 私有类，表示属性的下拉列表值
        /// </summary>
        private class ComboboxData
        {
            public decimal id { set; get; }
            public string text { set; get; }
        }
        //显示物料属性选择器
        public ActionResult ShowAttribute(string MaterialID)
        {
            ViewBag.MaterialID = MaterialID;
            return View();
        }

        /// <summary>
        /// 属性显示
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AttributeCollection(string MaterialID)
        { 
            //序号属性
            IEnumerable<MasterDefiInfo> msi = masterInfoService.GetOneSection("00050").OrderBy(a => a.SNo);
            //颜色列表
            IEnumerable<ComboboxData> colList = masterInfoService.GetOneSection("00051").OrderBy(a => a.SNo).Select(a =>
                new ComboboxData() 
                {
                    id=a.SNo,
                    text=a.AttrValue
                }
                );
            //硬度列表
            IEnumerable<ComboboxData> harList = masterInfoService.GetOneSection("00052").OrderBy(a => a.SNo).Select(a =>
                new ComboboxData() 
                {
                    id = a.SNo,
                    text = a.AttrValue
                }
                );
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                msiData=msi,
                colData=colList,
                harData=harList
            };
            jr.ContentType = "text/html";
            return jr;

        }

        /// <summary>
        /// 更新材料规格
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateSpecification(MaterialDecompose target)
        {
            string exMsg = "";
            bool isSuccess = true;
            try
            {
                productSchedulingService.UpdateSepcification(target);
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
                isSuccess = false;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new { result = isSuccess, ExMsg = exMsg };
            jr.ContentType = "text/html";
            return jr;


        }

        #endregion

        #region 正常库存批次操作

        /// <summary>
        /// 正常无规格物料预约（不涉及批次）
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NormalReserveWithoutSpec(VM_LockReserveShow target)
        {
            string exMsg = "";
            bool isSuccess = true;
            try
            {
                storeLockService.NormalReserveWithoutSpec(target);
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
                isSuccess = false;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new { result = isSuccess, ExMsg = exMsg };
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 正常品批次index
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult NormalInStore(VM_MatBtchStockSearch condition)
        {
            ViewBag.condition = condition;
            return View();
        }

        /// <summary>
        /// 正常品已锁+可锁信息显示
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetNormalMixBatch(VM_MatBtchStockSearch condition)
        {
            try
            {
                condition.Rows = 4;//返回行数暂时设为4
                IEnumerable<VM_LockReserveShow> list = storeLockService.GetNormalMixBatch(condition).ToList().Take(10);

                if (list.Count() == 0 || list == null)
                {
                    list = new List<VM_LockReserveShow>();
                }
                JsonResult jr = new JsonResult();
                jr.Data = new
                {
                    rows = list
                };
                jr.ContentType = "text/html";
                return jr;
            }
            catch (Exception)
            {
                
                throw;
            }
           

        }

        /// <summary>
        /// 锁存一个正常批次，｛Add,Update,Delete｝
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LockNormalBatch(Dictionary<string, string>[] dicList)
        {
            bool isSuccess = true;
            string exMsg = "";
            try
            {
                foreach (var item in dicList)
                {
                    VM_LockReserveShow target = new VM_LockReserveShow() {
                        OriginFlag = item["OriginFlag"],
                        WhID = item["WhID"],
                        ClientOrderID = item["ClientOrderID"],
                        OrderDetail = item["OrderDetail"],
                        ProductID = item["ProductID"],
                        MaterialID = item["MaterialID"],
                        BthID = item["BthID"],
                        TotAvailable = Convert.ToDecimal( item["TotAvailable"]),
                        Specification = item["Specification"],
                        OrderQuantity = Convert.ToDecimal( item["OrderQuantity"])
                    };
                    //未锁，新增
                    if (target.OriginFlag == BatchOrigin.Unlocked)
                    {
                        isSuccess = storeLockService.AddNormalReserveWithSpec(target);
                    }
                    //已锁，更新（包含删除功能）
                    else
                    {
                        isSuccess = storeLockService.UpdateNormalReserveWithSpec(target);
                    }
                }
               
               
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
                isSuccess = false;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new { Result = isSuccess, ExMsg = exMsg };
            jr.ContentType = "text/html";
            return jr;
        }

        #endregion

        #region 让步库存批次显示(与正常显示相似，考虑合并成一个方法，从界面传入辨别标志)

        /// <summary>
        /// 让步已锁批次+可锁批次 index
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult AbnormalInStore(VM_MatBtchStockSearch condition)
        {
            ViewBag.condition = condition;
            return View();
        
        }

        /// <summary>
        /// 让步已锁批次+可锁批次
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public JsonResult GetAbnormalMixBatch(VM_MatBtchStockSearch condition)
        {
            condition.Rows = 4;
            IEnumerable<VM_LockReserveShow> list = storeLockService.GetAbnormalMixBatch(condition).ToList();

            if (list.Count()==0||list == null)
            {
                list = new List<VM_LockReserveShow>();
            }
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                rows = list
            };
            jr.ContentType = "text/html";
            return jr;

        }

        /// <summary>
        /// 锁存一个让步批次，｛Add,Update,Delete｝
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LockAbnormalBatch(Dictionary<string, string>[] dicList)
        {
            bool isSuccess = true;
            string exMsg = "";
            try
            {
                foreach (var item in dicList)
                {
                    VM_LockReserveShow target = new VM_LockReserveShow()
                    {
                        OriginFlag = item["OriginFlag"],
                        WhID = item["WhID"],
                        ClientOrderID = item["ClientOrderID"],
                        OrderDetail = item["OrderDetail"],
                        ProductID = item["ProductID"],
                        MaterialID = item["MaterialID"],
                        BthID = item["BthID"],
                        TotAvailable = Convert.ToDecimal(item["TotAvailable"]),
                        Specification = item["Specification"],
                        GiveinCatID=item["GiveinCatID"],
                        OrderQuantity = Convert.ToDecimal(item["OrderQuantity"])
                    };
                    //未锁，新增
                    if (target.OriginFlag == BatchOrigin.Unlocked)
                    {
                        isSuccess = storeLockService.AddAbnormalReserve(target);
                    }
                    //已锁，更新（包含删除功能）
                    else
                    {
                        isSuccess = storeLockService.UpdateAbnormalReserve(target);
                    }
                }

            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
                isSuccess = false;
            }

            JsonResult jr = new JsonResult();
            jr.Data = new { Result = isSuccess, ExMsg = exMsg };
            jr.ContentType = "text/html";
            return jr;
        }
        #endregion

        #region 数据操作

        /// <summary>
        /// 返回一条物料分解信息
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public JsonResult GetSingleDecompose(MaterialDecompose target)
        {
            string exMsg = "";
            bool isSucceed = false;
            VM_ProductSchedulingShow data = new VM_ProductSchedulingShow();
            try
            {
                data = productSchedulingService.GetSingleMatScheduling(target);
                isSucceed = true;
            }
            catch (Exception e)
            {
                exMsg = e.InnerException.Message;
            }
          
            JsonResult jr = new JsonResult();
            jr.Data = new
            {
                row = data,
                exMsg = exMsg,//客户订单号
                oprResult = isSucceed
            };
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 更新排产中的一条计划,只更新除主键外非空的列
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateSingleDecompose(MaterialDecompose target)
        {
            bool result = true;
            try
            {
                productSchedulingService.UpdateNotNullColumn(target);
            }
            catch (Exception)
            {
                result = false;
            }
            return result.ToJson();

        }

       
        #endregion
    }
}
