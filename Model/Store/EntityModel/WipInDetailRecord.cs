/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipInDetailRecord.cs
// 文件功能描述：
//          在制品入库履历详细表的实体Model类
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
    [Serializable, Table("MC_WH_WIP_IN_DETAIL_RECORD")]
    public class WipInDetailRecord : Entity
    {
        public WipInDetailRecord()
        {
         
        }
        /**
         *在制品库入库履历详细表 MC_WH_WIP_IN_DETAIL_RECORD								
         * 
         * */

        //加工产品入库单据号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("TECN_PDT_IN_ID", Order = 0)]
        [StringLength(20)]
        public string TecnPdtInID { set; get; }

        //质检报告单号
        [Column("ISET_REP_ID")]
        [Required, StringLength(20)]
        public string IsetRepID { set; get; }

        //让步区分
        [Column("GI_CLS")]
        [Required, StringLength(3)]
        public string GiCls { set; get; }

        //零件ID
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PDT_ID", Order = 1)]
        [StringLength(15)]
        public string PdtID { set; get; }

        //零件名称
        [Column("PDT_NAME")]
        [StringLength(50)]
        public string PdtName{ set; get; }

        //规格型号
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { set; get; }

        //加工工艺
        [Column("TECN_PROCESS")]
        [Required, StringLength(20)]
        public string TecnProcess { set; get; }  

        //交仓合格数量
        [DecimalPrecision(10, 2), Column("QTY")]
        [Required]
        public decimal Qty { set; get; }

        //交仓报废数量
        [DecimalPrecision(10, 2), Column("PRO_SCRAP_QTY")]
        [Required]
        public decimal ProScrapQty { set; get; }

        //交仓料废数量
        [Column("PRO_MATERSCRAP_QTY")]
        [Required, StringLength(10)]
        public decimal ProMaterscrapQty { set; get; }

        //交仓余料数量
        [DecimalPrecision(10, 2), Column("PRO_OVER_QTY")]
        [Required]
        public decimal ProOverQty { set; get; }

        //交仓缺料数量
        [DecimalPrecision(10, 2), Column("PRO_LACK_QTY")]
        [Required]
        public decimal ProLackQty { set; get; }

        //交仓合计数量
        [DecimalPrecision(10, 2), Column("PRO_TOTAL_QTY")]
        [Required]
        public decimal ProTotalQty { set; get; }

        //单价
        [DecimalPrecision(10, 2), Column("PRCHS_UP")]
        [Required]
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

        //加工单价
        [DecimalPrecision(10, 2), Column("PROC_PRICE")]
        [Required]
        public decimal ProcPrice { set; get; }

        //金额
        [DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        [Required]
        public decimal NotaxAmt { set; get; }

        //仓管员ID
        [Column("WHKP_ID")]
        [Required, StringLength(10)]
        public string WhkpID { set; get; }

        //入库日期
        [Required, Column("IN_DATE")]
        public DateTime InDate { set; get; }

        //备注
        [Column("RMRS")]
        [MaxLength(500)]
        public string Rmrs { set; get; }

        //打印FLG
        [StringLength(1), Required, Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlg { set; get; }

      
    }
}
