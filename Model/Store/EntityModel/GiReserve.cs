using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    /// <summary>
    /// 让步仓库预约表
    /// M:梁龙飞
    /// </summary>
    [Serializable, Table("MC_WH_GI_RESERVE")]
    public class GiReserve : Entity
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key,Required, StringLength(8), Column("WH_ID",Order = 0)]
        public string WareHouseID { set; get; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, Required, StringLength(20), Column("PRHA_ODR_ID", Order = 1)]
        public string PrhaOrderID { set; get; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Key, Required, StringLength(2), Column("CLN_ODR_DTL", Order = 2)]
        public string ClientOrderDetail { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Required, StringLength(20), Column("ORD_PDT_ID")]
        public string OrdProductID { set; get; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, Required, StringLength(20), Column("PDT_ID", Order = 3)]
        public string ProductID { set; get; }

        //规格型号
        [StringLength(200), Column("PDT_SPEC")]
        public string ProductSpec { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Key, Required, StringLength(20), Column("BTH_ID", Order = 4)]
        public string BatchID { set; get; }

        //预约数量
        [DecimalPrecision(10, 2), Column("ORDE_QTY")]
        public decimal OrderQuantity { set; get; }

        //领料单开具数量
        [DecimalPrecision(10, 2), Column("PICK_ORDE_QTY")]
        public decimal PickOrderQuantity { set; get; }

        //已出库数量
        [DecimalPrecision(10, 2), Column("CMP_QTY")]
        public decimal CmpQuantity { set; get; }
       
    }
}
