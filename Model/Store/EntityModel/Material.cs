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
    /// <summary>
    /// 仓库表：MC_WH_MATERIAL
    /// M：梁龙飞：删除时间长度限制
    /// :规范注释
    /// </summary>
    [Serializable, Table("MC_WH_MATERIAL")]
    public class Material : Entity
    {
        public Material()
        {
           
        }
      

        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None),Column("WH_ID",Order=0)]
        [StringLength(8)]
        public string WhID { set; get; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PDT_ID",Order=1)]
        [StringLength(20)]
        public string PdtID { set; get; }

        /// <summary>
        /// 被预约的数量
        /// </summary>
        [DecimalPrecision(10, 2),Column("ALCT_QTY")]
        public decimal AlctQty { set; get; }

        /// <summary>
        /// 物料需求量
        /// </summary>
        [DecimalPrecision(10, 2), Column("REQUIRE_QTY")]
        public decimal RequiteQty { set; get; }

        /// <summary>
        /// 下单数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ORDER_QTY")]
        public decimal OrderQty { set; get; }

        /// <summary>
        /// 外协取料数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("CNSM_QTY")]
        public decimal CnsmQty { set; get; }

        /// <summary>
        /// 到货数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ARRIVE_QTY")]
        public decimal ArrveQty { set; get; }

        /// <summary>
        /// 检收数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ISPC_QTY")]
        public decimal IspcQty { set; get; }

        /// <summary>
        /// 可用在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("USEABLE_QTY")]
        public decimal UseableQty { set; get; }

        /// <summary>
        /// 实际在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("CURRENT_QTY")]
        public decimal CurrentQty { set; get; }

        /// <summary>
        /// 警戒在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ALERT_QTY")]
        public decimal AlertQty { set; get; }

        /// <summary>
        /// 报废数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("DIS_QTY")]
        public decimal DisQty { set; get; }

        /// <summary>
        /// 总价
        /// </summary>
        [DecimalPrecision(10, 2),Column("TOTAL_AMT")]
        public decimal TotalAmt { set; get; }

        /// <summary>
        /// 估价总价
        /// </summary>
        [DecimalPrecision(10, 2),Column("TOTAL_VALUAT_UP")]
        public decimal TotalValuatUp { set; get; }

        /// <summary>
        /// 最终出库日
        /// </summary>
        [Column("LAST_WHOUT_YMD")]
        public DateTime? LastWhoutYmd { set; get; }

        /// <summary>
        /// 最终入库日
        /// </summary>
        [Column("LAST_WHIN_YMD")]
        public DateTime? LastWhINYmd { set; get; }
      

    }
}
