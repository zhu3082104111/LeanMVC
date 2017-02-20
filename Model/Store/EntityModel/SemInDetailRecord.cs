using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Serializable, Table("MC_WH_SEM_IN_DETAIL_RECORD")]
    public class SemInDetailRecord:Entity
    {
        [Key, StringLength(20), Column("TECN_PDT_IN_ID",Order=0)]
        public string TecnPdtInId { set; get; }//加工产品入库单据号

        [Required, StringLength(20), Column("ISET_REP_ID")]
        public string IsetRepId { set; get; }//检验报告单号

        [StringLength(3), Column("GI_CLS")]
        public string GiCls { set; get; }//让步区分

        [Key, StringLength(15), Column("PDT_ID", Order = 1)]
        public string PdtId { set; get; }//零件ID

        [StringLength(50), Column("PDT_NAME")]
        public string PdtName { set; get; }//零件名称

        [ StringLength(200), Column("PDT_SPEC")]
        public string PdtSpec { set; get; }//规格型号

        [ StringLength(20), Column("TECN_PROCESS")]
        public string Tecnrocess { set; get; }//加工工艺

        [DecimalPrecision(10, 2), Column("QTY")]
        public decimal Qty { set; get; }//交仓合格数量

        [DecimalPrecision(10, 2), Column("PRO_SCRAP_QTY")]
        public decimal ProScrapQty { set; get; }//交仓报废数量

        [DecimalPrecision(10, 2), Column("PRO_MATERSCRAP_QTY")]
        public decimal ProMaterscrapQty { set; get; }//交仓料废数量

        [DecimalPrecision(10, 2), Column("PRO_OVER_QTY")]
        public decimal ProOverQty { set; get; }//交仓余料数量

        [DecimalPrecision(10, 2), Column("PRO_LACK_QTY")]
        public decimal ProLackQty { set; get; }//交仓缺料数量

        [DecimalPrecision(10, 2), Column("PRO_TOTAL_QTY")]
        public decimal ProTotalQty { set; get; }//交仓合计数量

        [DecimalPrecision(10, 2), Column("VALUAT_UP")]
        public decimal ValuatUp { set; get; }//估价

        [DecimalPrecision(10, 2), Column("PRCHS_UP")]
        public decimal PrchsUp { set; get; }//单价

        [DecimalPrecision(10, 2), Column("PROC_PRICE")]
        public decimal ProcPrice { set; get; }//加工单价

        [DecimalPrecision(10, 2), Column("SUM_VALUAT")]
        public decimal SumValuat { set; get; }//估价累加

        [DecimalPrecision(10, 2), Column("SUM_PRCHS")]
        public decimal SumPrchs { set; get; }//单价累加

        [DecimalPrecision(10, 2), Column("NOTAX_AMT")]
        public decimal NotaxAmt { set; get; }//金额

        [Required, StringLength(10), Column("WHKP_ID")]
        public string WhkpId { set; get; }//仓管员ID

        [Required, Column("IN_DATE")]
        public DateTime InDate { set; get; }//入库日期

        [StringLength(512), Column("RMRS")]
        public string Rmrs{ set; get; }//备注

        [Required, StringLength(1), Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlag { set; get; }



    }
}
