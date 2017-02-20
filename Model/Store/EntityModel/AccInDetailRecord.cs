/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：AccInDetailRecord.cs
// 文件功能描述：
//          附件库入库履历详细表的实体Model类
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
    [Serializable, Table("MC_WH_ACC_IN_DETAIL_RECORD")]
    public class AccInDetailRecord : Entity
    {
        public AccInDetailRecord()
        {
          
        }
        /**
         * 附件库入库履历详细表 MC_WH_ACC_IN_DETAIL_RECORD
         * 
         * */

        //物资验收入库单号
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("MC_ISET_IN_LIST_ID",Order=0)]
        [StringLength(20)]
        public string McIsetInListID { set; get; }

        //检验报告单号
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
        public string PdtName { set; get; }

        //规格型号
        [Column("PDT_SPEC")]
        [Required, StringLength(200)]
        public string PdtSpec { set; get; }

        //数量
        [DecimalPrecision(10, 2),Column("QTY")]
        [Required]
        public decimal Qty { set; get; }
      
        //估价
        [DecimalPrecision(10, 2),Column("VALUAT_UP")]
        [Required]
        public decimal ValuatUp { set; get; }

        //单价
        [DecimalPrecision(10, 2),Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }

        //单价累加价格
        [DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        [Required]
        public decimal SumPrchs { set; get; }

        //估价累加价格
        [DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        [Required]
        public decimal SumValuat { set; get; }

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

        //打印FLG
        [StringLength(1), Required, Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlg { set; get; }

        //备注
        [Column("RMRS")]
        [MaxLength(200)]
        public string Rmrs { set; get; }


    }
}
