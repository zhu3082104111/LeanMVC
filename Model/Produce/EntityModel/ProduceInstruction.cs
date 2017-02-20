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
    /// 20131121 梁龙飞C
    /// 对应表：生产指示表
    /// </summary>
    [Serializable]
    [Table("PD_PROD_INSTRUC")]
    public class ProduceInstruction:Entity
    {
        [Key, StringLength(20), Column("CLN_ODR_ID", Order = 0)]
        public string ClientOrderID { get; set; }//客户订单号

        [Key, StringLength(2), Column("CLN_ODR_DTL", Order = 1)]
        public string ClientOrderDetail { get; set; }//客户订单明细

        [Key, StringLength(15), Column("PPT_ID", Order = 2)]
        public string ProductsPartsID { get; set; }//产品零件ID

        [Required, StringLength(15), Column("PRODUCT_ID")]
        public string ProductID { get; set; }//产品ID

        [StringLength(1), Column(name: "PROC_FLG", TypeName = "char")]
        public string ProcessFlag { get; set; }

        [Column("STA_YMD")]
        public DateTime StartDate { get; set; }//开始时间

        [DecimalPrecision(10,2),Column("Ned_PROD_QTY")]
        public decimal NeedProcessQuantity { get; set; }//需要生产数量

        [StringLength(1), Column(name: "RECEIVE_FLAG", TypeName = "char")]
        public string ReceiveFlag { get; set; }//接收状态

        [Column("RECEIVE_YMD")]
        public DateTime ReceiveDate { get; set; }

    }
}
