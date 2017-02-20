/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MachineInfo.cs
// 文件功能描述：
//          机器信息表的实体Model类
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
    /// DB Table Name: PD_MACHINE_INFO
    /// DB Table Name(CHS): 机器信息表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_INFO")]
    public class MachineInfo : Entity
    {

        /// <summary>
        /// 机器ID
        /// </summary>
        [Column("MACH_ID")]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// 机器名称
        /// </summary>
        [Column("MACH_NAME")]
        [StringLength(200)]
        public string MachName { get; set; }

        /// <summary>
        /// 机器型号
        /// </summary>
        [Column("MACH_MOD")]
        [StringLength(200)]
        public string MachMod { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}