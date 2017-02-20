// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IWorkticketService.cs
// 文件功能描述：总装大工票控制器
// 
// 创建标识：代东泽 20131126
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using App_UI.Areas.Controllers;
using BLL;
using Extensions;
using Model.Produce;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.ServerMessage;
namespace App_UI.Areas.Produce.Controllers
{
    /// <summary>
    /// 代东泽 20131127
    /// </summary>
    public class FAWorkticketAllController : BaseController
    {
        //工票业务service
        private IWorkticketService workticketService;

        /// <summary>
        /// 构造方法
        /// 代东泽 20131127
        /// </summary>
        /// <param name="workticketService"></param>
        public FAWorkticketAllController(IWorkticketService workticketService) 
        {
            this.workticketService = workticketService;
        }

       
        /// <summary>
        /// 一览页面
        /// 代东泽 20131127
        /// </summary>
        /// <url> GET: /Produce/FAWorkticketAll/</url>
        /// <param name="id">模式</param>
        /// <returns></returns>
         public ActionResult Index(string id)
        {
            return View(0);
        }

        /// <summary>
        /// 获取一览数据
        /// 代东泽 20131127
        /// </summary>
        /// <param name="paging">分页对象</param>
        /// <param name="search">搜索条件对象</param>
        /// <returns>json-data</returns>
        [HttpPost]
        public JsonResult Get(Paging paging, VM_AssemBigBillForSearch search) 
        {

            IEnumerable<VM_AssemBigBillForTableShow> query = workticketService.GetAssemBigBillsForSearch(search, paging);
            if (query == null)
            {
                query = new List<VM_AssemBigBillForTableShow>();
                paging.total = 0;
            } 
            return query.ToJson(paging.total);

        }

        /// <summary>
        /// 数值字符串比较
        /// 代东泽 20131202
        /// </summary>
         private class StringComparer : IComparer<string>
         {
            
             public int Compare(string x, string y)
             {
                 int k=int.Parse(x);
                 int l=int.Parse(y);
                 if (k > l) 
                 {
                     return 1;
                 }
                 else if (k < l) 
                 {
                     return -1; 
                 }
                 return 0;
             }
         };
         /// <summary>
         /// 数值字符串比较
         /// 代东泽 20131202
         /// </summary>
         private class IntComparer : IComparer<int>
         {

             public int Compare(int k, int l)
             {
                 if (k > l)
                 {
                     return 1;
                 }
                 else if (k < l)
                 {
                     return -1;
                 }
                 return 0;
             }
         };
       
        
        /// <summary>
        /// 查看大工票详细信息
        /// 代东泽 20131213
        /// </summary>
        /// <url>GET: /Produce/FAWorkticketAll/Details/5</url>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {

            AssemBill assembill = new AssemBill();
            assembill.AssemBillID = id;
            //找出工票对应的所有客户订单
            IList<VM_AssemblyDispatch> adList = workticketService.GetCustomOrdersForAssemBigBill(assembill).ToList();
            ViewData["adList"] = adList;

            //取出该条总装大工票
            VM_AssemBigBillForDetailShow show= workticketService.GetAssemBigBillInfo(assembill);
            ViewBag.bill = show;

            //取出总装工票详细记录列表
            IList<VM_AssemBigBillPartForDetailShow> data=workticketService.GetAssemBigBillsDetailInfo(assembill).ToList();
            IDictionary<string, Object> model = new Dictionary<string, Object>();
            if (data != null)
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
                            quotNo.Add(m.ProcessOrderNO, m.QuotNo); //工序顺序号，定额编号
                            
