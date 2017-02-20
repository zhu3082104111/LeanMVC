/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductScheduleRealDetail.cs
// 文件功能描述：生产排期实绩详细表
// 
// 创建标识：朱静波 20131113
//
// 修改标识：杜兴军 20131204
// 修改描述：类名及文件名改为"ProduceRealDetail",并继承Entity
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
    /// 对应DB中文表名：生产排期实绩详细表
    /// 20131113 朱静波 建立
    /// </summary>
    [Serializable]
    [Table("PD_PROD_REAL_DETAIL")]
    public class ProduceRealDetail : Entity
    {
        [Key, StringLength(20), Column("CLN_ODR_ID", Order = 1)]
        public string ClientOrderID { get; set; }//客户订单号

        [Key, StringLength(2), Column("CLN_ODR_DTL", Order = 2)]
        public string ClientOrderDetails { get; set; }//客户订单明细

        [Key, StringLength(20), Column("EXPORT_ID", Order = 3)]
        public string ExportID { get; set; }//输出品ID

        [Key, MaxLength(1), Column(name: "PROD_TYP", Order = 4, TypeName = "char")]
        public string ProductType { get; set; }//生产类型

        [Key, StringLength(10), Column("DATE", Order= 5)]
        public DateTime Date { get; set; }//日期长，

        [Required, StringLength(15), Column("PDT_ID")]
        public string ProductID { get; set; }//产品ID

        [Required, StringLength(20), Column("PROC_ID")]
        public string ProcessID { get; set; }//工序ID

        [Required, StringLength(5), Column("SEQ_NO")]
        public string SequenceNo { get; set; }//工序顺序号

        [DecimalPrecision(10, 2), Column("REAL_STAR_DT")]
        public decimal ActualProcessNum { get; set; }//实际加工件数

        [Column("REMARK"), StringLength(400)]
        public string Remark { get; set; }//备注

    }
}
