// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IWorkticketService.cs
// 文件功能描述：加工流转卡控制器
// 
// 创建标识：代东泽 20131216
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Produce;
using Extensions;
using BLL;
using Model;
using App_UI.App_Start;
using BLL.ServerMessage;
namespace App_UI.Areas.Produce.Controllers
{
    /// <summary>
    /// 代东泽 20131216
    /// </summary>
    public class CirculateCardController : Controller
    {

        private IProcessTranslateService translateCardService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <author>代东泽</author>
        public CirculateCardController(IProcessTranslateService circulateCardService) 
        {
            this.translateCardService = circulateCardService;
        }
        
        
        /// <summary>
        /// 获取一览页面
        /// </summary>
        /// <url>GET: /Produce/CirculateCard/</url>
        /// <returns>一览视图</returns>
       
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取一览页面数据
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <param name="search">检索条件对象</param>
        /// <returns>json-data</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_ProcessTranslateCardForSearch search)
        {
           
            IEnumerable<VM_ProcessTranslateCardForTableShow> query = translateCardService.GetProcessTranslateCardsForSearch(paging,search);
            if (query == null)
            {
                query = new List<VM_ProcessTranslateCardForTableShow>();
                paging.total = 0;
            }
            return query.ToJson(paging.total);
        }

        /// <summary>
        /// 加工流转卡详细查看
        /// 代东泽 20131223
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(string id)
        {
            return GetDetailData(id,"Detail");
        }
        /// <summary>
        /// 私有方法
        /// 获取加工流转卡页面需要的数据结构
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        private ActionResult GetDetailData(string id,string viewName) 
        {
            ProcessTranslateCard card = new ProcessTranslateCard();
            card.ProcDelivID = id;
            //找出流转卡对应的所有客户订单
            IList<VM_CustomTranslateInfoForDetaiShow> adList = translateCardService.GetCustomOrdersForTranslateCard(card).ToList();
            ViewData["adList"] = adList;

            //取出该条总装大工票
            VM_ProcessTranslateCardForDetailShow show = translateCardService.GetTranslateCard(card);
            ViewBag.bill = show;

            //取出总装工票详细记录列表
            IList<VM_ProcessTranslateCardPartForDetailShow> data = translateCardService.GetTranslateDetailInfos(card).ToList();
           

            IDictionary<string, Object> model = new Dictionary<string, Object>();
            if (data.Count >1)
            {
                try
                {
                    Dictionary<int, string> processName = new Dictionary<int, string>();
                    Dictionary<int, string> quotNo = new Dictionary<int, string>();
                    //Dictionary<string, Dictionary<string, Object>> dy_data = new Dictionary<string, Dictionary<string, Object>>();
                    //找出所有工序,定额编号
                    foreach (var m in data)
                    {
                        string pn = "";
                        if (!processName.TryGetValue(m.ProcessOrderNO, out pn))//如果没有取到，证明是新的工序
                        {
                            processName.Add(m.ProcessOrderNO, m.ProcessName);//工序顺序号，工序名
                            //dy_number.Add(m.ProcessName,m.QuotNo);//正确
                            // quotNo.Add(m.ProcessOrderNO, m.QuotNo); //工序顺序号，定额编号

                            var _data = data.Where(n => n.ProcessOrderNO.Equals(m.ProcessOrderNO));//找出所有该工序的详细记录
                            Dictionary<string, Object> billDetais = new Dictionary<string, Object>();
                            foreach (var d in _data)
                            {
                                billDetais.Add(d.ProjectNO, d); //项目序号，同种记录    1，2，3 放到一个工序中
                            }
                            model.Add(m.ProcessOrderNO.ToString(), billDetais); //工序名称，该工序下的所有详细操作 1，2，3
                            Object temp_count;
                            bool f = model.TryGetValue("count", out temp_count);//找出最多操作人的工序
                            if (f)
                            {
                                int k = int.Parse(temp_count.ToString());
                                if (_data.Count() > k)
                                {
                                    model["count"] = _data.Count();//把最多操作人数放入count
                                }
                            }
                            else
                            {
                                model.Add("count", _data.Count());
                            }
                        }
                    }
                    IEnumerable<int> processOrderNO = processName.Keys.OrderBy(n => n, new ComParers.IntComparer());
                    model.Add("no", processOrderNO);//工序顺序
                    model.Add("process", processName);//工序名称
                    // model.Add("number", quotNo);//操作顺序
                  
                }
                catch
                {
                    return View(viewName, model);
                }
            }
            return View(viewName,model);
        
        }
        /// <summary>
        /// 加工流转卡计划
        /// 代东泽 20131223
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {

            return GetDetailData(id,"Edit");
        }

