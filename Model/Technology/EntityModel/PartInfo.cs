/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PartInfo.cs
// 文件功能描述：
//          零件信息表的实体Model类
//      
// 修改履历：2013-12-09 汪罡 修改
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
    /// DB Table Name: PD_PART_INFO
    /// DB Table Name(CHS): 零件信息表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PART_INFO")]
    public class PartInfo : Entity
    {

        /// <summary>
        /// 零件ID
        /// </summary>
        [Column("PART_ID")]
        [Key, StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// 零件类别
        /// </summary>
        [Column(name: "PART_CATG", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string PartCatg { get; set; }

        /// <summary>
        /// 零件子类别
        /// </summary>
        [Column("PART_SUB_CATG")]
        [Required]
        [StringLength(3)]
        public string PartSubCatg { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [Column("PART_NAME")]
        [Required]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// 零件略称
        /// </summary>
        [Column("PART_ABBREVI")]
        [StringLength(200)]
        public string PartAbbrevi { get; set; }

        /// <summary>
        /// 预备类别
        /// </summary>
        [Column(name: "PREP_CATG", TypeName = "char")]
        [StringLength(2)]
        public string PrepCatg { get; set; }

        /// <summary>
        /// 旧零件名称
        /// </summary>
        [Column("OLD_MODEL_ID")]
        [StringLength(200)]
        public string OldModelId { get; set; }

        /// <summary>
        /// 零件单位ID
        /// </summary>
        [Column("UNIT_ID")]
        [StringLength(20)]
        public string UnitId { get; set; }

        /// <summary>
        /// 材料及规格型号
        /// </summary>
        [Column("SPECIFICA")]
        [StringLength(400)]
        public string Specifica { get; set; }

        /// <summary>
        /// 零件属性1
        /// </summary>
        [Column(name: "ATTRIBUTE_1", TypeName = "char")]
        [StringLength(5)]
        public string Attribute1 { get; set; }

        /// <summary>
        /// 零件属性2
        /// </summary>
        [Column(name: "ATTRIBUTE_2", TypeName = "char")]
        [StringLength(5)]
        public string Attribute2 { get; set; }

        /// <summary>
        /// 零件属性3
        /// </summary>
        [Column(name: "ATTRIBUTE_3", TypeName = "char")]
        [StringLength(5)]
        public string Attribute3 { get; set; }

        /// <summary>
        /// 零件属性4
        /// </summary>
        [Column(name: "ATTRIBUTE_4", TypeName = "char")]
        [StringLength(5)]
        public string Attribute4 { get; set; }

        /// <summary>
        /// 零件属性5
        /// </summary>
        [Column(name: "ATTRIBUTE_5", TypeName = "char")]
        [StringLength(5)]
        public string Attribute5 { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [Column("PRICEE")]
        [DecimalPrecision(16, 6)]
        public decimal Pricee { get; set; }

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
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}