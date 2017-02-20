using Extensions;
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessTranslateDetail.cs
// 文件功能描述：加工流转卡视图model
// 
// 创建标识：代东泽 20131214
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
using System.Text;
using System.Threading.Tasks;

namespace Model.Produce
{
    /// <summary>
    /// 检索model
    /// <author>代东泽 20131216</author>
    /// </summary>
    public class VM_ProcessTranslateCardForSearch 
    {
        [EntityProperty("ProcDelivID")]
        public string ProcDelivID { get; set; }

        [EntityProperty("ExportID")]
        public string ProdModel { get; set; }
    }
    /// <summary>
    /// 一览表格model
    /// <author>代东泽 20131216</author>
    /// </summary>
    public class VM_ProcessTranslateCardForTableShow 
    {
        /// <summary>
        /// 流转卡号
        /// </summary>
        public string ProcDelivID { get; set; }
        

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProdModel { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        public decimal ReceQty { get; set; }

        /// <summary>
        /// 已完成的工序
        /// </summary>
        public string FinishProcess { get; set; }

        /// <summary>
        /// 正在进行的工序
        /// </summary>
        public string ProcessInDoing { get; set; }

        /// <summary>
        /// 最后一道工序完成数量
        /// </summary>
        public decimal LastProcessQty { get; set; }


        /// <summary>
        /// 计划开始日期
        /// 添加 20131220 杜兴军
        /// </summary>
        public DateTime PlanStartDate { get; set; }

        /// <summary>
        /// 计划结束日期
        /// 添加 20131220 杜兴军
        /// </summary>
        public DateTime PlanEndDate { get; set; }

        /// <summary>
        /// 进度估算
        /// </summary>
        public string ScheduleEstimate { get; set; }

        /// <summary>
        /// 进度状态
        /// </summary>
        public string ScheduleState { get; set; }
    }


    public class VM_ProcessTranslateCardForDetailShow 
    {
        /// <summary>
        /// 加工流转卡id
        /// </summary>
        public string ProcDelivID { get; set; } 

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductType { get; set; }//产品型号

        /// <summary>
        /// 初始领料数量
        /// </summary>
        public Decimal NewBeginCount { get; set; }//初始领料数量

        /// <summary>
        /// 加工开始日期
        /// </summary>
        public DateTime ProcessBeginDate { get; set; }//加工开始日期

        /// <summary>
        /// 交仓数
        /// </summary>
        public decimal GiveStoreCount { get; set; }
        /// <summary>
        /// 是否完结区分
        /// </summary>
        public string IsOver { get; set; }//是否完结
    
    
    }

    public class VM_ProcessTranslateCardPartForDetailShow
    {
        /// <summary>
        /// 工序
        /// </summary>
        public string ProcessName { get; set; }//工序
        /// <summary>
        /// 工序顺序号
        /// </summary>
        public int ProcessOrderNO { get; set; }//工序顺序号
        /// <summary>
        /// 项目序号
        /// </summary>
        public string ProjectNO { get; set; }//项目序号
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? OperateDate { get; set; }//日期
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }//操作员
        /// <summary>
        /// 操作数量
        /// </summary>
        public decimal OperateCount { get; set; }//操作数量
        /// <summary>
        /// 计划日期
        /// </summary>
        public DateTime? PlanOperateDate { get; set; }//日期
        /// <summary>
        /// 计划操作数量
        /// </summary>
        public decimal PlanOperateCount { get; set; }//操作数量

    }

    public class VM_CustomTranslateInfoForDetaiShow 
    {
        /// <summary>
        /// 加工流转卡号
        /// </summary>
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string CustomerOrderNum { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string CustomerOrderDetails { get; set; }


        /// <summary>
        /// 领料数量
        /// </summary>
        public decimal PlnQty { get; set; }
    
    }
}
