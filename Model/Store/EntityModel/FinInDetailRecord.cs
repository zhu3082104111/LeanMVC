using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MC_WH_FIN_IN_DETAIL_RECORD")]
    public class FinInDetailRecord : Entity
    {
        //成品入库单号
        [Key, Required, StringLength(20), Column("FS_IN_ID", Order = 0)]
        public string FsInID { set; get; }

        //检验报告单号
        [Key, Required, StringLength(20), Column("ISET_REP_ID", Order = 1)]
        public string IsetRepID { set; get; }

        //装配小组ID
        [StringLength(20), Column("TEAM_ID")]
        public string TeamID { set; get; }

        //让步区分
        [StringLength(3),Column("GI_CLS")]
        public string GiCls{ set; get; }

        //产品ID
        [Key, Required, StringLength(20), Column("PDT_ID", Order = 2)]
        public string ProductID { set; get; }

        //规格型号
        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpec { set; get; }

        //客户订单号
        [Key, Required, StringLength(20), Column("TECN_PROCESS", Order = 3)]
        public string TecnProcess { set; get; }

        //客户订单明细
        [Key, Required, StringLength(2), Column("CLN_ODR_DTL", Order = 4)]
        public string ClientOrderDetail { set; get; }

        //合格数量
        [Required, DecimalPrecision(10,2), Column("QTY")]
        public decimal Quantity { set; get; }

        //每箱数量
        [Required, DecimalPrecision(10, 2), Column("PRO_SCRAP_QTY")]
        public decimal ProScrapQuantity { set; get; }

        //箱数
        [Required, DecimalPrecision(10, 2), Column("PRO_MATERSCRAP_QTY")]
        public decimal ProMaterscrapQuantity { set; get; }

        //零头
        [Required, DecimalPrecision(10, 2), Column("PRO_OVER_QTY")]
        public decimal ProOverQuantity { set; get; }

        //估价
        [Required, DecimalPrecision(10,2) ,Column("VALUAT_UP")]
        public decimal ValuatUp { set; get; }

        //单价
        [Required, DecimalPrecision(10,2), Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }

        //金额
        [Required, DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        public decimal NotaxAmt { set; get; }
       
        //入库日期
        [Required, Column("IN_DATE")]
        public DateTime InDate { set; get; }

        //仓管员ID
        [Required, StringLength(20), Column("WHKP_ID")]
        public string WareHouseKpID { set; get; }

        //备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { set; get; }

        //单价累加价格
        [Required, DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        public decimal SumPrchs { set; get; }

        //估价累加价格
        [Required, DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        public decimal SumValuat { set; get; }

    }
}
