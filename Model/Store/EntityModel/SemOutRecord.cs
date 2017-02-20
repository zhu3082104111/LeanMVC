using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Serializable, Table("MC_WH_SEM_OUT_RECORD")]
    public class SemOutRecord : Entity
    {
        [Key, StringLength(20), Column("PICK_LIST_ID")]
        public string PickListId { set; get; }//领料单号
       
        [Required, StringLength(20), Column("PICK_LIST_TYPE_ID")]
        public string PickListTypeID { set; get; }//领料单类型

        [Required, StringLength(8), Column("WH_ID")]
        public string WhId { set; get; }//仓库编号

        [Required, StringLength(8), Column("OUT_MV_CLS")]
        public string OutMvCls { set; get; }//出库移动区分

        [Required, StringLength(20), Column("TECN_PDT_OUT_ID")]
        public string TecnPdtOutId { set; get; }//加工产品出库单据号

        [Required, StringLength(20), Column("CALLIN_UNIT_ID")]
        public string CallinUnitId { set; get; }//调入单位名称

    } //end SemOutRecord
}
