/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：MCSupplierCnsmInfo.cs
// 文件功能描述：
//          外协领料单信息表的实体Model类
//      
// 修改履历：2013/11/19 宋彬磊 新建
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
    /// 对应DB中文表名：外协领料单信息表
    /// 2013/11/19 宋彬磊 建立
    /// </summary>
    [Serializable]
    [Table("MC_SUPPLIER_CNSM_INFO")]
    public class MCSupplierCnsmInfo : Entity
    {
        // 领料单号
        [Key, StringLength(20), Column("MATER_REQ_NO", Order = 0)]
        public string MaterReqNo { set; get; }

        // 序号
        [Key, StringLength(2), Column("NO", Order = 1)]
        public string No { set; get; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书进行修改。
        /// <summary>
        /// 客户订单号
        /// </summary>
        [Column("CLN_ODR_ID")]
        [Required]
        [StringLength(20)]
        public string ClnOdrId { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书进行修改。
        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Column("CLN_ODR_DTL")]
        [Required]
        [StringLength(2)]
        public string ClnOdrDtl { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书进行修改。
        /// <summary>
        /// 输出品ID
        /// </summary>
        [Column("EXPORT_ID")]
        [Required]
        [StringLength(20)]
        public string ExportId { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书进行修改。
        /// <summary>
        /// 生产类型
        /// </summary>
        [Column(name: "PROD_TYP", TypeName = "char")]
        [Required]
        [StringLength(1)]
        public string ProdTyp { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书中已不存在“SUP_ODR_ID”，但暂时保留。
        // 外协加工调度单号
        [Required, StringLength(20), Column("SUP_ODR_ID")]
        public string SupOrderID { set; get; }

        // 零件ID
        [Required, StringLength(20), Column("MATERIAL_ID")]
        public string MaterialID { set; get; }

        // 批次号
        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[Required, StringLength(20), Column("BTH_ID")]
        [StringLength(20), Column("BTH_ID")]
        public string BatchID { set; get; }

        // 实领数量
        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[Required, DecimalPrecision(10, 2), Column("RECE_QTY")]
        [DecimalPrecision(10, 2), Column("RECE_QTY")]
        public decimal ReceiveQuantity { set; get; }

        // 请领数量
        [Required, DecimalPrecision(10, 2), Column("APPO_QTY")]
        public decimal ApplyQuantity { set; get; }

        // 让步区分
        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[StringLength(1), Column("SPEC_FLG", TypeName = "char")]
        [StringLength(3), Column("SPEC_FLG", TypeName = "varchar")]
        public string SpecFlg { set; get; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：最新的DB设计书中已不存在“QTY_UNIT_ID”，但暂时保留。
        // 数量单位ID
        [StringLength(20), Column("QTY_UNIT_ID")]
        public string QuantityUnitID { set; get; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改，实际此列已改名，但此处保留。
        // 材料及规格型号
        [StringLength(200), Column("SPEC_DETAIL")]
        public string MaterialsSpecReq { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        /// <summary>
        /// 材料及规格型号
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { get; set; }

        // 单价
        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[DecimalPrecision(10, 2), Column("UNIT_PRICE")]
        [DecimalPrecision(20, 0), Column("UNIT_PRICE")]
        public decimal UnitPrice { set; get; }

        // 单价单位ID
        [StringLength(20), Column("PRI_UNIT_ID")]
        public string PriceUnitID { set; get; }

        // 金额
        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[DecimalPrecision(10, 2), Column("TOTAL_PRICE")]
        [DecimalPrecision(20, 0), Column("TOTAL_PRICE")]
        public decimal TotalPrice { set; get; }

        // 金额单位ID
        [StringLength(20), Column("TOT_UNIT_ID")]
        public string TotalPriceUnitID { set; get; }

        // 工序ID
        [StringLength(20), Column("PROC_ID")]
        public string ProcID { set; get; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按最新的DB设计书进行修改。
        //[StringLength(20), Column("WH_ID")]
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("WH_ID")]
        [StringLength(8)]
        public string WhID { set; get; }

        // 备注
        [StringLength(512), Column("REMARK")]
        public string Remarks { set; get; }
    }
}
