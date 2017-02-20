/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：TeamMemberInfo.cs
// 文件功能描述：
//          班组成员配置表的实体Model类
//      
// 修改履历：2013-12-19 汪罡 修改
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
    /// DB Table Name: BI_TEAM_MEMBER_INFO
    /// DB Table Name(CHS): 班组成员配置表
    /// Edit by WangGang @ 2013-12-19 10:27:55 .
    /// </summary>
    [Serializable, Table("BI_TEAM_MEMBER_INFO")]
    public class TeamMemberInfo : Entity
    {

        /// <summary>
        /// 班组ID
        /// </summary>
        [Column("TEAM_ID", Order = 0)]
        [Key, StringLength(20)]
        public string TeamId { get; set; }

        /// <summary>
        /// 成员ID
        /// </summary>
        [Column("MEM_ID", Order = 1)]
        [Key, StringLength(20)]
        public string MemId { get; set; }

        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照最新的DB设计书，此表已被拆分成两部分。
        /*
        /// <summary>
        /// 班组名称
        /// </summary>
        [Column("TEAM_NAME")]
        [Required]
        [StringLength(200)]
        public string TeamName { get; set; }

        /// <summary>
        /// 班组所属生产单元
        /// </summary>
        [Column(name: "DEPART_ID", TypeName = "char")]
        [Required]
        [StringLength(2)]
        public string DepartId { get; set; }
        */

        /// <summary>
        /// 班组长区分
        /// </summary>
        [Column(name: "TEAM_LEAD_FLG", TypeName = "char")]
        [StringLength(1)]
        public string TeamLeadFlg { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}

