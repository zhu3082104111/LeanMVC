using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MC_WH_SEM_OUT_DETAIL_RECORD")]
    public class SemOutDetailRecord : Entity
    {
        //加工产品出库单据号
        [Key, Required, StringLength(20), Column("TECN_PDT_OUT_ID", Order = 0)]
        public string TecnProductOutID { set; get; }

        //批次号
        [Key, Required, StringLength(20), Column("BTH_ID", Order = 1)]
        public string BatchID { set; get; }

        //领料单详细号
        [Key, Required, StringLength(2), Column("PICK_LIST_DET_NO", Order = 2)]
        public string PickListDetNo { set; get; }

        //零件ID
        [Required, StringLength(20)]
        public string ProductID { set; get; }

        //零件名称
        [StringLength(50), Column("PDT_NAME")]
        public string ProductName { set; get; }

        //规格型号
        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpec { set; get; }

        //让步区分
        [StringLength(3), Column("GI_CLS")]
        public string GiCls { set; get; }

        //加工工艺
        [Required, StringLength(20), Column("TECN_PROCESS")]
        public string TecnProcess { set; get; }

        //数量
        [Required, DecimalPrecision(10, 2), Column("QTY")]
        public decimal Quantity { set; get; }

        //单价
        [Required, DecimalPrecision(10, 2), Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }

        //估价
        [DecimalPrecision(10, 2), Column("VALUAT_UP")]
        public decimal ValuatUp { set; get; }

        //出售价格
        [DecimalPrecision(10, 2), Column("SELL_PRC")]
        public decimal SellPrc { set; get; }

        //估价累加
        [DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        public decimal SumValuat { set; get; }

        //单价累加
        [DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        public decimal SumPrchs { set; get; }

        //金额
        [Required, DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        public decimal NotaxAmt { set; get; }

        //仓管员ID
        [Required, StringLength(20), Column("WHKP_ID")]
        public string WareHouseKpID { set; get; }

        //出库日期
        [Required, Column("OUT_DATE")]
        public DateTime OutDate { set; get; }

        //打印FLAG
        [StringLength(1), Required, Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlg { set; get; }

        //备注
        [StringLength(512), Column("RMRS")]
        public string Remarks { set; get; }
    }
}
