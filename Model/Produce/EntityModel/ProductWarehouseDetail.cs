/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouseDetail.cs
// 文件功能描述：成品交仓详细表
// 
// 创建标识：朱静波 20131121
//
// 修改标识：20131125 杜兴军
// 修改描述：ProducWarehouseDetail（类名）修改为ProductWarehouseDetail
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("PD_PROD_WAREH_DETAIL")]
    [Serializable]
    public class ProductWarehouseDetail : Entity
    {

        [Key, StringLength(20), Column("PROD_WAREH_ID",Order=0)]
        public string ProductWarehouseID { get; set; }//成品交仓单号

        [Key, StringLength(2), Column("WAREH_LINE_NO",Order=1)]
        public string WarehouseLineNO { get; set; }//交仓单行号

        [StringLength(20), Column("CLN_ODR_ID")]
        public string ClientOrderID { get; set; }//客户订单号

        [StringLength(2), Column("CLN_ODR_DTL")]
        public string ClientOrderDetail { get; set; }//客户订单明细

        [StringLength(20), Column("TEAM_ID")]
        public string TeamID { get; set; }//装配小组

        [StringLength(20), Column("ORD_PDT_ID")]
        public string OrderProductID { get; set; }//产品ID

        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpecification { get; set; }//材料及规格型号

        [DecimalPrecision(10, 2), Column("QUA_QTY")]
        public decimal QualifiedQuantity { get; set; }//合格数量

        [DecimalPrecision(10, 2), Column("EACH_BOX_QTY")]
        public decimal EachBoxQuantity { get; set; }//每箱数量

        [DecimalPrecision(10, 2), Column("BOX_QTY")]
        public decimal BoxQuantity { get; set; }//装箱箱数

        [DecimalPrecision(10, 2), Column("REMAIN_QTY")]
        public decimal RemianQuantity { get; set; }//装箱零头

        [StringLength(20), Column("ASS_DISP_ID")]
        public string AssemblyDispatchID { get; set; }//总装调度单ID

        [StringLength(20), Column("PROD_CHEC_ID")]
        public string ProductCheckID { get; set; }//成品检验单ID

        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }//备注

    }
}
