// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessTranslateCard.cs
// 文件功能描述：加工流转卡类
// 
// 创建标识：代东泽 20131203
//
// 修改标识：朱静波 20131206
// 修改描述：
//
// 修改标识：杜兴军 20131220 
// 修改描述：字段的添加及删除
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
    /// 代东泽 20131203
    /// 加工流转卡类
    /// </summary>
    [Table("PD_PROC_TRANS_CARD")]
    [Serializable]
    public  class ProcessTranslateCard:Entity
    {
        /// <summary>
        /// 加工流转卡号
        /// </summary>
        [Key,Column("PROC_DELIV_ID"),StringLength(20)]
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 输出品ID
        /// </summary>
        [Required, Column("EXPORT_ID"), StringLength(20)]
        public string ExportID { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Required, Column("PROC_ID"), StringLength(20)]
        public string ProcessID { get; set; }

        /// <summary>
        /// 批次号
        /// 注释掉 20131220 杜兴军
        /// </summary>
        //[Column("BTH_ID"), StringLength(20)]
        //public string BthID { get; set; }

        /// <summary>
        /// 需加工总件数
        /// </summary>
        [DecimalPrecision(10, 2), Column("NED_PROC_QTY")]
        public decimal NedProcQty { get; set; }

        /// <summary>
        /// 交仓数合计
        /// </summary>
        [DecimalPrecision(10, 2), Column("WAREH_TAL_QTY")]
        public decimal WarehTalQty { get; set; }

        /// <summary>
        /// 预计交仓合计
        /// </summary>
        [DecimalPrecision(10, 2),Column("PLN_TOTAL")]
        public decimal PlnTotal { get; set; }

        /// <summary>
        /// 领料数量
        /// </summary>
        //[DecimalPrecision(10, 2),Column("MATER_REQ_QTY")]
        //public decimal MaterReqQty { get; set; }

        /// <summary>
        /// 领料单号
        /// </summary>
        //[Column("MATER_REQ_NO"), StringLength(20)]
        //public string MaterReqNo { get; set; }

        /// <summary>
        /// 材料和规格要求
        /// 添加 20131220 杜兴军
        /// </summary>
        [StringLength(200), Column("SPECIFICA")]
        public string Specifica { get; set; }

        /// <summary>
        /// 计划开始日期
        /// 添加 20131220 杜兴军
        /// </summary>
        [Column("PLN_STR_DT")]
        public DateTime PlanStartDate { get; set; }

        /// <summary>
        /// 计划结束日期
        /// 添加 20131220 杜兴军
        /// </summary>
        [Column("PLN_END_DT")]
        public DateTime PlanEndDate { get; set; }

        /// <summary>
        /// 是否完结区分
        /// </summary>
        [StringLength(1), Column(name: "END_FLG", TypeName = "char")]
        public string EndFlag { get; set; }

        //备注
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }
    }
}
