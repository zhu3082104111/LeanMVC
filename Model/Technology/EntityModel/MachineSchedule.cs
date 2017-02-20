/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MachineSchedule.cs
// 文件功能描述：
//          机器产能实绩表的实体Model类
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
    /// DB Table Name: PD_MACHINE_SCHEDULE
    /// DB Table Name(CHS): 机器产能实绩表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_SCHEDULE")]
    public class MachineSchedule : Entity
    {

        /// <summary>
        /// 机器ID
        /// </summary>
        [Column("MACH_ID", Order = 0)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Column("RUN_DATE", Order = 1)]
        [Key]
        public DateTime RunDate { get; set; }

        /// <summary>
        /// 已占用小时数
        /// </summary>
        [Column("OCC_HOUR")]
        [DecimalPrecision(4, 2)]
        public decimal OccHour { get; set; }

    }
}