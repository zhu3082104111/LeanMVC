/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProduceScheduDetailRepository.cs
// 文件功能描述：
// 
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Microsoft.SqlServer.Server;

namespace Model
{
    /// <summary>
    /// 加工计划一览-显示
    /// </summary>
    public class VM_InProcessingPlanShow
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 成品略称
        /// </summary>
        public string ProductAbbrev { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        public string Specifica { get; set; }

        /// <summary>
        /// 计划需求数量
        /// </summary>
        public decimal DemondQuantity { get; set; }

        /// <summary>
        /// 计划开工日
        /// </summary>
        public DateTime? ScheduStartDt { get; set; }

        /// <summary>
        /// 计划完工日
        /// </summary>
        public DateTime? ScheduEndDt { get; set; }

        /// <summary>
        /// 计划所要日数
        /// </summary>
        public decimal PlanTotalTime { get; set; }

        /// <summary>
        /// 启动日(物料)
        /// </summary>
        public DateTime? StartDt { get; set; }

        /// <summary>
        /// 提供日(物料)
        /// </summary>
        public DateTime? ProvideDt { get; set; }

        /// <summary>
        /// 实绩完工日
        /// </summary>
        public DateTime? RealEndDt { get; set; }

        /// <summary>
        /// 实绩加工数量
        /// </summary>
        public decimal RealQuanlity { get; set; }

        /// <summary>
        /// 进度状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 计划量（计算）
        /// </summary>
        public decimal PlanQuanlity { get; set; }

        /// <summary>
        /// 有效总天数（计算）
        /// </summary>
        public decimal TotalDays { get; set; }

        /// <summary>
        /// 上班天数（计算）
        /// </summary>
        public decimal WorkDays { get; set; }

        /// <summary>
        /// 生产率（计算）
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 延迟状态
        /// </summary>
        public string DelayState { get; set; }

        public VM_InProcessingPlanShow()
        {
            DelayState = "0";
        }
    }

    /// <summary>
    /// 加工计划一览-搜索
    /// </summary>
    public class VM_InProcessingPlanSearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [EntityProperty("ClientOrderID",PropertyOperator.EQUAL,typeof(MaterialDecompose))]
        public string OrderNo { get; set; }
        
