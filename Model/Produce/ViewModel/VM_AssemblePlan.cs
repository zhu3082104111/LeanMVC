/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：VM_AssemblePlan.cs
// 文件功能描述：
//  总装计划视图类
// 
// 创建标识：2013/12/18  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Extensions;

namespace Model
{
    /// <summary>
    /// 总装计划一览
    /// </summary>
    public class VM_AssemblePlanShow
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 零件ID(暂时无用)
        /// </summary>
        public string PartId { get; set; }

        /// <summary>
        /// 工序ID(暂时无用)
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 产品ID(暂时无用)
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        public string ProductAbbrev { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        public string Specifica { get; set; }

        /// <summary>
        /// 计划需求数量
        /// </summary>
        public decimal DemondQuantity { get; set; }

        /// <summary>
        /// 启动日(预排产计划)
        /// </summary>
        public DateTime StartDt { get; set; }

        /// <summary>
        /// 提供日(预排产计划)
        /// </summary>
        public DateTime ProvideDt { get; set; }

        /// <summary>
        /// 开始日(生产计划/实际)
        /// </summary>
        public DateTime? ScheduStartDt { get; set; }

        /// <summary>
        /// 完成日(生产计划/实际)
        /// </summary>
        public DateTime? RealEndDt { get; set; }

        /// <summary>
        /// 生产率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// 计划所要日数
        /// </summary>
        public decimal PlanTotalTime { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public decimal RealQuanlity { get; set; }

        /// <summary>
        /// 进度状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 延误或延迟状态
        /// </summary>
        public string DelayState { get; set; }

    }

    /// <summary>
    /// 总装计划查询
    /// </summary>
    public class VM_AssemblePlanSearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [EntityProperty("ClientOrderID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string OrderId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [EntityProperty("ProductID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string ProductId { get; set; }

        /// <summary>
        /// 零件(零部件)ID
        /// </summary>
        [EntityProperty("ProductsPartsID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string PartId { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        [EntityProperty("Specifica", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string Specifica { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? StartDt { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? EndDt { get; set; }

        /// <summary>
        /// 进度状态
        /// </summary>
        public string State { get; set; }
    }

    /// <summary>
    /// 总装排产(中计划)显示
    /// </summary>
    public class VM_AssembleMiddlePlanShow
    {

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string ExportId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品略称
        /// </summary>
        public string ProductAbbrev { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        public string Specifica { get; set; }

        /// <summary>
        /// 计划加工数量合计(物料的购买明细 生产数量)
        /// </summary>
        public decimal ScheduQuanlity { get; set; }

        /// <summary>
        /// 总装数量
        /// </summary>
        public decimal AssembleQuantity { get; set; }

        /// <summary>
        /// 原料数量
        /// </summary>
        public decimal MaterialQuanlity { get; set; }

        /// <summary>
        /// 调度单号
        /// </summary>
        public string AssembleId { get; set; }

        /// <summary>
        /// 启动日(预排产计划)
        /// </summary>
        public DateTime StartDt { get; set; }

        /// <summary>
        /// 提供日(预排产计划)
        /// </summary>
        public DateTime ProvideDt { get; set; }

        /// <summary>
        /// 开始日(生产计划)
        /// </summary>
        public DateTime? ScheduStartDt { get; set; }

        /// <summary>
        /// 完成日(生产计划)
        /// </summary>
        public DateTime? ScheduEndDt { get; set; }

        /// <summary>
        /// 调度单号集
        /// </summary>
        public IEnumerable<string> DispatchIds { get; set; } 

        /// <summary>
        /// 是否已排产
        /// </summary>
        public bool IsPlaned { get; set; }

    }

    /// <summary>
    /// 总装排产(中计划)查询
    /// </summary>
    public class VM_AssembleMiddlePlanSearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [EntityProperty("ClientOrderID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string OrderId { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [EntityProperty("ProductID", PropertyOperator.EQUAL, typeof(MaterialDecompose))]
        public string ProductId { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// </summary>
        [EntityProperty("Specifica", PropertyOperator.CONTAINS, typeof(MaterialDecompose))]
        public string Specifica { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? StartDt { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? EndDt { get; set; }

    }

    /// <summary>
    /// 原料数量的检索条件
    /// </summary>
    public class VM_MaterialQuanlitySearch
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string ExportId { get; set; }
       
    }

    /// <summary>
    /// 原料数量显示
    /// </summary>
    public class VM_MaterialQuanlityShow
    {
        /// <summary>
        /// 零件ID
        /// </summary>
        public string ExportId { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string ExportName { get; set; }

        /// <summary>
        /// 零件略称
        /// </summary>
        public string ExportAbbrev { get; set; }

        /// <summary>
        /// 最大装配数量
        /// </summary>
        public int MaxQuanlity { get; set; }

        /// <summary>
        /// 实际在库数量
        /// </summary>
        public decimal RealInQuanlity { get; set; }

    }
}
