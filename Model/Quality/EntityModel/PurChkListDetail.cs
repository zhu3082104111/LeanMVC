/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurChkListDetail.cs
// 文件功能描述：
//          进货检验单详细表的实体Model类
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
    /// DB Table Name: QU_PUR_CHK_LIST_DETAIL
    /// DB Table Name(CHS): 进货检验单详细表
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PUR_CHK_LIST_DETAIL")]
    public class PurChkListDetail : Entity
    {

        /// <summary>
        /// 检验单号
        /// </summary>
        [Column("CHK_LIST_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// 检验单序号
        /// </summary>
        [Column("LIST_NUM", Order = 1)]
        [Key, StringLength(2)]
        public string ListNum { get; set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        [Column("CHK_ITEM")]
        [StringLength(200)]
        public string ChkItem { get; set; }

        /// <summary>
        /// 检验标准
        /// </summary>
        [Column("CHK_STAND")]
        [StringLength(200)]
        public string ChkStand { get; set; }

        /// <summary>
        /// 供应商检验内容1
        /// </summary>
        [Column("SUP_CONT_1")]
        [StringLength(200)]
        public string SupCont1 { get; set; }

        /// <summary>
        /// LDK检验内容1
        /// </summary>
        [Column("LDK_CONT_1")]
        [StringLength(200)]
        public string LdkCont1 { get; set; }

        /// <summary>
        /// 供应商检验内容2
        /// </summary>
        [Column("SUP_CONT_2")]
        [StringLength(200)]
        public string SupCont2 { get; set; }

        /// <summary>
        /// LDK检验内容2
        /// </summary>
        [Column("LDK_CONT_2")]
        [StringLength(200)]
        public string LdkCont2 { get; set; }

        /// <summary>
        /// 供应商检验内容3
        /// </summary>
        [Column("SUP_CONT_3")]
        [StringLength(200)]
        public string SupCont3 { get; set; }

        /// <summary>
        /// LDK检验内容3
        /// </summary>
        [Column("LDK_CONT_3")]
        [StringLength(200)]
        public string LdkCont3 { get; set; }

        /// <summary>
        /// 供应商检验内容4
        /// </summary>
        [Column("SUP_CONT_4")]
        [StringLength(200)]
        public string SupCont4 { get; set; }

        /// <summary>
        /// LDK检验内容4
        /// </summary>
        [Column("LDK_CONT_4")]
        [StringLength(200)]
        public string LdkCont4 { get; set; }

        /// <summary>
        /// 供应商检验内容5
        /// </summary>
        [Column("SUP_CONT_5")]
        [StringLength(200)]
        public string SupCont5 { get; set; }

        /// <summary>
        /// LDK检验内容5
        /// </summary>
        [Column("LDK_CONT_5")]
        [StringLength(200)]
        public string LdkCont5 { get; set; }

        /// <summary>
        /// 供应商判定结果
        /// </summary>
        [Column(name: "SUP_RES", TypeName = "char")]
        [StringLength(1)]
        public string SupRes { get; set; }

        /// <summary>
        /// LDK判定结果
        /// </summary>
        [Column(name: "LDK_RES", TypeName = "char")]
        [StringLength(1)]
        public string LdkRes { get; set; }

    }
}