using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Serializable, Table("MC_WH_DISCARD")]
    public class Discard : Entity
    {
            
            [Key, StringLength(20),Column("DISCARD_ID",Order=0)]
            public string DiscardID { set; get; }//报废单号ID

            [Key, StringLength(8), Column("WH_ID", Order = 1)]
            public string WareHouseID { set; get; }//仓库编号

            [Key, StringLength(15), Column("PDT_ID",Order=2)]
            public string PartDtID { set; get; }//零件ID

            [Key, StringLength(20), Column("BTH_ID", Order = 3)]
            public string BThId { set; get; }//批次号

            [StringLength(50),Column("PDT_NAME")]
            public string PartDtName { set; get; }//零件名称

            [StringLength(200),Column("PDT_SPEC")]
            public string PartDtSpec { set; get; }//规格型号

            [DecimalPrecision(10, 2),Column("DIS_QTY")]
            public decimal Quantity { set; get; } //报废数量

            [DecimalPrecision(10, 2),Column("PRCHS_UP")]
            public decimal PrchsUp { set; get; } //单价

            [DecimalPrecision(10, 2),Column("TOTAL_AMT")]
            public decimal TotalAmt { set; get; } //总价

            [DecimalPrecision(10, 2), Column("VALUAT_UP")]
            public decimal ValuatUp { set; get; } //估价

            [DecimalPrecision(10, 2), Column("SUM_VALUAT")]
            public decimal SumValuat { set; get; } //估价累计价格

            [DecimalPrecision(10, 2), Column("SUM_PRCHS")]
            public decimal SumPrchs { set; get; } //单价累计价格

            [StringLength(200),Column("QLTY_PRO")]
            public string QualityPro { set; get; }//质量问题

            [StringLength(200),Column("SLT_PLN")]
            public string SltPlan { set; get; }//处理方案

            [Column("SLT_DATE")]
            public DateTime? SltDate { set; get; } //处理日期

            [StringLength(20),Column("APL_USER_ID")]
            public string AplUserID { set; get; }//编制人员ID

            [StringLength(20),Column("VRF_USER_ID")]
            public string VrfUserID { set; get; }//审核人员ID

            [StringLength(20),Column("APROV_USER_ID")]
            public string AppovUserID { set; get; }//批准人员ID

            [StringLength(2),Column("ST_CLS")]
            public string StoreCls { set; get; } //在库区分
            
            [StringLength(1),Column(name: "APPROVAL_FLG", TypeName = "char")]
            public string ApprovalFlg { set; get; }//审批FLG

            [StringLength(1),Column(name: "STATE", TypeName = "char")]
            public string State { set; get; }//状态


    }


}
