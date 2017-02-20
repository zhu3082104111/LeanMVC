using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 物理表名：外协指示表
    /// 20131121 梁龙飞C
    /// </summary>
    [Serializable]
    [Table("PD_ASSI_INSTRUC")]
    public class AssistInstruction:Entity
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, StringLength(20), Column("CLN_ODR_ID", Order = 0)]
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Key, StringLength(2), Column("CLN_ODR_DTL", Order = 1)]
        public string ClientOrderDetail { get; set; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, StringLength(15), Column("PPT_ID", Order = 2)]
        public string ProductsPartsID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Required, StringLength(15), Column("PRODUCT_ID")]
        public string ProductID { get; set; }

        /// <summary>
        /// 产品状态
        /// </summary>
        [StringLength(1), Column(name: "RECEIVE_FLAG", TypeName = "char")]
        public string ReceiveFlag { get; set; }

        /// <summary>
        /// 接受日期
        /// </summary>
        [Column("RECEIVE_DT")]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [StringLength(20)]
        public string ProcessID { get; set; }

        /// <summary>
        /// 已排产数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("SCHE_QTY")]
        public decimal ScheduledQtt { get; set; }

        /// <summary>
        /// 生产单元
        /// </summary>
        [StringLength(2), Column("DEPART_ID")]
        public string DepartmentID { get; set; }

    }
}
