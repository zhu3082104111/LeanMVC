// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemBill.cs
// 文件功能描述：总装工票详细实体类
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
    ///  总装工票详细实体类
    ///  代东泽 20131119
    /// </summary>
    [Table("PD_ASSEM_BILL_DETAIL")]
    [Serializable]
    public class AssemBillDetail:Entity
    {
        [Key, StringLength(20), Column("ASS_BILL_ID",Order=0)]
        public string AssemBillID { get; set; }//总装工票ID

        [Key, Column("PROC_SEQU_NO", Order = 1)]
        public int ProcessOrderNO { get; set; }//工序顺序号

        [Key, StringLength(2), Column("NUMBER", Order = 2)]
        public string ProjectNO { get; set; }//项目序号

        [StringLength(20), Column("OPTOR_ID")]
        public string OperatorID { get; set; }//操作员ID

        [Column("OPERA_DT")]
        public DateTime? OperateDate { get; set; }//操作日期

        [DecimalPrecision(10, 2), Column("OPT_REAL_ASS_QTY")]
        public decimal OperatorRealCount { get; set; }//操作员实际数量

        //备注
        [Column("REMARK")]
        [MaxLength(200)]
        public string Remark { get; set; }

    }
}
