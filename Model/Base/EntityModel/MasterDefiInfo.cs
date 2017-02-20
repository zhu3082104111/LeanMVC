/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MasterDefiInfo.cs
// 文件功能描述：
//          雷迪克Master数据管理表
//      
// 修改履历：2013/12/18 汪罡 修改
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
    /// DB Table Name: BI_MASTER_DEFI_INFO
    /// DB Table Name(CHS)：雷迪克Master数据管理表
    /// Edit by WangGang @ 2013-12-18 11:35:10.
    /// </summary>
    [Serializable, Table("BI_MASTER_DEFI_INFO")]
    public class MasterDefiInfo : Entity
    {
        /// <summary>
        /// 分段类别代码
        /// </summary>
        //[Column("SECTION_CD", Order = 0)]
        [Column(name: "SECTION_CD", TypeName = "char", Order = 0)]
        [Key, StringLength(5)]
        public string SectionCd { get; set; }

        /// <summary>
        /// 内部序号（自增）
        /// </summary>
        [Column("S_NO", Order = 1)]
        //[Key, StringLength(5)]
        [Key, DecimalPrecision(5, 0)]
        //public string SNo { get; set; }
        public decimal SNo { get; set; }

        /// <summary>
        /// 分段类别名称
        /// </summary>
        [Column("SECTION_VALUE")]
        [StringLength(400)]
        public string SectionValue { get; set; }

        /// <summary>
        /// 属性代码
        /// </summary>
        [Column("ATTR_CD")]
        [StringLength(5)]
        public string AttrCd { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [Column("ATTR_VALUE")]
        [StringLength(400)]
        public string AttrValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }
    }
}