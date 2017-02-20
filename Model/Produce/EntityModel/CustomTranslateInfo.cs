// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：CustomTranslateInfo.cs
// 文件功能描述：客户订单流转卡关系表 实体类
// 
// 创建标识：代东泽 20131203
//
// 修改标识：朱静波 20131206
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
    /// 客户订单流转卡关系表
    /// 代东泽 20131203
    /// </summary>
    [Table("PD_CUS_TRANS_INFO")]
    [Serializable]
    public class CustomTranslateInfo:Entity
    {

        /// <summary>
        /// 加工流转卡号
        /// </summary>
        [Column("PROC_DELIV_ID", Order = 0), StringLength(20), Key]
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        [StringLength(20), Column("CLN_ODR_ID", Order = 1), Key]
        public string CustomerOrderNum { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        [StringLength(2), Column("CLN_ODR_DTL", Order = 2), Key]
        public string CustomerOrderDetails { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Required, Column("PRODUCT_ID"), StringLength(20)]
        public string ProductID { get; set; }

        /// <summary>
        /// 单次交仓件数
        /// </summary>
        [DecimalPrecision(10, 2), Column("WAREH_QTY")]
        public decimal WarehQty { get; set; }

      
        /// <summary>
        /// 计划加工数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("PLN_QTY")]
        public decimal PlnQty { get; set; }

        //备注
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }
    }
}
