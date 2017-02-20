/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：PurChkList.cs
// 文件功能描述：
//          进货检验单表的实体Model类
//      
// 修改履历：2013-12-02 汪罡 修改
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
    /// DB Table Name: QU_PUR_CHK_LIST
    /// DB Table Name(CHS): 进货检验单表
    /// Edit by WangGang @ 2013-12-02 15:55:05 .
    /// </summary>
    [Serializable, Table("QU_PUR_CHK_LIST")]
    public class PurChkList : Entity
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
        /// 供应商ID
        /// </summary>
        [Column("COMP_ID")]
        [StringLength(20)]
        public string CompId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("COMP_NAME")]
        [StringLength(200)]
        public string CompName { get; set; }

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
        /// 检验数量
        /// </summary>
        [Column("CHK_QTY")]
        [DecimalPrecision(12, 2)]
        public decimal ChkQty { get; set; }

        /// <summary>
        /// 检验内容外观
        /// </summary>
        [Column(name: "CHK_CNT_APR", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntApr { get; set; }

        /// <summary>
        /// 检验内容值数
        /// </summary>
        [Column(name: "CHK_CNT_VAL", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntVal { get; set; }

        /// <summary>
        /// 检验内容材质
        /// </summary>
        [Column(name: "CHK_CNT_MAT", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntMat { get; set; }

        /// <summary>
        /// 检验内容性能
        /// </summary>
        [Column(name: "CHK_CNT_PER", TypeName = "char")]
        [StringLength(1)]
        public string ChkCntPer { get; set; }

        /// <summary>
        /// 供货状态
        /// </summary>
        [Column("SUP_STAT")]
        [StringLength(200)]
        public string SupStat { get; set; }

        /// <summary>
        /// 供货商综合判定
        /// </summary>
        [Column(name: "SUP_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string SupComRes { get; set; }

        /// <summary>
        /// LDK综合判定
        /// </summary>
        [Column(name: "LDK_COM_RES", TypeName = "char")]
        [StringLength(1)]
        public string LdkComRes { get; set; }

        /// <summary>
        /// 品保部门主管ID
        /// </summary>
        [Column("QUA_MAG_ID")]
        [StringLength(20)]
        public string QuaMagId { get; set; }

        /// <summary>
        /// 检验员ID
        /// </summary>
        [Column("CHK_PSN_ID")]
        [StringLength(20)]
        public string ChkPsnId { get; set; }

        /// <summary>
        /// 入库状态
        /// </summary>
        [Column(name: "STO_STAT", TypeName = "char")]
        [StringLength(1)]
        public string StoStat { get; set; }

        /// <summary>
        /// 外购外协区分
        /// </summary>
        [Column(name: "OS_SUP_FLG", TypeName = "char")]
        [StringLength(1)]
        public string OsSupFlg { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [Column("PROCESS_ID")]
        [StringLength(20)]
        public string ProcessId { get; set; }

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

        /// <summary>
        /// 送货单号
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