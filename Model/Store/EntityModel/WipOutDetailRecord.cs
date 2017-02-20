/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：WipOutDetailRecord.cs
// 文件功能描述：
//          在制品出库履历详细表的实体Model类
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
    [Serializable, Table("MC_WH_WIP_OUT_DETAIL_RECORD")]
    public class WipOutDetailRecord : Entity
    {
        public WipOutDetailRecord()
        {
           
        }
        /**
         *在制品库出库履历详细表 MC_WH_WIP_OUT_DETAIL_RECORD								
         * 
         * */

        //加工产品出库单据号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("TECN_PDT_OUT_ID", Order = 0)]
        [StringLength(20)]
        public string TecnPdtOutID { set; get; }

        //批次号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("BTH_ID", Order = 1)]
        [StringLength(20)]
        public string BthID { set; get; }

        //领料单详细号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PICK_LIST_DET_NO", Order = 2)]
        [StringLength(2)]
        public string PickListDetNo { set; get; }

        //零件ID
        [Column("PDT_ID")]
        [StringLength(20)]
        public string PdtID { set; get; }

        //零件名称
        [Column("PDT_NAME")]
        [StringLength(50)]
        public string PdtName { set; get; }

        //规格型号
        [Column("PDT_SPEC")]
        [Required, StringLength(200)]
        public string PdtSpec { set; get; }

        //让步区分
        [Column("GI_CLS")]
        [Required, StringLength(3)]
        public string GiCls { set; get; }

        //加工工艺
        [Column("TECN_PROCESS")]
        [Required, StringLength(20)]
        public string TecnProcess { set; get; }     

        //数量
        [DecimalPrecision(10, 2), Column("QTY")]
        [Required]
        public decimal Qty { set; get; }

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

        //出售价格
        [DecimalPrecision(10, 2), Column("SELL_PRC")]
        public decimal SellPrc { set; get; }

        //金额
        [DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        [Required]
        public decimal NotaxAmt { set; get; }

        //仓管员ID
        [Column("WHKP_ID")]
        [Required, StringLength(10)]
        public string WhkpID { set; get; }

        //出库日期
        [Column("Out_DATE")]
        [MaxLength(10)]
        public DateTime? OutDate { set; get; }

        //备注
        [Column("RMRS")]
        [MaxLength(200)]
        public string Rmrs { set; get; }

        //打印FLG
        [StringLength(1), Required, Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlg { set; get; }

    }
}
