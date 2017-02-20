/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：ProcessDeliveryDetail.cs
// 文件功能描述：
//      
// 创建标识：2013/11/21 冯吟夷 新建
// 修改表示：2013/12/26  朱静波  修改
// 修改描述：添加属性
/*****************************************************************************/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// 加工送货详细表
    /// </summary>
    [Table("PD_PROC_DELIV_DETAIL")]
    [Serializable]
    public class ProcessDeliveryDetail:Entity
    {
        [Column("PROC_DELIV_ID",Order=0), Key, StringLength(20)]
        public string ProcessDeliveryID { get; set; } //加工送货单号

        [Column("PART_ID", Order = 1), StringLength(20), Key]
        public string PartID { get; set; } //零件ID

        [Column("PART_NAME"), StringLength(200)]
        public string PartName { get; set; } //零件名称

        [Column("PROCESS_ID"), StringLength(20)]
        public string ProcessID { get; set; } //工序ID

        [Column("PROC_NAME"), StringLength(200)]
        public string ProcessName { get; set; } //工序名称

        [Column("UNIT_ID"), StringLength(20)]
        public string UnitID { get; set; } //单位ID

        //不映射到数据库--朱静波
        [NotMapped]
        public string UnitName { get; set; }//单位名称

        [Column("QUA_QTY"), DecimalPrecision(10, 0)]
        public decimal QualifiedQuantity { get; set; } //合格数量

        [Column("SCRAP_QTY"), DecimalPrecision(10, 0)]
        public decimal ScrapQuantity { get; set; } //报废数量

        [Column("WAST_QTY"), DecimalPrecision(10, 0)]
        public decimal WasteQuantity { get; set; } //废料数量

        [Column("EXCE_QTY"), DecimalPrecision(10, 0)]
        public decimal ExcessQuantity { get; set; } //多余原料数量

        [Column("LACK_QTY"), DecimalPrecision(10, 0)]
        public decimal LackQuantity { get; set; } //缺少原料数量

        [Column("PLN_TOTAL"), DecimalPrecision(10, 0)]
        public decimal PlanTotal { get; set; } //预计交仓合计

        [Column("CHEC_ID"), StringLength(20)]
        public string CheckID { get; set; } //检验报告单ID，说明：外协的质检单ID，或者自加工质检单ID

        [Column("WH_ID"), StringLength(8)]
        public string WarehouseID { get; set; }//仓库编码

        [Column("GI_CLS"), StringLength(3)]
        public string ConcessionPart { get; set; }//让步分区

        [Column("REMARK"), StringLength(400)]
        public string Remark { get; set; } //备注
    }
}