        /// <summary>
        /// 加工流转卡计划保存操作
        /// 代东泽 20131128
        /// </summary>
        /// <url>POST: /Produce/CirculateCard/Edit/</url>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PlanSave(FormCollection collection)
        {
            HandleResult hr = new HandleResult();
            try
            {
                char[] d = new char[] { ',' };
                ProcessTranslateCard bill = new ProcessTranslateCard();
                bill.ProcDelivID = collection["ProcDelivID"];
                bill.PlanStartDate = DateTime.Parse(collection["PlanStartDate"]);
                bill.EndFlag = Constant.TranslateCardState.NOTOVER;
                string newData = collection["newData"];
                string[] newDataArr = newData.Split(d);

                string orderNo = collection["no"];//顺序
                string operateCount = collection["count"];//操作数量
                string processNo = collection["op_pro_no"];//工序号
                string operate = collection["opetator"];//员工
                string date = collection["date"];//操作日期
                IList<ProcessTranslateDetail> list = new List<ProcessTranslateDetail>();
                IList<string> flags = new List<string>();
                if (operate!=null&&!"".Equals(operate)) {
                    string[] orderNoArr = orderNo.Split(d);//顺序
                    string[] processNoArr = processNo.Split(d);//工序号
                    string[] operatorArr = operate.Split(d);//员工
                    decimal[] operateCountArr = convertDecimals(operateCount.Split(d));//操作数量
                    DateTime[] dateArr = convertDates(date.Split(d));//操作日期
                    int offset = 0;
                    if (orderNoArr.Length != operatorArr.Length)
                    {
                        offset = orderNoArr.Length - operatorArr.Length;//算出未编辑旧数据状态下的开始下标数
                    }
                    for (int i = 0; i < operatorArr.Length; i++)
                    {
                        string s = operatorArr[i];
                        if (!s.Equals(""))
                        {
                            ProcessTranslateDetail ptd = new ProcessTranslateDetail();
                            //ptd.RalOprDt = dateArr[i];
                            ptd.PlnOprDt = dateArr[i];
                            ptd.OptorID = s;
                            ptd.ItemNo = orderNoArr[offset + i];//orderNoArr[i]
                            ptd.SeqNo = int.Parse(processNoArr[offset + i]);//int.Parse(processNoArr[i])
                            //ptd.RalOprQty = operateCountArr[i];
                            ptd.PlnOprQty = operateCountArr[i];
                            ptd.ProcDelivID = bill.ProcDelivID;
                            list.Add(ptd);
                            flags.Add(newDataArr[offset + i]);
                        }

                    }
                
                }
                
         
                //string[] assemblyDispatchIDs = collection["AssemblyDispatchID"].Split(d);
                string[] customerOrderNum = collection["CustomerOrderNum"].Split(d);
                string[] customerOrderDetails = collection["CustomerOrderDetails"].Split(d);
                string[] plnQty= collection["PlnQty"].Split(d);
                IList<CustomTranslateInfo> ctiList = new List<CustomTranslateInfo>();
                for (int i = 0; i < customerOrderNum.Length; i++)
                {
                    CustomTranslateInfo cti = new CustomTranslateInfo();
                    cti.ProcDelivID = bill.ProcDelivID;
                    cti.CustomerOrderNum = customerOrderNum[i];
                    cti.CustomerOrderDetails = customerOrderDetails[i];
                    cti.PlnQty = decimal.Parse(plnQty[i]);
                    bill.NedProcQty += cti.PlnQty;
                    ctiList.Add(cti);
                }
                translateCardService.SaveTranslateCardPlan(bill, list, ctiList, flags);
               /* 
                no = null;
                processNo = null;
                oprate = null;
                loadCount = null;
                date = null;
                list = null;
                assemblyDispatchIDs = null;
                assemblyPlanNum = null;
                actualAssemblyNum = null;
                assemBillList = null;
                // hr.Message = SM_Produce.SaveSucess;*/
                hr.IsSuccess = true;
                return hr.ToJson();
            }
            catch (NotImplementedException e)
            {
                hr.IsSuccess = false;
                hr.Message = ResourceHelper.ConvertMessage(e.Message, new string[] { "" });
                return hr.ToJson();
            }
        }


