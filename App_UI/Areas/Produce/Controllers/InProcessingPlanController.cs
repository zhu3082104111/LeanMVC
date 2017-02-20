/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：InProcessingPlanController.cs
// 文件功能描述：内部加工计划控制器
// 
// 创建标识：201310 杜兴军
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
using System.Web.Mvc;
using BLL.Common;
using Model;
using BLL;
using App_UI.Areas.Controllers;
using Extensions;

namespace App_UI.Areas.Produce.Controllers
{
    public class InProcessingPlanController : BaseController
    {
        private IInProcessingPlanService inProcessingPlanService;
        private IAutoCreateOddNoService autoCreateOddNoService;

        public InProcessingPlanController(IInProcessingPlanService inProcessingPlanService, IAutoCreateOddNoService autoCreateOddNoService)
        {
            this.inProcessingPlanService = inProcessingPlanService;
            this.autoCreateOddNoService = autoCreateOddNoService;
        }

        //
        // GET: /Produce/InProcessingPlan/
        #region 视图

        /// <summary>
        /// 主视图
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index(int model=0)
        {
            //get the author for user
            int author = 0;
            return View();
        }

        /// <summary>
        /// 加工排产视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ToProcessSchedu()
        {
            //get the author for user
            int author = 0;
            return View();
        } 

        #endregion 视图


        #region 列表/查询

        /// <summary>
        /// 返回"内部加工计划"主页面的数据(一览)
        /// </summary>
        /// <param name="forSearch">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Get(VM_InProcessingPlanSearch forSearch,Paging paging)
        {
            return inProcessingPlanService.GetPlanViewByPage(forSearch,paging).ToJson(paging.total);
        }


        /// <summary>
        /// 获取中计划数据(中计划)
        /// </summary>
        /// <param name="monthPlanSearch">查询条件</param>
        /// <param name="paging">分页参数</param>
        /// <returns></returns>
        public JsonResult GetMiddleData(VM_InProcessingMiddlePlanSearch monthPlanSearch,Paging paging)
        {
            return inProcessingPlanService.GetMiddlePlanByPage(monthPlanSearch, paging).ToJson(paging.total);
        }


        #endregion 列表/查询


        #region 数据操作

        /// <summary>
        /// 中计划修改
        /// </summary>
        /// <param name="formCollection">FormData</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditProduceSchedu(FormCollection formCollection)
        {
            List<ProduceSchedu> schedus=new List<ProduceSchedu>();
            var message = new {result=true,mess="操作成功"};
            try
            {
                string userId = Session["UserID"].ToString();//当前用户ID
                string[] isPlaned = formCollection["IsPlaned"].Split(',');//标识是否已排产
                string[] ordeIds = formCollection["OrderId"].Split(',');//客户订单号
                string[] orderDetails = formCollection["OrderDetail"].Split(',');//订单详细
                string[] exportIds = formCollection["ExportId"].Split(',');//输出品ID
                //string[] processIds = formCollection["ProcessId"].Split(',');//工序ID
                string[] productIds = formCollection["ProductId"].Split(',');//产品ID
                string[] scheduStartDts = formCollection["ScheduStartDt"].Split(',');//计划开始日期
                string[] scheduEndDts = formCollection["ScheduEndDt"].Split(',');//计划结束日期
                string[] planTotalQuanlitys = formCollection["ScheduQuanlity"].Split(',');//计划加工数量
                string productType = formCollection["ProductType"];
                if (String.IsNullOrEmpty(productType))
                {
                    productType = Constant.ProduceType.SELFPROD;//自生产
                }
                DateTime now = DateTime.Now;//当前时间
                for (int i = 0, len = ordeIds.Length; i < len; i++)
                {
                    ProduceSchedu schedu=new ProduceSchedu()
                    {
                        OrderID = ordeIds[i],
                        OrderDetail = orderDetails[i],
                        ExportId = exportIds[i],
                        ProductType = productType,
                        //ProductType = Constant.ProduceType.SELFPROD,//自生产
                        ScheduStartDt = DateTime.Parse(scheduStartDts[i]),
                        ScheduEndDt = DateTime.Parse(scheduEndDts[i]),
                        ProcessId = inProcessingPlanService.GetProcessIdByExport(exportIds[i]),
                        ProductId = productIds[i],
                        PlanTotalQuanlity = Convert.ToDecimal(planTotalQuanlitys[i]),
                        ActualTotalQuanlity=0
                    };
                    if (isPlaned[i].Equals("true"))//已排产，将更新；否则为添加(根据CreUsrID和UpdUsrID判断)
                    {
                        schedu.CreUsrID = null;
                        schedu.UpdUsrID = userId;
                        schedu.UpdDt = now;
                    }
                    else
                    {
                        schedu.UpdUsrID = null;
                        schedu.CreUsrID = userId;
                        schedu.CreDt = now;
                        schedu.EffeFlag = Constant.GLOBAL_EffEFLAG_ON;
                        schedu.DelFlag = Constant.GLOBAL_DELFLAG_ON;
                    }
                    schedus.Add(schedu);
                }
                inProcessingPlanService.AddOrUpdateMiddlePlan(schedus);
            }
            catch (Exception e)
            {
                message = new {result=false,mess=e.ToString()};
            }
            return message.ToJson();
        }

