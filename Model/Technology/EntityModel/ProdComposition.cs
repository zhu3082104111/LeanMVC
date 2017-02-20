/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdComposition.cs
// 文件功能描述：
//          成品构成信息表的实体Model类
//      
// 修改履历：2013-12-12 汪罡 修改
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
    /// DB Table Name: PD_PROD_COMPOSITION
    /// DB Table Name(CHS): 成品构成信息表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PROD_COMPOSITION")]
    public class ProdComposition : Entity
    {

        #region C:梁龙飞（禁止移除：非映射到数据库字段，供视图用）
        /// <summary>
        /// NotMapped：零件在产品结构中的排序，产品本身为1
        /// </summary>
        [NotMapped]
        public int MatSequenceNo { get; set; }

        /// <summary>
        /// NotMapped:工序在零件工序树中的位置，产品下直系零件的编号为1
        /// </summary>
        [NotMapped]
        public int ProcessSequenceNo { get; set; }
        #endregion

        /// <summary>
        /// 父项目ID
        /// </summary>
        [Column("PAR_ITEM_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ParItemId { get; set; }

        /// <summary>
        /// 子项目ID
        /// </summary>
        [Column("SUB_ITEM_ID", Order = 1)]
        [Key, StringLength(20)]
        public string SubItemId { get; set; }

        /// <summary>
        /// 父项目类型
        /// </summary>
        [Column(name: "PAR_ITEM_CATG", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string ParItemCatg { get; set; }

        /// <summary>
        /// 子项目类型
        /// </summary>
        [Column(name: "SUB_ITEM_CATG", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string SubItemCatg { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        [Column("UNIT_ID")]
        [Required]
        [StringLength(20)]
        public string UnitId { get; set; }

        /// <summary>
        /// 构成数量
        /// </summary>
        [Column("CONST_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal ConstQty { get; set; }

        /// <summary>
        /// 代替品ID
        /// </summary>
        [Column("SUBSTITUTE_ID")]
        [StringLength(20)]
        public string SubstituteId { get; set; }

        /// <summary>
        /// 父工序ID
        /// </summary>
        [Column("PAR_PROC_ID")]
        [StringLength(20)]
        public string ParProcId { get; set; }

        /// <summary>
        /// 子工序ID
        /// </summary>
        [Column("SUB_PROC_ID")]
        [StringLength(20)]
        public string SubProcId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}