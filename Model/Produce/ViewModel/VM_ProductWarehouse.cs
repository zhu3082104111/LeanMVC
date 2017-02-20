/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：VM_ProductWarehouse.cs
// 文件功能描述：成品交仓单 viewModel 集
// 
// 
// 创建标识：2013/11/22  杜兴军 创建
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
    /// 成品交仓单  一览
    /// 显示
    /// </summary>
    public class VM_ProductWarehouseShow
    {
        /// <summary>
        /// 交仓单号
        /// </summary>
        public string ProductWarehouseID { get; set; }

        /// <summary>
        /// 交仓部门
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 交仓日期
        /// </summary>
        public DateTime? WarehouseDT { get; set; }

        /// <summary>
        /// 交仓人
        /// </summary>
        public string WarehousePersonName { get; set; }

        /// <summary>
        /// 成品检验员
        /// </summary>
        public string CheckPersonName { get; set; }

        /// <summary>
        /// 调度员
        /// </summary>
        public string DispatherPersonName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatherID { get; set; }

        /// <summary>
        /// 交仓单状态
        /// </summary>
        public string WarehouseState { get; set; }
    }

    /// <summary>
    /// 成品交仓单  一览
    /// 查询
    /// </summary>
    public class VM_ProductWarehouseSearch
    {
        [EntityProperty("ClientOrderID",PropertyOperator.CONTAINS)]
        public string ClientOrderID { get; set; }//客户订单号

        [EntityProperty("OrderProductID",PropertyOperator.CONTAINS)]
        public string OrderProductID { get; set; }//产品型号

        [EntityProperty("WarehouseDT",PropertyOperator.GE)]
        public DateTime? StartDt { get; set; }//交仓日期 开始

        [EntityProperty("WarehouseDT", PropertyOperator.LE)]
        public DateTime? EndDt { get; set; }//交仓日期  结束

        [EntityProperty("DepartmentID",PropertyOperator.CONTAINS)]
        public string DepartmentID { get; set; }//交仓部门

        [EntityProperty("ProductWarehouseID",PropertyOperator.CONTAINS)]
        public string ProductWarehouseID { get; set; }//交仓单号

        [EntityProperty("TeamID",PropertyOperator.CONTAINS)]
        public string TeamID { get; set; }//班组

        [EntityProperty("WarehouseState",PropertyOperator.EQUAL)]
        public string ProductWarehouseState { get; set; }//状态

        [EntityProperty("BatchID",PropertyOperator.CONTAINS)]
        public string BathID { get; set; }//批次号

    }

    /// <summary>
    /// 详细 头信息
    /// </summary>
    public class VM_ProductWarehouseDetailHeadData
    {
        public string ProductWarehouseID { get; set; }//单号

        public string DepartmentID { get; set; }//交仓部门ID

        public string DepartmentName { get; set; }//交仓部门名称

        public string BatchID { get; set; }//批次号

        public DateTime? WarehouseDT { get; set; }//交仓日期

        public IEnumerable<VM_ProductWarehouseDetailDetailBodyData> Children { get; set; }//具体数据  

        public string WarehousePersonID { get; set; }//交仓人ID

        public string WarehousePersonName { get; set; }//交仓人姓名

        public string CheckPersonID { get; set; }//检验员ID

        public string CheckPersonName { get; set; }//成品检验员姓名

        public string DispatherID { get; set; }//调度员ID

        public string DispatherName { get; set; }//调度员姓名

        /// <summary>
        /// 交仓单状态
        /// </summary>
        public string WarehouseState { get; set; }
    }

    /// <summary>
    /// 详细 具体
    /// </summary>
    public class VM_ProductWarehouseDetailDetailBodyData
    {
        public string WarehouseLineNO { get; set; }//交仓单行号

        public string ClientOrderID { get; set; } //客户订单号

        public string TeamID { get; set; }//装配小组ID

        public string TeamName { get; set; } //装配小组 名

        public string OrderProductID { get; set; }//产品ID (物料编号)

        public string ProductName { get; set; }//产品名称(产成品名称)

        public string ProductSpecification { get; set; }//规格

        public string Unit { get; set; }//单位

        public decimal QualifiedQuantity { get; set; }//合格数量

        public decimal EachBoxQuantity { get; set; }//每箱数量

        public decimal BoxQuantity { get; set; }//装箱箱数

        public decimal RemianQuantity { get; set; }//装箱零头

        public string Remark { get; set; }//备注

        public string AssemblyDispatchID { get; set; }//总装调度单号

        public string ProductCheckID { get; set; }//成品检验单ID


    }
}