        /// <summary>
        /// 添加 加工流转卡 
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddTranslateCard(FormCollection formCollection)
        {
            ProcessTranslateCard translateCardInfo=new ProcessTranslateCard();//加工流转卡
            List<CustomTranslateInfo> customTranslateList=new List<CustomTranslateInfo>();//客户订单流转卡集
            List<ProcessTranslateDetail> translateDetailList=new List<ProcessTranslateDetail>();//加工流转卡详细集
            bool result = true;//存放结果，是否操作成功
            string message = "操作成功";
            string backId = null;//返回页面的加工流转卡号
            try
            {
                string userId = Session["UserID"].ToString();//当前用户ID
                DateTime now = DateTime.Now;//当前时间
                string exportId = formCollection["ExportId"];//输出品ID
                //string processId = formCollection["ProcessId"];//工序ID
                string processId = inProcessingPlanService.GetProcessIdByExport(exportId);//工序ID
                string specifica = formCollection["Specifica"];//材料和规格要求
                string[] orderIds = formCollection["OrderIds"].Split(',');//客户订单号集
                string[] orderDetails = formCollection["OrderDetails"].Split(',');//客户订单明细集
                string[] productIds = formCollection["ProductIds"].Split(',');//产品ID集
                string[] materialQuanlitys = formCollection["MaterialQuanlitys"].Split(',');//原料数量集
                string[] scheduStartDts = formCollection["ScheduStartDt"].Split(',');//计划开始日期集
                string[] scheduEndDts = formCollection["ScheduEndDt"].Split(',');//计划结束日期集
                string translateCardId = autoCreateOddNoService.GetProcessTranslateCardId(userId);//获取加工流转卡号，具体实现待修改
                List<int> processSequenceList = inProcessingPlanService.GetProcessSequence(processId).ToList();//工序顺序号
                string itemNo = "1";//项目序号
                backId = translateCardId;
                //流转卡信息
                translateCardInfo.ProcDelivID = translateCardId;//流转卡号
                translateCardInfo.ExportID = exportId;//零件ID
                translateCardInfo.ProcessID = processId;//工序ID
                translateCardInfo.CreUsrID = userId;//添加人员
                translateCardInfo.CreDt = now;//当前时间
                translateCardInfo.EffeFlag = Constant.GLOBAL_EffEFLAG_ON;//有效
                translateCardInfo.DelFlag = Constant.GLOBAL_DELFLAG_ON;//未删除
                translateCardInfo.PlanStartDate=scheduStartDts.Select(s => Convert.ToDateTime(s)).Min();//计划开始日期
                translateCardInfo.PlanEndDate=scheduEndDts.Select(s => Convert.ToDateTime(s)).Min();//计划结束日期
                translateCardInfo.Specifica = specifica;//材料和规格要求
                translateCardInfo.EndFlag = Constant.TranslateCardState.NOTOVER;//未完结
                translateCardInfo.WarehTalQty = 0;//交仓数合计
                translateCardInfo.PlnTotal = 0;//预计交仓合计
                translateCardInfo.NedProcQty = materialQuanlitys.Sum(mq => Convert.ToDecimal(mq));//需加工总件数

                //客户订单流转卡关系
                for (int i = 0; i < orderIds.Length; i++)
                {
                    customTranslateList.Add(new CustomTranslateInfo()
                    {
                        ProcDelivID = translateCardId,
                        CustomerOrderNum = orderIds[i],
                        CustomerOrderDetails = orderDetails[i],
                        ProductID=productIds[i],//产品ID，待修改
                        PlnQty = Convert.ToDecimal(materialQuanlitys[i]),
                        WarehQty = 0,
                        CreDt = now,
                        CreUsrID = userId,
                        EffeFlag = Constant.GLOBAL_EffEFLAG_ON,
                        DelFlag = Constant.GLOBAL_DELFLAG_ON
                    });
                }

                //流转卡详细
                for (int i = 0, len = processSequenceList.Count(); i < len; i++)
                {
                    translateDetailList.Add(new ProcessTranslateDetail()
                    {
                        ProcDelivID = translateCardId,
                        SeqNo = processSequenceList[i],
                        ItemNo = itemNo,
                        PlnOprQty = 0,
                        RalOprQty = 0,
                        CreDt = now,
                        CreUsrID = userId,
                        EffeFlag = Constant.GLOBAL_EffEFLAG_ON,
                        DelFlag = Constant.GLOBAL_DELFLAG_ON
                    });
                }
                inProcessingPlanService.AddProcessTranslateCard(translateCardInfo, customTranslateList, translateDetailList);//添加加工流转卡信息
            }
            catch (Exception e)
            {
                result = false;
                message = e.ToString();
                backId = null;
            }
            return new {result = result, message = message, backId = backId}.ToJson();
        }

        #endregion 数据操作
    }
}
