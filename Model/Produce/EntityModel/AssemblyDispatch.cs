/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemblyDispatch.cs
// 文件功能描述：总装调度单表
// 
// 创建标识：朱静波 20131120
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 总装调度单
    /// </summary>
    [Table("PD_ASSEM_DISPATCH")]
    [Serializable]
    public class AssemblyDispatch : Entity
    {
        [Key, StringLength(20), Column("ASS_DISP_ID")]
        public string AssemblyDispatchID { get; set; }//总装调度单号

        [Required, StringLength(20), Column("CLN_ODR_ID")]
        public string CustomerOrderNum { get; set; }//客户订单号

        [Required, StringLength(2), Column("CLN_ODR_DTL")]
        public string CustomerOrderDetails { get; set; }//客户订单明细

        [Required, StringLength(20), Column("PRODUCT_ID")]
        public string ProductID { get; set; }//产品ID

        [StringLength(20), Column("ASS_BILL_ID")]
        public string AssemblyTicketID { get; set; }//总装工票ID

        [StringLength(200), Column("CLN_NAME")]
        public string CustomerName { get; set; }//客户名称

        [Column("DISPATCH_DT")]
        public DateTime? SchedulingOrderDate { get; set; }//调度单日期

        [StringLength(20), Column("TEAM_ID")]
        public string Team { get; set; }//班组

        [DecimalPrecision(10,2), Column("REAL_ASSE_QTY")]
        public decimal ActualAssemblyNum { get; set; }//实际装配数量

        [DecimalPrecision(10, 2), Column("PLN_QTY")]
        public decimal AssemblyPlanNum { get; set; }//计划装配数量

        [Column("PLN_DELI_DT")]
        public DateTime? PlanDeliveryDate { get; set; }//计划交期

        [StringLength(20), Column("DISPCHER_ID")]
        public string Dispatcher { get; set; }//调度员

        [StringLength(20), Column("CHK_PSN_ID")]
        public string Inspector { get; set; }//质检员

        [StringLength(20), Column("WAREH_PEOP_ID")]
        public string WareHousePerson { get; set; }//交仓人

        [StringLength(20), Column("WAREH_KEPER_ID")]
        public string WareHouseKeeperID { get; set; }//产成品仓管员ID

        [StringLength(200), Column("CONT")]
        public string TypingContent { get; set; }//打字内容

        [Column("WAREHOU_DT")]
        public DateTime? WareHouseDate { get; set; }//入库日期

        [StringLength(200), Column("PACKING_1")]
        public string PackingFirst { get; set; }//包装1

        [StringLength(200), Column("PACKING_2")]
        public string PackingSecond { get; set; }//包装2

        [StringLength(200), Column("PACKING_3")]
        public string PackingThird { get; set; }//包装3


        [DecimalPrecision(10, 2), Column("WAREHOU_BOX_QTY")]
        public decimal WareHouseBoxQuantity { get; set; }//装箱入库数量_箱数

        [DecimalPrecision(10, 2), Column("WAREHOU_BOX_CER")]
        public decimal WareHouseBoxCer { get; set; }//装箱入库数量_每箱套数

        [DecimalPrecision(10, 2), Column("WAREHOU_ODD")]
        public decimal WareHouseOdd { get; set; }//装箱入库数量_零头套数

        [DecimalPrecision(10, 2), Column("TOTAL")]
        public decimal TotalNumberOfSets { get; set; }//合计套数

        [StringLength(1), Column(name: "DISP_STA", TypeName = "char")]
        public string DispatchStatus { get; set; }//总装调度单状态

        [StringLength(400), Column("REMARK")]
        public string Remarks { get; set; }//备注

        [NotMapped]
        public string ProdAbbrev { get; set; }//产品略称

        [NotMapped]
        public string TeamName { get; set; }//班组名称

        [NotMapped]
        public string DispatcherName { get; set; }//调度员姓名

        [NotMapped]
        public string InspectorName { get; set; }//质检员姓名

        [NotMapped]
        public string WareHousePersonName { get; set; }//交仓人姓名

        [NotMapped]
        public string WareHouseKeeperIDName { get; set; }//产成品仓管员姓名
    }
}
