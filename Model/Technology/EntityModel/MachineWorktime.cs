/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MachineWorktime.cs
// 文件功能描述：
//          机器运转时间表的实体Model类
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
    /// DB Table Name: PD_MACHINE_WORKTIME
    /// DB Table Name(CHS): 机器运转时间表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_MACHINE_WORKTIME")]
    public class MachineWorktime : Entity
    {

        /// <summary>
        /// 机器ID
        /// </summary>
        [Column("MACH_ID", Order = 0)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// 适用开始时间
        /// </summary>
        [Column("ACTIV_STR_DT", Order = 1)]
        [Key]
        public DateTime ActivStrDt { get; set; }

        /// <summary>
        /// 适用终止日期
        /// </summary>
        [Column("ACTIV_END_DT")]
        public DateTime? ActivEndDt { get; set; }

        /// <summary>
        /// 每天最大运转小时数
        /// </summary>
        [Column("MAX_WORK_TIME")]
        [DecimalPrecision(4, 2)]
        public decimal MaxWorkTime { get; set; }

    }
}