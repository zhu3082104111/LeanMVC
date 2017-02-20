using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 生成排期详细表
    /// </summary>
    [Serializable]
    [Table("PD_PROD_SCHEDU_DETAIL")]
    public class ProduceScheduDetail:Entity
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, StringLength(20), Column("CLN_ODR_ID",Order = 0)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Key, StringLength(2), Column("CLN_ODR_DTL",Order = 1)]
        public string OrderNumber { get; set; }

        /// <summary>
        /// 输出品ID
        /// </summary>
        [Key, StringLength(20), Column("EXPORT_ID",Order = 2)]
        public string ExportId { get; set; }//输出品ID

        /// <summary>
        /// 生产类型,1：自生产，2：外购，3：外协加工，4：外协注塑，5：总装
        /// </summary>
        [Key, StringLength(1), Column("PROD_TYP", Order = 3)]
        public string ProductType { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Key, StringLength(10), Column("PLN_DT", Order = 4)]
        public DateTime Date { get; set; }//日期

        /// <summary>
        /// 产品ID
        /// </summary>
        [StringLength(20), Column("PDT_ID"),Required]
        public string ProductId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [StringLength(20), Column("PROC_ID"),Required]
        public string ProcessId { get; set; }

        /// <summary>
        /// 计划加工件数
        /// </summary>
        [DecimalPrecision(10, 2), Column("PLN_QTY")]
        public decimal ScheduQuanlity { get; set; }

        /// <summary>
        /// 实际加工件数
        /// </summary>
        [DecimalPrecision(10,2), Column("FINI_QTY")]
        public decimal FinishedQuanlity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }


    }
}
