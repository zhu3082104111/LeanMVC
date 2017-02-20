/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：ProductWarehouse.cs
// 文件功能描述：成品交仓表
// 
// 创建标识：20131121 梁龙飞
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// 成品交仓单表
    /// 20131121 梁龙飞C
    /// </summary>
    [Serializable]
    [Table("PD_PROD_WAREH")]
    public class ProductWarehouse:Entity
    {
        [Key, StringLength(20),Column("PROD_WAREH_ID")]
        public string ProductWarehouseID { get; set; }//成品交仓单号

        [StringLength(20),Column("BTH_ID")]
        public string BatchID { get; set; }//批次号
        
        /// <summary>
        /// 交仓单状态 0：未提交，1：已提交（交仓单打印后，变为已提交状态），2：已入库
        /// </summary>
        [StringLength(1), Column(name: "WAREH_STA", TypeName = "char")]
        public string WarehouseState { get; set; }

        /// <summary>
        /// 交仓部门ID
        /// </summary>
        [StringLength(2), Column("DEPART_ID")]
        public string DepartmentID { get; set; }

        /// <summary>
        /// 交仓日期
        /// </summary>
        [Column("WAREH_DT")]
        public DateTime WarehouseDT { get; set; }

        /// <summary>
        /// 交仓人ID
        /// </summary>
        [StringLength(20), Column("WAREH_PSON_ID")]
        public string WarehousePersonID { get; set; }

        /// <summary>
        /// 检验人ID
        /// </summary>
        [StringLength(20), Column("CHEC_PSON_ID")]
        public string CheckPersonID { get; set; }

        /// <summary>
        /// 调度人ID
        /// </summary>
        [StringLength(20), Column("DISPATCHER_ID")]
        public string DispatherID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(400), Column("REMARK")]
        public string Remark { get; set; }

    }
}
