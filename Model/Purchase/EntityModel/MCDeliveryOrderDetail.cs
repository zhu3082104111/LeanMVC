/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCDeliveryOrderDetail.cs
// 文件功能描述：
//          送货单详细表的实体Model类
//      
// 修改履历：2013/11/19 宋彬磊 新建
/*****************************************************************************/
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
    /// 对应DB中文表名：送货单详细表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_DELIVERY_ORDER_DTL")]
    public class MCDeliveryOrderDetail : Entity
    {
        // 送货单号
        [Key, StringLength(20), Column("DLY_ODR_ID", Order = 0)]
        public string DeliveryOrderID { get; set; }

        // 物料ID
        [Key, StringLength(20), Column("PDT_ID", Order = 1)]
        public string MaterielID { get; set; }

        // 仓库编码
        [Required, StringLength(8), Column("WH_ID")]
        public string WarehouseID { get; set; }

        // 规格
        [StringLength(200), Column("PDT_SPEC")]
        public string MaterialsSpec { get; set; }

        // 单位
        [Required, StringLength(20), Column("UNIT")]
        public string Unit { set; get; }

        // 数量
        [Required, DecimalPrecision(10, 2), Column("QTY")]
        public decimal Quantity { set; get; }

        // 含税单价
        [DecimalPrecision(10, 2), Column("PRICE_TAX")]
        public decimal PriceWithTax { set; get; }

        // 核实含税单价
        [DecimalPrecision(10, 2), Column("CK_PRICE_TAX")]
        public decimal CkPriceWithTax { set; get; }

        // 包装情况 每件数量
        [DecimalPrecision(10, 2), Column("PKG_INNUM_QTY")]
        public decimal InnumQuantity { set; get; }

        // 包装情况 件数
        [DecimalPrecision(10, 2), Column("PKG_NUM")]
        public decimal Num { set; get; }

        // 包装情况 零头
        [DecimalPrecision(10, 2), Column("PKG_SCRAP")]
        public decimal Scrap { set; get; }

        // 实收数量
        [DecimalPrecision(10, 2), Column("ACT_REC_QTY ")]
        public decimal ActualQuantity { set; get; }

        // 备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { set; get; }
    }
}
