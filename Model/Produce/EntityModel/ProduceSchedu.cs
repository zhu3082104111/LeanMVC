/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IProduceScheduDetailRepository.cs
// 文件功能描述：
// 
// 
// 创建标识：2013/11/19  杜兴军 创建
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
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
    /// 生成排期计划表
    /// </summary>
    [Serializable]
    [Table("PD_PROD_SCHEDU")]
    public class ProduceSchedu:Entity
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key,StringLength(20),Column("CLN_ODR_ID",Order = 0)]
        public string OrderID { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        [Key, StringLength(2), Column("CLN_ODR_DTL",Order = 1)]
        public string OrderDetail { get; set; }

        /// <summary>
        /// 输出品ID
        /// </summary>
        [Key,StringLength(20),Column("EXPORT_ID",Order = 2)]
        public string ExportId { get; set; }

        /// <summary>
        /// 生产类型,1：自生产，2：外购，3：外协加工，4：外协注塑，5：总装
        /// </summary>
        [Key, StringLength(1), Column("PROD_TYP",Order=3)]
        public string ProductType { get; set; }

        /// <summary>
        /// //产品ID
        /// </summary>
        [StringLength(20), Column("PDT_ID"),Required]
        public string ProductId { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [StringLength(20), Column("PROC_ID"),Required]
        public string ProcessId { get; set; }

        /// <summary>
        /// 计划开工日
        /// </summary>
        [Column("SCHE_STAR_DT")]
        public DateTime? ScheduStartDt { get; set; }
         
        /// <summary>
        /// 计划完工日
        /// </summary>
        [ Column("SCHE_END_DT")]
        public DateTime? ScheduEndDt { get; set; }

        /// <summary>
        /// 计划所要日数
        /// </summary>
        [DecimalPrecision(10, 2), Column("NED_PROD_TIME")]
        public decimal EndProduceTime { get; set; }
        
        /// <summary>
        /// 计划加工数量合计
        /// </summary>
        [DecimalPrecision(10, 2), Column("PLN_TAL_QTY")]
        public decimal PlanTotalQuanlity { get; set; }

        /// <summary>
        /// 实际加工数量合计
        /// </summary>
        [DecimalPrecision(10, 2), Column("ACT_TAL_QTY")]
        public decimal ActualTotalQuanlity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }

    }
}
