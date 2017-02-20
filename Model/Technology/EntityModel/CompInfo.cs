/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：CompInfo.cs
// 文件功能描述：
//          供货商信息表的实体Model类
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
    /// DB Table Name: PD_COMP_INFO
    /// DB Table Name(CHS): 供货商信息表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_COMP_INFO")]
    public class CompInfo : Entity
    {

        /// <summary>
        /// 供货商ID
        /// </summary>
        [Column("COMP_ID")]
        [Key, StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// 单位区分
        /// </summary>
        [Column(name: "COMP_TYPE", TypeName = "char")]
        [StringLength(1)]
        public string CompType { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Column("COMP_NAME")]
        [StringLength(200)]
        public string CompName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [Column("COMP_ADD")]
        [StringLength(300)]
        public string CompAdd { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Column("COMP_ZC")]
        [StringLength(10)]
        public string CompZc { get; set; }

        /// <summary>
        /// 信誉等级
        /// </summary>
        [Column(name: "CRE_LEVEL", TypeName = "char")]
        [StringLength(1)]
        public string CreLevel { get; set; }

        /// <summary>
        /// 电话1
        /// </summary>
        [Column("TEL_NO_1")]
        [StringLength(16)]
        public string TelNo1 { get; set; }

        /// <summary>
        /// 电话2
        /// </summary>
        [Column("TEL_NO_2")]
        [StringLength(16)]
        public string TelNo2 { get; set; }

        /// <summary>
        /// 传真1
        /// </summary>
        [Column("FAX_1")]
        [StringLength(16)]
        public string Fax1 { get; set; }

        /// <summary>
        /// 传真2
        /// </summary>
        [Column("FAX_2")]
        [StringLength(16)]
        public string Fax2 { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Column("CONTACTS_NAME")]
        [StringLength(32)]
        public string ContactsName { get; set; }

    }
}