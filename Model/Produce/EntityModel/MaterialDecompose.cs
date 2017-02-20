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
    /// 物料分解信息表
    /// 20131107 C:梁龙飞
    /// PD_MATERIAL_DECOM
    /// 20131113 朱静波 修改
    /// </summary>
    [Table("PD_MATERIAL_DECOM")]
    [Serializable]
    public class MaterialDecompose:Entity
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, StringLength(20), Column("CLN_ODR_ID",Order=0)]
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, StringLength(2), Column("CLN_ODR_DTL", Order = 1)]
        public string ClientOrderDetail { get; set; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, StringLength(15), Column("PPT_ID", Order = 2)]
        public string ProductsPartsID { get; set; }//产品零件ID

        /// <summary>
        /// 产品ID
        /// </summary>
        [Required,StringLength(15), Column("PRODUCT_ID")]
        public string ProductID { get; set; }//产品ID

        ///// <summary>
        ///// 零件在产品结构中的排序，产品本身为1(不可为空，否则取出的数据无结构)
        ///// </summary>
        //[Required, StringLength(4), Column("MAT_SEQNO")]
        //public string MatSequenceNo { get; set; }

        ///// <summary>
        ///// 工序在零件工序树中的位置，产品下直系零件的编号为0（不可为空，否则取出的数据无结构）
        ///// </summary>
        //[Required, StringLength(3), Column("PRO_SEQNO")]
        //public string ProcessSequenceNo { get; set; }

        /// <summary>
        /// 零件在产品结构中的排序，产品本身为1(不可为空，否则取出的数据无结构)
        /// </summary>
        [Column("MAT_SEQNO")]
        public int MatSequenceNo { get; set; }

        /// <summary>
        /// 工序在零件工序树中的位置，产品下直系零件的编号为0（不可为空，否则取出的数据无结构）
        /// </summary>
        [Column("PRO_SEQNO")]
        public int ProcessSequenceNo { get; set; }

        /// <summary>
        /// 原客户订单号
        /// </summary>
        [StringLength(20), Column("OLD_ODR_ID")]
        public string OldOrderID { get; set; }

        /// <summary>
        /// 计划需求数量
        /// </summary>
        [DecimalPrecision(10,2),Column("PLN_DEMAND_QTY")]
        public decimal DemondQuantity { get; set; }

        /// <summary>
        /// 计划购买明细 生产数量
        /// </summary>
        [DecimalPrecision(10,2),Column("PLN_PRD_QTY")]
        public decimal ProduceNeedQuantity { get; set; }
       
        /// <summary>
        /// 计划购买明细 外购数量
        /// </summary>
        [DecimalPrecision(10,2),Column("PLN_PUR_QTY")]
        public decimal PurchNeedQuantity { get; set; }
        
        /// <summary>
        /// 计划购买明细 外协数量
        /// </summary>
        [DecimalPrecision(10,2),Column("PLN_OUTS_QTY")]
        public decimal AssistNeedQuantity { get; set; }
      
        /// <summary>
        /// 提供日期
        /// </summary>
        [Column("PROV_YMD")]
        public DateTime ProvideDate { get; set; }

        /// <summary>
        /// 启动日期
        /// </summary>
        [Column("STA_YMD")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 工序ID 
        /// </summary>
        [StringLength(15), Column("PROC_ID")]
        public string ProcessID { get; set; }

        /// <summary>
        /// 备料工期（计划）
        /// </summary>
        [DecimalPrecision(10,2),Column("PREP_PERIOD")]
        public decimal PreparationPeriod { get; set; }

        /// <summary>
        /// 生产投产数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("PRD_QTY")]
        public decimal ProduceProductionQuantity { get; set; }

        /// <summary>
        /// 外购投产数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("PUR_QTY")]
        public decimal PurchProductionQuantity { get; set; }

        /// <summary>
        /// 外协投产数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("OUTS_QTY")]
        public decimal AssistProductionQuantity { get; set; }

        /// <summary>
        /// 材料及规格要求
        /// </summary>
        [StringLength(200), Column("SPECIFICA")]
        public string Specifica { get; set; }

        /// <summary>
        /// 正常物料锁库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("NORM_LOK_QTY")]
        public decimal NormalLockQuantity { get; set; }

        /// <summary>
        /// 单配物料锁库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("SPEC_LOK_QTY")]
        public decimal AbnormalLockQuantity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }


    }
}