                            var _data = data.Where(n => n.ProcessName == m.ProcessName);//找出所有该工序的详细记录
                            Dictionary<string, Object> billDetais = new Dictionary<string, Object>();
                            foreach (var d in _data)
                            {
                                billDetais.Add(d.ProjectNO, d); //项目序号，同种记录    1，2，3 放到一个工序中
                            }
                            model.Add(m.ProcessName, billDetais); //工序名称，该工序下的所有详细操作 1，2，3
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
                    IEnumerable<int> processOrderNO = processName.Keys.OrderBy(n => n, new IntComparer());
                    model.Add("no", processOrderNO);//工序顺序
                    model.Add("process", processName);//工序名称
                    model.Add("number", quotNo);//操作顺序

                }
                catch 
                {
                    return View(model);
                }
            }
            return View(model);
        }

        //
        // GET: /Produce/FAWorkticketAll/Edit/5

        private ActionResult Edit(int id)
        {
            //this.workticketService.SaveAssemBigBill(null,null,null);
            return View();
        }

    
        /// <summary>
        /// 大工票保存操作
        /// 代东泽 20131128
        /// </summary>
        /// <url>POST: /Produce/FAWorkticketAll/Edit/</url>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(FormCollection collection)
        {
            HandleResult hr = new HandleResult();
            try
            {
                AssemBill bill = new AssemBill();
                bill.AssemBillID = collection["AssemBillID"];
                //bill.RealAssemCount=int.Parse(collection.Get(1));
                bill.DispatcherID = collection["DispatcherID"];
                bill.CheckerID = collection["CheckerID"];
                bill.TeamLeaderID = collection["TeamLeaderID"];
                bill.EndFlag = collection["isOver"];
                bill.Remark = collection["remark"];
                //bill.CheckResult = collection["checkResult"];

                char[] d = new char[] { ',' };
                string newData = collection["newData"];
                string[] newDataArr = newData.Split(d);

                string _no = collection["no"];//顺序
                string _count = collection["count"];//操作数量
                string _processNo = collection["op_pro_no"];//工序号
                string _oprate = collection["opetator"];//员工

                IList<AssemBillDetail> list = new List<AssemBillDetail>();
                IList<string> flags = new List<string>();

                if (_oprate != null) 
                {
                    string _date = collection["date"];//操作日期

                    string[] no = _no.Split(d);//顺序
                    string[] processNo = _processNo.Split(d);//工序号
                    string[] oprate = _oprate.Split(d);//员工
                    decimal[] loadCount = convertDecimals(_count.Split(d));//操作数量
                    DateTime[] date = convertDates(_date.Split(d));//操作日期

                    int offset = 0;
                    if (no.Length != oprate.Length)
                    {
                        offset = no.Length - oprate.Length;//算出未编辑旧数据状态下的开始下标数
                    }
                  
                    for (int i = 0; i < oprate.Length; i++)
                    {
                        string s = oprate[i];
                        if (!s.Equals(""))
                        {
                            AssemBillDetail asm = new AssemBillDetail();
                            asm.AssemBillID = bill.AssemBillID;
                            asm.OperatorID = s;
                            asm.OperateDate = date[i];
                            asm.ProjectNO = no[offset + i];
                            asm.OperatorRealCount = loadCount[i];
                            asm.ProcessOrderNO = int.Parse(processNo[offset + i]);
                            list.Add(asm);
                            flags.Add(newDataArr[offset + i]);
                        }

                    }
                    no = null;
                    processNo = null;
                    oprate = null;
                    loadCount = null;
                    date = null;
                }
                
                string[] assemblyDispatchIDs = collection["AssemblyDispatchID"].Split(d);
                string[] assemblyPlanNum = collection["AssemblyPlanNum"].Split(d);
                string[] actualAssemblyNum = collection["ActualAssemblyNum"].Split(d);
                IList<AssemblyDispatch> assemBillList=new List<AssemblyDispatch>();
                for(int i=0;i<assemblyDispatchIDs.Length;i++){
                    AssemblyDispatch asmbill = new AssemblyDispatch();
                    asmbill.AssemblyDispatchID = assemblyDispatchIDs[i];
                    asmbill.AssemblyPlanNum = decimal.Parse(assemblyPlanNum[i]);
                    asmbill.ActualAssemblyNum =  decimal.Parse(actualAssemblyNum[i]);
                    assemBillList.Add(asmbill);
                }
                workticketService.SaveAssemBigBill(bill, list, assemBillList, flags);
              
                list = null;
                assemblyDispatchIDs = null;
                assemblyPlanNum = null;
                actualAssemblyNum = null;
                assemBillList = null;
                hr.IsSuccess = true;
                
                return hr.ToJson();
            }
            catch (NotImplementedException e)
            {
                hr.IsSuccess = false;
                hr.Message =ResourceHelper.ConvertMessage(e.Message,new string[]{""});
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
            decimal[] de=new decimal[str.Length];
            for(int i=0;i<str.Length;i++){
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
        //
        // GET: /Produce/FAWorkticketAll/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Produce/FAWorkticketAll/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
