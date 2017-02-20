using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{

    [Serializable, Table("MC_WH_FIN_OUT_RECORD")]
    public class FinOutRecord : Entity
    {
        //仓库编号
        [Required, StringLength(8), Column("WH_ID")]
        public string WareHouseID { set; get; }

        //出库移动区分
        [Required, StringLength(2), Column("OUT_MV_CLS")]
        public string OutMoveCls { set; get; }

        //内部成品送货单据号
        [Required, StringLength(20),Column("INER_FIN_OUT_ID")]
        public string InerFinOutID { set; get; }

        //提货单编号
        [Key, Required, StringLength(20),Column("LADI_ID")]
        public string LadiID { set; get; }

        //仓管员ID
        [Required, StringLength(10),Column("WHKP_ID")]
        public string WareHouseKpID { set; get; }

        //备注
        [StringLength(512),Column("RMRS")]
        public string Remarks { set; get; }

        //出库日期
        [Required, Column("OUT_DATE")]
        public DateTime OutDate { set; get; }

        //打印FLAG
        [Required, StringLength(1), Column(name: "PRINT_FLAG", TypeName = "char")]
        public string PrintFlag { set; get; }

    }
}
