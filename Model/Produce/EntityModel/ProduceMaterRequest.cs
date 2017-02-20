// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProduceMaterRequest.cs
// 文件功能描述：生产领料单信息实体类
// 
// 创建标识：代东泽 20131119
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
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
    ///  生产领料单信息实体类
    ///  代东泽 20131119
    /// </summary>
    [Serializable, Table("PD_PROD_MATER_REQU")]
    public class ProduceMaterRequest : Entity
    {
        public ProduceMaterRequest()
        {
        }

        //领料单号。string类型 默认对应数据库nvarchar类型
        /// <summary>
        /// 领料单号
        /// </summary>
        [Key, StringLength(20), Column("MATER_REQ_NO")]
        public string MaterReqNo { get; set; }

        //char类型的配置。stringlength配置长度.required表示不允许为null
        /// <summary>
        /// 领料单类型
        /// </summary>
        [Required, StringLength(2), Column(name: "MATER_REQ_TYPE", TypeName = "char")]
        public string MaterReqType { get; set; }

        /// <summary>
        /// 领用部门ID
        /// </summary>
        [StringLength(20), Column("DEPA_ID")]
        public string DeptID { get; set; }

        /// <summary>
        /// 用途描述
        /// </summary>
        [StringLength(200), Column("PURPOSE")]
        public string Purpose { get; set; }

        /// <summary>
        /// 仓库员ID
        /// </summary>
        [StringLength(20), Column("WH_PSN_ID")]
        public string WhPsnID { get; set; }

        /// <summary>
        /// 部门审核区分
        /// </summary>
        [StringLength(1), Column(name: "DEPA_AUDIT_FLG", TypeName = "char")]
        public string DeptAuditFlag { get; set; }

        /// <summary>
        /// 部门审核人ID
        /// </summary>
        [StringLength(20), Column("DEPA_AUDITOR_ID")]
        public string DeptAuditorID { get; set; }

        /// <summary>
        /// 领料人
        /// </summary>
        [StringLength(20), Column("MATER_HANDLER_ID")]
        public string MaterHandlerID { get; set; }

        /// <summary>
        /// 领料日期
        /// </summary>
        [Column("REQU_DT")]
        public DateTime? RequestDate { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按照最新的DB设计书修改长度。
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
        [MaxLength(400)]
        public string Remark { get; set; }


    }
}