        /// <summary>
        /// 加工流转卡登录操作
        /// 代东泽 20131128
        /// </summary>
        /// <url>POST: /Produce/CirculateCard/Edit/</url>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DetailSave(FormCollection collection)
        {
            HandleResult hr = new HandleResult();
            try
            {
                char[] d = new char[] { ',' };
                ProcessTranslateCard bill = new ProcessTranslateCard();
                bill.ProcDelivID = collection["ProcDelivID"];
                //bill.PlanStartDate = DateTime.Parse(collection["PlanStartDate"]);
                bill.EndFlag = collection["IsOver"];

                string newData = collection["newData"];
                string[] newDataArr=newData.Split(d);
                /*bill.AssemBillID = collection["AssemBillID"];
                //bill.RealAssemCount=int.Parse(collection.Get(1));
                bill.DispatcherID = collection["DispatcherID"];
                bill.CheckerID = collection["CheckerID"];
                bill.TeamLeaderID = collection["TeamLeaderID"];
                bill.EndFlag = collection["isOver"];
                bill.Remark = collection["remark"];
                bill.CheckResult = collection["checkResult"];*/

                string orderNo = collection["no"];//顺序
                string operateCount = collection["count"];//操作数量
                string processNo = collection["op_pro_no"];//工序号
                string operate = collection["opetator"];//员工

                IList<ProcessTranslateDetail> list = new List<ProcessTranslateDetail>();
                IList<string> flags = new List<string>();

                if (operate != null) 
                {
                    string date = collection["date"];//操作日期

                    string[] orderNoArr = orderNo.Split(d);//顺序
                    string[] processNoArr = processNo.Split(d);//工序号
                    string[] operatorArr = operate.Split(d);//员工
                    decimal[] operateCountArr = convertDecimals(operateCount.Split(d));//操作数量
                    DateTime[] dateArr = convertDates(date.Split(d));//操作日期
                    int offset = 0;
                    if (orderNoArr.Length != operatorArr.Length)
                    {
                        offset = orderNoArr.Length - operatorArr.Length;//算出未编辑旧数据状态下的开始下标数
                    }
                   
                    for (int i = 0; i < operatorArr.Length; i++)
                    {
                        string s = operatorArr[i];
                        if (!s.Equals(""))
                        {
                            ProcessTranslateDetail ptd = new ProcessTranslateDetail();
                            ptd.RalOprDt = dateArr[i];
                            ptd.OptorID = s;
                            ptd.ItemNo = orderNoArr[offset + i];//orderNoArr[i]
                            ptd.SeqNo = int.Parse(processNoArr[offset + i]);//int.Parse(processNoArr[i])
                            ptd.RalOprQty = operateCountArr[i];
                            ptd.ProcDelivID = bill.ProcDelivID;
                            list.Add(ptd);
                            flags.Add(newDataArr[offset + i]);
                        }

                    }
                }
                
                translateCardService.SaveTranslateCard(bill, list, flags);
                /*string[] customerOrderNum = collection["CustomerOrderNum"].Split(d);
                string[] customerOrderDetails = collection["CustomerOrderDetails"].Split(d);
                string[] plnQty = collection["PlnQty"].Split(d);
                IList<CustomTranslateInfo> ctiList = new List<CustomTranslateInfo>();
                for (int i = 0; i < customerOrderNum.Length; i++)
                {
                    CustomTranslateInfo cti = new CustomTranslateInfo();
                    cti.ProcDelivID = bill.ProcDelivID;
                    cti.CustomerOrderNum = customerOrderNum[i];
                    cti.CustomerOrderDetails = customerOrderDetails[i];
                    cti.PlnQty = decimal.Parse(plnQty[i]);
                    ctiList.Add(cti);
                }*/
                /* workticketService.SaveAssemBigBill(bill, list, assemBillList);
                 no = null;
                 processNo = null;
                 oprate = null;
                 loadCount = null;
                 date = null;
                 list = null;
                 assemblyDispatchIDs = null;
                 assemblyPlanNum = null;
                 actualAssemblyNum = null;
                 assemBillList = null;
                 // hr.Message = SM_Produce.SaveSucess;*/
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







        /// <summary>
        /// 把字符串数组转化为decimal类型
        /// 代东泽 20131202
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private decimal[] convertDecimals(string[] str)
        {
            decimal[] de = new decimal[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (!"".Equals(str[i]))
                {
                    de[i] = decimal.Parse(str[i]);
                }
            }
            return de;
        }
        /// <summary>
        /// 把数组转换称日期
        /// 代东泽 20131202
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private DateTime[] convertDates(string[] str)
        {
            DateTime[] de = new DateTime[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                if (!"".Equals(str[i]))
                {
                    de[i] = DateTime.Parse(str[i]);
                }
            }
            return de;
        }


    }
}
