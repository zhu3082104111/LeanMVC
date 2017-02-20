/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProdChkList.cs
// 文件功能描述：
//          成品检验单表的实体Model类
//      
// 修改履历：2013-12-26 汪罡 修改
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
    /// DB Table Name: QU_PROD_CHK_LIST
    /// DB Table Name(CHS): 成品检验单表
    /// Edit by WangGang @ 2013-12-26 16:58:05 .
    /// </summary>
    [Serializable, Table("QU_PROD_CHK_LIST")]
    public class ProdChkList : Entity
    {

        /// <summary>
        /// 检验单号
        /// </summary>
        [Column("CHK_LIST_ID")]
        [Key, StringLength(20)]
        public string ChkListId { get; set; }

        /// <summary>
        /// 检验日期
        /// </summary>
        [Column("CHK_DT")]
        public DateTime? ChkDt { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [Column("CLN_ODR_ID")]
        [StringLength(20)]
        public string ClnOdrId { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Column("CLN_ODR_DTL")]
        [StringLength(2)]
        public string ClnOdrDtl { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Column("PRODUCT_ID")]
        [StringLength(20)]
        public string ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("PROD_NAME")]
        [StringLength(200)]
        public string ProdName { get; set; }

        /// <summary>
        /// 产品型号
        /// </summary>
        [Column("PROD_ABBREV")]
        [StringLength(20)]
        public string ProdAbbrev { get; set; }

        /// <summary>
        /// 产品图号
        /// </summary>
        [Column("PROD_SCHE")]
        [StringLength(100)]
        public string ProdSche { get; set; }

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
        ///// 检验结果
        /// 检验结论
        /// </summary>
        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：根据最新的DB设计书作对应。
        //[Column(name: "CHK_RES", TypeName = "char")]
        //[StringLength(1)]
        [Column("CHK_RES")]
        [StringLength(400)]
        public string ChkRes { get; set; }

        /// <summary>
        /// 生产单元组长ID
        /// </summary>
        [Column("TEAM_LEAD_ID")]
        [StringLength(20)]
        public string TeamLeadId { get; set; }

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
        /// 材料及规格要求
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { get; set; }

        /// <summary>
        /// 总装调度单ID
        /// </summary>
        [Column("ASS_DISP_ID")]
        [StringLength(20)]
        public string AssDispId { get; set; }

        /// <summary>
        /// 成品交仓单号
        /// </summary>
        [Column("PROD_WAREH_ID")]
        [StringLength(20)]
        public string ProdWarehId { get; set; }

        //修 改 人：汪罡
        //修改日期：2013-12-19
        //修改原因：按照最新的DB设计书添加新的字段。
        /// <summary>
        /// 让步品使用区分
        /// </summary>
        [Column(name: "USD_GI_FLG", TypeName = "char")]
        [StringLength(1)]
        public string UsdGiFlg { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [StringLength(400)]
        public string Remark { get; set; }

    }
}