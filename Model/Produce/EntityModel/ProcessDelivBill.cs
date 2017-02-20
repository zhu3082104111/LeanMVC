// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemBill.cs
// 文件功能描述：加工送货单流转卡对应关系表
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
    ///  加工送货单工票对应关系表
    ///  代东泽 20131119
    /// </summary>
    [Table("PD_PROC_DELIV_BILL")]
    [Serializable]

    public class ProcessDelivBill:Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "AUTO_NO", TypeName = "bigint")]
        [Key]
        public long AutoNo { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[ DecimalPrecision(10, 0), Column("SELF_INC_ID")]
        //public decimal NO { get; set; }

        [Required,StringLength(20),Column("PROC_DELIV_ID")]
        public string ProcDelivID { get; set; }

        [Column("PART_ID"), StringLength(20)]
        public string PartID { get; set; } //零件ID

        [StringLength(20), Column("PROC_TRAN_ID")]
        public string ProcessTranID { get; set; }

        //备注
        [Column("REMARK")]
        [MaxLength(400)]
        public string Remark { get; set; }
    }
}
