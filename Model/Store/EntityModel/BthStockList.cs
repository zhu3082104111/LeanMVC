/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：BthStockList.cs
// 文件功能描述：
//          批次别库存表的实体Model类
//      
// 修改履历：2013/11/15 杨灿 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Model
{
    /// <summary>
    /// M:梁龙飞：增加注释
    ///  批次别库存表 MC_WH_BTH_STOCK_LIST
    /// </summary>
    [Serializable, Table("MC_WH_BTH_STOCK_LIST")]
    public class BthStockList : Entity
    {
        public BthStockList()
        {
          
        }
        /**
         * 批次别库存表 MC_WH_BTH_STOCK_LIST
         * 
         * */

        /// <summary>
        /// 单据类型
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("BILL_TYPE", Order = 0)]
        [StringLength(2)]
        public string BillType { set; get; }

        /// <summary>
        /// 单据号
        /// </summary>
        [Column("PRHA_ODR_ID")]
        [Required, StringLength(20)]
        public string PrhaOdrID { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("BTH_ID",Order=1)]
        [StringLength(20)]
        public string BthID { set; get; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("WH_ID",Order=2)]
        [StringLength(8)]
        public string WhID { set; get; }

        /// <summary>
        /// 零件产品ID
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PDT_ID",Order=3)]
        [StringLength(20)]
        public string PdtID { set; get; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { set; get; }

        /// <summary>
        /// 让步区分
        /// </summary>
        [Column("GI_CLS")]
        [Required, StringLength(3)]
        public string GiCls { set; get; }

        //单价
        [DecimalPrecision(10, 2), Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }

        //估价
        [DecimalPrecision(10, 2), Column("VALUAT_UP")]
        [Required]
        public decimal ValuatUp { set; get; }

        //单价累加价格
        [DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        [Required]
        public decimal SumPrchs { set; get; }

        //估价累加价格
        [DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        [Required]
        public decimal SumValuat { set; get; }

        //出售价格
        [DecimalPrecision(10, 2), Column("SELL_PRC")]
        public decimal SellPrc { set; get; }

        /// <summary>
        /// 总数量
        /// </summary>
        [DecimalPrecision(10, 2),Column("QTY")]
        [Required]
        public decimal Qty { set; get; }

        /// <summary>
        /// 预约数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ORDE_QTY")]
        [Required]
        public decimal OrdeQty { set; get; }

        /// <summary>
        /// 已出库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("CMP_QTY")]
        public decimal CmpQty { set; get; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("DIS_QTY")]
        public decimal DisQty { set; get; }

        /// <summary>
        /// 入库日期
        /// </summary>
        [Column("IN_DATE")]
        [Required]
        public DateTime? InDate { set; get; }

        /// <summary>
        /// 报废FLG
        /// </summary>
        [StringLength(1), Required, Column(name: "DISCARD_FLG", TypeName = "char")]
        public string DiscardFlg { set; get; }

    }
}
