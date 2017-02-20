/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：Process.cs
// 文件功能描述：
//          工序信息表的实体Model类
//      
// 修改履历：2014-01-06 汪罡 修改
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
    /// DB Table Name: PD_PROCESS
    /// DB Table Name(CHS): 工序信息表
    /// Edit by WangGang @ 2014-01-06 11:26:01 .
    /// </summary>
    [Serializable, Table("PD_PROCESS")]
    public class Process : Entity
    {

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID")]
        [Key, StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// 单位时间加工件数（自加工）
        /// </summary>
        [Column("SE_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal SeUtProdQty { get; set; }

        /// <summary>
        /// 单位时间加工件数（外协）
        /// </summary>
        [Column("AS_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal AsUtProdQty { get; set; }

        /// <summary>
        /// 单位时间加工件数（外购）
        /// </summary>
        [Column("PU_UT_PROD_QTY")]
        [Required]
        [DecimalPrecision(12, 2)]
        public decimal PuUtProdQty { get; set; }

        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("PROC_NAME")]
        [Required]
        [StringLength(200)]
        public string ProcName { get; set; }

        /// <summary>
        /// 自加工可否区分
        /// </summary>
        [Column(name: "SE_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string SeFlg { get; set; }

        /// <summary>
        /// 外协可否区分
        /// </summary>
        [Column(name: "AS_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string AsFlg { get; set; }

        /// <summary>
        /// 外购可否区分
        /// </summary>
        [Column(name: "PU_FLG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string PuFlg { get; set; }

        /// <summary>
        /// 默认工序类别
        /// </summary>
        [Column(name: "DEF_PROC_CATG", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string DefProcCatg { get; set; }

        /// <summary>
        /// 单位ID（自加工）
        /// </summary>
        [Column("SE_UNIT_ID")]
        [StringLength(20)]
        public string SeUnitId { get; set; }

        /// <summary>
        /// 单位ID（外协）
        /// </summary>
        [Column("AS_UNIT_ID")]
        [StringLength(20)]
        public string AsUnitId { get; set; }

        /// <summary>
        /// 单位ID（外购）
        /// </summary>
        [Column("PU_UNIT_ID")]
        [StringLength(20)]
        public string PuUnitId { get; set; }

        /// <summary>
        /// 自加工单价
        /// </summary>
        [Column("SE_UNIT_PRICE")]
        [DecimalPrecision(16, 6)]
        public decimal SeUnitPrice { get; set; }

        /// <summary>
        /// 单价单位ID
        /// </summary>
        [Column("PRI_UNIT_ID")]
        [StringLength(20)]
        public string PriUnitId { get; set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        [Column("DESCRIPTION")]
        [StringLength(400)]
        public string Description { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-06
        //修改原因：按照最新的DB设计书增加“注塑区分”字段。
        /// <summary>
        /// 注塑区分
        /// </summary>
        [Column(name: "IM_FLG", TypeName = "char")]
        [StringLength(1)]
        public string ImFlg { get; set; }

        /// <summary>
        /// 耗损率
        /// </summary>
        [Column("ATTRI_RATE")]
        [DecimalPrecision(12, 2)]
        public decimal AttriRate { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-06
        //修改原因：按照最新的DB设计书增加“必要准备日数（自加工）”字段。
        /// <summary>
        /// 必要准备日数（自加工）
        /// </summary>
        [Column("SE_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal SeNpDays { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-06
        //修改原因：按照最新的DB设计书增加“必要准备日数（外协）”字段。
        /// <summary>
        /// 必要准备日数（外协）
        /// </summary>
        [Column("AS_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal AsNpDays { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-06
        //修改原因：按照最新的DB设计书增加“必要准备日数（外购）”字段。
        /// <summary>
        /// 必要准备日数（外购）
        /// </summary>
        [Column("PU_NP_DAYS")]
        [DecimalPrecision(12, 2)]
        public decimal PuNpDays { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}