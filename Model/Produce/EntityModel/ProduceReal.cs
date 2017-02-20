/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductScheduleRealDetail.cs
// 文件功能描述：生产排期实际表
// 
// 创建标识：朱静波 20131203
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
    /// 生产排期实际表
    /// </summary>
    [Serializable]
    [Table("PD_PROD_REAL")]
    public class ProduceReal : Entity
    {
        [Key, StringLength(20), Column("CLN_ODR_ID", Order = 0)]
        public string OrderNo { get; set; }//订单号

        [Key, StringLength(2), Column("CLN_ODR_DTL", Order = 1)]
        public string OrderNumber { get; set; }//订单明细

        [Key, StringLength(20), Column("EXPORT_ID", Order = 2)]
        public string ExportId { get; set; }//输出品ID

        [Key, MaxLength(1), Column(name: "PROD_TYP", Order = 3, TypeName = "char")]
        public string ProductType { get; set; }//生产类型

        [StringLength(20), Column("PDT_ID"), Required]
        public string ProductId { get; set; }//产品ID

        [StringLength(20), Column("PROC_ID"), Required]
        public string ProcessId { get; set; }//工序ID

        [Column("REAL_STAR_DT")]
        public DateTime? RealStartDt { get; set; }//实绩开工日

        [Column("REAL_END_DT")]
        public DateTime? RealEndDt { get; set; }//实绩完工日

        [DecimalPrecision(10, 2), Column("NED_PROD_TIME")]
        public decimal NeedProduceTime { get; set; }//实绩所要日数

        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }//备注
    }
}
