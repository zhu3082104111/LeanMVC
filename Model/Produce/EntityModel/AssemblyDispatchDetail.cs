/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：AssemblyDispatchDetail.cs
// 文件功能描述：总装调度详细表
// 
// 创建标识：朱静波 20131120
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
    [Table("PD_ASSEM_DISPATCH_DETAIL")]
    [Serializable]
    public class AssemblyDispatchDetail : Entity
    {
        [Key, StringLength(20), Column("ASS_DISP_ID", Order = 0)]
        public string AssemblyDispatchID { get; set; }//总装调度单ID

        [Key, Required, StringLength(3), Column("SEQU_NO", Order = 1)]
        public string SequenceNum { get; set; }//明细顺序号

        [Required, StringLength(15), Column("PART_ID")]
        public string PartID { get; set; }//零件ID

        [Required, StringLength(100), Column("PART_NAME")]
        public string PartName { get; set; }//零件名称

        [DecimalPrecision(10,2), Column("UNIT_QTY")]
        public decimal UnitQuantity { get; set; }//单位数量

        [StringLength(20), Column("BATCH_NUM")]
        public string BatchNum { get; set; }//批次号ID

        [StringLength(400), Column("SPECIFICA")]
        public string Specifica { get; set; }//材料及规格型号

        [StringLength(200), Column("REMARK")]
        public string Remark { get; set; }//备注

    }
}
