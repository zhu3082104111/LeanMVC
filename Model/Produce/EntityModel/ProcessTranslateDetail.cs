// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProcessTranslateDetail.cs
// 文件功能描述：加工流转卡详细 实体类
// 
// 创建标识：代东泽 20131203
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
    /// 加工流转卡详细 实体类
    /// 代东泽 20131203
    /// </summary>
    [Table("PD_PROC_TRANS_DETAIL")]
    [Serializable]
    public class ProcessTranslateDetail:Entity
    {
        /// <summary>
        /// 加工流转卡号
        /// </summary>
        [Key, Column("PROC_DELIV_ID",Order=0), StringLength(20)]
        public string ProcDelivID { get; set; }

        /// <summary>
        /// 工序顺序号
        /// </summary>
        [Key,Column("SEQ_NO",Order=1)]
        public int SeqNo { get; set; }

        /// <summary>
        /// 项目序号
        /// </summary>
        [Key,Column("ITEM_NO",Order=2), StringLength(2)]
        public string ItemNo { get; set; }

        /// <summary>
        /// 计划操作日期
        /// </summary>
        [Column("PLN_OPR_DT")]
        public DateTime? PlnOprDt { get; set; }

        /// <summary>
        /// 实际操作日期
        /// </summary>
        [Column("RAL_OPR_DT")]
        public DateTime? RalOprDt { get; set; }

        /// <summary>
        /// 计划操作数量
        /// </summary>
        [DecimalPrecision(10, 2),Column("PLN_OPR_QTY")]
        public decimal PlnOprQty { get; set; }

        /// <summary>
        /// 实际操作数量
        /// </summary>
        [DecimalPrecision(10, 2), Column("RAL_OPR_QTY")]
        public decimal RalOprQty { get; set; }


        /// <summary>
        /// 操作者ID
        /// </summary>
        [StringLength(20), Column("OPTOR_ID")]
        public string OptorID { get; set; }

        //备注
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }
    }
}
