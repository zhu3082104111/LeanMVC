using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MC_WH_FIN_OUT_DETAIL_RECORD")]
    public class FinOutDetailRecord : Entity
    {
        //内部成品送货单号
        [Key, Required, StringLength(20), Column("INER_FIN_OUT_ID",Order = 0)]
        public string InerFinOutID { set; get; }

        //产品ID
        [Key, Required, StringLength(20), Column("ORD_PDT_ID", Order = 3)]
        public string OrdPdtID { set; get; }


        //客户订单号
        [Key, Required, StringLength(20), Column("CLN_ODR_ID", Order = 1)]
        public string ClientOrderID { set; get; }

        //客户订单明细
        [Key, Required, StringLength(2), Column("CLN_ODR_DTL", Order = 2)]
        public string ClientOrderDetail { set; get; }


        //批次号
        [Key, Required, StringLength(20), Column("BTH_ID", Order = 4)]
        public string BatchID { set; get; }

        //规格型号
        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpec { set; get; }

        //让步区分
        [StringLength(3), Column("GI_CLS")]
        public string GiCls { set; get; }

        //单位
        [StringLength(10), Column("UNIT")]
        public string Unit { set; get; }

        //数量
        [Required, DecimalPrecision(10, 2), Column("QTY")]
        public decimal Quantity { set; get; }

        //包装每件数量
        [Required, DecimalPrecision(10, 2), Column("PACK_PRE_PIECE_QTY")]
        public decimal PackPrePieceQuantity { set; get; }

        //包装件数
        [Required, DecimalPrecision(10, 2), Column("PACK_PIECE_QTY")]
        public decimal PackPieceQuantity { set; get; }

        //包装零头
        [Required, DecimalPrecision(10, 2), Column("FRAC_QTY")]
        public decimal FracQuantity { set; get; }

        //实收数量
        [Required, DecimalPrecision(10, 2), Column("GET_QTY")]
        public decimal GetQuantity { set; get; }

        //单价
        [Required, DecimalPrecision(10, 2), Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }

        //金额
        [Required, DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        public decimal NotaxAmt { set; get; }

        //备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { set; get; }

        //估价
        [Required, DecimalPrecision(10, 2), Column("VALUAT_UP")]
        public decimal ValuatUp { set; get; }

        //出售价格
        [Required, DecimalPrecision(10, 2), Column("SELL_PRC")]
        public decimal SellPrice { set; get; }

        //单价累加价格
        [Required, DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        public decimal SumPrchs { set; get; }

        //估价累加价格
        [Required, DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        public decimal SumValuat { set; get; }

    }
}
