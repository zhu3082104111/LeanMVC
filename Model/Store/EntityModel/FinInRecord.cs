using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
        [Serializable, Table("MC_WH_FIN_IN_RECORD")]
        public class FinInRecord : Entity
        {
            //成品交仓单号
            [Key, Required, StringLength(20), Column("PLAN_ID")]
            public string PlanID { set; get; }

            //批次号
            [Required, StringLength(20), Column("BTH_ID")]
            public string BatchID { set; get; }


            //仓库编号
            [Required, StringLength(8), Column("WH_ID")]
            public string WareHouseID { set; get; }

            //仓位
            [Required, StringLength(3), Column("WH_POSI_ID")]
            public string WareHousePositionID { set; get; }

            //入库移动区分
            [Required, StringLength(2), Column("IN_MV_CLS")]
            public string InMoveCls { set; get; }

            //成品入库单号
            [Required, StringLength(20), Column("FS_IN_ID")]
            public string FsInID { set; get; }

            //入库日期
            [Required, Column("IN_DATE")]
            public DateTime InDate { set; get; }

            //仓管员ID
            [Required, StringLength(10), Column("WHKP_ID")]
            public string WareHouseKpID { set; get; }

            //备注
            [StringLength(200), Column("RMRS")]
            public string Remarks { set; get; }

    }
}
