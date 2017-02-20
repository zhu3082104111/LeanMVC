/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：UnitInfo.cs
// 文件功能描述：
//          单位信息表的实体Model类
//      
// 修改履历：2013-12-02 汪罡 修改
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
    /// DB Table Name: BI_UNIT_INFO
    /// DB Table Name(CHS): 单位信息表
    /// Edit by WangGang @ 2013-12-02 11:07:32 .
    /// </summary>
    [Serializable, Table("BI_UNIT_INFO")]
    public class UnitInfo : Entity
    {

        /// <summary>
        /// 单位ID
        /// </summary>
        [Column("UNIT_ID")]
        [Key, StringLength(20)]
        public string UnitId { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [Column("UNIT_NAME")]
        [Required]
        [StringLength(100)]
        public string UnitName { get; set; }

        /// <summary>
        /// 单位说明
        /// </summary>
        [Column("UNIT_EXP")]
        [StringLength(400)]
        public string UnitExp { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}