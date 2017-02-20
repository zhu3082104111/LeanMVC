// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProduceMaterDetail.cs
// 文件功能描述：生产领料单信息详细实体类
// 
// 创建标识：代东泽 20131119
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
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
    ///  生产领料单详细实体类
    ///  代东泽 20131119
    /// </summary>
    [Serializable, Table("PD_PROD_MATER_DETAIL")]
    public class ProduceMaterDetail : Entity
    {
        //。string类型 默认对应数据库nvarchar类型
        /// <summary>
        /// 领料单号
        /// </summary>
        [Key, StringLength(20), Column("MATER_REQ_NO", Order = 0)]
        public string MaterReqNo { get; set; }

        //领料单详细号。string类型 默认对应数据库nvarchar类型
        /// <summary>
        /// 领料单详细号
        /// </summary>
        [Key, StringLength(2), Column("MATER_REQ_DET_NO", Order = 1)]
        public string MaterReqDetailNo { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [Required, StringLength(20), Column("CLN_ODR_ID")]
        public string CustomerOrderNum { get; set; }//客户订单号

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Required, StringLength(2), Column("CLN_ODR_DTL")]
        public string CustomerOrderDetails { get; set; }//

        /// <summary>
        /// 输出品
        /// </summary>
        [Required, StringLength(20), Column("EXPORT_ID")]
        public string ExportID { get; set; }//ID

        /// <summary>
        /// 生产类型
        /// </summary>
        [Required, StringLength(1), Column("PROD_TYP", TypeName = "char")]
        public string ProdType { get; set; }//

        /// <summary>
        /// 零件号
        /// </summary>
        [StringLength(20), Column("MATERIAL_ID")]
        public string MaterialID { get; set; }

        /// <summary>
        /// 批次号
        /// 代东泽 20131231 修改长度为 20，根据仓库预约表中的长度
        /// </summary>
        [StringLength(20), Column("BTH_ID")]
        public string BthID { get; set; }

        /// <summary>
        /// 实领数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("RECE_QTY")]
        public decimal ReceQty { get; set; }

        /// <summary>
        /// 请领数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("APPO_QTY")]
        public decimal AppoQty { get; set; }

        /// <summary>
        /// 让步区分
        /// </summary>
        [StringLength(3), Column(name: "SPEC_FLG")]
        public string SpecFlag { get; set; }

        //数量单位ID
        //[StringLength(20), Column("QTY_UNIT_ID")]
        // public string QtyUnitID { get; set; }

        /// <summary>
        /// 材料及规格型号
        /// </summary>
        [StringLength(200), Column("PDT_SPEC")]
        public string PdtSpec { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按照最新的DB设计书修改长度。
        /*
        /// <summary>
        /// 单价
        /// </summary>
        [DecimalPrecision(20, 0), Column("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }
         */
        /// <summary>
        /// 单价
        /// </summary>
        [DecimalPrecision(16, 6), Column("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 单价单位ID
        /// </summary>
        [StringLength(20), Column("PRI_UNIT_ID")]
        public string PriceUnitID { get; set; }

        //修 改 人：汪罡
        //修改日期：2014-01-13
        //修改原因：按照最新的DB设计书修改长度。
        /*
        /// <summary>
        /// 金额
        /// </summary>
        [DecimalPrecision(20, 0), Column("TOTAL_PRICE")]
        public decimal TotalPrice { get; set; }
         */
        /// <summary>
        /// 金额
        /// </summary>
        [DecimalPrecision(16, 6), Column("TOTAL_PRICE")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 金额单位ID
        /// </summary>
        [StringLength(20), Column("TOT_UNIT_ID")]
        public string TotalUnitID { get; set; }

        /// <summary>
        /// 工序ID
        /// </summary>
        [StringLength(20), Column("PROC_ID")]
        public string ProcessID { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [StringLength(8), Column("WH_ID")]
        public string WHID { get; set; }

        /// <summary>
        /// 加工流转卡号
        /// </summary>
        [StringLength(20), Column("PROC_DELIV_ID")]
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 总装工票ID
        /// </summary>
        [StringLength(20), Column("ASS_BILL_ID")]
        public string AssBillID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }
    }
}
