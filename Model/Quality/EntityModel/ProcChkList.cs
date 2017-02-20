/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcChkList.cs
// 文件功能描述：
//          过程检验单表的实体Model类
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
    /// DB Table Name: QU_PROC_CHK_LIST
    /// DB Table Name(CHS): 过程检验单表
    /// Edit by WangGang @ 2013-12-19 11:30:06 .
    /// </summary>
    [Serializable, Table("QU_PROC_CHK_LIST")]
    public class ProcChkList : Entity
    {

        /// <summary>
        /// 检验单号
        /// </summary>
        [Column("CHK_LIST_ID")]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// 质检日期
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        [Column("PART_ID")]
        [StringLength(20)]
        public string PartId { get; set; }

        /// <summary>
        /// 零件名称
        /// </summary>
        [Column("PART_NAME")]
        [StringLength(200)]
        public string PartName { get; set; }

        /// <summary>
        /// 零件型号
        /// </summary>
        [Column("PART_MOD")]
        [StringLength(200)]
        public string PartMod { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

        /// <summary>
        /// 进货日期
        /// </summary>
        [Column("PUR_DT")]
        public DateTime? PurDt { get; set; }

        /// <summary>
        /// 交库数量
        /// </summary>
        [Column("STO_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal StoQty { get; set; }

        /// <summary>
        /// 入库状态
        /// </summary>
        [Column(name: "STO_STAT", TypeName = "char")]
        [StringLength(1)]
        public string StoStat { get; set; }

        /// <summary>
        /// 让步区分
        /// </summary>
        [Column("GI_CLS")]
        [StringLength(3)]
        public string GiCls { get; set; }

        /// <summary>
        /// 材料及规格要求
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { get; set; }

        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：根据DB设计书，增加“检验综合判定”字段。
        /// <summary>
        /// 检验综合判定
        /// </summary>
        [Column(name: "CHK_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string ChkComRes { get; set; }

        /// <summary>
        /// 加工送货单号
        /// </summary>
        [Column("DLY_ODR_ID")]
        [StringLength(20)]
        public string DlyOdrId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}