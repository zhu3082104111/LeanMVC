// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：GiMaterial.cs
// 文件功能描述：仓库预约表实体类
// 
// 创建标识：
//
// 修改标识：代东泽 20131226
// 修改描述：添加注释
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Model
{
    /// <summary>
    /// M:梁龙飞：仓库预约表：MC_WH_RESERVE
    /// </summary>
    [Serializable, Table("MC_WH_RESERVE")]
    public class Reserve : Entity
    {
        public Reserve()
        {
           
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("WH_ID", Order = 0)]
        [StringLength(8)]
        public string WhID { set; get; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("CLN_ODR_ID", Order = 1)]
        [StringLength(20)]
        public string ClnOdrID { set; get; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("CLN_ODR_DTL", Order = 2)]
        [StringLength(2)]
        public string ClnOdrDtl { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Column("ORD_PDT_ID")]
        [Required, StringLength(20)]
        public string OrdPdtID { set; get; }

        /// <summary>
        /// 产品零件ID
        /// </summary>
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.None), Column("PDT_ID",Order=3)]
        [StringLength(20)]
        public string PdtID { set; get; }

        /// <summary>
        /// 预约批次详细单号
        /// </summary>
        [Column("ORDE_BTH_DTAIL_LIST_ID")]
        [Required]
        public int OrdeBthDtailListID { set; get; }

        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("PDT_SPEC")]
        [StringLength(200)]
        public string PdtSpec { set; get; }

        /// <summary>
        /// 预约总数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("ORDE_QTY")]
        public decimal OrdeQty { set; get; }

        /// <summary>
        /// 实际在库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("RECV_QTY")]
        public decimal RecvQty { set; get; }

        /// <summary>
        /// 领料单开具数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("PICK_ORDE_QTY")]
        public decimal PickOrdeQty { set; get; }

        /// <summary>
        /// 已出库数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("CMP_QTY")]
        public decimal CmpQty { set; get; }


    }
}
