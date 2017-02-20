/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompMaterialInfo.cs
// 文件功能描述：
//          供货商供货信息表的实体Model类
//      
// 修改履历：2013-12-16 汪罡 修改
/*****************************************************************************/
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
    /// DB Table Name: PD_COMP_MATERIAL_INFO
    /// DB Table Name(CHS): 供货商供货信息表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_COMP_MATERIAL_INFO")]
    public class CompMaterialInfo : Entity
    {

        /// <summary>
        /// 供货商ID
        /// </summary>
        [Column("COMP_ID", Order = 0)]
        [Key, StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Column("PDT_ID", Order = 1)]
        [Key, StringLength(20)]
        public string PdtId { get; set; }

        /// <summary>
        /// 供货区分
        /// </summary>
        [Column(name: "COMP_TYPE", TypeName = "char", Order = 2)]
        [Key, StringLength(1)]
        public string CompType { get; set; }

        /// <summary>
        /// 适用开始日期
        /// </summary>
        [Column("ACTIV_STR_DT", Order = 3)]
        [Key]
        public DateTime ActivStrDt { get; set; }

        /// <summary>
        /// 适用终止日期
        /// </summary>
        [Column("ACTIV_END_DT")]
        [Required]
        public DateTime ActivEndDt { get; set; }

        /// <summary>
        /// 供应商提供型号
        /// </summary>
        [Column("VEN_MODEL_ID")]
        [StringLength(200)]
        public string VenModelId { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Column("UNIT_PRICE")]
        [DecimalPrecision(16, 6)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 单价单位ID
        /// </summary>
        [Column("PRI_UNIT_ID")]
        [StringLength(20)]
        public string PriUnitId { get; set; }

        /// <summary>
        /// 估价
        /// </summary>
        [Column("EVALUATE")]
        [DecimalPrecision(16, 6)]
        public decimal Evaluate { get; set; }

        /// <summary>
        /// 估价单位ID
        /// </summary>
        [Column("EVA_UNIT_ID")]
        [StringLength(20)]
        public string EvaUnitId { get; set; }

        /// <summary>
        /// 供应商产能
        /// </summary>
        [Column("SUP_CAPA")]
        [DecimalPrecision(16, 6)]
        public decimal SupCapa { get; set; }

        /// <summary>
        /// 供应商产能单位ID
        /// </summary>
        [Column("SUPY_UNIT_ID")]
        [StringLength(20)]
        public string SupyUnitId { get; set; }

        /// <summary>
        /// 供货物流周期日数
        /// </summary>
        [Column("SUP_CYC_DAY")]
        [DecimalPrecision(12, 2)]
        public decimal SupCycDay { get; set; }

        /// <summary>
        /// 供应质量评定等级
        /// </summary>
        [Column(name: "QLT_LEVEL", TypeName = "char")]
        [StringLength(1)]
        public string QltLevel { get; set; }

        /// <summary>
        /// 月最大供应数量
        /// </summary>
        [Column("MOTH_MAX_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal MothMaxQty { get; set; }

        /// <summary>
        /// 最小订货数量
        /// </summary>
        [Column("MIN_ODR_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal MinOdrQty { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}