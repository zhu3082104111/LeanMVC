/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessDetail.cs
// 文件功能描述：
//          工序详细信息表的实体Model类
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
    /// DB Table Name: PD_PROCESS_DETAIL
    /// DB Table Name(CHS): 工序详细信息表
    /// Edit by WangGang @ 2013-12-19 14:00:46 .
    /// </summary>
    [Serializable, Table("PD_PROCESS_DETAIL")]
    public class ProcessDetail : Entity
    {

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID", Order = 0)]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        [Column("SEQ_NO", Order = 1)]
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照最新的DB设计书及指摘，需要改成Integer类型。
        //[Key, StringLength(5)]
        //public string SeqNo { get; set; }
        [Key]
        public int SeqNo { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [Column("SUB_PROC_NAME")]
        [Required]
        [StringLength(50)]
        public string SubProcName { get; set; }

        /// <summary>
        /// 定额编号
        /// </summary>
        [Column("QUOT_NUM")]
        [StringLength(5)]
        public string QuotNum { get; set; }

        /// <summary>
        /// 操作倍数
        /// </summary>
        [Column("OPERA_RAT")]
        [DecimalPrecision(7, 2)]
        public decimal OperaRat { get; set; }

        /// <summary>
        /// 单位时间加工件数
        /// </summary>
        [Column("SE_UT_PROD_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal SeUtProdQty { get; set; }

        /// <summary>
        /// 单位ID（自加工）
        /// </summary>
        [Column("SE_UNIT_ID")]
        [StringLength(20)]
        public string SeUnitId { get; set; }

        /// <summary>
        /// 自加工单价
        /// </summary>
        [Column("UNIT_PRICE")]
        [DecimalPrecision(16, 6)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 单价单位ID
        /// </summary>
        [Column("PRI_UNIT_ID")]
        [StringLength(20)]
        public string PriUnitId { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        [Column("DESCRIPTION")]
        [StringLength(400)]
        public string Description { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}