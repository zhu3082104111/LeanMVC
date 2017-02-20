/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdMaterRequ1.cs
// 文件功能描述：
//          外协领料单表的实体Model类
//      
// 修改履历：2014-01-13 汪罡 修改
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
    /// DB Table Name: PD_PROD_MATER_REQU1
    /// DB Table Name(CHS): 外协领料单表
    /// Edit by WangGang @ 2014-01-13 11:09:01 .
    /// </summary>
    [Serializable, Table("PD_PROD_MATER_REQU1")]
    public class ProdMaterRequ1 : Entity
    {

        /// <summary>
        /// 领料单号
        /// </summary>
        [Column("MATER_REQ_NO")]
        [Key, StringLength(20)]
        public string MaterReqNo { get; set; }

        /// <summary>
        /// 领料单类型
        /// </summary>
        [Column(name: "MATER_REQ_TYPE", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string MaterReqType { get; set; }

        /// <summary>
        /// 领用部门ID
        /// </summary>
        [Column("DEPA_ID")]
        [StringLength(20)]
        public string DepaId { get; set; }

        /// <summary>
        /// 用途描述
        /// </summary>
        [Column("PURPOSE")]
        [StringLength(200)]
        public string Purpose { get; set; }

        /// <summary>
        /// 仓库员ID
        /// </summary>
        [Column("WH_PSN_ID")]
        [StringLength(20)]
        public string WhPsnId { get; set; }

        /// <summary>
        /// 部门审核区分
        /// </summary>
        [Column(name: "DEPA_AUDIT_FLG", TypeName = "char")]
        [StringLength(1)]
        public string DepaAuditFlg { get; set; }

        /// <summary>
        /// 部门审核人ID
        /// </summary>
        [Column("DEPA_AUDITOR_ID")]
        [StringLength(20)]
        public string DepaAuditorId { get; set; }

        /// <summary>
        /// 领料人ID
        /// </summary>
        [Column("MATER_HANDLER_ID")]
        [StringLength(20)]
        public string MaterHandlerId { get; set; }

        /// <summary>
        /// 领料日期
        /// </summary>
        [Column("REQU_DT")]
        public DateTime? RequDt { get; set; }

        /// <summary>
        /// 领料单关联单据号
        /// </summary>
        [Column("RELA_BIL_ID")]
        [StringLength(20)]
        public string RelaBilId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}