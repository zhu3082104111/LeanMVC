using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Serializable, Table("MC_WH_SEM_IN_RECORD")]
    public class SemInRecord:Entity
    {
        [Required, StringLength(20), Column("PLAN_ID")]
        public string PlanId { set; get; }//生产计划单号

        [Required, StringLength(20), Column("PLAN_CLS")]
        public string PlanCls { set; get; }//计划单区分

        [Required, StringLength(20), Column("BTH_ID")]
        public string BthId { set; get; }//批次号

        [Key, StringLength(20), Column("DLVY_LIST_ID")]
        public string DlvyListId { set; get; }//送货单号

        [Required, StringLength(8), Column("WH_ID")]
        public string WhId { set; get; }//仓库编号

        [Required, StringLength(3), Column("WH_POSI_ID")]
        public string WhPostId { set; get; }//仓位

        [Required, StringLength(2), Column("IN_MV_CLS")]
        public string InMvCls { set; get; }//入库移动区分

        [Required, StringLength(20), Column("TECN_PDT_IN_ID")]
        public string TecnPdtInId { set; get; }//加工产品入库单据号

        [Required, StringLength(20), Column("PROC_UNIT")]
        public string ProcUnit { set; get; }//加工单位


    }
}