        /// <summary>
        /// 产品ID
        /// </summary>
        [EntityProperty("ProductID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string ProductId { get; set; }//产品型号

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }//零件名称

        /// <summary>
        /// 零件ID
        /// </summary>
        [EntityProperty("ProductsPartsID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string PartId { get; set; }//零件ID

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        [EntityProperty("Specifica", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string MaterialSize { get; set; }//材料和规格要求

        /// <summary>
        /// 提供日期-开始日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? StartDt { get; set; }//开始日期

        /// <summary>
        /// 提供日期-结束日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? EndDt { get; set; }//结束日期

        /// <summary>
        /// 进度状态
        /// </summary>
        public string State { get; set; }//进度状态(全部,完成>=100%,生产中>0% <100%,延迟(实绩值<计划值),延误(完成日>提供日))

    }
    
    /// <summary>
    /// 中计划-显示
    /// </summary>
    public class VM_InProcessingMiddlePlanShow
    {       
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        public string ProductAbbrev { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 输出品ID
        /// </summary>
        public string ExportId { get; set; }

        //public string ProcessName { get; set; }//工序

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        public string Specifica { get; set; }

        /// <summary>
        /// 加工数量(用于显示)
        /// </summary>
        public decimal ProduceQuanlity { get; set; }

        /// <summary>
        /// 原料数量(仓库预约表)
        /// </summary>
        public decimal MaterialQuanlity { get; set; }

        /// <summary>
        /// 仓库实际在库数量
        /// </summary>
        public decimal ReserveInQty { get; set; }

        /// <summary>
        /// 已请领数量
        /// </summary>
        public decimal HaveAppoQty { get; set; }

        /// <summary>
        /// 需请领数量（全部）
        /// </summary>
        public decimal NeedAppoQty { get; set; }

        /// <summary>
        /// 计划需求数量(物料分解表)
        /// </summary>
        public decimal ScheduQuanlity { get; set; }

        /// <summary>
        /// 预排产 启动日
        /// </summary>
        public DateTime? StartDt { get; set; }

        /// <summary>
        /// 预排产 提供日
        /// </summary>
        public DateTime? ProvideDate { get; set; }

        /// <summary>
        /// 排产 开始日
        /// </summary>
        public DateTime? ScheduStartDt { get; set; }

        /// <summary>
        /// 排产 完成日
        /// </summary>
        public DateTime? ScheduEndDt { get; set; }

        /// <summary>
        /// 计划所要日数
        /// </summary>
        public decimal ScheduTotalDays { get; set; }

        /// <summary>
        /// 标识是否已排产(用于查询及页面向后台传值来标记是更新还是添加)
        /// </summary>
        public bool IsPlaned { get; set; }

        /// <summary>
        /// 标识是否有加工流转卡
        /// </summary>
        public bool IsTranslateCarded { get; set; }

        /// <summary>
        /// 是否流转卡记录全部完结
        /// </summary>
        public bool IsTranslateAllEnd { get; set; }
    }

    /// <summary>
    /// 中计划-搜索
    /// </summary>
    public class VM_InProcessingMiddlePlanSearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [EntityProperty("ClientOrderID", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string OrderId { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [EntityProperty("ProductID", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string ProductId { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        [EntityProperty("ProductsPartsID", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string PartId { get; set; }

        /// <summary>
        /// 提供日期(开始)
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? StartDt { get; set; }

        /// <summary>
        /// 提供日期(结束)
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? EndDt { get; set; }

        /// <summary>
        /// 材料和规划要求
        /// </summary>
        [EntityProperty("Specifica", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string Specifica { get; set; }

        /// <summary>
        /// 排产状态(1已排产，2未排产，3已投料，4已完成)
        /// </summary>
        public string ScheduState { get; set; }

    }

    /// <summary>
    /// 小计划-显示
    /// </summary>
    public class VM_InProcessingLittlePlanShow
    {
        public string OrderNo { get; set; }//订单号

        public string OrderNumber { get; set; }//订单明细

        public string ProductId { get; set; }//产品型号

        public string PartName { get; set; }//零件名称

        public string ProcessName { get; set; }//工序名称

        public string MaterialSize { get; set; }//材料和规格要求

        public string EquipmentName { get; set; }//加工设备

        public int ScheduQuanlity { get; set; }//计划需求数量(物理分解表)

        public int ScheduFinishingQuanlity { get; set; }//计划加工件数(排产详细表)

        public int ScheduFinishedQuanlity { get; set; }//完成件数(排产详细)

        public int RealFinishQuanlity { get; set; }//实绩加工件数(实绩详细表)

        public DateTime? ScheduStartDt { get; set; }//计划开工日

        public DateTime? ScheduEndDt { get; set; }//计划完工日

        public DateTime? RealStartDt { get; set; }//实绩开工日

        public DateTime? RealEndDt { get; set; }//实绩完工日

        public IEnumerable<VM_ProduceRealDetail>  ProduceRealDetails { get; set; }//实绩详细

        public IEnumerable<VM_ProduceScheduDetail> ProduceScheduDetails { get; set; } //计划详细
    }

    /// <summary>
    /// 小计划-搜索
    /// </summary>
    public class VM_InProcessingLittlePlanSearch
    {
        public string ProductId { get; set; }//产品型号

        public string PartName { get; set; }//零件型号

        public string ProcessId { get; set; }//工序编号

        public string MaterialSize { get; set; }//材料和规格要求

        public DateTime? StartDt { get; set; }//开始

        public DateTime? EndDt { get; set; }//结束
    }

    /// <summary>
    /// 工序-显示
    /// </summary>
    public class VM_InProcessingProcessShow
    {

        public string OrderNo { get; set; }//订单号

        public string OrderNumber { get; set; }//订单明细

        public string ExportId { get; set; }//输出品ID

        public DateTime Date { get; set; }//日期

        public string ProcessId { get; set; }//工序ID

        public string ProcessName { get; set; }//工序名称
        
        public string ProductId { get; set; }//产品ID

        public string ProductSecondName { get; set; }//产品略称

        public decimal ScheduDayQuanlity { get; set; }//当天计划数量

        public decimal RealDayQuanlity { get; set; }//当天实绩数量

        public decimal ScheduAllQuanlity { get; set; }//计划所有数量

        public decimal RealAllQuanlity { get; set; }//实绩所有数量

        public DateTime? TicketDt { get; set; }//工票日期
    }

    /// <summary>
    /// 生成工票-搜索
    /// </summary>
    public class VM_InProcessingTickSearch
    {
        //日期、计划订单号、产品型号、工序、班组(TeamId)
        public DateTime? PlanDt { get; set; }//计划日期

        public string OrderId { get; set; }//计划单号

        public string ProductId { get; set; }//产品型号

        public string ProcessId { get; set; }//工序ID

        public string TeamId { get; set; }//班组编号
    }

    public class VM_InProcessingTeamShow
    {

    }


    public class VM_ProduceRealDetail
    {
        public string OrderNo { get; set; }

        public string OrderNumber { get; set; }

        public DateTime Date { get; set; }

        public int Quanlity { get; set; }

        public string WorkFlag { get; set; }//是否为工作日
    }

    public class VM_ProduceScheduDetail
    {
        public string OrderNo { get; set; }

        public string OrderNumber { get; set; }

        public DateTime Date { get; set; }

        public string ProductId { get; set; }

        public string ProcessId { get; set; }

        public decimal Quanlity { get; set; }

        public decimal FinishedQuanlity { get; set; }

        public string WorkFlag { get; set; }//是否为工作日
    }
}
