using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    /// <summary>
    /// 总装调度单搜索Model
    /// </summary>
    public class VM_FAScheduleBillForSearch
    {
        [EntityProperty("CustomerOrderNum", PropertyOperator.EQUAL)]
        public string TxtCustomerOrderNum { get; set; }//客户订单号

        public string TxtProductionUnits { get; set; }//生产单元

        [EntityProperty("Team", PropertyOperator.EQUAL)]
        public string TxtTeam { get; set; }//班组

        [EntityProperty("PlanDeliveryDate",PropertyOperator.GE)]
        public DateTime? TxtDateFrom { get; set; }//计划交期

        [EntityProperty("PlanDeliveryDate",PropertyOperator.LE)]
        public DateTime? TxtDateToo { get; set; }

        [EntityProperty("ProdAbbrev", PropertyOperator.EQUAL)]       
        public string TxtProductType { get; set; }//产品型号

        [EntityProperty("AssemblyDispatchID", PropertyOperator.EQUAL)]
        public string TxtSchedulOrder { get; set; }//调度单号

        [EntityProperty("AttrCd", PropertyOperator.EQUAL)]
        public string TxtCurrentState { get; set; }//当前状态
    }

    /// <summary>
    /// 总装调度单一览视图
    /// </summary>
    public class VM_AssemblyDispatch
    {
        public string AssemblyDispatchID { get; set; }//总装调度单号

        public string CustomerOrderNum { get; set; }//客户订单号

        public string CustomerOrderDetails { get; set; }//客户订单明细

        public string ProductID { get; set; }//产品ID

        public string AssemblyTicketID { get; set; }//总装工票ID

        public string CustomerName { get; set; }//客户名称

        public DateTime? SchedulingOrderDate { get; set; }//调度单日期

        public string Team { get; set; }//班组

        public decimal ActualAssemblyNum { get; set; }//实际装配数量

        public decimal AssemblyPlanNum { get; set; }//计划装配数量

        public DateTime? PlanDeliveryDate { get; set; }//计划交期

        public DateTime? WareHouseDate { get; set; }//入库日期

        public string Remarks { get; set; }//备注

        public string ProdAbbrev { get; set; }//产品略称

        public string DispatchStatus { get; set; }//调度单状态

        public string AttrCd { get; set; }//调度单状态值
    }

    /// <summary>
    /// 总装调度单详细子Grid Model
    /// </summary>
    public class VM_NewBillDataGrid
    {
        /// <summary>
        /// 子项目ID
        /// </summary>
        public string SubItemID { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// 构成数量
        /// </summary>
        public decimal ConstQty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 材料及规格型号
        /// </summary>
        public string Specifica { get; set; }
    }
}
