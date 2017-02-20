/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessMachine.cs
// 文件功能描述：
//          工序和机器对应表的实体Model类
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
    /// DB Table Name: PD_PROCESS_MACHINE
    /// DB Table Name(CHS): 工序和机器对应表
    /// Edit by WangGang @ 2013-12-09 17:31:54 .
    /// </summary>
    [Serializable, Table("PD_PROCESS_MACHINE")]
    public class ProcessMachine : Entity
    {

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// 工序顺序号
        /// </summary>
        [Column("SEQ_NO", Order = 1)]
        [Key, StringLength(5)]
        public string SeqNo { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        [Column("PART_ID", Order = 2)]
        [Key, StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// 机器ID
        /// </summary>
        [Column("MACH_ID", Order = 3)]
        [Key, StringLength(20)]
        public string MachId { get; set; }

        /// <summary>
        /// 单位时间加工件数
        /// </summary>
        [Column("UT_PROD_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal UtProdQty { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        [Column("UNIT_ID")]
        [StringLength(20)]
        public string UnitId { get; set; }

    }
}

