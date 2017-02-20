/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CodeMake.cs
// 文件功能描述：
//          编码生成表的实体Model类
//      
// 修改履历：2013-12-18 汪罡 修改
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
    /// DB Table Name: BI_CODE_MAKE
    /// DB Table Name(CHS): 编码生成表
    /// Edit by WangGang @ 2013-12-18 11:50:32 .
    /// </summary>
    [Serializable, Table("BI_CODE_MAKE")]
    public class CodeMake : Entity
    {

        /// <summary>
        /// 编码区分
        /// </summary>
        [Column(name: "CD_CATG", TypeName = "char", Order = 0)]
        [Key, StringLength(5)]
        public string CdCatg { get; set; }

        /// <summary>
        /// 部门区分
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char", Order = 1)]
        [Key, StringLength(2)]
        public string DepartId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Column("DATE", Order = 2)]
        //[Key, StringLength(400)]
        [Key, StringLength(8)]
        public string Date { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Column("OD_NUM")]
        [StringLength(7)]
        public string OdNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}